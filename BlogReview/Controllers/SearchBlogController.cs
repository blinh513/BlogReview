using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogReview.Controllers
{
    public class SearchBlogController : Controller
    {
        public IActionResult Index(string search)
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            BlogDAO blogDAO = new BlogDAO();
            UserDAO userDAO = new UserDAO();
            ViewBag.listAccept = blogDAO.getBlogSearch(search);
            ViewBag.userDAO = userDAO;
            ViewBag.locationDAO = locationDAO;
            ViewBag.mainContent = mainContent;

            ViewBag.search=search;
            return View();
        }
    }
}
