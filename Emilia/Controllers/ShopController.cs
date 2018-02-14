using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emilia.Models;
using Emilia.Data;
using Microsoft.EntityFrameworkCore;
using Emilia.Models.ShopViewModel;

namespace Emilia.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationDbContext db;
       
        public ShopController(ApplicationDbContext db)
        {
            this.db = db;
        }
        
        public IActionResult Page(int? id)
        {
            if(!id.HasValue)
            {
                return BadRequest("Bad request!");
            }

            var shop = db.Sellers.AsNoTracking()
                .SingleOrDefault(s => s.Id == id);

            if(shop == null)
                return NotFound();

            var model = new ShopIndexViewmodel {
                Id = shop.Id,
                Name = shop.Name,
                Logo = shop.Logo,
                Cover = shop.Cover,
                Type = shop.ShopType,
                Created = shop.CreatedDate.ToShortDateString()
            };

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
