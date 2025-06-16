using Microsoft.AspNetCore.Mvc;
using NguyenVanThinh_Buoi4.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace NguyenVanThinh_Buoi4.Controllers
{
    public class SanPhamController : Controller
    {
        //Mục đích controller này để hiển thị layout sản phẩm cho thật đẹp
        //Và user bấm add to cart
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public SanPhamController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(string search)
        {
            var products = await _productRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
            ViewData["Title"] = "Tất cả sản phẩm";
            return View(products);
        }

        // Action mới để hiển thị sản phẩm theo danh mục
        public async Task<IActionResult> ByCategory(int categoryId)
        {
            var products = await _productRepository.GetAllAsync();
            var filteredProducts = products.Where(p => p.CategoryId == categoryId);

            // Lấy tên danh mục để hiển thị
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            ViewData["CategoryName"] = category?.Name ?? "Danh mục";
            ViewData["Title"] = $"Sản phẩm - {category?.Name}";

            return View("Index", filteredProducts);
        }

        // Hiển thị chi tiết sản phẩm
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}