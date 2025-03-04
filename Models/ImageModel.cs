namespace Esp32ImageApi.Models
{
    public class ImageModel
    {
        public int Id { get; set; }
        public string ImageData { get; set; } 
        public DateTime CapturedAt { get; set; }
    }
}