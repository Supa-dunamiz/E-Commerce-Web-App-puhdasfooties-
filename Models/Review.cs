using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuhdasApp.Models
{
    public class Review
    {
        [Key]
        public int? Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Reviewer { get; set; }
        [ForeignKey("Product")]
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
