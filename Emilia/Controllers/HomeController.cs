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
using Emilia.Extensions;
using Emilia.Models.HomeViewmodel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Emilia.Controllers
{
    
    public class HomeController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;
        public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> manager)
        {
            this.db = db;
            this.manager = manager;
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
        public async Task<IActionResult> Search(string keyword, string category, string price, int? page)
        {
            ViewData["keyword"] = keyword;
            ViewData["category"] = category;
            ViewData["price"] = price;

            if(keyword != null)
                page = 1;

            var query = db.Products.Where(x => x.Published);

            if(!String.IsNullOrEmpty(keyword))
            {
                query = query.Where(p =>p.Name.Contains(keyword));
            }

            if(!String.IsNullOrEmpty(category) && category != "all")
            {
                query = query.Where(p => p.category.ToString().ToLower() == category );
            }

            if(!String.IsNullOrEmpty(price) && price != "all")
            {
                switch(price)
                {
                    case "lower":
                        query = query.Where(p => p.UnitPrice < 10);
                        break;
                    case "1":
                        query = query.Where(p =>p.UnitPrice >= 10 && p.UnitPrice < 50);
                        break;
                    case "2":
                        query= query.Where(p =>p.UnitPrice >= 50 && p.UnitPrice < 100);
                        break;
                    case "higher":
                        query= query.Where(p =>p.UnitPrice >= 100);
                        break;
                }
                query = query.OrderBy(p => p.UnitPrice);
            }

            var items = query.Select(p => new ShopItem {
                Name = p.Name,
                Price = p.UnitPrice,
                Image= p.FirstImage(),
                Id = p.Id
            });
            var model = await PagingList<ShopItem>.CreateAsyn(items.AsNoTracking(), page??1, 4);

            return View(model);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET: /home/Product/1
        public IActionResult Product(int id)
        {
            var model = new ProductHomeViewModel(db.Products.Include(pd=>pd.Details).Include(s=>s.Seller).Where(p=>p.Id==id).ToList());
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Product(CreateOrderingViewModel modal)
        {

            if(!ModelState.IsValid)
            {

            }
            var user = await manager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NoContent();
            }
             
           
            var cid =  (await db.Customers.SingleOrDefaultAsync(x => x.UserId == user.Id)).Id;
            
            //var cus = await db.Customers.Where(s => s.Id == user.CustomerID).AsNoTracking().SingleOrDefaultAsync();
            var order = new Order
            {
                CustomerId = cid ,
                Quanity = modal.Quanity,
                SellerId = modal.SellerId,
                TotalPrice = modal.TotalPrice,
                OrderDate = modal.orderDate,
                ProductId = modal.ProductId,
                Delivery = modal.Delivery

            };

            db.Orders.Add(order);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }
            
        
            return Ok();
        }
    }
}
