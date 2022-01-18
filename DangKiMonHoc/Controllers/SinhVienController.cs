using DangKiMonHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DangKiMonHoc.Controllers
{
    public class SinhVienController : Controller
    {
        DBContext db = new DBContext(); 
        // GET: SinhVien
        public ActionResult Index()
        {
            return View();
        }

        #region Đăng ký môn học
        public ActionResult DangKy()
        {
            if (HttpContext.Session["CurrentLogin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var now = DateTime.Now;
                var v = db.monhocmodks.Where(tg => tg.close == false).FirstOrDefault();
                if (v == null)
                {
                    return RedirectToAction("Index", "SinhVien");
                }
                else
                {
                    string msv = HttpContext.Session["CurrentLogin"].ToString();
                    var sv = db.web_user.Find(msv);

                    ViewData["MoDK"] = db.monhocmodks.ToList();
                    ViewData["MonHoc"] = db.monhocs.ToList();

                    ViewData["KetQuaDK"] = db.dangkymonhocs.Where(dk => dk.mssv == sv.mssv).ToList();
                    return View();
                }
            }
        }
        [HttpGet]
        public ActionResult DangKyMon(int? id)
        {
            string msv = HttpContext.Session["CurrentLogin"].ToString();
            var sv = db.web_user.Find(msv);
            var u = db.monhocmodks.Where(mdk => mdk.id == id).FirstOrDefault();
            string monhoc = u.mamon;
            var mh = db.monhocs.Find(monhoc);
            var v = db.dangkymonhocs.Where(dk => dk.mamon == u.mamon && dk.mssv==sv.mssv).FirstOrDefault();
            if (v == null)
            {

                dangkymonhoc dk = new dangkymonhoc
                {
                    mamon = u.mamon,
                    mssv = sv.mssv,
                    mahocky = u.mahocky,
                    sotinchi = mh.sotinchi,
                    sotien = (mh.sotinchihocphi.Value*560000)

                };
                db.dangkymonhocs.Add(dk);
                db.SaveChanges();
                TempData["Success"] = "Đăng ký thành công!";
                return RedirectToAction("DangKy", "SinhVien");
            }
            else
            {
                TempData["Error"] = "Đăng ký thất bại! Bạn đã đăng ký môn học này. Vui lòng chọn môn học khác.";
                return RedirectToAction("DangKy", "SinhVien");
            }
        }

        [HttpPost]
        public ActionResult HuyDangKy(int id)
        {
            if (ModelState.IsValid)
            {
                dangkymonhoc dk = db.dangkymonhocs.Single(d => d.id == id);
                db.dangkymonhocs.Remove(dk);
                db.SaveChanges();

                TempData["Success"] = "Hủy đăng ký môn học thành công!";
            }
            else
            {
                TempData["Error"] = "Hủyđăng ký môn học thất bại!";
            }
            return RedirectToAction("DangKy", "SinhVien");
        }

        # endregion

        #region Thông tin sinh viên
        public ActionResult Profile()
        {
            if (HttpContext.Session["CurrentLogin"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                string us = HttpContext.Session["CurrentLogin"].ToString();
                ViewData["SinhVien"] = db.sinhviens.SingleOrDefault(sv => sv.mssv.Equals(us));
                return View();
            }

        }
        [HttpPost]
        public ActionResult EditSV(string HoTen, int GioiTinh, DateTime NgaySinh)
        {
            string msv = HttpContext.Session["CurrentLogin"].ToString();
            var v = db.web_user.Find(msv);
            bool gt = false;
            int vt = HoTen.LastIndexOf(' ');
            string ten = HoTen.Substring(vt);
            string hodem = HoTen.Remove(vt).Trim();
            if (GioiTinh == 1)
            {
                gt = true;
            }
            if (v != null)
            {

                sinhvien sv = db.sinhviens.Single(s => s.mssv.Equals(v.mssv));
                {
                    sv.ten = ten;
                    sv.hodem = hodem;
                    sv.gioitinh = gt;
                    sv.ngaysinh = NgaySinh;
                };

                db.SaveChanges();
                TempData["Success"] = "Sửa thành công";
                return RedirectToAction("Profile", "SinhVien");
            }
            else
            {
                TempData["Error"] = "Sửa thất bại!";
                return RedirectToAction("Profile", "SinhVien");
            }
            return RedirectToAction("Profile", "SinhVien");
        }
        [HttpPost]
        public ActionResult EditPassword(string Password, string NewPassword, string ConfirmPassword)
        {
            string msv = HttpContext.Session["CurrentLogin"].ToString();
            var v = db.web_user.Find(msv);
            if (v != null)
            {
                if (Password.Equals(v.password.Trim()))
                {
                    if (string.Compare(Password, NewPassword, true) == 0)
                    {
                        TempData["Error"] = "Mật khẩu cũ và mới giống nhau";
                    }
                    else if (string.Compare(NewPassword, ConfirmPassword, true) != 0)
                    {
                        TempData["Error"] = "Mật khẩu nhập lại không trùng khớp";
                    }
                    else
                    {
                        web_user wu = db.web_user.Single(w => w.mssv.Equals(v.mssv));
                        {
                            wu.password = NewPassword;

                        }
                        db.SaveChanges();
                        TempData["Success"] = "Sửa thành công";
                    }
                }
                else
                {
                    TempData["Error"] = "Sai mật khẩu";
                    return RedirectToAction("Profile", "SinhVien");
                }

                return RedirectToAction("Profile", "SinhVien");
            }
            else
            {
                TempData["Error"] = "Sửa thất bại!";
                return RedirectToAction("Profile", "SinhVien");
            }
            return RedirectToAction("Profile", "SinhVien");
        }

        #endregion


        #region QuenMatKhau
        public ActionResult ForgotPassword()
        {
            return View();
        }
        #endregion
    }
}