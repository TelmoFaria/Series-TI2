﻿using System.Web.Mvc;

namespace TF_TI2_19269_19262.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
