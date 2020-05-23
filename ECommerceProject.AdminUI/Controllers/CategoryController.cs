using System;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using ECommerceProject.AdminUI.Models.Category;
using ECommerceProject.Business.Abstract;
using ECommerceProject.DataAccess.Abstract;
using ECommerceProject.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace ECommerceProject.AdminUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
    


        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            
        }
        
        public IActionResult Index()
        {

            var returnModel = _categoryService.GetAllWithSubNames()
                .Where(f => f.IsDeleted == false)
                .Select(x => new AddCategoryViewModel(x));
            
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
                Title = model.Title,
             
                

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
                ParentCategoryId = model.ParentCategoryId
            });

            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {

            _categoryService.DeleteCategoryTree(id);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var getCategory = _categoryService.GetCategory(id);
            return View(new AddCategoryViewModel()
            {
                CategoryId = getCategory.Id,
                ParentCategoryId = getCategory.ParentCategoryId
            });
        }

        [HttpPost]
        public IActionResult UpdateCategory(AddCategoryViewModel model)
        {
            _categoryService.UpdateCategory(new Category()
            {
                Id = model.CategoryId,
                ParentCategoryId = model.ParentCategoryId,
                Title = model.Title,
            });


            return RedirectToAction("Index");
        }

        //Test Page
        //public IActionResult StatusPage(int id)
        //{
        //    var category = _categoryDal.GetBy(x => x.Id == id);
        //        var yo = new AddCategoryViewModel()
        //        {
        //            CategoryId = category.Id,
        //            Title = category.Title,
        //            IsDeleted = category.IsDeleted,
        //            ParentCategoryId = category.ParentCategoryId
        //        };
        //    return View(yo);
        //}

        
    }
}