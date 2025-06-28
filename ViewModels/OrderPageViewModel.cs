using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppUIDemo.Models;
using MauiAppUIDemo.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace MauiAppUIDemo.ViewModels
{
    public partial class OrderPageViewModel : ObservableObject
    {
        public ObservableCollection<Product> Products { get; } = new();
        public ObservableCollection<OrderItem> OrderItems { get; } = new();
        public ObservableCollection<string> Categories { get; } = new();

        // ✅ Topping dùng chung toàn bộ sản phẩm
        private readonly List<Topping> commonToppings = new()
        {
            new Topping { Name = "Trân châu", Price = 5000 },
            new Topping { Name = "Thạch trái cây", Price = 7000 },
            new Topping { Name = "Kem cheese", Price = 10000 }
        };

        [ObservableProperty]
        private string selectedCategory;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private string totalPriceFormatted;

        public OrderPageViewModel()
        {
            // ✅ Danh sách sản phẩm
            Products.Add(new Product { Id = 1, Name = "Trà sữa", Price = 30000, ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-3WBPlxFD7_TAC2WmagCKeQfSwpdGCYBRYQ&s", Category = "Trà", Status = true });
            Products.Add(new Product { Id = 2, Name = "Cà phê", Price = 25000, ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-3WBPlxFD7_TAC2WmagCKeQfSwpdGCYBRYQ&s", Category = "Cà phê", Status = true });
            Products.Add(new Product { Id = 3, Name = "Trà đào", Price = 28000, ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-3WBPlxFD7_TAC2WmagCKeQfSwpdGCYBRYQ&s", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 4, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = true });
            Products.Add(new Product { Id = 5, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = true });
            Products.Add(new Product { Id = 6, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 7, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 8, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 9, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false         });
            Products.Add(new Product { Id = 10, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 11, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = true });
            Products.Add(new Product { Id = 31, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 32, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 33, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 34, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 35, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 37, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 39, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 40, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });
            Products.Add(new Product { Id = 21, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = true });
            // ✅ Tạo danh mục từ sản phẩm
            Categories.Add("Tất cả");
            foreach (var cat in Products.Select(p => p.Category).Distinct())
                Categories.Add(cat);

            SelectedCategory = "Tất cả";
            UpdateTotal();
        }

        // ✅ Danh sách sản phẩm được lọc theo search và category
        public IEnumerable<Product> FilteredProducts =>
                  Products.Where(p =>
                      (SelectedCategory == "Tất cả" || p.Category == SelectedCategory) &&
                      (string.IsNullOrWhiteSpace(SearchText) ||
                       RemoveDiacritics(p.Name).Contains(RemoveDiacritics(SearchText), StringComparison.OrdinalIgnoreCase))
                  );
        // ✅ Cập nhật tổng tiền
        private void UpdateTotal()
        {
            TotalPriceFormatted = $"{OrderItems.Sum(x => x.Subtotal):N0} đ";
        }

        // ✅ Tự động gọi lại khi thay đổi tìm kiếm hoặc danh mục
        partial void OnSearchTextChanged(string value) => OnPropertyChanged(nameof(FilteredProducts));
        partial void OnSelectedCategoryChanged(string value) => OnPropertyChanged(nameof(FilteredProducts));

        // ✅ Thêm sản phẩm
        [RelayCommand]
        private async Task AddToOrder(Product product)
        {
            var selectedToppings = await ShowToppingPopup(commonToppings);
            if (selectedToppings == null)
                return;

            OrderItems.Add(new OrderItem
            {
                Name = product.Name,
                Price = product.Price,
                Image = product.ImageUrl,
                Quantity = 1,
                Toppings = selectedToppings
            });

            UpdateTotal();
        }

        [RelayCommand]
        private void IncreaseQuantity(OrderItem item)
        {
            item.Quantity++;
            UpdateTotal();
        }

        [RelayCommand]
        private void DecreaseQuantity(OrderItem item)
        {
            if (item.Quantity > 1)
                item.Quantity--;
            else
                OrderItems.Remove(item);

            UpdateTotal();
        }

        [RelayCommand]
        private void RemoveItem(OrderItem item)
        {
            OrderItems.Remove(item);
            UpdateTotal();
        }

        // ✅ Hiện popup chọn topping
        private async Task<List<Topping>> ShowToppingPopup(List<Topping> toppings)
        {
            var popup = new SelectToppingPopup(toppings);
            var result = await Application.Current.MainPage.ShowPopupAsync(popup);
            return result as List<Topping>;
        }
        private string RemoveDiacritics(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var normalized = input.Normalize(System.Text.NormalizationForm.FormD);
            var chars = normalized.Where(c => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c) != System.Globalization.UnicodeCategory.NonSpacingMark);
            return new string(chars.ToArray()).Normalize(System.Text.NormalizationForm.FormC);
        }
    }
}
