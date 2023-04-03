using ClothStore.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothStore.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        int MaHD = -1;
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Tracking()
        {
            var userid = User.Identity.GetUserId();
            ViewBag.Order = _dbContext.HoaDon.Where(x => x.Id == userid).ToList();
            return View();
        }

        public ActionResult ChangeStatus(HoaDon hoaDon)
        {
            var item = _dbContext.HoaDon.Find(hoaDon.MaHoaDon);

            item.TrangThai = false;

            _dbContext.SaveChanges();

            return RedirectToAction("Tracking", "Customer");
        }

        public List<CT_HoaDon> LayCTHD(int MaHoaDon)
        {
            List<CT_HoaDon> lstCt = _dbContext.CT_HoaDon.Where(x => x.MaHoaDon == MaHoaDon).ToList();
            MaHD = MaHoaDon;
            return lstCt;
        }

        public ActionResult OrderedDetail(int MaHoaDon)
        {
            ViewBag.CTHD = LayCTHD(MaHoaDon);
            MaHD = MaHoaDon;

            return View();
        }
    }
}