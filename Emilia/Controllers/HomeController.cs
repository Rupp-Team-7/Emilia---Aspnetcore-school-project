using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emilia.Models;
using Emilia.Data;
using Emilia.Models.HomeViewModels;
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
            var model = new ProductHomeViewModel(db.Products.Where(p=>p.Id==id).ToList(), db.ProductDetail.Where(p=>p.Id==GetProductsId(id)).ToList(), db.Sellers.ToList());
            return View(model);
        }
        [HttpPost]
        public IActionResult Ordering(CreateOrderingViewModel modal)
        {
            var order = new Order
            {
                CustomerId = modal.CustomerId,
                Quanity = modal.Quanity,
                SellerId = modal.SellerId,
                TotalPrice = modal.TotalPrice,
                orderDate = modal.orderDate,
                product = new Product
                {
                    Id = modal.ProductId
                },
                Delivery = modal.Delivery

            };
            this.db.Orders.AddAsync(order);
            db.SaveChangesAsync();
            return View();
        }
        public int GetProductsId(int id)
        {
            var detail = db.Products.SingleOrDefault(d => d.Id == id);
            return detail.DetailId;
        }
        public int GetSellerId(int id)
        {
            var seller = db.Products.SingleOrDefault(p => p.Id == id);
            return seller.SellerId;
        }
        
    }
}
