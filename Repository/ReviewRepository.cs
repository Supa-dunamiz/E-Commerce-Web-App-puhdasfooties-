using Microsoft.EntityFrameworkCore;
using PuhdasApp.Data;
using PuhdasApp.Interfaces;
using PuhdasApp.Models;

namespace PuhdasApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly AppDbContext _context;

        public ReviewRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Review review)
        {
            _context.Reviews.Add(review);
            return Save();
        }

        public bool Delete(Review review)
        {
            _context.Reviews.Remove(review);
            return Save();
        }

        public async Task<IEnumerable<Review>> GetReviewsOfAProductAsync(int productId)
        {
            return await _context.Reviews.Where(p => p.ProductId ==  productId).ToListAsync();   
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Review review)
        {
            _context.Reviews.Update(review);
            return Save();
        }
    }
}
