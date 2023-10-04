using PuhdasApp.Models;

namespace PuhdasApp.Interfaces
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetReviewsOfAProductAsync(int productId);
        bool Add(Review review);
        bool Update(Review review);
        bool Delete(Review review);
        bool Save();
    }
}
