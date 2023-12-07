using BlogReview.Models;
namespace BlogReview.DAO
{
    public class UserDAO
    {
        PRN211_FA23_SE1733Context con = new PRN211_FA23_SE1733Context();
        public void createUser(UserHe173248 user)
        {
            int id= con.RoleHe173248s.Select(x => x).Where(d => d.RoleName.Equals("Viewer")).FirstOrDefault().RoleId;
            user.RoleId= id;
            con.UserHe173248s.Add(user);
            con.SaveChanges();
        }
        
        public UserHe173248? getUserByUsername(string username)
        {
            UserHe173248? acc = con.UserHe173248s.Select(x => x).Where(d => d.Username.Equals(username)).FirstOrDefault();
            return acc;
        }
        public UserHe173248? getUserByNameDisplay(string name)
        {
            UserHe173248? acc = con.UserHe173248s.Select(x => x).Where(d => d.NameDisplay.Equals(name)).FirstOrDefault();
            return acc;
        }
        public UserHe173248? getUserByID(int id)
        {
            UserHe173248? acc = con.UserHe173248s.Select(x => x).Where(d => d.UserId==id).FirstOrDefault();
            return acc;
        }
        public UserHe173248? getUserByBlogID(int idBlog)
        {
            BlogDAO dao = new BlogDAO();
            UserHe173248? acc = con.UserHe173248s.Select(x => x).Where(d => d.UserId == dao.getBlogDetailByBlogId(idBlog).UserId).FirstOrDefault();
            return acc;
        }
        public string getRoleNameByRoleID(int RoleID)
        {
            string name = con.RoleHe173248s.Select(x => x).Where(d => d.RoleId==RoleID).FirstOrDefault().RoleName;
            return name;
        }
        public void updateRoleUserByUserId(int userId,string newRole)
        {
            UserHe173248? acc=getUserByID(userId);
            int idNew = con.RoleHe173248s.Select(x => x).Where(d => d.RoleName.Equals(newRole)).FirstOrDefault().RoleId;
            acc.RoleId= idNew;
            con.SaveChanges();
        }

        public List<NotificationHe173248> getNotificationByUser(int userId)
        {
            var list=con.NotificationHe173248s.Select(x=>x).Where(x=>x.UserId==userId).OrderByDescending(x => x.CreateOn).ToList();
            foreach(var item in list)
            {
                item.IsRead= true;
            }
            con.SaveChanges();
            return list;
        }

        public int getNumberNotiNotReadByUser(int userId)
        {
            var list = con.NotificationHe173248s.Select(x => x).Where(x => x.UserId == userId && x.IsRead==false).ToList();
            return list.Count();
        }

        public void addNotification(int userId,string content)
        {
            NotificationHe173248 n=new NotificationHe173248();
            n.UserId= userId;
            n.Content= content;
            n.IsRead= false;
            DateTime currentDateTime = DateTime.Now;
            n.CreateOn = DateTime.Now;
            con.NotificationHe173248s.Add(n);
            con.SaveChanges() ;
        }
        public int getStarByBlogIDandUserId(int userid, int blogId)
        {
            RateHe173248? rateBlog = con.RateHe173248s.Select(x => x).Where(x => x.BlogId == blogId && x.UserId == userid).FirstOrDefault();
            if (rateBlog == null)
            {
                return 0;
            }
            else
            {
                return rateBlog.Number;
            }
        }

        public int countBlogInNow(int userId)
        {
            DateTime ngayHienTai = DateTime.Now;
            DateTime batdauNgay = ngayHienTai.Date;
            DateTime ketThucNgay = ngayHienTai.Date.AddDays(1).AddMilliseconds(-1);
            int i = con.BlogHe173248s.Select(x => x).Where(x => x.UserId == userId && x.CreateOn >= batdauNgay && x.CreateOn <= ketThucNgay).Count();
            return i;
        }
    }
}
