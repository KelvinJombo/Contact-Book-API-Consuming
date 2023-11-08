using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyContactBookAPI.Data;
using MyContactBookAPI.Models.Dtos;
using MyContactBookAPI.Models.Domain;
using MyContactBookAPI.Core.Interfaces;

namespace MyContactBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        //HttpPost/api/images/upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                //Convert Dto to Domain Model
                var imageModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileDescription = request.FileDescription,
                    FileName = request.FileName,
                };

                // User Repository Uploads Image
                await imageRepository.Upload(imageModel);

                return Ok(imageModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtentions = new string[] { ".jpg", ".jpeg", ".png" };

            if (allowedExtentions.Contains(Path.GetExtension(request.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported file Extension");
            }

            if (request.File.Length > 10485760) //10mb
            {
                ModelState.AddModelError("file", "File Size more than 10mb, Pls Upload Compartible File");
            }
        }


        



    }
}
