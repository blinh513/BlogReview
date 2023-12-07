using BlogReview.Models;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.IO;
using System.Linq;

namespace BlogReview.DAO
{
    public class BlogDAO
    {
        PRN211_FA23_SE1733Context con = new PRN211_FA23_SE1733Context();
        public int getMaxProjectID()
        {
            try
            {
                int count = con.BlogHe173248s.Max(x => x.BlogId);
                return count;
            }
            catch (Exception ex)
            {

            }
            return 0;
        }

        public int createBlog(BlogHe173248 bl,string role, IFormFile? image)
        {
            var directory_mydoc = "C:\\PRN211Code\\BlogReview\\BlogReview\\wwwroot\\Blog\\BlogIMG";
            int id = con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals("Pending")).FirstOrDefault().StatusId;
            int idAc = con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals("Approved")).FirstOrDefault().StatusId;
            if (role == "Blogger" || role== "Viewer") { 
                bl.StatusId = id;
            }
            else { bl.StatusId = idAc; }
            DateTime currentDateTime = DateTime.Now;
            bl.CreateOn = currentDateTime;
            bl.ModifiOn = currentDateTime;
            con.BlogHe173248s.Add(bl);
            con.SaveChanges();
            int idBlog = bl.BlogId;
            string ImageBlog = "";
            if (image != null)
            {
                string strExtension = Path.GetExtension(image.FileName).Trim();
                ImageBlog = idBlog + "_img" + strExtension;
                string destFile = System.IO.Path.Combine(directory_mydoc, ImageBlog);

                if (image != null && image.Length > 0)
                {
                    using (var stream = new FileStream(destFile, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                }
            }
            var blogImage=getBlogDetailByBlogId(idBlog);
            blogImage.ImageBlog = ImageBlog;
            con.SaveChanges();

            return idBlog;
        }

        public void createTagDraf(string otherlocation, string othercontent,int blogID)
        {
            TagDraftHe173248 tagDraftHe173248 = new TagDraftHe173248();
            if(otherlocation!=null) tagDraftHe173248.LocationDraft = otherlocation;
            if (othercontent != null) tagDraftHe173248.CategoryDraft = othercontent;
            if(othercontent != null || otherlocation != null)
            {
                tagDraftHe173248.BlogId = blogID;
                con.TagDraftHe173248s.Add(tagDraftHe173248);
            }

            con.SaveChanges();
        }
        public List<BlogHe173248> getBlogPending()
        {
            int id = con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals("Pending")).FirstOrDefault().StatusId;

            var b=con.BlogHe173248s.Select(x => x).Where(d => d.StatusId==id).OrderByDescending(x => x.ModifiOn).ToList();
            return b;
        }
        public List<BlogHe173248> getBlogApproved()
        {
            int id = con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals("Approved")).FirstOrDefault().StatusId;

