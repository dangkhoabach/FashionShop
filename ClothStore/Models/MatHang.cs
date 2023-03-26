namespace ClothStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MatHang")]
    public partial class MatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MatHang()
        {
            CT_HoaDon = new HashSet<CT_HoaDon>();
        }

        [Key]
        [StringLength(10)]
        public string MaMatHang { get; set; }

        [StringLength(500)]
        public string TenMatHang { get; set; }

        [StringLength(50)]
        public string Dvt { get; set; }

        public long? DonGia { get; set; }

        public string MoTa { get; set; }

        [StringLength(10)]
        public string MaLoai { get; set; }

        [StringLength(10)]
        public string MaNCC { get; set; }

        public string HinhAnh { get; set; }

        public int? SoLuong { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CT_HoaDon> CT_HoaDon { get; set; }

        public virtual LoaiHang LoaiHang { get; set; }

        public virtual NCC NCC { get; set; }
    }
}
