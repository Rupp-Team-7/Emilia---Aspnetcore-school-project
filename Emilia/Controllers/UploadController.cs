
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Emilia.Models.ShopManagementViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Emilia.Models.ProductViewModel;
using System.Text;

namespace Emilia.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment environment;

        public UploadController(IHostingEnvironment environment)
        {
            this.environment = environment;
        }


        //AJAX-POST: /Upload/Photos
        [HttpPost]
        public async Task<IActionResult> Photos(List<IFormFile> files)
        {
            var path = Path.Combine(environment.WebRootPath, "images");

            StringBuilder builder = new StringBuilder();
            int count = 0;

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //var filename = Path.GetTempFileName();
                    using (var fs = new FileStream(Path.Combine(path, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fs);
                    }

                    builder.Append($"images/{file.FileName}").Append(";");
                    count++;
                }
            }

            
            var model = new ProductPhotoViewModel { 
                PhotoPath = builder.ToString(),
                PhotoCount = count
            };
            return Ok(model);

        }
    }
}