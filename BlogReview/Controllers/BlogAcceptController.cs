using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogReview.Controllers
{
    public class BlogAcceptController : Controller
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
            ViewBag.userDAO = userDAO;
            ViewBag.logList = logDAO.getBlogApproved();
            return View();
        }

        public IActionResult Change(int id)
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            BlogDAO logDAO = new BlogDAO();
            UserDAO userDAO = new UserDAO();
            ViewBag.logList = logDAO.getBlogApproved();
            ViewBag.userDAO = userDAO;


            logDAO.updateBlogStatus(id, "Pending");
            UserHe173248 user = userDAO.getUserByBlogID(id);
            userDAO.addNotification(user.UserId, "Your New Blog is Pending");
            return RedirectToAction("Index","BlogAccept");
        }
    }
}
