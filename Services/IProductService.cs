using MauiAppUIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Services
{
    public interface IProductService
    {
        Task<PagedResponse<Product>> GetProductsAsync(int page = 1, int pageSize = 20, string search = "", List<string> categoryId = null);
    }
}
