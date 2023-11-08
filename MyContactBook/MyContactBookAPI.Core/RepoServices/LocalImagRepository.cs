using MyContactBookAPI.Core.Interfaces;
using Microsoft.Extensions.Hosting;
using MyContactBookAPI.Models.Domain;
using Microsoft.AspNetCore.Http;
using MyContactBookAPI.Data;

namespace MyContactBookAPI.Core.RepoServices
{
    public class LocalImagRepository : IImageRepository
    {
        private readonly IHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly MyContactDbContext myContactDbContext;

        public LocalImagRepository(IHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, MyContactDbContext myContactDbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.myContactDbContext = myContactDbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment
                .ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");

            //Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);


            //https://localhost:1234/images/image.jpg
            //var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{HttpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            //image.FilePath = urlFilePath;

            //Add Image to the Images Table

            await myContactDbContext.Images.AddAsync(image);
            await myContactDbContext.SaveChangesAsync();

            return image;
        }
    }
}
