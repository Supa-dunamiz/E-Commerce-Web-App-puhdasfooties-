using PuhdasApp.Data;
using PuhdasApp.Interfaces;
using PuhdasApp.Models;

namespace PuhdasApp.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardRepository(AppDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Order>> GetOrdersAsync()
        {
            var curUserId = _httpContextAccessor.HttpContext?.User.GetUserId();
            var UserOrders =  _context.Orders.Where(u => u.AppUserId == curUserId);
            return UserOrders.ToList();
        }
    }
}
