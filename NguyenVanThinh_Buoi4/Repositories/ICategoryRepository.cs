using NguyenVanThinh_Buoi4.Models;

namespace NguyenVanThinh_Buoi4.Repositories
{
    public interface ICategoryRepository
    {
        //khai bao các hàm
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(int id);
    }
}
