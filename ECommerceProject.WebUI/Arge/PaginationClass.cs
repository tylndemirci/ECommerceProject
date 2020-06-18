using System.Collections.Generic;
using cloudscribe.Pagination.Models;
using ECommerceProject.WebUI.Models.Category;

namespace ECommerceProject.WebUI.Arge
{
    public class PaginationClass<T> where T : class
    {
        public PaginationClass(List<T> data, long totalItems, long pageNumber, int pageSize)
        {
            Data = data;
            TotalItems = totalItems;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public List<T> Data { get; set; }

        public long TotalItems { get; set; }

        public long PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 1;

        public  PagedResult<T> ReturnPagedResult()
        {
            return new PagedResult<T>()
            {
                Data = Data,
                TotalItems = TotalItems,
                PageNumber = PageNumber,
                PageSize = PageSize,
            };
        }
    }
}