using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace BlogReview.Controllers
{
    public class BlogRegisterController : Controller
    {
        public IActionResult Index()
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            UserDAO u=new UserDAO();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            string username = HttpContext.Session.GetString("Username");
            string role = u.getRoleNameByRoleID(u.getUserByUsername(username).RoleId);
            ViewBag.role = role;
            ViewBag.userDAO = u;
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection f)
        {
            Boolean imageErr = true, blogContentErr = true, locaErr=true, cateErr=true;
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            String blogname = f["blogname"];
            String description = f["description"];
            String otherLocation = f["otherLocation"];
            String otherCategory = f["otherCategory"];
            String blogContent = f["blogContent"];

            ViewBag.enterBlogTitle = blogname;
            ViewBag.enterBlogDes = description;
            ViewBag.otherLocation = otherLocation;
            ViewBag.otherCategory = otherCategory;
            ViewBag.blogContent=blogContent;

            IFormFile? image = f.Files["image"];
            List<String>locaCheck=new List<String>();
            List<String> cateCheck = new List<String>();
            if (list != null) {
                foreach (var localtion in list)
                {
                    if (f[localtion.LocaContent.ToString()].Equals(localtion.LocaContent.ToString()))
                    {
                        locaCheck.Add(localtion.LocaContent.ToString());
                    }
                } 
            }

            if (listCon != null)
            {
                foreach (var content in listCon)
                {
                    if (f[content.MainContent.ToString()].Equals(content.MainContent.ToString()))
                    {
                        cateCheck.Add(content.MainContent.ToString());
                    }
                }
            }
            ViewBag.locaCheck=locaCheck;
            ViewBag.cateCheck=cateCheck;
            if(locaCheck.Count==0 && otherLocation.Trim().Length==0)
            {
                ViewBag.locaErr = "Choose Location!!";
                locaErr = false;
            }
            if (cateCheck.Count == 0 && otherCategory.Trim().Length == 0)
            {
                ViewBag.cateErr = "Choose Category!!";
                cateErr = false;
            }
            ViewBag.enterBlogName = blogname;
            ViewBag.enterBlogDescription=description;

            string exten = ".png.jpg";

            if (image != null)
            {
                string strExtension = Path.GetExtension(image.FileName).Trim();
                if (!exten.Contains(strExtension))
                {
                    ViewBag.imageErr = "Image not valid!";
                    imageErr = false;
                }
            }
            else {
                ViewBag.imageErr = "Required Image!";
                imageErr = false;
            }
            if (blogContent.Trim().Length == 0)
            {
                ViewBag.blogContentErr = "Required Blog Content!";
                blogContentErr = false;
            }

            
            BlogDAO blogDAO = new BlogDAO();
            int count = blogDAO.getMaxProjectID();


            if (blogContentErr && imageErr&& locaErr && cateErr)
            {
                string ImageBlog = "";
                

                BlogHe173248 bl = new BlogHe173248();
                bl.BlogName = blogname;
                bl.BlogDescription = description;
                bl.Content = blogContent;
                bl.ImageBlog = ImageBlog;
                String username=HttpContext.Session.GetString("Username");
                string role = userDAO.getRoleNameByRoleID(userDAO.getUserByUsername(username).RoleId);
                ViewBag.role = role;
                UserHe173248 user=userDAO.getUserByUsername(username);
                bl.UserId = user.UserId;
                bl.AverRate = 0;
                if ((locaCheck.Count == 0 || cateCheck.Count == 0) && (role.Equals("ADMIN")))
                {
                    role = "Blogger";
                }
               int idNew=blogDAO.createBlog(bl, role, image);
                foreach(var locStr in locaCheck)
                {
                    locationDAO.addBlogLoca(locStr, idNew);
                }
                foreach (var cateStr in cateCheck)
                {
                    mainContent.addBlogCate(cateStr, idNew);
                }
                blogDAO.createTagDraf(otherLocation, otherCategory, idNew);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}
