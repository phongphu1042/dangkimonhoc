using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace DangKiMonHoc.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext1")
        {
        }

        public virtual DbSet<dangkymonhoc> dangkymonhocs { get; set; }
        public virtual DbSet<hedaotao> hedaotaos { get; set; }
        public virtual DbSet<hocky> hockies { get; set; }
        public virtual DbSet<loaimon> loaimons { get; set; }
        public virtual DbSet<lophoc> lophocs { get; set; }
        public virtual DbSet<monhoc> monhocs { get; set; }
        public virtual DbSet<monhocmodk> monhocmodks { get; set; }
        public virtual DbSet<sinhvien> sinhviens { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<web_user> web_user { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<dangkymonhoc>()
                .Property(e => e.mamon)
                .IsFixedLength();

            modelBuilder.Entity<dangkymonhoc>()
                .Property(e => e.mssv)
                .IsFixedLength();

            modelBuilder.Entity<dangkymonhoc>()
                .Property(e => e.mahocky)
                .IsFixedLength();

            modelBuilder.Entity<hedaotao>()
                .Property(e => e.mahe)
                .IsFixedLength();

            modelBuilder.Entity<hocky>()
                .Property(e => e.mahocky)
                .IsFixedLength();

            modelBuilder.Entity<hocky>()
                .HasMany(e => e.monhocmodks)
                .WithRequired(e => e.hocky)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<loaimon>()
                .Property(e => e.maloai)
                .IsFixedLength();

            modelBuilder.Entity<lophoc>()
                .Property(e => e.malop)
                .IsFixedLength();

            modelBuilder.Entity<monhoc>()
                .Property(e => e.mamon)
                .IsFixedLength();

            modelBuilder.Entity<monhoc>()
                .Property(e => e.maloai)
                .IsFixedLength();

            modelBuilder.Entity<monhoc>()
                .HasMany(e => e.monhocmodks)
                .WithRequired(e => e.monhoc)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<monhocmodk>()
                .Property(e => e.mamon)
                .IsFixedLength();

            modelBuilder.Entity<monhocmodk>()
                .Property(e => e.mahocky)
                .IsFixedLength();

            modelBuilder.Entity<sinhvien>()
                .Property(e => e.mssv)
                .IsFixedLength();

            modelBuilder.Entity<sinhvien>()
                .Property(e => e.malop)
                .IsFixedLength();

            modelBuilder.Entity<sinhvien>()
                .Property(e => e.mahe)
                .IsFixedLength();

            modelBuilder.Entity<web_user>()
                .Property(e => e.username)
                .IsFixedLength();

            modelBuilder.Entity<web_user>()
                .Property(e => e.mssv)
                .IsFixedLength();

            modelBuilder.Entity<web_user>()
                .HasMany(e => e.dangkymonhocs)
                .WithOptional(e => e.web_user)
                .HasForeignKey(e => e.mssv);
        }
    }
}
