namespace ClothStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CT_HoaDon
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string MaMatHang { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaHoaDon { get; set; }

        public int? SoLuong { get; set; }

        public virtual HoaDon HoaDon { get; set; }

        public virtual MatHang MatHang { get; set; }
    }
}
