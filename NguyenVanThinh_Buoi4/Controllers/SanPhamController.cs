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

        public SanPhamController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index(string search)
        {
            var products = await _productRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
            }
            return View(products);
        }
    }
}
