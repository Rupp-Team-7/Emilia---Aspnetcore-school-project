using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
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

namespace Emilia.Controllers
{
    public class ShopManagementController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ApplicationDbContext db;


        public ShopManagementController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        //GET: /ShopManagement

        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var seller = db.Sellers.FirstOrDefault(x => x.Id == user.SellerID);

            // return Ok(seller);

            EditShopViewModel model = new EditShopViewModel
            {
                Countries = SeedData.Get.Country,
                Name = seller.Name,
                About = seller.About,
                Home = seller.Address,
                City = seller.Address,
                Street = seller.Address,
                Tel = seller.Tel,
                Type = (ShopType)Enum.Parse(typeof(ShopType),seller.ShopType)
            };

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

            await db.SaveChangesAsync();

            return Ok(seller);
        }

        //GET: /ShopManagement/CreateShop
        public IActionResult CreateShop()
        {
            return View();
        }

        //POST: /ShopManagement/CreateShop
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
                    CreatedDate = DateTime.UtcNow
                };
                user.seller = seller;
                var resutl = await userManager.UpdateAsync(user);
                return Ok(user);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid filed information");
                return View();
            }

        }


    }
}