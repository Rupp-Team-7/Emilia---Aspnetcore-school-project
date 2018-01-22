using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Emilia.Models
{
    public class Seller 
    {
        public int Id {get; set;}
        public string Name {get; set;}
        public string ShopType {get; set;}
        public string Address { get; set; } 
        public string Tel {get; set;}
        public string About { get; set; }   
        public DateTime CreatedDate {get; set;}
    }
}