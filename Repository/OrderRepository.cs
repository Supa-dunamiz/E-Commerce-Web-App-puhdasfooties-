using Microsoft.EntityFrameworkCore;
using PuhdasApp.Data;
using PuhdasApp.Interfaces;
using PuhdasApp.Models;

namespace PuhdasApp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool Add(Order order)
        {
            _context.Orders.Add(order);
            return Save();
        }

        public bool Delete(Order order)
        {
            _context.Orders.Remove(order);
            return Save();
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders.Where(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _context.Orders.OrderByDescending(d => d.CreatedAt).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetProductOrders(int productId)
        {
            return await _context.Orders.Where(o => o.ProductId == productId).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Order order)
        {
            _context.Orders.Update(order);
            return Save();
        }
    }
}
