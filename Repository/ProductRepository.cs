using Microsoft.EntityFrameworkCore;
using PuhdasApp.Data;
using PuhdasApp.Interfaces;
using PuhdasApp.Models;

namespace PuhdasApp.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _context.Products.Remove(product);
            return Save();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.Where(p => p.Id == id).Include(r => r.Reviews).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByIdNoTracking(int id)
        {
            return await _context.Products.Where(p => p.Id == id).
                AsNoTracking().Include(r => r.Reviews).Include(o => o.Orders).
                FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Products.Where(n => n.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.OrderBy(n => n.Name).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Product product)
        {
            _context.Products.Update(product); 
            return Save();
        }
    }
}
