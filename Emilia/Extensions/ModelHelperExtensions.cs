using System;
using System.Text;
using Emilia.Models;
using Emilia.Models.ProductViewModel;


namespace Emilia.Extensions
{
    public static class CreateProductViewModelExtensions
    {
        public static string[] GeImagePath(this CreateProductViewModel m)
        {
            if (!String.IsNullOrEmpty(m.PhotoPath))
                // return m.PhotoPath.Split(";",StringSplitOptions.None);
                 return m.PhotoPath.Split(';');

            return new string[0];
        }
    }

    public static class ProductExtension
    {

        public static string FirstImage(this Product p)
        {
            if (!String.IsNullOrEmpty(p.ImgPath))
            {
                string[] path = p.ImagePath();
                return path[0];
            }

            return String.Empty;
        }

        public static int ImageCount(this Product p)
        {
            if (!String.IsNullOrEmpty(p.ImgPath))
            {
                string[] path = p.ImagePath();
                return (path.Length - 1) < 0 ? 0 : (path.Length - 1);
            }

            return 0;
        }

        public static string[] ImagePath(this Product p)
        {
            if (!String.IsNullOrEmpty(p.ImgPath))
                return p.ImgPath.Split(';');

            return new string[0];
        }
    }
}