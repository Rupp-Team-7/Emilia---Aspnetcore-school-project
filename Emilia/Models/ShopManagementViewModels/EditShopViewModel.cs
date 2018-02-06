
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Emilia.Models;
using Emilia.Models.ShopManagementViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;

namespace Emilia.Models.ShopManagementViewModels
{

    public class EditShopViewModel
    {
        public string Name {get; set;}
        public ShopType Type {get; set;}
        public string Home {get; set;}
        public string Street {get; set;}
        public string City {get; set;}
        public string Tel {get; set;}
        public string About {get; set;}
        public string LogoPath {get; set;}
        public string BackgroundPath {get; set;}
    
        public IEnumerable<SelectListItem> Countries {get;set;}
        public String Message {get;set;}

    }

    public class SeedData 
    {    
        #region singleton
        private static SeedData __SeedData;

        public static SeedData Get {get {
            if(__SeedData == null)
                __SeedData = new SeedData();

                return __SeedData;
        }}

        private SeedData()
        {
            countries = new List<SelectListItem>();
            this.Sample();
            
        }

        #endregion

        private List<SelectListItem> countries;

        private void Sample()
        {
            countries.AddRange(new SelectListItem[]{
                new SelectListItem {Text = "Phnom Penh", Value="Phnom penh"},
                new SelectListItem {Text = "Takeo", Value="Takeo"},
                new SelectListItem {Text = "Kandal", Value="Kandal"},
            });
        }

        public List<SelectListItem> Country {get{return this.countries;}}

    }

}