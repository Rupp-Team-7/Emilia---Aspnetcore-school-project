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
    [Authorize]
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
                    Code = model.ProductCode,
                    Tags = model.Tags
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
        public async Task<IActionResult> Edit(int? id, string state)
        {
            if(state != null)
            {
                ViewBag.ReturnMessage = "Product has been " + state;
            }

            if (!id.HasValue)
                return BadRequest();

            var sellerid = await GetSellerID();
            var product = await db.Products.Include(p => p.Details)
                .SingleOrDefaultAsync(p => p.SellerId == sellerid && p.Id == id);

            CreateProductViewModel model = new CreateProductViewModel(product);

            ViewBag.IdToEdit = product.Id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? id, CreateProductViewModel model)
        {
            if (!id.HasValue)
                return BadRequest();

            var sellerid = await GetSellerID();
            var productToEdit = await db.Products.Include(p => p.Details)
                .SingleOrDefaultAsync(p => p.SellerId == sellerid && p.Id == id);

            if (productToEdit.Name != model.Name)
                productToEdit.Name = model.Name;
            if (productToEdit.category != model.Category)
                productToEdit.category = model.Category;
            if (productToEdit.UnitPrice != model.UnitPrice)
                productToEdit.UnitPrice = model.UnitPrice;

            if (productToEdit.Details.BrandName != model.BrandName)
                productToEdit.Details.BrandName = model.BrandName;
            if (productToEdit.Details.Origin != model.Origin)
                productToEdit.Details.Code = model.ProductCode;
            if (productToEdit.Details.Specification != model.Specification)
                productToEdit.Details.Specification = model.Specification;
            if (productToEdit.Details.Description != model.Description)
                productToEdit.Details.Description = model.Description;

            if (productToEdit.ImgPath != model.PhotoPath)
                productToEdit.ImgPath = model.PhotoPath;

            if(productToEdit.Details.Tags != model.Tags)
                productToEdit.Details.Tags = model.Tags;

            await db.SaveChangesAsync();

            return RedirectToAction(nameof(Edit), new { Id = productToEdit.Id, State ="updated" });
        }

        //Get: /Product/Detail/1
        public async Task<IActionResult> Detail(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var sellerId = await GetSellerID();
            var product = await db.Products
                .Include(p => p.Details)
                .Where(p => p.SellerId == sellerId)
                .SingleOrDefaultAsync(p => p.Id == id);

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Publish(int? id, bool? published)
        {
            if(!id.HasValue || !published.HasValue)
                return BadRequest(new {error = published});

            var sellerid = await GetSellerID();
            var product = await db.Products.SingleOrDefaultAsync(p => p.SellerId == sellerid && p.Id == id);

            if(product == null)
                return NotFound("product null");

          
                product.Published =  published.Value;
                await db.SaveChangesAsync();

            return Ok(published);
        }

        private async Task<int> GetSellerID()
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var seller = await db.Sellers.Where(s => s.Id == user.SellerID).AsNoTracking().SingleOrDefaultAsync();

            if (seller == null)
                return -1;
            return seller.Id;
        }
    }
}