using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;



namespace Services.Dashboard
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public FileUploadService(IWebHostEnvironment hostEnvironment)
        {
            _hostingEnvironment = hostEnvironment;
        }

        public string Upload(IFormFile file)
        {
            string unique = null;
            
            string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            unique = Guid.NewGuid().ToString() + "_" + file.FileName;
            string filePath = Path.Combine(uploadsFolder, unique);
            file.CopyTo(new FileStream(filePath, FileMode.Create));
            return unique;
        }
    }
}
