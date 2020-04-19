using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerceProject.Entities.Concrete
{
    public class Category
    {
        public int Id { get; set; }
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }

        public string Title { get; set; }

        public ICollection<Category> SubCategories { get; set; }
    }
}
