
using System;
using System.Collections.Generic;

namespace Emilia.Models.ProductViewModel
{
    public class ProductIndexViewModel
    {
        public List<Product> Products { get; set; }


        public ProductIndexViewModel(List<Product> products)
        {
            this.Products = products;
        }
    }

    public static class ProductExtension
    {
        public static string FirstImage(this Product p)
        {
            if (!String.IsNullOrEmpty(p.ImgPath))
            {
                string[] path = p.ImgPath.Split(";");
                return path[0];
            }

            return String.Empty;
        }

        public static int ImageCount(this Product p)
        {
            if (!String.IsNullOrEmpty(p.ImgPath))
            {
                string[] path = p.ImgPath.Split(";");
                return (path.Length - 1) < 0 ? 0 : (path.Length - 1);
            }

            return 0;
        }
    }
}