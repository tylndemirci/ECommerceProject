using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using ECommerceProject.AdminUI.Models.Category;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceProject.AdminUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryDal _categoryDal;


        public CategoryController(ICategoryService categoryService, ICategoryDal categoryDal)
        {
            _categoryService = categoryService;
            _categoryDal = categoryDal;
        }

        public IActionResult Index()
        {

            var returnModel = _categoryDal.GetAll().Select(x => new AddCategoryViewModel(x));
            return View(returnModel);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(AddCategoryViewModel model)
        {
            _categoryService.AddCategory(new Category()
            {
                Id = model.CategoryId,
                Title = model.Title
            });
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddSubCategory(int id)
        {
            var returnModel = new AddCategoryViewModel(new Category()
            {
                ParentCategoryId = id
            });

            return View(returnModel);
        }

        [HttpPost]
        public IActionResult AddSubCategory(AddCategoryViewModel model)
        {
            _categoryService.AddCategory(new Category()
            {
                Title = model.Title,
                ParentCategoryId = model.ParentCategoryId,

            });

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {

            var getCategory = _categoryDal.GetBy(x => x.Id == id);
            var getSubCategory = _categoryDal.GetBy(p => p.ParentCategoryId == id);
            if (getSubCategory != null)
            {

                _categoryService.DeleteCategoryTree(getSubCategory.Id);

            }

            if (getCategory != null)
            {
                _categoryService.DeleteCategoryTree(getCategory.Id);

            }







            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var getCategory = _categoryDal.GetBy(x => x.Id == id);
            return View(new AddCategoryViewModel()
            {
                CategoryId = getCategory.Id
            });
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoryService.UpdateCategory(category);
            return RedirectToAction("Index");
        }
    }
}