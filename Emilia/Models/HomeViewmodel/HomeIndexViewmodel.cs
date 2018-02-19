
using System.Collections.Generic;

namespace Emilia.Models.HomeViewmodel
{
    public class IndexViewmodel 
    {
        public List<ShopItem> ShopItems {get; set;}
        public List<SellerItem> SellerItems {get; set;}
    }

    public class ShopItem 
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string Image {get; set;}
        public decimal Price {get; set;}
    }

    public class SellerItem 
    {
        public int Id {get; set;}
        public string Name {get;set;}
        public string Logo {get; set;}
    }
}