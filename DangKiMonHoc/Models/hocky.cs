namespace DangKiMonHoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hocky")]
    public partial class hocky
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public hocky()
        {
            dangkymonhocs = new HashSet<dangkymonhoc>();
            monhocmodks = new HashSet<monhocmodk>();
        }

        [Key]
        [StringLength(10)]
        public string mahocky { get; set; }

        [StringLength(50)]
        public string tenhocky { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dangkymonhoc> dangkymonhocs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<monhocmodk> monhocmodks { get; set; }
    }
}
