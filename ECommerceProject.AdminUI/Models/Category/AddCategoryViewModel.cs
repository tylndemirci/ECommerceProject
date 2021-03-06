﻿using System.Linq;

namespace ECommerceProject.AdminUI.Models.Category
{
    public class AddCategoryViewModel
    {
        public AddCategoryViewModel(Entities.Concrete.Category category)
        {
            CategoryId = category.Id;
            ParentCategoryId = category.ParentCategoryId;
            IsDeleted = false;
            Title = category.Title;
            ParentCategory = category.ParentCategory != null ? category.ParentCategory.Title : "";
            SubCategories = category.SubCategories != null ? string.Join(",", category.SubCategories.Where(x=>x.IsDeleted==false).Select(x => x.Title)) : "";
            
            
            

        }

        public AddCategoryViewModel()
        {

        }



        public int CategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public string Title { get; set; }
        public string SubCategories { get; set; }
        public string ParentCategory { get; set; }
    }
}
