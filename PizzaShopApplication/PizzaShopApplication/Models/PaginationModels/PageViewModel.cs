using System;

namespace PizzaShopApplication.Models.PaginationModels
{
    public class PageViewModel
    { 
        // Номер текущей страницы.
        public int PageNumber { get; private set; }
        // Общее число страниц.
        public int TotalPages { get; private set; }
        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        // Узнает, есть ли страницы до текущей.
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
        // Узнает, есть ли страницы после текущей.
        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }

    }
}
