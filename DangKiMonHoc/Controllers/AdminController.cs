using DangKiMonHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DangKiMonHoc.Controllers
{
    public class AdminController : Controller
    {
        DBContext db = new DBContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        # region Quản lý tài khoản
        [HttpGet]
        public ActionResult Account(int? username)
        {
            //ViewData["Nhom"] = db.group.ToList();
            if (username == null)
            {
                ViewData["ListUsers"] = db.web_user.OrderByDescending(us => us.username).ToList();
                ViewData["ListSinhVien"] = db.sinhviens.OrderByDescending(sv => sv.mssv).ToList();
            }
            else
            {
                ViewData["ListUsers"] = db.web_user.Where(u => u.group == username).OrderByDescending(u => u.username).ToList();
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddUser(string MaDN, string MatKhau, string NhapLaiMK, string MSSV, int Nhom)
        {
            if (MaDN == "" || MatKhau == "" || NhapLaiMK == "")
            {
                TempData["Error"] = "Thêm tài khoản thất bại!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
            else
            {
                if (!MatKhau.Equals(NhapLaiMK))
                {
                    TempData["Error"] = "Thêm tài khoản thất bại!";
                    ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                    return RedirectToAction("Account", "Admin");
                }
                else
                {
                    var v = db.web_user.Where(us => us.username.Equals(MaDN)).FirstOrDefault();
                    if (v == null)
                    {
                        if (Nhom ==0)
                        {
                            web_user us = new web_user
                            {
                                username = MaDN,
                                password = MatKhau,
                                mssv = MSSV,
                                group = Nhom,
                                locked = false
                            };
                            db.web_user.Add(us);
                            db.SaveChanges();
                        }
                        else
                        {
                            web_user us = new web_user
                            {
                                username = MaDN,
                                password = MatKhau,
                                mssv = null,
                                group = Nhom,
                                locked = false
                            };

                            db.web_user.Add(us);
                            db.SaveChanges();
                        }
                        TempData["Success"] = "Thêm tài khoản thành công!";
                        ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                        return RedirectToAction("Account", "Admin");
                    }
                    else
                    {
                        TempData["Error"] = "Thêm tài khoản thất bại! Người dùng đã tồn tại.";
                        ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                        return RedirectToAction("Account", "Admin");
                    }
                }
            }
        }

        public ActionResult DelUser(string username)
        {
            if (ModelState.IsValid)
            {
                var us = db.web_user.Find(username);
                db.web_user.Remove(us);
                db.SaveChanges();
                TempData["Success"] = "Xóa tài khoản thành công!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
            else
            {
                TempData["Error"] = "Xóa tài khoản thất bại!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
        }

        public ActionResult BanUser(string username)
        {
            if (ModelState.IsValid)
            {
                web_user us = db.web_user.Single(u => u.username.Equals(username));
                us.locked = true;
                db.SaveChanges();
                TempData["Success"] = "Khóa tài khoản thành công!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
            else
            {
                TempData["Error"] = "Khóa tài khoản thất bại!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
        }

        public ActionResult RestoreUser(string username)
        {
            if (ModelState.IsValid)
            {
                web_user us = db.web_user.Single(u => u.username.Equals(username));
                us.locked = false;
                db.SaveChanges();
                TempData["Success"] = "Khôi phục tài khoản thành công!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
            else
            {
                TempData["Error"] = "Khôi phục tài khoản thất bại!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
        }

        public ActionResult ResetPass(string username)
        {
            if (ModelState.IsValid)
            {
                web_user us = db.web_user.Single(u => u.username.Equals(username));
                us.password = "123456";
                db.SaveChanges();
                TempData["Success"] = "Khôi phục mật khẩu thành công!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
            else
            {
                TempData["Error"] = "Khôi phục mật khẩu thất bại!";
                ViewData["ListUsers"] = db.web_user.OrderByDescending(u => u.username).ToList();
                return RedirectToAction("Account", "Admin");
            }
        }
        #endregion



        #region Danh sách môn học mở đăng ký
        [HttpGet]
        public ActionResult Monhoc()
        {
            int hk = Convert.ToInt32(HttpContext.Session["HocKy"]);
            ViewData["HocKy"] = db.hockies.OrderByDescending(u => u.mahocky).ToList();
            return View();
        }
        #endregion
    }
}