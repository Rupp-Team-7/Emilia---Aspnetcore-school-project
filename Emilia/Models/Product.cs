using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Emilia.Models
{

    public enum Category
    {
        None = 0, Shirt, Shoes, Bag
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public Category Category { get; set; } = Category.None;
    }
}
