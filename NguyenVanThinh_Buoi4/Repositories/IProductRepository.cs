using NguyenVanThinh_Buoi4.Models;

namespace NguyenVanThinh_Buoi4.Repositories
{
    public interface IProductRepository
    {
        //Khai bao các hàm
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);

    }
}
