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
    public class HomeController : Controller
    {

       
        //Get: /, /home/
        public IActionResult Index()
        {
            return View();
        }

        //Get: /Search?querystring...
        public IActionResult Search()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //GET: /home/Product/1
        public IActionResult Product(int id)
        {
            return View();
        }
    }
}
