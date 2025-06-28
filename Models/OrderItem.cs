using CommunityToolkit.Mvvm.ComponentModel;
using MauiAppUIDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace MauiAppUIDemo.Models // ← Bắt buộc phải có
{
    public partial class OrderItem : ObservableObject
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private double price;

        [ObservableProperty]
        private string image;

        [ObservableProperty]
        private int quantity = 1;

        [ObservableProperty]
        private List<Topping> toppings = new();

        public double Subtotal => (Price + Toppings.Sum(t => t.Price)) * Quantity;

        public string ToppingSummary =>
            Toppings != null && Toppings.Any()
            ? $"Topping: {string.Join(", ", Toppings.Select(t => t.Name))}"
            : string.Empty;
    }
}
