using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Emilia.Models.ShopManagementViewModels
{

    public enum ShopType {Fashion, Electronic, Auto, Accessories}
    
    public class CreateShopViewModel
    {
        [Display(Name = "Your shop name")]
        [Required]
        public string ShopName {get; set;}

        [Display(Name = "Choose shop type")]
        public ShopType ShopType {get; set;}
        
        
    }
}