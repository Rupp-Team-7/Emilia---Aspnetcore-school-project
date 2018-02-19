using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Emilia.Models.ShopViewModel
{
    public class ShopIndexViewmodel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Cover { get; set; }
        public string Type { get; set; }
        public string Created { get; set; }

        public List<Product> Items { get; set; }

    }

    public class ShopAboutViewmodel :  ShopIndexViewmodel
    {
        public string Address { get; set; } 
        public string Tel {get; set;}
        public string About { get; set; }
    }
}