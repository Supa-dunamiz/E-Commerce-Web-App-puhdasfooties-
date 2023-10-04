using PuhdasApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PuhdasApp.ViewModels
{
    public class CreateReviewViewModel
    {
        public int? Id { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Reviewer { get; set; }
        public int? ProductId { get; set; }
    }
}
