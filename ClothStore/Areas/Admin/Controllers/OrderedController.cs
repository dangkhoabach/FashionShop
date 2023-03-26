using ClothStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothStore.Areas.Admin.Controllers
{
    public class OrderedController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Admin/Ordered
        public ActionResult Index()
        {
            return View();
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
            ViewBag.CTHD = _dbContext.CT_HoaDon.Where(x => x.MaHoaDon==MaHoaDon).ToList();
            return View();
        }
    }
}