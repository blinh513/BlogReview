using BlogReview.DAO;
using BlogReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace BlogReview.Controllers
{
    public class ItemManageController : Controller
    {
        public IActionResult Index()
        {
            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();

            List<int> listExist = locationDAO.getLocaBlogExist();
            List<int> listConExist = mainContent.getMainConBlogExist();
            List<LocationHe173248> localDel = new List<LocationHe173248>();
            List<MainContentHe173248> contDel = new List<MainContentHe173248>();
            foreach (var item in list)
            {
                if (!listExist.Contains(item.LocaId))
                {
                    localDel.Add(item);
                }
            }
            foreach (var item in listCon)
            {
                if (!listConExist.Contains(item.MainConId))
                {
                    contDel.Add(item);
                }
            }
            ViewBag.local = list;
            ViewBag.cont = listCon;
            ViewBag.localDel = localDel;
            ViewBag.contDel = contDel;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return View();
        }
        [HttpPost]
            public IActionResult Index(IFormCollection f)
        {
            string location = f["location"];
            string category= f["category"];

            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            PRN211_FA23_SE1733Context con =new PRN211_FA23_SE1733Context();
            if(location.Trim().Length != 0)
            {
                LocationHe173248? l = con.LocationHe173248s.Select(x=>x).Where(d=>d.LocaContent.Equals(location)).FirstOrDefault();
                if (l == null) { locationDAO.addLocation(location); }
            }
            if (category.Trim().Length != 0)
            {
                MainContentHe173248? m = con.MainContentHe173248s.Select(x => x).Where(d => d.MainContent.Equals(category)).FirstOrDefault();
                if (m == null) { mainContent.addMainContent(category); }
            }

            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();

            List<int> listExist = locationDAO.getLocaBlogExist();
            List<int> listConExist = mainContent.getMainConBlogExist();
            List<LocationHe173248> localDel = new List<LocationHe173248>();
            List<MainContentHe173248> contDel = new List<MainContentHe173248>();
            foreach (var item in list)
            {
                if (!listExist.Contains(item.LocaId))
                {
                    localDel.Add(item);
                }
            }
            foreach (var item in listCon)
            {
                if (!listConExist.Contains(item.MainConId))
                {
                    contDel.Add(item);
                }
            }
            ViewBag.localDel = localDel;
            ViewBag.contDel = contDel;
            ViewBag.local = list;
            ViewBag.cont = listCon;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return RedirectToAction("Index", "ItemManage");
        }


        public IActionResult DeleteLoca(int id)
        {

            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            locationDAO.deleteLocation(id);
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();

            List<int> listExist = locationDAO.getLocaBlogExist();
            List<int> listConExist = mainContent.getMainConBlogExist();
            List<LocationHe173248> localDel = new List<LocationHe173248>();
            List<MainContentHe173248> contDel = new List<MainContentHe173248>();
            foreach (var item in list)
            {
                if (!listExist.Contains(item.LocaId))
                {
                    localDel.Add(item);
                }
            }
            foreach (var item in listCon)
            {
                if (!listConExist.Contains(item.MainConId))
                {
                    contDel.Add(item);
                }
            }
            ViewBag.localDel = localDel;
            ViewBag.contDel = contDel;
            ViewBag.local = list;
            ViewBag.cont = listCon;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return RedirectToAction("Index", "ItemManage");
        }

        public IActionResult DeleteCate(int id)
        {

            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            mainContent.deleteMainCon(id);
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();

            List<int> listExist = locationDAO.getLocaBlogExist();
            List<int> listConExist = mainContent.getMainConBlogExist();
            List<LocationHe173248> localDel = new List<LocationHe173248>();
            List<MainContentHe173248> contDel = new List<MainContentHe173248>();
            foreach (var item in list)
            {
                if (!listExist.Contains(item.LocaId))
                {
                    localDel.Add(item);
                }
            }
            foreach (var item in listCon)
            {
                if (!listConExist.Contains(item.MainConId))
                {
                    contDel.Add(item);
                }
            }
            ViewBag.localDel = localDel;
            ViewBag.contDel = contDel;
            ViewBag.local = list;
            ViewBag.cont = listCon;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return RedirectToAction("Index", "ItemManage");
        }

        public IActionResult UpdateLoca(IFormCollection f)
        {

            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();
            
            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            foreach(var item in list)
            {
                string l = "'loca'" + item.LocaId;
                if (f[l].Count>0)
                {
                    locationDAO.updateLocation(item, f[l].ToString());
                }
            }
            List<int> listExist = locationDAO.getLocaBlogExist();
            List<int> listConExist = mainContent.getMainConBlogExist();
            List<LocationHe173248> localDel = new List<LocationHe173248>();
            List<MainContentHe173248> contDel = new List<MainContentHe173248>();
            foreach (var item in list)
            {
                if (!listExist.Contains(item.LocaId))
                {
                    localDel.Add(item);
                }
            }
            foreach (var item in listCon)
            {
                if (!listConExist.Contains(item.MainConId))
                {
                    contDel.Add(item);
                }
            }
            ViewBag.localDel = localDel;
            ViewBag.contDel = contDel;
            ViewBag.local = list;
            ViewBag.cont = listCon;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return RedirectToAction("Index", "ItemManage");
        }
        public IActionResult UpdateCate(IFormCollection f)
        {

            LocationDAO locationDAO = new LocationDAO();
            MainContentDAO mainContent = new MainContentDAO();

            List<LocationHe173248> list = locationDAO.Loca();
            List<MainContentHe173248> listCon = mainContent.Cont();
            foreach (var item in listCon)
            {
                string l = "'cate'" + item.MainConId;
                if (f[l].Count > 0)
                {
                    mainContent.updateMainCon(item, f[l].ToString());
                }
            }
            List<int> listExist = locationDAO.getLocaBlogExist();
            List<int> listConExist = mainContent.getMainConBlogExist();
            List<LocationHe173248> localDel = new List<LocationHe173248>();
            List<MainContentHe173248> contDel = new List<MainContentHe173248>();
            foreach (var item in list)
            {
                if (!listExist.Contains(item.LocaId))
                {
                    localDel.Add(item);
                }
            }
            foreach (var item in listCon)
            {
                if (!listConExist.Contains(item.MainConId))
                {
                    contDel.Add(item);
                }
            }
            ViewBag.localDel = localDel;
            ViewBag.contDel = contDel;
            ViewBag.local = list;
            ViewBag.cont = listCon;
            UserDAO userDAO = new UserDAO();
            ViewBag.userDAO = userDAO;
            return RedirectToAction("Index", "ItemManage");
        }
    }
}
