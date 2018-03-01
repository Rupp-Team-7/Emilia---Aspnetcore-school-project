using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emilia.Models.OrderingViewModels
{
    public class OrderingDetailViewModel
    {
        public List<Order> Orders {get;set;}


        public OrderingDetailViewModel(List<Order> od)
        {
            this.Orders = od;

        }
    }
}
