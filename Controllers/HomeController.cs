﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Reflection.Emit;
using System.Web.Security;
using BROWSit.Models;
using BROWSit.Helpers;

namespace BROWSit.Controllers
{
    public class HomeController : Controller
    {
        BROWSit.DAL.BROWSitContext db = new BROWSit.DAL.BROWSitContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Title = "Login";
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                User userMatch = db.Users.FirstOrDefault(u => u.Username == user.Username);
                if (userMatch != null)
                {
                    if (Helpers.LoginHelper.IsPasswordMatch(user.Hash, userMatch.Salt, userMatch.Hash))
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect login.");
                    }
                }
            }
            return View(user);
        }
    }
}
