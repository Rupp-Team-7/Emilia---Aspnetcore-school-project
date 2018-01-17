using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace Emilia.Controllers
{

    public class DashboardController: Controller
    {

        public DashboardController()
        {

        }

        public IActionResult Index()
        {
            return View();
        }

    }

}