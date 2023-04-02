using ClothStore.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothStore.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        public ActionResult Index()
        {
            return View();
        }
        private readonly ApplicationDbContext dbContext = new ApplicationDbContext();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGiohang = Session["Giohang"] as List<GioHang>;
            if (lstGiohang == null)
            {
                lstGiohang = new List<GioHang>();
                Session["Giohang"] = lstGiohang;
            }
            return lstGiohang;
        }

        public ActionResult ThemGioHang(string MaMatHang, string strURL)
        {
            List<GioHang> lstGiohang = LayGioHang();
            GioHang sanpham = lstGiohang.Find(n => n.maMatHang == MaMatHang);
            if (sanpham == null)
            {
                sanpham = new GioHang(MaMatHang);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.SoLuong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Sum(n => n.SoLuong);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tsl = lstGioHang.Count;
            }
            return tsl;
        }
        private long? TongTien()
        {
            long? tt = 0;
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang != null)
            {
                tt = lstGioHang.Sum(n => n.ThanhTien);
            }
            return tt;
        }

        public ActionResult GioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongsoLuong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongsoLuong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }
        public ActionResult XoaGioHang(string MaMatHang)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.maMatHang == MaMatHang);
            if (sanpham != null)
            {
                lstGioHang.RemoveAll(n => n.maMatHang == MaMatHang);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapNhatGioHang(string MaMatHang, FormCollection collection)
        {
            List<GioHang> lstGioHang = LayGioHang();
            GioHang sanpham = lstGioHang.SingleOrDefault(n => n.maMatHang == MaMatHang);
            if (sanpham != null)
            {
                sanpham.SoLuong = int.Parse(collection["txtSoLg"].ToString());
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            lstGioHang.Clear();
            return RedirectToAction("GioHang");
        }
        [Authorize]
        [HttpGet]
        public ActionResult DatHang()
        {
            List<GioHang> lstGioHang = LayGioHang();
            ViewBag.TongsoLuong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            ViewBag.TenKH = User.Identity.GetUserName();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindById(User.Identity.GetUserId());
            ViewBag.Name = user.Name;
            ViewBag.SoDT = user.PhoneNumber;
            ViewBag.DiaChi = user.Address;
            return View(lstGioHang);
        }
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            HoaDon hoadon = new HoaDon();
            MatHang mathang = new MatHang();

            List<GioHang> lstCart = LayGioHang();
            ///var ngaygiao = String.Format("{0:dd/MM/yyyy}", collection["NgayGiao"]);
            var userId = User.Identity.GetUserId();
            hoadon.Id = userId;
            hoadon.NgayLap = DateTime.Now;
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = userManager.FindById(User.Identity.GetUserId());
            /*hoadon.DiaChiGiao = user.Address;*/
            //order.Delivery_date = DateTime.Parse(ngaygiao);
            hoadon.GiaoHang = false;
            hoadon.TongTien = TongTien();
            hoadon.ThanhToan = false;
            hoadon.TrangThai = true;
            hoadon.DiaChiGiao = user.Address;

            dbContext.HoaDon.Add(hoadon);
            dbContext.SaveChanges();

            foreach (var item in lstCart)
            {
                CT_HoaDon detail = new CT_HoaDon();
                detail.MaHoaDon = hoadon.MaHoaDon;
                detail.MaMatHang = item.maMatHang;
                detail.SoLuong = item.SoLuong;
                mathang = dbContext.MatHang.FirstOrDefault(n => n.MaMatHang == item.maMatHang);
                mathang.SoLuong -= item.SoLuong;
                dbContext.SaveChanges();

                dbContext.CT_HoaDon.Add(detail);
            }
            dbContext.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }
}