using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NguyenVanThinh_Buoi4.Models;

using NguyenVanThinh_Buoi4.Repositories; // Add this using

namespace NguyenVanThinh_Buoi4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository; // Add this line

        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository) // Update constructor
        {
            _logger = logger;
            _productRepository = productRepository; // Assign here
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync(); // Fetch products
            return View(products); // Pass products to the view
        }

       

public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
