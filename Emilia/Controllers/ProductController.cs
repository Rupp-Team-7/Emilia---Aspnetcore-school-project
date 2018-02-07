using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Emilia.Data;
using Emilia.Models;
using Emilia.Models.ProductViewModel;

namespace Emilia.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;

        public ProductController(ApplicationDbContext db, UserManager<ApplicationUser> manager)
        {
            this.db = db;
            this.manager = manager;
        }

        // GET: /Product/index
        public async Task<IActionResult> Index(string term)
        {
            var model = new ProductIndexViewModel(await db.Products.ToListAsync());

            return View(model);
        }

        // GET: /Product/Create
        public IActionResult Create()
        {
            return View();
        }

        //POST: /Product/Create
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await manager.GetUserAsync(HttpContext.User);
            var seller = await db.Sellers.Where(s => s.Id == user.SellerID).AsNoTracking().SingleOrDefaultAsync();

            var product = new Product
            {
                Name = model.Name,
                category = model.Category,
                UnitPrice = model.UnitPrice,
                SellerId = seller.Id,
                Created = DateTime.UtcNow,
                Details = new ProductDetail
                {
                    BrandName = string.IsNullOrEmpty(model.BrandName) ? "No Brand" : model.BrandName,
                    Description = model.Description,
                    Specification = model.Specification,
                    Origin = string.IsNullOrEmpty(model.Origin) ? "N/A" : model.Origin,
                    Material = model.ProductCode
                },
                ImgPath = model.PhotoPath
            };

            await this.db.Products.AddAsync(product);
            await this.db.SaveChangesAsync();

            var created = await db.Products.AsNoTracking()
                .Where(p => p.Id == product.Id)
                .Include(p => p.Details)
                .SingleOrDefaultAsync();

            return View();

        }


        // GET: /Product/Edit/1
        public IActionResult Edit()
        {
            return View();
        }
    }
}