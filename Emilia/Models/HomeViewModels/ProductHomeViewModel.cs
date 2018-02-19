using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emilia.Models.HomeViewModels
{
    public class ProductHomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
        public List<Seller> Sellers { get; set; }
        public ProductHomeViewModel(List<Product> p,List<ProductDetail> pd,List<Seller> sel)
        {
            this.Products = p;
            this.ProductDetails = pd;
            this.Sellers = sel;
        }
    }
}
