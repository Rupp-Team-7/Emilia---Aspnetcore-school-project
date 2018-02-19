using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emilia.Models.OrderingViewModels
{
    public class OrderingDetailViewModel
    {
        public List<Order> Orders {get;set;}
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }

        public OrderingDetailViewModel(List<Order> od,List<Product> pro,List<Customer> cus)
        {
            this.Orders = od;
            this.Customers = cus;
            this.Products = pro;
        }
    }
}
