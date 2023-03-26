using ClothStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothStore.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdProductController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();

        // GET: Admin/AdProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListProduct()
        {
            return View(_dbContext.MatHang.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.NCC = _dbContext.NCC.ToList();
            ViewBag.LoaiHang = _dbContext.LoaiHang.ToList();
            return View();
        }

        public ActionResult Details(string MaMatHang)
        {
            MatHang Mh = _dbContext.MatHang.Find(MaMatHang);
            ViewBag.NCC = _dbContext.NCC.ToList();
            ViewBag.LoaiHang = _dbContext.LoaiHang.ToList();
            return View(Mh);
        }
    }
}