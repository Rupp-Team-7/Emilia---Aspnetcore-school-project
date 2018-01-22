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

namespace Emilia.Controllers
{
    public class ShopManagementController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private SignInManager<ApplicationUser> signInManager;
        private ApplicationDbContext db;


        public ShopManagementController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDbContext db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        //GET: /ShopManagement

        public IActionResult Index()
        {
            //  var user = await userManager.GetUserAsync(HttpContext.User);
             
            //  var seller =  db.Sellers.FirstOrDefault(x=> x.Id == user.SellerID);
            
            // return Ok(seller);
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateShopViewModel model)
        {
            if(ModelState.IsValid)
            {
               var user = await userManager.GetUserAsync(HttpContext.User);
               Seller seller  = new Seller {
                   Name = model.ShopName, ShopType = model.ShopType.ToString(),About = string.Empty,
                   Address = string.Empty,Tel = string.Empty, CreatedDate = DateTime.UtcNow
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