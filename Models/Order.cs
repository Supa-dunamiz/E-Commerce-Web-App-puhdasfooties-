using PuhdasApp.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuhdasApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? Quantity { get; set; }
        public Sizes? Size { get; set; }
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("AppUser")]
        public string? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}

