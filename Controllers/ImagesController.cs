using Microsoft.AspNetCore.Mvc;
using Esp32ImageApi.Data;
using Esp32ImageApi.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Esp32ImageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImagesController(AppDbContext context)
        {
            _context = context;
        }

        // POST method (already implemented)
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromBody] ImageModel request)
        {
            if (string.IsNullOrEmpty(request.ImageData))
            {
                return BadRequest(new { message = "Image data is missing." });
            }

            try
            {
                // Convert Base64 string to byte array
                byte[] imageBytes = Convert.FromBase64String(request.ImageData);

                // Create an ImageModel instance to store in the database
                var image = new ImageModel
                {
                    ImageData = request.ImageData, // Store Base64 string
                    CapturedAt = DateTime.UtcNow   // Store the current timestamp
                };

                // Save the image data to the database
                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Image uploaded successfully!", id = image.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Invalid Base64 format.", error = ex.Message });
            }
        }


        // GET method to retrieve a specific image by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);  // Find the image by its ID

            if (image == null)
            {
                return NotFound();  // Return a 404 if the image is not found
            }

            return Ok(image);  // Return the image data
        }
        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _context.Images.ToListAsync();  // Retrieve all images from the database

            if (images == null || images.Count == 0)
            {
                return NotFound();  // Return a 404 if no images are found
            }

            // Ensure each image has valid `capturedAt` and Base64 format
            var imageList = images.Select(image => new
            {
                image.Id,
                image.ImageData,
                capturedAt = image.CapturedAt.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList();

            return Ok(imageList);  // Return the list of images
        }

    }
}
