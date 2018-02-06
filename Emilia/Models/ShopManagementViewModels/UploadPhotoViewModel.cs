

using Microsoft.AspNetCore.Http;

namespace Emilia.Models.ShopManagementViewModels
{
    public class UploadPhotoViewModel 
    {
        public IFormFile Files {get; set;}
        public string Source {get; set;}
    }
}