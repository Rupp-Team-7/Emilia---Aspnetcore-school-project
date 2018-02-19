using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emilia.Models.HomeViewModels
{
    public class CreateOrderingViewModel
    {
        public int ProductId { get; set; }
        public int Quanity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime orderDate { get; set; }
        public int CustomerId { get; set; }
        public int SellerId { get; set; }
        public Boolean Delivery { get; set; }

        public CreateOrderingViewModel() { }
        
        public CreateOrderingViewModel(Order ord)
        {
            this.ProductId = ord.product.Id;
            this.Quanity = ord.Quanity;
            this.TotalPrice = ord.TotalPrice;
            this.orderDate = ord.orderDate;
            this.CustomerId = ord.CustomerId;
            this.SellerId = ord.SellerId;
            this.Delivery = ord.Delivery;
        }
    }
}
