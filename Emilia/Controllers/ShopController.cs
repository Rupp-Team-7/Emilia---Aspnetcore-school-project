using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Emilia.Models;
using Emilia.Models.ManageViewModels;
using Emilia.Services;
using Emilia.Models.ShopManagementViewModels;
using Emilia.Data;
using Microsoft.EntityFrameworkCore;

namespace Emilia.Controllers
{
    public class ShopController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext db;
        public ShopController(ApplicationDbContext db, UserManager<ApplicationUser> manager)
        {
            this.userManager = manager;
            this.db = db;
        }

        //Get: /, /shop/
        public async Task<IActionResult> Index()
        {

            var user = await userManager.GetUserAsync(HttpContext.User);
            var seller = await db.Sellers
                .Include(s => s.Products)
                .SingleOrDefaultAsync(s => s.Id == user.SellerID);

            return View(seller);
        }

        
        public IActionResult Profile(int id)
        {
            var model = db.Sellers.SingleOrDefault(s => s.Id == id);

            // return (model);

            return View(model);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
