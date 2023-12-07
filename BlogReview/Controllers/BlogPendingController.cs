using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogReview.Controllers
{
    public class BlogPendingController : Controller
    {
        public IActionResult Index()
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            BlogDAO logDAO = new BlogDAO();
            UserDAO userDAO = new UserDAO();
            ViewBag.logList=logDAO.getBlogPending();
            ViewBag.userDAO = userDAO;
            return View();
        }


        public IActionResult Delete(int id)
        {
            BlogDAO blogDAO = new BlogDAO();
            blogDAO.deleteAllBlog(id);
            return Redirect("/BlogPending");
        }
    }
}
