﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abc.MvcWebUI.Entity;

namespace Abc.MvcWebUI.Controllers
{
    public class HomeController : Controller
    {
        DataContext _context = new DataContext();
        // GET: Home
        public ActionResult Index()
        {

            return View(_context.Products.Where(x => x.IsHome && x.IsApproved).ToList());
        }

        public ActionResult Details(int id)
        {
            return View(_context.Products.FirstOrDefault(x => x.Id == id));
        }

        public ActionResult List()
        {
            return View(_context.Products.Where(x => x.IsApproved).ToList());
        }
    }
}