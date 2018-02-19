using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emilia.Models;
using Emilia.Data;
using Emilia.Models.OrderingViewModels;
using Microsoft.EntityFrameworkCore;

namespace Emilia.Controllers
{
    public class OrderingController : Controller
    {
        private ApplicationDbContext db;
        public OrderingController(ApplicationDbContext db)
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail()
        {
            var model = new OrderingDetailViewModel(db.Orders.ToList(),db.Products.ToList(),db.Customers.ToList());
            return View(model);
        }
        [HttpPost]
        public IActionResult Detail(int? id,Boolean ord)
        {
            var OrderChangeState = db.Orders.SingleOrDefault(o => o.Id == id);
            if (OrderChangeState.Delivery != ord)
                OrderChangeState.Delivery = ord;
            db.SaveChangesAsync();
            return View("Detail");
        }
        public IActionResult History()
        {
            return View();
        }
    }
}