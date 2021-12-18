using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DangKiMonHoc.Models;

namespace DangKiMonHoc.Controllers
{
    public class AccountsController : Controller
    {
        DBContext db = new DBContext();
        // GET: Accounts
        public ActionResult Index()
        {
            if (HttpContext.Session["CurrentLogin"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["Pages"] = "Login";
                return View();
            }
        }
        [HttpPost]
        public ActionResult Login(string MaTK, string MatKhau)
        {
            var v = db.web_user.Where(us => us.username.Equals(MaTK) && us.password.Equals(MatKhau)).FirstOrDefault();
            if (v != null && v.locked == false && v.group==1)
            {
                string group = v.group.ToString();
                string account = v.username.ToString();
                Session["CurrentLogin"] = account;
                Session["Group"] = group;
                Session["Pages"] = "Admin";
                    return RedirectToAction("Index", "Admin");
            }
            else if (v != null && v.locked == false && v.group == 2)
            {
                string group = v.group.ToString();
                string account = v.username.ToString();
                Session["CurrentLogin"] = account;
                Session["Group"] = group;
                Session["Pages"] = "SinhVien";
                return RedirectToAction("Index", "SinhVien");
            }
            else
            {
                return RedirectToAction("Index", "Accounts");
            }
        }

        public ActionResult Logout()
        {
            RemoveCookieSession();
            return RedirectToAction("Index", "Home");
        }
        public void RemoveCookieSession()
        {
            Session.RemoveAll();
            FormsAuthentication.SignOut();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);

            HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie2);
        }
    }
}