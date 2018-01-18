using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emilia.Data;

namespace Emilia.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db;

        public ProductController(ApplicationDbContext db )
        {
            this.db = db;
        }

        // GET: /Product/index
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // GET: /Product/Edit/1
        public IActionResult Edit()
        {
            return View();
        }
    }
}