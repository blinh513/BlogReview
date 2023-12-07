using BlogReview.Models;
namespace BlogReview.DAO
{
    public class MainContentDAO
    {
        public PRN211_FA23_SE1733Context con = new PRN211_FA23_SE1733Context();
        public List<MainContentHe173248> Cont()
        {
            List<MainContentHe173248> cont = con.MainContentHe173248s.Select(x => x).ToList();
            return cont;
        }
        public void addMainContent(string conten)
        {
            MainContentHe173248 cont = new MainContentHe173248();
            cont.MainContent = conten;
            con.MainContentHe173248s.Add(cont);
            con.SaveChanges();
        }
        public void addBlogCate(string cont, int blogID)
        {
            
            int id = con.MainContentHe173248s.Select(x => x).Where(d => d.MainContent.Equals(cont)).FirstOrDefault().MainConId;
            MainConBlogHe173248 mainConBlogHe173248 = new MainConBlogHe173248();
            mainConBlogHe173248.MainConId = id;
            mainConBlogHe173248.BlogId = blogID;
            con.MainConBlogHe173248s.Add(mainConBlogHe173248);
            con.SaveChanges();
        }
        public void deleteBlogCate(string cont, int blogID)
        {
            int id = con.MainContentHe173248s.Select(x => x).Where(d => d.MainContent.Equals(cont)).FirstOrDefault().MainConId;
            var b = con.MainConBlogHe173248s.Select(x => x).Where(d => d.MainConId == id && d.BlogId == blogID).FirstOrDefault();
            if (b != null)
            {
                con.MainConBlogHe173248s.Remove(b);
                con.SaveChanges();
            }
        }

        public void deleteBlogCateById(int cont, int blogID)
        {
            var b = con.MainConBlogHe173248s.Select(x => x).Where(d => d.MainConId == cont && d.BlogId == blogID).FirstOrDefault();
            if (b != null)
            {
                con.MainConBlogHe173248s.Remove(b);
                con.SaveChanges();
            }
        }
        public void deleteMainCon(MainContentHe173248 item)
        {
                con.MainContentHe173248s.Remove(item);
                con.SaveChanges();
        }
        public void deleteMainCon(int id)
        {
            var b = con.MainContentHe173248s.Select(x => x).Where(d => d.MainConId == id).FirstOrDefault();
            con.MainContentHe173248s.Remove(b);
            con.SaveChanges();
        }
        public void updateMainCon(MainContentHe173248 item, string category)
        {
            item.MainContent = category;
            con.SaveChanges();
        }
        public List<int> getMainConBlogExist()
        {
            List<int> list = con.MainConBlogHe173248s.Select(x => x.MainConId).ToList();
            return list;
        }
        public List<MainConBlogHe173248> getMainConBlogByBlogID(int blogId)
        {
            List<MainConBlogHe173248> list = new List<MainConBlogHe173248>();
            list = con.MainConBlogHe173248s.Select(x => x).Where(d => d.BlogId == blogId).ToList();
            return list;
        }

        public string getMainConNameByMainConID(int mainId)
        {
            string s = con.MainContentHe173248s.Select(x => x).Where(d => d.MainConId == mainId).FirstOrDefault().MainContent;
            return s;
        }
        public int countMainConinProject(int loca)
        {
            BlogDAO blogDAO = new BlogDAO();
            List<BlogHe173248> blogApp = blogDAO.getBlogApproved();
            List<int> blogAppID = new List<int>();
            foreach (var item in blogApp)
            {
                blogAppID.Add(item.BlogId);
            }
            int i = con.MainConBlogHe173248s.Select(x => x).Where(x => x.MainConId == loca && blogAppID.Contains(x.BlogId)).Count();
            return i;
        }
    }
}
