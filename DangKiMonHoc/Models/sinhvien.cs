namespace DangKiMonHoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("sinhvien")]
    public partial class sinhvien
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public sinhvien()
        {
            web_user = new HashSet<web_user>();
        }

        [Key]
        [StringLength(11)]
        public string mssv { get; set; }

        [StringLength(50)]
        public string hodem { get; set; }

        [StringLength(50)]
        public string ten { get; set; }

        public bool? gioitinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ngaysinh { get; set; }

        [Column(TypeName = "date")]
        public DateTime? namvao { get; set; }

        [StringLength(10)]
        public string malop { get; set; }

        [StringLength(10)]
        public string mahe { get; set; }

        public virtual hedaotao hedaotao { get; set; }

        public virtual lophoc lophoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<web_user> web_user { get; set; }
    }
}
