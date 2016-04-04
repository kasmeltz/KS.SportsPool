﻿using KS.SportsPool.MVC.Utility;
using System.Web.Mvc;

namespace KS.SportsPool.MVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            return View();
        }

        public ActionResult Pools()
        {
            ViewBag.Title = UIUtilities.SiteTitle;
            return View();
        }
    }
}