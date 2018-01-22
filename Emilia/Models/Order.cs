using System;
using System.Collections.Generic;


namespace  Emilia.Models
{
    public class Order 
    {
        public int Id {get; set;}
        public Product product {get; set;}
        public int Quanity {get;set;}
        public decimal TotalPrice {get; set;}
        public DateTime orderDate {get;set;}
        public int CustomerId {get; set;}
        public int  SellerId {get; set;}

        public virtual Customer Customer {get; set;}
        public virtual Seller Seller {get; set;}

        // public int DeliveryId { get; set; }
        // public Delivery Delivery { get; set; }
    }
}