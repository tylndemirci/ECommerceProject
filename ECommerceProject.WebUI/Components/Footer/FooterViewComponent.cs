using System.Linq;
using ECommerceProject.Business.Abstract;
using ECommerceProject.WebUI.Models.ViewComponent.Footer;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.WebUI.Components.Footer
{
    public class FooterViewComponent: ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public FooterViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IViewComponentResult Invoke()
        {
            var returnCategory = _categoryService.ListCategories().Where(x => x.ParentCategoryId == null).Take(5).Select(x=>new FooterViewModel(x));
            return View(returnCategory);
        }
    }
}
