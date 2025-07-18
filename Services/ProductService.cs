using MauiAppUIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Services
{
    public class ProductService: IProductService
    {
        private readonly IApiService _apiService;

        public ProductService(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<PagedResponse<Product>> GetProductsAsync(int page = 1, int pageSize = 20, string search = "", List<string> categoryId = null)
        {
            var query = $"/Product/GetItemsMenu?PageNumber=1&PageSize=10&isPaging=false&isDecPrice=true";
            return await _apiService.PostAsync<PagedResponse<Product>>(query,null, requireAuth: true);
        }
    }
}
