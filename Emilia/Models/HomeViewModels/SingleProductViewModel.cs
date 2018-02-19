using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emilia.Models.HomeViewModels
{
    public class SingleProductViewModel
    {
        public List<Product> Products { get; set; }
        public SingleProductViewModel(List<Product> pro)
        {
            this.Products = pro;
        }
    }
}
