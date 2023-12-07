using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogReview.Controllers
{
    public class NotificationController : Controller
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
            string username=HttpContext.Session.GetString("Username");
            UserHe173248 u=userDAO.getUserByUsername(username);
            ViewBag.notList = userDAO.getNotificationByUser(u.UserId);
            return View();
        }
    }
}
