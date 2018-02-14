using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Emilia.Models;
using Emilia.Models.ManageViewModels;
using Emilia.Services;
using Emilia.Models.ShopManagementViewModels;
using Emilia.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Emilia.Controllers
{
    [Authorize]
    public class ShopManagementController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ApplicationDbContext db;
        private IHostingEnvironment environment;

        [TempData]
        public string Message { get; set; }


        public ShopManagementController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db, IHostingEnvironment env)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
            this.environment = env;
        }

        //GET: /ShopManagement

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var seller = await db.Sellers.SingleOrDefaultAsync(x => x.Id == user.SellerID);

            // return Ok(seller);

            if (seller == null)
            {
                return RedirectToAction(nameof(CreateShop));
            }

            EditShopViewModel model = new EditShopViewModel
            {
                Countries = SeedData.Get.Country,
                Name = seller.Name,
                About = seller.About,
                Home = seller.Address,
                City = seller.Address,
                Street = seller.Address,
                Tel = seller.Tel,
                Type = (ShopType)Enum.Parse(typeof(ShopType), seller.ShopType),
                Message = this.Message,
                LogoPath = seller.Logo,
                BackgroundPath = seller.Cover
            };

            ViewBag.ID = seller.Id;
            return View(model);
        }


        //POST: /Shopmenagement
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(EditShopViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.GetUserAsync(HttpContext.User);
            var seller = db.Sellers.FirstOrDefault(x => x.Id == user.SellerID);

            if (seller.Logo != model.LogoPath)
            {
                seller.Logo = model.LogoPath;
            }

            if (seller.Cover != model.BackgroundPath)
            {
                seller.Cover = model.BackgroundPath;
            }

            if (seller.Name != model.Name)
            {
                seller.Name = model.Name;
            }

            if (seller.ShopType != model.Type.ToString())
                seller.ShopType = model.Type.ToString();

            //Not yet: check address home/street/ county
            if (seller.Tel != model.Tel)
                seller.Tel = model.Tel;

            if (seller.About != model.About)
                seller.About = model.About;

            // if(seller.LogoImg != model.LogoPath)
            //     seller.LogoImg == model.LogoPath;

            await db.SaveChangesAsync();
            Message = "Your information has been updated.";

            return RedirectToAction(nameof(Index));
        }

        //GET: /ShopManagement/CreateShop
        public IActionResult CreateShop()
        {
            return View();
        }

        //POST: /ShopManagement/Create
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateShopViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                Seller seller = new Seller
                {
                    Name = model.ShopName,
                    ShopType = model.ShopType.ToString(),
                    About = string.Empty,
                    Address = string.Empty,
                    Tel = string.Empty,
                    CreatedDate = DateTime.UtcNow,
                };

                db.Add(seller);
                var effect = await db.SaveChangesAsync();
                seller = db.Sellers.SingleOrDefault(s => s == seller);
                user.SellerID = seller.Id;
                await userManager.UpdateAsync(user);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid filed information");
                return View();
            }

        }

        //POST: /ShopManegement/ChangeLogo
        [HttpPost]
        public async Task<IActionResult> ChangeLogo(UploadPhotoViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model = await SavePhoto(model);

            return Ok(model);
        }

        //POST: /ShopManegement/ChangeBackground
        [HttpPost]
        public async Task<IActionResult> ChangeBackground(UploadPhotoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            model = await SavePhoto(model);

            return Ok(model);

        }

        private async Task<UploadPhotoViewModel> SavePhoto(UploadPhotoViewModel model)
        {
            var file = model.Files;
            if (model.Files.Length > 0)
            {
                var path = Path.Combine(environment.WebRootPath, "images");

                using (var fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                {
                    await file.CopyToAsync(fs);
                }
                model.Source = $"images/{file.FileName}";

            }
            return model;
        }
    }
}