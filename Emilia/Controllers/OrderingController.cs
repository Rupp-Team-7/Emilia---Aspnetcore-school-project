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
using Emilia.Models.OrderingViewModels;

namespace Emilia.Controllers
{
    public class OrderingController : Controller
    {
        private ApplicationDbContext db;
        private UserManager<ApplicationUser> manager;

        public OrderingController(ApplicationDbContext db, UserManager<ApplicationUser> manager)
        {
            this.db = db;
            this.manager = manager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Detail()
        {
            var model = new OrderingDetailViewModel(db.Orders.Include(c=>c.Customer).Include(p=>p.Product).ToList());
            return View(model);
        }
        [HttpPost]
        public IActionResult Detail(int? id,Boolean ord)
        {
            var OrderChangeState = db.Orders.SingleOrDefault(o => o.Id == id);
            if (OrderChangeState.Delivery != ord)
                OrderChangeState.Delivery = ord;
            db.SaveChangesAsync();
            return View("Detail");
        }

        [Authorize]
        public async Task<IActionResult> History()
        {
            var cusid = await GetCusID();
            var his = await db.Orders.Include(o=>o.Product).Include(s=>s.Seller).Include(c=>c.Customer).Where(o => o.CustomerId == cusid).ToListAsync();
            var model = new OrderingDetailViewModel(his);
            return View(model);
        }



        private async Task<int> GetCusID()
        {
            var user = await manager.GetUserAsync(HttpContext.User);
            var customer = await db.Customers.Where(c =>c.UserId == user.Id).AsNoTracking().SingleOrDefaultAsync();

            if (customer == null)
                return -1;
            return customer.Id;
        }
    }
}