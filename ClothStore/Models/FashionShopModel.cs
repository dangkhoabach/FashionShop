using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ClothStore.Models
{
    public partial class FashionShopModel : DbContext
    {
        public FashionShopModel()
            : base("name=FashionShopModel")
        {
        }

        public virtual DbSet<CT_HoaDon> CT_HoaDon { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<LoaiHang> LoaiHang { get; set; }
        public virtual DbSet<MatHang> MatHang { get; set; }
        public virtual DbSet<NCC> NCC { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CT_HoaDon>()
                .Property(e => e.MaMatHang)
                .IsFixedLength();

            modelBuilder.Entity<CT_HoaDon>()
                .Property(e => e.MaLoai)
                .IsFixedLength();

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.CT_HoaDon)
                .WithRequired(e => e.HoaDon)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LoaiHang>()
                .Property(e => e.MaLoai)
                .IsFixedLength();

            modelBuilder.Entity<MatHang>()
                .Property(e => e.MaMatHang)
                .IsFixedLength();

            modelBuilder.Entity<MatHang>()
                .Property(e => e.MaLoai)
                .IsFixedLength();

            modelBuilder.Entity<MatHang>()
                .Property(e => e.MaNCC)
                .IsFixedLength();

            modelBuilder.Entity<MatHang>()
                .HasMany(e => e.CT_HoaDon)
                .WithRequired(e => e.MatHang)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NCC>()
                .Property(e => e.MaNCC)
                .IsFixedLength();
        }
    }
}
