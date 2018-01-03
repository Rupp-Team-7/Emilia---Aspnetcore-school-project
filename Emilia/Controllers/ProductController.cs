using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Emilia.Data;

namespace Emilia.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db;

        public ProductsController(ApplicationDbContext db )
        {
            this.db = db;
        }

        //Get: /Products/Detail/1
        public IActionResult Detail(int id)
        {
            //get a product that id relevelant

            return View();
        }        
    }
}