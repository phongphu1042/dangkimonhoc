namespace DangKiMonHoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("monhocmodk")]
    public partial class monhocmodk
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [Required]
        [StringLength(10)]
        public string mamon { get; set; }

        [Required]
        [StringLength(10)]
        public string mahocky { get; set; }

        public DateTime? ngaymo { get; set; }

        public DateTime? ngaydong { get; set; }

        public int? soluotmo { get; set; }

        public virtual hocky hocky { get; set; }

        public virtual monhoc monhoc { get; set; }
    }
}
