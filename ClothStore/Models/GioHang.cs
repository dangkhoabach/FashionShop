using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothStore.Models
{
    public class GioHang
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        public string maMatHang { get; set; }
        public string TenMatHang { get; set; }
        public string HinhAnh { get; set; }
        public long? DonGia { get; set; }
        public int SoLuong { get; set; }
        public string Dvt { get; set; }
        public long? ThanhTien { get { return DonGia * SoLuong; } }
        public GioHang(string mahang)
        {
            maMatHang = mahang;
            MatHang matHang = _dbContext.MatHang.Single(n => n.MaMatHang == maMatHang);
            TenMatHang = matHang.TenMatHang;
            HinhAnh = matHang.HinhAnh;
            DonGia = matHang.DonGia;
            Dvt = matHang.Dvt;
            SoLuong = 1;
        }
    }
}