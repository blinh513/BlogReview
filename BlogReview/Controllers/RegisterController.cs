using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace BlogReview.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return View();
        }
        [HttpPost]
        public IActionResult Index(string name, string username, string password, string passwordCom, string email, IFormFile image)
        {
            ViewBag.enterName=name;
            ViewBag.enterUser=username;
            ViewBag.enterPass=password;
            ViewBag.enterComPass = passwordCom;
            ViewBag.enterEmail=email;

            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;

            string exten =  ".png.jpg";
            Boolean imageErr = true, userErr = true, passErr=true, passComErr=true, nameErr=true;
            if (image != null)
            {
                string strExtension = Path.GetExtension(image.FileName).Trim();
                    if (!exten.Contains(strExtension)) {
                        ViewBag.imageErr = "Image not valid!";
                        imageErr =false;
                    }
            }


            var directory_mydoc = "C:\\PRN211Code\\BlogReview\\BlogReview\\wwwroot\\Blog\\UserIMG";
            UserDAO userDAO = new UserDAO();
            UserHe173248? user = userDAO.getUserByUsername(username);
            if(user != null)
            {
                ViewBag.userErr = "Username exist!";
                userErr = false;
            }
            user = userDAO.getUserByNameDisplay(name);
            if (user != null)
            {
                ViewBag.nameErr = "Your name exist!";
                nameErr = false;
            }
            if (username.Count()<3)
            {
                ViewBag.userErr = "Username too short!";
                userErr = false;
            }
            if (password.Count() < 8)
            {
                ViewBag.passErr = "Password too short!";
                passErr = false;
            }
            if (!password.Equals(passwordCom))
            {
                ViewBag.passComErr = "Password and confirm password does not match.!";
                passComErr = false;
            }
            if (userErr && imageErr && passErr && passComErr && nameErr)
            {
                string ImageName = "";
                if (image != null)
                {
                    string strExtension = Path.GetExtension(image.FileName).Trim();
                    ImageName = username + "_img" + strExtension;
                    string destFile = System.IO.Path.Combine(directory_mydoc, ImageName);

                    if (image != null && image.Length > 0)
                    {
                        using (var stream = new FileStream(destFile, FileMode.Create))
                        {
                            image.CopyTo(stream);
                        }
                    }
                }

                UserHe173248 us=new UserHe173248();
                us.NameDisplay= name;
                us.Username= username;
                us.Email= email;
                us.Password= password;
                us.ImageUser = ImageName;
                userDAO.createUser(us);

                HttpContext.Session.SetString("Username", username);
                HttpContext.Session.SetString("NameDisplay", name);
                HttpContext.Session.SetString("Role", "Viewer");
                ViewBag.userDAO = userDAO;
                return RedirectToAction("Index", "Home");
            }
            ViewBag.userDAO = userDAO;
            return View();
        }
    }
}