            var b = con.BlogHe173248s.Select(x => x).Where(d => d.StatusId == id).OrderByDescending(x => x.ModifiOn).ToList();
            return b;
        }

        public List<BlogHe173248> getBlogSearch(string search)
        {
            List < BlogHe173248 > b=getBlogApproved();
            int id = con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals("Approved")).FirstOrDefault().StatusId;
            if (search!=null && search.Trim().Length > 0)
            {
                b = con.BlogHe173248s.Select(x => x).Where(d => d.StatusId == id && d.BlogName.Contains(search)).OrderByDescending(x => x.ModifiOn).ToList();
            }
            return b;
        }
        public BlogHe173248 getBlogDetailByBlogId(int blogId)
        {
            var b = con.BlogHe173248s.Select(x => x).Where(d => d.BlogId == blogId).FirstOrDefault();
            return b;
        }
        public TagDraftHe173248 getTagDraftByBlogId(int blogId)
        {
            var b = con.TagDraftHe173248s.Select(x => x).Where(d => d.BlogId == blogId).FirstOrDefault();
            return b;
        }
        public void updateBlogStatus(int blogId,string status)
        {
            var id= con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals(status)).FirstOrDefault().StatusId;
            var b = con.BlogHe173248s.Select(x => x).Where(d => d.BlogId == blogId).FirstOrDefault();
            b.StatusId = id;
            DateTime currentDateTime = DateTime.Now;
            b.ModifiOn = currentDateTime;
            con.SaveChanges();
        }

        public List<BlogHe173248> getBlogByLocation(int locaId)
        {
            int id = con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals("Approved")).FirstOrDefault().StatusId;
            LocationDAO locationDAO = new LocationDAO();
            List<BlogHe173248> b = con.BlogHe173248s
                .Where(blog => con.LocaBlogHe173248s
                                .Where(locaBlog => locaBlog.LocaId == locaId)
                                .Select(locaBlog => locaBlog.BlogId)
                                .Contains(blog.BlogId) && blog.StatusId == id).OrderByDescending(x=>x.ModifiOn)
                .ToList();

            return b;
        }

        public List<BlogHe173248> getBlogByCategory(int cateId)
        {
            int id = con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals("Approved")).FirstOrDefault().StatusId;
            MainContentDAO mainContentDAO = new MainContentDAO();
            List<BlogHe173248> b = con.BlogHe173248s
                .Where(blog => con.MainConBlogHe173248s
                                .Where(MainConBlog => MainConBlog.MainConId == cateId)
                                .Select(MainConBlog => MainConBlog.BlogId)
                                .Contains(blog.BlogId) && blog.StatusId == id).OrderByDescending(x => x.ModifiOn)
                .ToList();

            return b;
        }

        public string getBlogStatusNameByStatusID(int id)
        {
            string name=con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusId==id).FirstOrDefault().StatusName;
            return name;
        }


        public void deleteBlog(int id)
        {
            LocationDAO locationDAO = new LocationDAO();
            List<LocaBlogHe173248> b=locationDAO.getLocaBlogByBlogID(id);
            foreach (var item in b)
            {
                locationDAO.deleteBlogLocaById(item.LocaId, item.BlogId);
            }

            MainContentDAO mainContentDAO = new MainContentDAO();
            List<MainConBlogHe173248> b1 = mainContentDAO.getMainConBlogByBlogID(id);
            foreach (var item in b1)
            {
                mainContentDAO.deleteBlogCateById(item.MainConId, item.BlogId);
            }
            var tagDraf=con.TagDraftHe173248s.Select(x=>x).Where(x=>x.BlogId==id).FirstOrDefault();
            if (tagDraf != null) { con.TagDraftHe173248s.Remove(tagDraf); }
            

            var blog=getBlogDetailByBlogId(id);
            con.BlogHe173248s.Remove(blog);
            con.SaveChanges();
        }
        public void deleteCommentByIDComment(int id)
        {
            var blog = con.CommentHe173248s.Select(x => x).Where(x => x.CommentId == id).FirstOrDefault();
   
            con.CommentHe173248s.Remove(blog);
            con.SaveChanges();
        }
        public CommentHe173248 getCommentById(int id)
        {
            var b = con.CommentHe173248s.Select(x => x).Where(x => x.CommentId == id).FirstOrDefault();
            return b;
        }
        public List<BlogHe173248> getBlogByUser(UserHe173248 user)
        {
            var b = con.BlogHe173248s.Select(x => x).Where(d => d.UserId == user.UserId).OrderByDescending(x => x.ModifiOn).ToList();
            return b;
        }

        internal void deleteAllBlog(int id)
        {
            LocationDAO locationDAO = new LocationDAO();
            List<LocaBlogHe173248> b = locationDAO.getLocaBlogByBlogID(id);
            foreach (var item in b)
            {
                locationDAO.deleteBlogLocaById(item.LocaId, item.BlogId);
            }

            MainContentDAO mainContentDAO = new MainContentDAO();
            List<MainConBlogHe173248> b1 = mainContentDAO.getMainConBlogByBlogID(id);
            foreach (var item in b1)
            {
                mainContentDAO.deleteBlogCateById(item.MainConId, item.BlogId);
            }
            var tagDraf = con.TagDraftHe173248s.Select(x => x).Where(x => x.BlogId == id).FirstOrDefault();
            if (tagDraf != null)
            {
                con.TagDraftHe173248s.Remove(tagDraf);
            }
            

            var comment = con.CommentHe173248s.Select(x => x).Where(x => x.BlogId == id).ToList();
            foreach(var item in comment)
            {
                con.CommentHe173248s.Remove(item);
            }
            
            var rate = con.RateHe173248s.Select(x => x).Where(x => x.BlogId == id).ToList();
            foreach (var item in rate)
            {
                con.RateHe173248s.Remove(item);
            }

            var blog = getBlogDetailByBlogId(id);
            string image = blog.ImageBlog;

            var directoryimg = "C:\\PRN211Code\\BlogReview\\BlogReview\\wwwroot\\Blog\\BlogIMG\\";
            File.Delete(directoryimg + image);
            con.BlogHe173248s.Remove(blog);
            con.SaveChanges();
        }

        public void updateBlog(BlogHe173248 bl)
        {
           var blogOld=getBlogDetailByBlogId(bl.BlogId);
            blogOld.BlogName=bl.BlogName;
            blogOld.BlogDescription = bl.BlogDescription;
            DateTime currentDateTime = DateTime.Now;
            blogOld.ModifiOn = currentDateTime;
            blogOld.Content = bl.Content;
            int id = con.BlogStatusHe173248s.Select(x => x).Where(d => d.StatusName.Equals("Pending")).FirstOrDefault().StatusId;
            blogOld.StatusId = id;
            con.SaveChanges();
        }

        public void updateTagDraf(string otherLocation, string otherCategory, int idBlog)
        {
            var v = getTagDraftByBlogId(idBlog);
            if (otherLocation.Trim().Length>0) { 
            v.LocationDraft = otherLocation;
            }
            if (otherCategory.Trim().Length > 0)
            {
                v.CategoryDraft = otherCategory;
            }
            con.SaveChanges();
        }

        public List<CommentHe173248> getAllCommentByBlogID(int id)
        {
            var v = con.CommentHe173248s.Select(x => x).Where(x => x.BlogId == id)
                .OrderByDescending(x=>x.ModifiOn)
                .ToList();
            return v;
        }
        public void createCommemt(string comment, int userId,int blogId)
        {
            CommentHe173248 c=new CommentHe173248();
            c.Content = comment;
            c.UserId = userId;
            c.BlogId = blogId;
            DateTime currentDateTime = DateTime.Now;
            c.CreateOn = currentDateTime;
            c.ModifiOn = currentDateTime;
            con.CommentHe173248s.Add(c);
            con.SaveChanges();
        }

        public int countCommemt(int blogId)
        {
            int i=con.CommentHe173248s.Select(x=>x).Where(x=>x.BlogId==blogId).Count();
            return i;
        }

        public void upRate(int rate,int idBlog,int idUser)
        {
            RateHe173248? rateBlog = con.RateHe173248s.Select(x => x).Where(x => x.BlogId == idBlog && x.UserId == idUser).FirstOrDefault();
            BlogHe173248 b=con.BlogHe173248s.Select(x => x).Where(x => x.BlogId == idBlog).FirstOrDefault();
            if (rateBlog == null)
            {
                rateBlog = new RateHe173248();
                rateBlog.UserId=idUser;
                rateBlog.Number = rate;
                rateBlog.BlogId = idBlog;
                con.RateHe173248s.Add(rateBlog);
                con.SaveChanges();
            }
            else
            {
                rateBlog.Number=rate;
                con.SaveChanges();
            }

            List<RateHe173248> rateNumber= con.RateHe173248s.Select(x => x).Where(x => x.BlogId == idBlog).ToList();
            if (rateNumber != null)
            {
                decimal tong = 0;
                int soluong = rateNumber.Count();

                foreach (var item in rateNumber)
                {
                    tong += item.Number;
                }

                if (soluong > 0)
                {
                    b.AverRate = (double)tong / soluong;
                }
                con.SaveChanges();
            }
            
        }




    }
}
