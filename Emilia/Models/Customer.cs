using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Emilia.Models
{
    public class Customer 
    {
        public int Id {get; set;}
        public string Firstname {get; set;}
        public string Lastname {get;set;}
        public string Phone { get; set; }
        public string UserId {get;set;}
    }
}