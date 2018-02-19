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
using Emilia.Extensions;
using Emilia.Models.HomeViewmodel;

namespace Emilia.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        public HomeController(ApplicationDbContext db)
        {
            this.db = db;
        }
        //Get: /, /home/
        public async Task<IActionResult> Index()
        {
            var products = db.Products.AsNoTracking().Where(x => x.Published).OrderBy(x => x.Created);
            var seller = db.Sellers.AsNoTracking().OrderBy(x => x.CreatedDate);

            var model = new IndexViewmodel {
                ShopItems = await products.Select(p => new ShopItem {
                    Name = p.Name,
                    Price = p.UnitPrice,
                    Image= p.FirstImage(),
                    Id = p.Id
                }).Take(16).ToListAsync(),

                SellerItems = await seller.Select(s => new SellerItem {
                    Id = s.Id,
                    Name = s.Name,
                    Logo = s.Logo
                }).Take(8).ToListAsync()
            };

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
