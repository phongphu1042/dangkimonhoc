namespace DangKiMonHoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("dangkymonhoc")]
    public partial class dangkymonhoc
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(10)]
        public string mamon { get; set; }

        [StringLength(11)]
        public string mssv { get; set; }

        [StringLength(10)]
        public string mahocky { get; set; }

        public int? sotinchi { get; set; }

        public int sotien { get; set; }

        public virtual hocky hocky { get; set; }

        public virtual monhoc monhoc { get; set; }

        public virtual web_user web_user { get; set; }
    }
}
