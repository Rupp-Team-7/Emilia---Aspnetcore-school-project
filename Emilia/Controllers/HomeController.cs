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
        public async Task<IActionResult> Search(string keyword, string category, string price, int? page)
        {
            if(keyword != null)
                page = 1;

            var query = db.Products.Select(p => new ShopItem {
                Name = p.Name,
                Price = p.UnitPrice,
                Image= p.FirstImage(),
                Id = p.Id
            });

            if(!String.IsNullOrEmpty(keyword))
            {
                query = query.Where(p =>p.Name.Contains(keyword));
            }

            var model = await PagingList<ShopItem>.CreateAsyn(query.AsNoTracking(), page??1, 5);

            return View(model);
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
