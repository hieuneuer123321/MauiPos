using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Models
{
    public class PagedResponse<T>
    {
        public int TotalItem { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int MaxPage { get; set; }
        public List<T> Items { get; set; }
    }
}
