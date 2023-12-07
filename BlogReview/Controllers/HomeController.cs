using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace BlogReview.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            BlogDAO blogDAO = new BlogDAO();
            UserDAO userDAO = new UserDAO();
            ViewBag.listAccept = blogDAO.getBlogApproved();
            ViewBag.userDAO = userDAO;
            ViewBag.locationDAO = locationDAO;
            ViewBag.mainContent = mainContent;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("NameDisplay");
            return RedirectToAction("Index", "Home");
        }

    }
}
