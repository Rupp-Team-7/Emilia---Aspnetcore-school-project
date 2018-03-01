using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emilia.Models.HomeViewmodel
{
    public class ProductHomeViewModel
    {
        public List<Product> Products { get; set; }
        public ProductHomeViewModel(List<Product> p)
        {
            this.Products = p;

        }
    }
}
