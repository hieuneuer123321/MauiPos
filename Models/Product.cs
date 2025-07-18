using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Models
{
    //public class Product
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public int Price { get; set; }
    //    public string ImageUrl { get; set; }
    //    public string Category { get; set; }
    //    public bool Status { get; set; }  // true = còn hàng, false = hết hàng
    //    public string StatusText => Status ? "Còn hàng" : "Hết hàng";
    //    public Color StatusColor => Status ? Colors.Green : Colors.Red;

    //    //public List<Topping> AvailableToppings { get; set; } = new();
    //}
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public string ProductDes { get; set; }
        public string ProductImgUrl { get; set; }
        public bool ProductStatus { get; set; }
        public Category ProductCategory { get; set; }

        public string Name => ProductName;
        public string ImageUrl => string.IsNullOrEmpty(ProductImgUrl) ? "https://giadungsato.com/Uploads/images/sinh_to_moi_ngay_2.jpg" : ProductImgUrl;
        public int Price => ProductPrice;
        public string StatusText => ProductStatus ? "Còn bán" : "Tạm ngừng";
        public Color StatusColor => ProductStatus ? Colors.Green : Colors.Gray;
    }

    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }


}
