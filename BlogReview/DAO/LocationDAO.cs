using BlogReview.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogReview.DAO
{
    public class LocationDAO
    {
        public PRN211_FA23_SE1733Context con = new PRN211_FA23_SE1733Context();
        public List<LocationHe173248> Loca()
        {
        List<LocationHe173248> loca = con.LocationHe173248s.Select(x => x).ToList();
        return loca;
        }

        public void addLocation(string loca)
        {
            LocationHe173248 local=new LocationHe173248();
            local.LocaContent = loca;
            con.LocationHe173248s.Add(local);
            con.SaveChanges();
        }
        public void deleteLocation(LocationHe173248 item)
        {
            con.LocationHe173248s.Remove(item);
            con.SaveChanges();
        }
        public void updateLocation(LocationHe173248 item,string location)
        {
            item.LocaContent = location;
            con.SaveChanges();
        }
        public void deleteLocation(int id)
        {
            var b = con.LocationHe173248s.Select(x => x).Where(d => d.LocaId==id).FirstOrDefault();
            con.LocationHe173248s.Remove(b);
            con.SaveChanges();
        }
        public List<int> getLocaBlogExist()
        {
            List<int> list = con.LocaBlogHe173248s.Select(x=>x.LocaId).ToList();
            return list;
        }
        public void addBlogLoca(string loca,int blogID)
        {
            int id = con.LocationHe173248s.Select(x => x).Where(d => d.LocaContent.Equals(loca)).FirstOrDefault().LocaId;
            LocaBlogHe173248 locaBlogHe173248 = new LocaBlogHe173248();
            locaBlogHe173248.LocaId = id;
            locaBlogHe173248.BlogId = blogID;
            con.LocaBlogHe173248s.Add(locaBlogHe173248);
            con.SaveChanges();
        }
        public void deleteBlogLoca(string loca, int blogID)
        {
            
            int id = con.LocationHe173248s.Select(x => x).Where(d => d.LocaContent.Equals(loca)).FirstOrDefault().LocaId;
            var b = con.LocaBlogHe173248s.Select(x => x).Where(d => d.LocaId==id && d.BlogId==blogID).FirstOrDefault();
            if (b != null)
            {
                con.LocaBlogHe173248s.Remove(b);
                con.SaveChanges();
            }
        }

        public void deleteBlogLocaById(int loca, int blogID)
        {
            var b = con.LocaBlogHe173248s.Select(x => x).Where(d => d.LocaId == loca && d.BlogId == blogID).FirstOrDefault();
            if (b != null)
            {
                con.LocaBlogHe173248s.Remove(b);
                con.SaveChanges();
            }
        }

        public List<LocaBlogHe173248> getLocaBlogByBlogID(int blogId)
        {
            List<LocaBlogHe173248> list=new List<LocaBlogHe173248>();
            PRN211_FA23_SE1733Context con = new PRN211_FA23_SE1733Context();
            list = con.LocaBlogHe173248s.Select(x => x).Where(d => d.BlogId==blogId).ToList();
            return list;
        }

        public string getLocaNameByLocaID(int locaId)
        {
            PRN211_FA23_SE1733Context con = new PRN211_FA23_SE1733Context();
            string s = con.LocationHe173248s.Select(x => x).Where(d => d.LocaId == locaId).FirstOrDefault().LocaContent;
            return s;
        }

        public int countLocainProject(int loca)
        {
            BlogDAO blogDAO = new BlogDAO();
            List<BlogHe173248> blogApp = blogDAO.getBlogApproved();
            List<int> blogAppID = new List<int>();
            foreach(var item in blogApp)
            {
                blogAppID.Add(item.BlogId);
            }
            int i=con.LocaBlogHe173248s.Select(x=>x).Where(x=>x.LocaId==loca && blogAppID.Contains(x.BlogId)).Count();
            return i;
        }

    }
}
