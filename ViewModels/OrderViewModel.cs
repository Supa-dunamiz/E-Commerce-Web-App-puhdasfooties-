using PuhdasApp.Data.Enum;
using PuhdasApp.Models;


namespace PuhdasApp.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string? Price { get; set; }
        public string? Name { get; set; }
        public int? Quantity { get; set; }
        public Sizes? Size { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? ProductId { get; set; }
        public string? AppUserId { get; set; }
        public string? Image { get; set; }
        public string? CustomerName { get; set; }
    }
}
