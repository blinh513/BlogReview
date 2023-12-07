using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogReview.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            if (HttpContext.Session.GetString("Username") != null)
            {
                return RedirectToAction("Index", "Home");
            }
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormCollection f)
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;

            UserDAO userDAO=new UserDAO();
            ViewBag.userDAO = userDAO;
            string u = f["u"];
            string p = f["p"];
            PRN211_FA23_SE1733Context con=new PRN211_FA23_SE1733Context();
            UserHe173248? acc=con.UserHe173248s.Select(x=>x).Where(d=>d.Username.Equals(u)).FirstOrDefault();
            String mes = "";
            if(acc == null || !acc.Password.Equals(p))
            {
                mes = "Login Failed!!";
                ViewBag.mes = mes;
                return View();
            }
            else
            {
                HttpContext.Session.SetString("Username", u);
                HttpContext.Session.SetString("NameDisplay", acc.NameDisplay);
                ViewBag.NAME = acc.NameDisplay;
                return RedirectToAction("Index", "Home");
            }
            
            
        }

    }
}
