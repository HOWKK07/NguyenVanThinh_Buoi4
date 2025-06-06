using Microsoft.EntityFrameworkCore;
using NguyenVanThinh_Buoi4.Models;

namespace NguyenVanThinh_Buoi4.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        //Khai báo biến _context để giữ kết nối đến csdl
        private readonly ApplicationDbContext _context;
        public EFProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Lấy về toàn bộ ds sản phẩm
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category) // Include thông tin về category
                .ToListAsync();
        }
        /// <summary>
        /// Thông tin sản phẩm thông qua ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Thêm sản phẩm mới
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Update sản phẩm
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Delete sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
