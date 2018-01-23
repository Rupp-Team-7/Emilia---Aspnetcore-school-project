using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emilia.Models;
using Emilia.Data;
using Microsoft.EntityFrameworkCore;

namespace Emilia.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db;
       
        public ShopController(ApplicationDbContext db)
        {
            this.db = db;
        }

        //Get: /, /shop/
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Profile(int id)
        {
            var model = db.Sellers.SingleOrDefault(s => s.Id == id);

            // return (model);

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
