namespace DangKiMonHoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class web_user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public web_user()
        {
            dangkymonhocs = new HashSet<dangkymonhoc>();
        }

        [Key]
        [StringLength(11)]
        public string username { get; set; }

        [StringLength(100)]
        public string password { get; set; }

        [StringLength(11)]
        public string mssv { get; set; }

        public bool? locked { get; set; }

        public int? group { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<dangkymonhoc> dangkymonhocs { get; set; }

        public virtual sinhvien sinhvien { get; set; }
    }
}
