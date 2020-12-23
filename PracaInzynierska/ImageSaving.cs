using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInzynierska
{
    public class ImageSaving
    {
        public string ToTmp(IFormFile file)
        {
            var filePath = Path.GetTempFileName();
            using (var image = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(image);
            }

            return filePath;
        }

        public string ToByteArray(string path)
        {
            byte[] imageArray = null;
            imageArray = File.ReadAllBytes(path);
            string base64Data = Convert.ToBase64String(imageArray);
            var imageSource = String.Format("data:image/png;base64,{0}", base64Data);
            return imageSource;
        }
    }
}
