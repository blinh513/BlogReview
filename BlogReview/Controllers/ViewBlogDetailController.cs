using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using System.Web;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using System.Reflection.Metadata;

namespace BlogReview.Controllers
{
    public class ViewBlogDetailController : Controller
    {
        public IActionResult Index(int id)
        {
            BlogDAO blogDAO = new BlogDAO();
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();



            ViewBag.local = list;
            ViewBag.cont = listCon;

            var blog = blogDAO.getBlogDetailByBlogId(id);
            ViewBag.blogDetail=blog;
            ViewBag.id = id;

            List<LocaBlogHe173248> listLocaBlog = locationDAO.getLocaBlogByBlogID(id);
            List<MainConBlogHe173248> listMainCon = mainContent.getMainConBlogByBlogID(id);
            TagDraftHe173248 tagDraftHe173248 = blogDAO.getTagDraftByBlogId(id);
            List<String> locaCheck = new List<String>();
            List<String> cateCheck = new List<String>();
            if (listLocaBlog != null)
            {
                foreach (var localtion in listLocaBlog)
                {
                    locaCheck.Add(locationDAO.getLocaNameByLocaID(localtion.LocaId));
                }
            }

            if (listMainCon != null)
            {
                foreach (var content in listMainCon)
                {
                    cateCheck.Add(mainContent.getMainConNameByMainConID(content.MainConId));
                }
            }
            ViewBag.locaCheck = locaCheck;
            ViewBag.cateCheck = cateCheck;
            ViewBag.locationDraf = tagDraftHe173248.LocationDraft;
            ViewBag.categoryDraft = tagDraftHe173248.CategoryDraft;

            ViewBag.statusBlog = blogDAO.getBlogStatusNameByStatusID(blog.StatusId);
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return View();
        }
        [HttpPost]
        public IActionResult Index(IFormCollection f)
        {
            //Nếu if f không tồn tại nhưng trong LocalCheck thì mình xóa
            //Nếu if f tồn tại nhưng không trong LocalCheck thì mình add
            BlogDAO blogDAO = new BlogDAO();
            int idBlog = int.Parse(f["idBlog"]);
            Boolean locaErr = false, cateErr = false;
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            ViewBag.local = list;
            ViewBag.cont = listCon;
            List<LocaBlogHe173248> listLocaBlog = locationDAO.getLocaBlogByBlogID(idBlog);
            List<MainConBlogHe173248> listMainCon = mainContent.getMainConBlogByBlogID(idBlog);
            List<String> locaCheck = new List<String>();
            List<String> cateCheck = new List<String>();
            if (listLocaBlog != null)
            {
                foreach (var localtion in listLocaBlog)
                {
                    locaCheck.Add(locationDAO.getLocaNameByLocaID(localtion.LocaId));
                }
            }

            if (listMainCon != null)
            {
                foreach (var content in listMainCon)
                {
                    cateCheck.Add(mainContent.getMainConNameByMainConID(content.MainConId));
                }
            }

            if (list != null)
            {
                foreach (var localtion in list)
                {

                    if (f[localtion.LocaContent.ToString()].Equals(localtion.LocaContent.ToString()))
                    {
                        locaErr = true;
                    }
                }
            }

            if (listCon != null)
            {
                foreach (var content in listCon)
                {
                    if (f[content.MainContent.ToString()].Equals(content.MainContent.ToString()))
                    {
                        cateErr = true;
                    }
                }
            }

            if (!locaErr)
            {
                ViewBag.locaErr = "Choose Location!!";
            }
            if (!cateErr)
            {
                ViewBag.cateErr = "Choose Category!!";
            }
            if (f["Reject"].Equals("Reject"))
            {
                UserDAO userDAO = new UserDAO();
                UserHe173248 user = userDAO.getUserByBlogID(idBlog);
                blogDAO.updateBlogStatus(idBlog, "Reject");
                userDAO.addNotification(user.UserId, "Your New Blog is Rejected");
                return Redirect("/BlogPending");
            }else
            if (locaErr && cateErr)
            {
                if (list != null)
                {
                    foreach (var localtion in list)
                    {

                        if (f[localtion.LocaContent.ToString()].Equals(localtion.LocaContent.ToString()) && !locaCheck.Contains(localtion.LocaContent.ToString()))
                        {
                            locationDAO.addBlogLoca(localtion.LocaContent.ToString(), idBlog);
                        }
                        if (!f[localtion.LocaContent.ToString()].Equals(localtion.LocaContent.ToString()) && locaCheck.Contains(localtion.LocaContent.ToString()))
                        {
                            locationDAO.deleteBlogLoca(localtion.LocaContent.ToString(), idBlog);
                        }
                    }
                }

                if (listCon != null)
                {
                    foreach (var content in listCon)
                    {
                        if (f[content.MainContent.ToString()].Equals(content.MainContent.ToString()) && !cateCheck.Contains(content.MainContent.ToString()))
                        {
                            mainContent.addBlogCate(content.MainContent.ToString(), idBlog);
                        }
                        if (!f[content.MainContent.ToString()].Equals(content.MainContent.ToString()) && cateCheck.Contains(content.MainContent.ToString()))
                        {
                            mainContent.deleteBlogCate(content.MainContent.ToString(), idBlog);
                        }
                    }
                }
                if (f["Accept"].Equals("Accept"))
                {
                    blogDAO.updateBlogStatus(idBlog, "Approved");
                    UserDAO userDAO = new UserDAO();
                    UserHe173248 user = userDAO.getUserByBlogID(idBlog);
                    string role=userDAO.getRoleNameByRoleID(user.RoleId);
                    if (role.Equals("Viewer"))
                    {
                        userDAO.updateRoleUserByUserId(user.UserId, "Blogger");
                        userDAO.addNotification(user.UserId, "You have become a blogger. You can post on our website");

                    }
                    userDAO.addNotification(user.UserId, "Your New Blog is Approved");
                }
                return Redirect("/BlogPending");
                
                
            }
            else
            {
                ViewBag.statusBlog = blogDAO.getBlogStatusNameByStatusID(blogDAO.getBlogDetailByBlogId(idBlog).StatusId);
                ViewBag.local = list;
                ViewBag.cont = listCon;

                ViewBag.blogDetail = blogDAO.getBlogDetailByBlogId(idBlog);
                ViewBag.id = idBlog;
                TagDraftHe173248 tagDraftHe173248 = blogDAO.getTagDraftByBlogId(idBlog);
                ViewBag.locaCheck = locaCheck;
                ViewBag.cateCheck = cateCheck;
                if (tagDraftHe173248 != null) { 
                ViewBag.locationDraf = tagDraftHe173248.LocationDraft;
                ViewBag.categoryDraft = tagDraftHe173248.CategoryDraft;
                }
                UserDAO userDAO = new UserDAO();
                ViewBag.userDAO = userDAO;
                return View();
            }
          
        }
    }
}
