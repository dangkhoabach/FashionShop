using ClothStore.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
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

        [HttpPost]
        public ActionResult SaveProduct(MatHang matHang, HttpPostedFileBase HinhAnh)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.NCC = _dbContext.NCC.ToList();
                    ViewBag.LH = _dbContext.LoaiHang.ToList();
                    return View("Create", matHang);
                }

                string path = Path.Combine(Server.MapPath("/Content/images/collection/"),
                    Path.GetFileName(HinhAnh.FileName));
                HinhAnh.SaveAs(path);
                matHang.HinhAnh = "/Content/images/collection/" + Path.GetFileName(HinhAnh.FileName);
                _dbContext.MatHang.Add(matHang);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Error update images";
            }
            return RedirectToAction("Create", "AdProduct");
        }

        public ActionResult Details(string MaMatHang)
        {
            MatHang Mh = _dbContext.MatHang.Find(MaMatHang);
            ViewBag.NCC = _dbContext.NCC.ToList();
            ViewBag.LoaiHang = _dbContext.LoaiHang.ToList();
            return View(Mh);
        }

        [HttpPost]
        public ActionResult Edit(MatHang mh, HttpPostedFileBase HinhAnh)
        {
            var item = _dbContext.MatHang.Find(mh.MaMatHang);
            try
            {
                if (item == null)
                {
                    return HttpNotFound();
                }
                ViewBag.LoaiHang = _dbContext.LoaiHang.ToList();
                ViewBag.NhaCungCap = _dbContext.NCC.ToList();
                if (HinhAnh != null)
                {
                    string path = Path.Combine(Server.MapPath("/Content/images/collection/"),
                        Path.GetFileName(HinhAnh.FileName));
                    HinhAnh.SaveAs(path);
                    item.HinhAnh = "/Content/images/collection/" + Path.GetFileName(HinhAnh.FileName);
                }
                item.MaLoai = mh.MaLoai;
                item.MoTa = mh.MoTa;
                item.MaNCC = mh.MaNCC;
                item.DonGia = mh.DonGia;
                item.TenMatHang = mh.TenMatHang;
                item.Dvt = mh.Dvt;
                item.SoLuong = mh.SoLuong;
                _dbContext.MatHang.AddOrUpdate(item);
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                ViewBag.FileStatus = "Eror update images";
            }
            return RedirectToAction("ListProduct", "AdProduct");
        }

        public ActionResult Delete(MatHang matHang)
        {
            var item = _dbContext.MatHang.Find(matHang.MaMatHang);
            if (item == null)
            {
                return HttpNotFound();
            }
            _dbContext.MatHang.Remove(item);
            _dbContext.SaveChanges();
            return RedirectToAction("ListProduct", "AdProduct");
        }
    }
}