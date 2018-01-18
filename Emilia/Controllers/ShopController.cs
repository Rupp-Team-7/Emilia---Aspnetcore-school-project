using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Emilia.Models;
using Emilia.Data;
using Microsoft.EntityFrameworkCore;

namespace Emilia.Controllers
{
    public class ShopController : Controller
    {

       
        //Get: /, /shop/
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
