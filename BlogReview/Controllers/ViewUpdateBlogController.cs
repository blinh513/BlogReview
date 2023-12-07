using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.IO;
namespace BlogReview.Controllers
{
    public class ViewUpdateBlogController : Controller
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
            ViewBag.blogDetail = blog;
            ViewBag.id = id;
            ViewBag.username = HttpContext.Session.GetString("Username");
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
            if (tagDraftHe173248 != null) { 
            if (tagDraftHe173248.LocationDraft != null) { ViewBag.locationDraf = tagDraftHe173248.LocationDraft; }
            if (tagDraftHe173248.CategoryDraft != null) { ViewBag.categoryDraft = tagDraftHe173248.CategoryDraft; }
            }

            ViewBag.statusBlog = blogDAO.getBlogStatusNameByStatusID(blog.StatusId);
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection f)
        {
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            BlogDAO blogDAO = new BlogDAO();
            int idBlog = int.Parse(f["idBlog"]);
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();

            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            if (f["Reject"].Equals("Reject"))
            {
                blogDAO.updateBlogStatus(idBlog, "Reject");
                return Redirect("/BlogPending");
            }
            else 
            {
                Boolean imageErr = true, blogContentErr = true, locaErr = false, cateErr = false;

                ViewBag.local = list;
                ViewBag.cont = listCon;
                
                String blogname = f["blogname"];
                String description = f["description"];
                String otherLocation = f["otherLocation"];
                String otherCategory = f["otherCategory"];
                String blogContent = f["blogContent"];

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

                ViewBag.enterBlogTitle = blogname;
                ViewBag.enterBlogDes = description;
                ViewBag.otherLocation = otherLocation;
                ViewBag.otherCategory = otherCategory;
                ViewBag.blogContent = blogContent;
                ViewBag.id = idBlog;
                IFormFile? image = f.Files["image"];

                ViewBag.locaCheck = locaCheck;
                ViewBag.cateCheck = cateCheck;
                if (f["Accept"].Equals("Accept"))
                {
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
                }
                else
                {
                    locaErr = true; cateErr = true;
                    if (list != null)
                    {
                        foreach (var localtion in list)
                        {

                            if (f[localtion.LocaContent.ToString()].Equals(localtion.LocaContent.ToString()) && !locaCheck.Contains(localtion.LocaContent.ToString()))
                            {
                                locaCheck.Add(localtion.LocaContent.ToString());
                            }
                            if (!f[localtion.LocaContent.ToString()].Equals(localtion.LocaContent.ToString()) && locaCheck.Contains(localtion.LocaContent.ToString()))
                            {
                                locaCheck.Remove(localtion.LocaContent.ToString());
                            }
                        }
                    }

                    if (listCon != null)
                    {
                        foreach (var content in listCon)
                        {
                            if (f[content.MainContent.ToString()].Equals(content.MainContent.ToString()) && !cateCheck.Contains(content.MainContent.ToString()))
                            {
                                cateCheck.Add(content.MainContent.ToString());
                            }
                            if (!f[content.MainContent.ToString()].Equals(content.MainContent.ToString()) && cateCheck.Contains(content.MainContent.ToString()))
                            {
                                cateCheck.Remove(content.MainContent.ToString());
                            }
                        }
                    }
                    ViewBag.locaCheck = locaCheck;
                    ViewBag.cateCheck = cateCheck;
                    if (locaCheck.Count == 0 && otherLocation.Trim().Length == 0)
                    {
                        ViewBag.locaErr = "Choose Location!!";
                        locaErr = false;
                    }
                    if (cateCheck.Count == 0 && otherCategory.Trim().Length == 0)
                    {
                        ViewBag.cateErr = "Choose Category!!";
                        cateErr = false;
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
                ViewBag.enterBlogName = blogname;
                ViewBag.enterBlogDescription = description;

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

                if (blogContent.Trim().Length == 0)
                {
                    ViewBag.blogContentErr = "Required Blog Content!";
                    blogContentErr = false;
                }

                var directory_mydoc = "C:\\PRN211Code\\BlogReview\\BlogReview\\wwwroot\\Blog\\BlogIMG";

                int count = idBlog;


                if (blogContentErr && imageErr && locaErr && cateErr)
                {
                    string ImageBlog = "";
                    if (image != null)
                    {
                        string strExtension = Path.GetExtension(image.FileName).Trim();
                        ImageBlog = count + "_img" + strExtension;
                        string destFile = System.IO.Path.Combine(directory_mydoc, ImageBlog);

                        if (image != null && image.Length > 0)
                        {
                            System.IO.File.Delete(destFile);
                            using (var stream = new FileStream(destFile, FileMode.Create))
                            {
                                image.CopyTo(stream);
                            }
                        }

                    }

                    BlogHe173248 bl = new BlogHe173248();
                    bl.BlogName = blogname;
                    bl.BlogDescription = description;
                    bl.Content = blogContent;
                    if (ImageBlog.Trim().Length > 0)
                    {
                        bl.ImageBlog = ImageBlog;
                    }


                    String username = HttpContext.Session.GetString("Username");
                    String role = HttpContext.Session.GetString("Role");
                    UserHe173248 user = userDAO.getUserByUsername(username);
                    bl.UserId = user.UserId;
                    bl.BlogId = idBlog;
                    blogDAO.updateBlog(bl);

                    locaCheck = new List<String>();
                    cateCheck = new List<String>();
                    listLocaBlog = locationDAO.getLocaBlogByBlogID(idBlog);
                    listMainCon = mainContent.getMainConBlogByBlogID(idBlog);
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
                    blogDAO.updateTagDraf(otherLocation, otherCategory, idBlog);
                    if (f["Accept"].Equals("Accept"))
                    {
                        blogDAO.updateBlogStatus(idBlog, "Approved");
                        return Redirect("/BlogPending");
                    }
                }
                locaCheck = new List<String>();
                cateCheck = new List<String>();
                listLocaBlog = locationDAO.getLocaBlogByBlogID(idBlog);
                listMainCon = mainContent.getMainConBlogByBlogID(idBlog);
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
                TagDraftHe173248 tagDraftHe173248 = blogDAO.getTagDraftByBlogId(idBlog);
                var blog = blogDAO.getBlogDetailByBlogId(idBlog);
                ViewBag.blogDetail = blog;
                ViewBag.locaCheck = locaCheck;
                ViewBag.cateCheck = cateCheck;
                if (tagDraftHe173248 != null) { 
                ViewBag.locationDraf = tagDraftHe173248.LocationDraft;
                ViewBag.categoryDraft = tagDraftHe173248.CategoryDraft;
                }
                ViewBag.username = HttpContext.Session.GetString("Username");
                ViewBag.statusBlog = blogDAO.getBlogStatusNameByStatusID(blog.StatusId);

                    return View();
            }
                
        }
        
    }
}
