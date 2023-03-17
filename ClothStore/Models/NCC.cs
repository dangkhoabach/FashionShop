namespace ClothStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NCC")]
    public partial class NCC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NCC()
        {
            MatHang = new HashSet<MatHang>();
        }

        [Key]
        [StringLength(10)]
        public string MaNCC { get; set; }

        [StringLength(500)]
        public string TenNCC { get; set; }

        public int? Sdt { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MatHang> MatHang { get; set; }
    }
}
