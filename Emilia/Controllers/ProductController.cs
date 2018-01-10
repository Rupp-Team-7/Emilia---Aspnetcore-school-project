using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emilia.Data;

namespace Emilia.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db;

        public ProductsController(ApplicationDbContext db )
        {
            this.db = db;
        }

        public  IActionResult Index()
        {
            return View();
        }
    }
}