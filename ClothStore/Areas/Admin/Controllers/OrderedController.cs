using ClothStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace ClothStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderedController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        int MaHD = -1;
        // GET: Admin/Ordered
        public ActionResult Index()
        {
            return View();
        }
        public List<CT_HoaDon> LayCTHD(int MaHoaDon) 
        {
            List<CT_HoaDon> lstCt = _dbContext.CT_HoaDon.Where(x => x.MaHoaDon == MaHoaDon).ToList();
            MaHD = MaHoaDon;
            return lstCt;
        }

        public ActionResult List()
        {
            var listordered = _dbContext.HoaDon.ToList();
            return View(listordered);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Details(int MaHoaDon)
        {
            ViewBag.CTHD = LayCTHD(MaHoaDon);
            MaHD = MaHoaDon;
            
            return View();
        }

        public ActionResult Delete(HoaDon hoaDon)
        {
            var item = _dbContext.HoaDon.Find(hoaDon.MaHoaDon);
            if (item == null)
            {
                return HttpNotFound();
            }
            _dbContext.HoaDon.Remove(item);
            _dbContext.SaveChanges();
            return RedirectToAction("List", "Ordered");
        }
    }
}