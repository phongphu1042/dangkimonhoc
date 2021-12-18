namespace DangKiMonHoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("monhoc")]
    public partial class monhoc
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public monhoc()
        {
            dangkymonhocs = new HashSet<dangkymonhoc>();
            monhocmodks = new HashSet<monhocmodk>();
        }

        [Key]
        [StringLength(10)]
        public string mamon { get; set; }

        [StringLength(100)]
        public string tenmon { get; set; }

        public int? sotinchi { get; set; }

        public int? sotinchihocphi { get; set; }

        [StringLength(10)]
        public string maloai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dangkymonhoc> dangkymonhocs { get; set; }

        public virtual loaimon loaimon { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<monhocmodk> monhocmodks { get; set; }
    }
}
