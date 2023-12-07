using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogReview.Controllers
{
    public class BlogDetailCusController : Controller
    {
        public IActionResult Index(int id)
        {
            BlogDAO blogDAO = new BlogDAO();
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            string username = HttpContext.Session.GetString("Username");
            ViewBag.idBlog = id;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            ViewBag.local = list;
            ViewBag.cont = listCon;
            ViewBag.locationDAO = locationDAO;
            ViewBag.mainContent = mainContent;
            ViewBag.blogDetail = blogDAO.getBlogDetailByBlogId(id);
            

            if (HttpContext.Session.GetString("NameDisplay") != null)
            {
                ViewBag.login = true;
                ViewBag.NameDisplay = HttpContext.Session.GetString("NameDisplay");
                UserHe173248 u = userDAO.getUserByUsername(username);
                int idUser = u.UserId;
                ViewBag.idUser = idUser;
            }
            ViewBag.countComment = blogDAO.countCommemt(id);
            List<CommentHe173248> listCom = blogDAO.getAllCommentByBlogID(id);
            ViewBag.listCom = listCom;

            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection f)
        {
            string comment = f["comment"];
            int idBlog = int.Parse(f["idBlog"]);
            string delete = f["delete"];
            string RateStar = f["RateStar"];

            BlogDAO blogDAO = new BlogDAO();
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();


            UserDAO userDAO = new UserDAO();
            int idUser = 0;
            ViewBag.userDAO = userDAO;
            ViewBag.local = list;
            ViewBag.cont = listCon;
            ViewBag.locationDAO = locationDAO;
            ViewBag.mainContent = mainContent;
            ViewBag.blogDetail = blogDAO.getBlogDetailByBlogId(idBlog);
            ViewBag.idBlog = idBlog;

            if (HttpContext.Session.GetString("NameDisplay") != null)
            {
                ViewBag.login = true;
                ViewBag.NameDisplay = HttpContext.Session.GetString("NameDisplay");
            }


            string username = HttpContext.Session.GetString("Username");
            UserHe173248 u = userDAO.getUserByUsername(username);
            idUser = u.UserId;
            ViewBag.idUser = idUser;
            int idDelete = 0, rate = 0 ;
            if (delete != null && delete.Trim().Length > 0)
            {
                idDelete = int.Parse(f["delete"]);
                blogDAO.deleteCommentByIDComment(idDelete);
            }
            else
            {
                if (RateStar!=null && RateStar.Trim().Length > 0)
                {
                    rate = int.Parse(f["rate"]);
                    blogDAO.upRate(rate, idBlog, idUser);
                }
                else
                {
                    if (comment.Trim().Length > 0)
                    {
                        blogDAO.createCommemt(comment, u.UserId, idBlog);
                    }
                    else
                    {
                        ViewBag.comtErr = "Enter your comment!";
                    }
                }
            }
            
            List<CommentHe173248> listCom = blogDAO.getAllCommentByBlogID(idBlog);
            ViewBag.listCom = listCom;
            ViewBag.countComment = blogDAO.countCommemt(idBlog);
            return View();
        }

        public IActionResult Delete(int id, int idBlog)
        {
            BlogDAO blogDAO = new BlogDAO();
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();

            ViewBag.idBlog = id;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            ViewBag.local = list;
            ViewBag.cont = listCon;
            ViewBag.locationDAO = locationDAO;
            ViewBag.mainContent = mainContent;
            ViewBag.blogDetail = blogDAO.getBlogDetailByBlogId(id);
            ViewBag.id = id;

            if (HttpContext.Session.GetString("NameDisplay") != null)
            {
                ViewBag.login = true;
                ViewBag.NameDisplay = HttpContext.Session.GetString("NameDisplay");
            }
            ViewBag.countComment = blogDAO.countCommemt(id);
            List<CommentHe173248> listCom = blogDAO.getAllCommentByBlogID(id);
            ViewBag.listCom = listCom;

            blogDAO.deleteCommentByIDComment(id);

            return RedirectToAction("/BlogDetailCus/Index", new { id = idBlog });
        }
    }
}
