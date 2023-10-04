using PuhdasApp.Models;

namespace PuhdasApp.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Order>> GetOrdersAsync();
    }
}
