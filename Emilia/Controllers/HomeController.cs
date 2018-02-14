using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emilia.Models;
using Emilia.Data;
using Emilia.Models.ProductViewModel;
using Microsoft.EntityFrameworkCore;

namespace Emilia.Controllers
{
    public class HomeController : Controller
    {

        public ApplicationDbContext db;
        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }
        //Get: /, /home/
        public IActionResult Index()
        {
            var model = new ProductHomeViewModel(db.Products.ToList(), db.ProductDetail.ToList(), db.Sellers.ToList());
            return View(model);
        }

        //Get: /Search?querystring...
        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET: /home/Product/1
        public IActionResult Product(int id)
        {
            var model = new ProductHomeViewModel(db.Products.Where(p=>p.Id==id).ToList(), db.ProductDetail.ToList(), db.Sellers.ToList());
            return View(model);
        }
        public IActionResult Ordering(Product pro)
        {
            
            return View();
        }
    }
}
