using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DangKiMonHoc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["Pages"] = "Home";
            return View();
        }
    }
}