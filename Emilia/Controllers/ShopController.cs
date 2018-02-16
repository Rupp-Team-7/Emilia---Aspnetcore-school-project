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
        
        //GET: /Shop/Page/1?f=all
        public async Task<IActionResult> Page(int? id, string f)
        {
            if(!id.HasValue)
            {
                return BadRequest("Bad request!");
            }

            ShopIndexViewmodel model = null;

            switch(f)
            {
                case "all":
                    model = await GetAllItem(id);
                break;
                case "best":
                    return View("~/Views/Shared/Maintain.cshtml");

                case "pick":
                    return View("~/Views/Shared/Maintain.cshtml");
            
                case "new":
                case null:
                case "":
                    model = await GetNewItem(id);
                break;
                default:
                    return View("~/Views/Shared/Maintain.cshtml");
            }

            if(model == null)
                return NotFound("Not found");


            return View(model);
        }

        public async Task<IActionResult> Find(int? id, string key = null)
        {
            if(!id.HasValue)
                return BadRequest();
            if(String.IsNullOrEmpty(key))
            {
                RedirectToAction(nameof(Page), new { id = id.Value, f = "all" });
            }

            var model = await db.Sellers.AsNoTracking()
                .Include(s => s.Products)
                .Select(s => new ShopIndexViewmodel{
                    Id = s.Id,
                    Name = s.Name,
                    Logo = s.Logo,
                    Cover = s.Cover,
                    Created = s.CreatedDate.ToShortDateString(),
                    Type = s.ShopType,
                    Items = s.Products.Where(p => p.Published && p.Name.Contains(key)).OrderBy(p => p.Created).ToList()
                }).SingleOrDefaultAsync(s => s.Id == id);

            ViewBag.Key = key;
            return View("Page", model);

        }

        //GET: /Shop/About/1
        public async Task<IActionResult> About(int? id)
        {
            if(!id.HasValue)
                return BadRequest("Bad Request.");

            var seller = await db.Sellers.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            var model = new ShopAboutViewmodel
            {
                Id = seller.Id,
                Name = seller.Name,
                Address= seller.Address,
                Cover = seller.Cover,
                Created= seller.CreatedDate.ToShortDateString(),
                Logo = seller.Logo,
                Type = seller.ShopType,
                Tel = seller.Tel,
                About = seller.About
            };

            return View(model);
        }


        private async Task<ShopIndexViewmodel> GetAllItem(int? id)
        {
             var model = await db.Sellers.AsNoTracking()
                .Include(s => s.Products)
                .Select(s => new ShopIndexViewmodel{
                    Id = s.Id,
                    Name = s.Name,
                    Logo = s.Logo,
                    Cover = s.Cover,
                    Created = s.CreatedDate.ToShortDateString(),
                    Type = s.ShopType,
                    Items = s.Products.Where(p => p.Published).Take(100).ToList()
                }).SingleOrDefaultAsync(s => s.Id == id);

                return model;
        }

        private async Task<ShopIndexViewmodel> GetNewItem(int? id)
        {
             var model = await db.Sellers.AsNoTracking()
                .Include(s => s.Products)
                .Select(s => new ShopIndexViewmodel{
                    Id = s.Id,
                    Name = s.Name,
                    Logo = s.Logo,
                    Cover = s.Cover,
                    Created = s.CreatedDate.ToShortDateString(),
                    Type = s.ShopType,
                    Items = s.Products.Where(p => p.Published).OrderBy(p => p.Created).Take(16).ToList()
                }).SingleOrDefaultAsync(s => s.Id == id);

                return model;
        }

        
    }
}
