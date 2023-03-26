using ClothStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothStore.Controllers
{
    public class ProductController : Controller
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View(_dbContext.MatHang.ToList());
        }

        public ActionResult Details(string id)
        {
            var Mh = _dbContext.MatHang.Find(id);
            return View(Mh);
        }
    }
}