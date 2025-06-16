using Microsoft.AspNetCore.Mvc;
using NguyenVanThinh_Buoi4.Repositories;

namespace NguyenVanThinh_Buoi4.ViewComponents
{


    public class CategoryDropdownViewComponent : ViewComponent
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryDropdownViewComponent(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return View(categories);
        }
    }
}