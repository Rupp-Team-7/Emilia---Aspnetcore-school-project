using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Emilia.Models;

namespace Emilia.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        
        public DbSet<Seller> Sellers {get; set;}
        public DbSet<Customer> Customers {get;set;}
        public DbSet<Order> Orders {get; set;}
        public DbSet<Product> Products {get; set;}
        public DbSet<ProductDetail> ProductDetail {get; set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Seller>().ToTable("Seller").HasKey(s => s.Id);
            builder.Entity<Customer>().ToTable("Customer").HasKey(c => c.Id);
            builder.Entity<Order>().ToTable("Order").HasKey(o => o.Id);
            builder.Entity<Product>().ToTable("Product").HasKey(p => p.Id);
            builder.Entity<ProductDetail>().ToTable("ProductDetail").HasKey(d => d.Id);
            
        }
    }
}
