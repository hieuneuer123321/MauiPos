using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAppUIDemo.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
        public bool Status { get; set; }  // true = còn hàng, false = hết hàng
        public string StatusText => Status ? "Còn hàng" : "Hết hàng";
        public Color StatusColor => Status ? Colors.Green : Colors.Red;

        //public List<Topping> AvailableToppings { get; set; } = new();
    }

}
