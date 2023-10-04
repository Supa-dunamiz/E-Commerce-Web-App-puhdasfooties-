using PuhdasApp.Models;

namespace PuhdasApp.ViewModels
{
    public class EditViewModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Price { get; set; }
        public IFormFile? Image { get; set; }
        public string? Url { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
