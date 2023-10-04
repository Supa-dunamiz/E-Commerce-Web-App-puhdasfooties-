using PuhdasApp.Models;

namespace PuhdasApp.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> GetProductByIdNoTracking(int id);
        Task<Product> GetProductByName(string name);
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(Product product);
        bool Save();
    }
}
