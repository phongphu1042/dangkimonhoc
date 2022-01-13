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
        public ActionResult Account(int? id)
        {
            ViewData["Nhom"] = id;
            if (id == null)
            {
                ViewData["ListUsers"] = db.web_user.OrderByDescending(us => us.username).ToList();
                ViewData["ListSinhVien"] = db.sinhviens.OrderByDescending(sv => sv.mssv).ToList();
            }
            else
            {
                ViewData["ListUsers"] = db.web_user.Where(u => u.group == id).OrderByDescending(u => u.username).ToList();
                ViewData["ListSinhVien"] = db.sinhviens.OrderByDescending(sv => sv.mssv).ToList();
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
                        if (Nhom == 0)
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
                us.password = username;
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
        public ActionResult AddAll()
        {
            var svs = db.sinhviens.ToList();
            int add = 0;
            foreach(var sv in svs)
            {
                web_user us = db.web_user.SingleOrDefault(u=>u.mssv.Equals(sv.mssv));
                if (us == null)
                {
                    web_user u = new web_user
                    {
                        username = sv.mssv,
                        password = sv.mssv,
                        mssv = sv.mssv,
                        group = 2,
                        locked = false
                    };
                    add++;
                    db.web_user.Add(u);
                }
            }
            if (add > 0)
            {
                TempData["Success"] = "Thêm tài khoản cho " +add+" sinh viên thành công";
            }
            else
            {
                TempData["Error"] = "Tất cả sinh viên đã có tài khoản!";
            }
            db.SaveChanges();
            return RedirectToAction("Account", "Admin");
        }
        #endregion



        #region Quản lý môn học
        [HttpGet]
        public ActionResult Monhoc()
        {
            ViewData["LoaiMon"] = db.loaimons.ToList();
            ViewData["MonHoc"] = db.monhocs.ToList();
            ViewData["HocKy"] = db.hockies.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddMon(string MaMon, string TenMon, int SoTC, int SoTCHP, string HocKy)
        {

            return RedirectToAction("Monhoc", "Admin");
        }

        #endregion

        #region Quản lý đăng ký
        [HttpGet]
        public ActionResult Qldangky()
        {
            ViewData["MonHoc"] = db.monhocs.ToList();
            ViewData["HocKy"] = db.hockies.ToList();
            ViewData["MoDK"] = db.monhocmodks.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult OpenDK(string HocKy , string NGAYMO, string NGAYDONG, int? SOSV)
        {
            
            DateTime _ngay1 = DateTime.ParseExact(NGAYMO, "dd/MM/yyyy HH:mm:ss tt", null);
            DateTime _ngay2 = DateTime.ParseExact(NGAYDONG, "dd/MM/yyyy HH:mm:ss tt", null);
            var _hocky = db.hockies.Where(l => l.mahocky == HocKy).FirstOrDefault();
            //List<monhoc> listmh = new List<monhoc>();
            if (_hocky != null)
            {
                var v = db.monhocmodks.Where(us => us.mahocky == _hocky.mahocky).FirstOrDefault();
                if (v == null)
                {
                    var listmh = db.monhocs.Where(us => us.mahocky == _hocky.mahocky).OrderByDescending(us => us.mamon).ToList();
                    foreach (var mh in listmh)
                    {
                        monhocmodk dk = new monhocmodk
                        {
                            mamon = mh.mamon,
                            mahocky = _hocky.mahocky,
                            ngaymo = _ngay1,
                            ngaydong = _ngay2,
                            soluotmo = SOSV,
                            close = false
                        };
                        db.monhocmodks.Add(dk);
                    }
                    db.SaveChanges();
                    TempData["Success"] = "Mở đăng ký thành công!";
                    return RedirectToAction("Qldangky", "Admin");
                }
                else if (v.close == true)
                {
                    var list = db.monhocmodks.Where(us => us.mahocky == HocKy).OrderByDescending(us => us.mamon).ToList();
                    foreach (var mh in list)
                    {
                        monhocmodk tg = db.monhocmodks.Single(l => l.id == mh.id);
                        {
                            tg.ngaymo = _ngay1;
                            tg.ngaydong = _ngay2;
                            tg.soluotmo = SOSV;
                            tg.close = false;
                        };
                    }
                    db.SaveChanges();
                    TempData["Success"] = "Mở đăng ký thành công!";
                    return RedirectToAction("Qldangky", "Admin");
                }
                else
                {
                    TempData["Error"] = "Mở đăng ký thất bại! Môn đăng ký đã mở.";
                    return RedirectToAction("Qldangky", "Admin");
                }
            }
            else
            {
                TempData["Error"] = "Mở đăng ký thất bại! Chưa chọn học kỳ mở đăng ký";
                return RedirectToAction("Qldangky", "Admin");
            }
        }
         [HttpPost]
        public ActionResult CloseDK(string HocKy)
        {
            if (HocKy != null)
            {
                var listmh = db.monhocmodks.Where(us => us.mahocky == HocKy).OrderByDescending(us => us.mamon).ToList();
                if (listmh.Count() > 0)
                {
                    foreach (var mh in listmh)
                    {
                        monhocmodk tg = db.monhocmodks.Single(l => l.id == mh.id);
                        {
                            tg.ngaymo = null;
                            tg.ngaydong = null;
                            tg.soluotmo = null;
                            tg.close = true;
                        };
                    }
                    db.SaveChanges();

                    TempData["Success"] = "Đóng đăng ký thành công!";
                    return RedirectToAction("Qldangky", "Admin");
                }
                else
                {
                    TempData["Error"] = "Đóng đăng ký thất bại!";
                    return RedirectToAction("Qldangky", "Admin");
                }
            }
            else
            {
                TempData["Error"] = "Đóng đăng ký thất bại! Chưa chọn học kỳ để hủy.";
                return RedirectToAction("Qldangky", "Admin");
            }
        }

        [HttpPost]
        public ActionResult CloseID(int id)
        {
            if (ModelState.IsValid)
            {
                var mh = db.monhocmodks.Where(m => m.id == id).FirstOrDefault();
                mh.ngaymo = null;
                mh.ngaydong = null;
                mh.soluotmo = null;
                mh.close = true;
                db.SaveChanges();
                TempData["Success"] = "Đóng đăng ký môn "+mh.monhoc.tenmon +" thành công";
                return RedirectToAction("Qldangky", "Admin");
            }
            else
            {
                TempData["Error"] = "Đóng đăng ký môn học thất bại!";
                return RedirectToAction("Qldangky", "Admin");
            }
        }    

        [HttpPost]
        public ActionResult OpenSinger(string Mon, string HocKy )
        {
            var _hocky = db.hockies.Where(l => l.mahocky == HocKy).FirstOrDefault();
            var _monhoc = db.monhocs.Where(m => m.mamon == Mon).FirstOrDefault();
            if (_hocky != null)
            {
                if (_hocky != null)
                {
                    var vs = db.monhocmodks.Where(us => us.mahocky == _hocky.mahocky).FirstOrDefault();
                    var v = db.monhocmodks.Where(us => us.mahocky == _hocky.mahocky && us.mamon == Mon).FirstOrDefault();
                    if (v == null && vs == null)
                    {
                        monhocmodk dk = new monhocmodk
                        {
                            mamon = Mon,
                            mahocky = _hocky.mahocky,
                            ngaymo = null,
                            ngaydong = null,
                            soluotmo = null,
                            close = true
                        };
                        db.monhocmodks.Add(dk);
                        db.SaveChanges();
                        TempData["Success"] = "Thêm môn vào danh sách "+_hocky.tenhocky+" thành công!";
                        return RedirectToAction("Qldangky", "Admin");
                    }
                    else if (v == null && vs != null)
                    {
                        monhocmodk tg = new monhocmodk
                        {
                            mamon = Mon,
                            mahocky = _hocky.mahocky,
                            ngaymo = vs.ngaymo,
                            ngaydong = vs.ngaydong,
                            soluotmo = vs.soluotmo,
                            close = false
                        };
                        db.monhocmodks.Add(tg);
                        db.SaveChanges();
                        TempData["Success"] = "Thêm môn vào danh sách " + _hocky.tenhocky + " thành công!";
                        return RedirectToAction("Qldangky", "Admin");
                    }
                    else
                    {
                        TempData["Error"] = "Mở đăng ký thất bại! Môn đăng ký đã có trong danh sách " + _hocky.tenhocky;
                        return RedirectToAction("Qldangky", "Admin");
                    }
                }
                else
                {
                    TempData["Error"] = "Mở đăng ký thất bại! Chưa có môn cần mở";
                    return RedirectToAction("Qldangky", "Admin");
                }
            }
            else
            {
                TempData["Error"] = "Mở đăng ký thất bại! Chưa chọn học kỳ mở đăng ký";
                return RedirectToAction("Qldangky", "Admin");
            }
            return RedirectToAction("Qldangky", "Admin");
        }

        public JsonResult SeachMon(string search)
        {
            List<String> allsearch = db.monhocs.Where(x => x.tenmon.Contains(search)).Select(x=>x.tenmon).ToList();
            //return new JsonResult { Data = allsearch, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            return Json(new
            {
                data = allsearch,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}