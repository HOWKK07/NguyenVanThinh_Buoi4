using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenVanThinh_Buoi4.Models;
using NguyenVanThinh_Buoi4.Repositories;

namespace NguyenVanThinh_Buoi4.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Manager")]
    public class ProductController : Controller
    {
        //Khai báo thông tin repo t??ng ?ng ?? thao tác csdl
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        // Hi?n th? danh sách s?n ph?m


        // Hi?n th? thông tin chi ti?t s?n ph?m
        public async Task<IActionResult> Display(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Hi?n th? form c?p nh?t s?n ph?m
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // X? lý c?p nh?t s?n ph?m
        [HttpPost]
        public async Task<IActionResult> Update(int id, Product product, IFormFile imageUrl)
        {
            ModelState.Remove("ImageUrl"); // Lo?i b? xác th?c ModelState cho ImageUrl
            if (id != product.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var existingProduct = await
               _productRepository.GetByIdAsync(id); // Gi? ??nh có ph??ng th?c GetByIdAsync
                                                    // Gi? nguyên thông tin hình ?nh n?u không có hình m?i ???c t?i lên
                if (imageUrl == null)
                {
                    product.ImageUrl = existingProduct.ImageUrl;
                }
                else
                {
                    // L?u hình ?nh m?i
                    product.ImageUrl = await SaveImage(imageUrl);
                }
                // C?p nh?t các thông tin khác c?a s?n ph?m
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;
                existingProduct.CategoryId = product.CategoryId;
                existingProduct.ImageUrl = product.ImageUrl;
                await _productRepository.UpdateAsync(existingProduct);

                return RedirectToAction(nameof(Index));
            }
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        // Hi?n th? form xác nh?n xóa s?n ph?m
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        // X? lý xóa s?n ph?m
        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Hi?n th? form thêm s?n ph?m m?i
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        // Vi?t thêm hàm SaveImage (tham kh?o bài 02)
        private async Task<string> SaveImage(IFormFile image)
        {
            //Thay ??i ???ng d?n theo c?u hình c?a b?n
            var savePath = Path.Combine("wwwroot/images", image.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName; // Tr? v? ???ng d?n t??ng ??i
        }

        // X? lý thêm s?n ph?m m?i
        [HttpPost]
        public async Task<IActionResult> Add(Product product, IFormFile imageUrl)
        {
            if (ModelState.IsValid)
            {
                if (imageUrl != null)
                {
                    // L?u hình ?nh ??i di?n tham kh?o bài 02 hàm SaveImage
                    product.ImageUrl = await SaveImage(imageUrl);
                }
                await _productRepository.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            // N?u ModelState không h?p l?, hi?n th? form v?i d? li?u ?ã nh?p
            var categories = await _categoryRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }
        public async Task<IActionResult> Index()
        {
            var products = await _productRepository.GetAllAsync();
            return View(products);
        }
    }
}