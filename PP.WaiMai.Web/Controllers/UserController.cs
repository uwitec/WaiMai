﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WaiMai.Web.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Restaurant()
        {
            return View();
        }
        public ActionResult FoodMenuCategory()
        {
            return View();
        }
        public ActionResult Add() {

            return View();
        }
	}
}