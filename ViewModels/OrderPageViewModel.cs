using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppUIDemo.Models;
using MauiAppUIDemo.Services;
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
        public ObservableCollection<DiscountCode> AvailableDiscounts { get; } = new();

        [ObservableProperty]
        private DiscountCode selectedDiscount;

        [ObservableProperty]
        private string discountAmountFormatted;

        [ObservableProperty]
        private string selectedCategory;

        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private string totalPriceFormatted;

        // ✅ Topping dùng chung cho mọi sản phẩm
        private readonly List<Topping> commonToppings = new()
        {
            new Topping { Name = "Trân châu", Price = 5000 },
            new Topping { Name = "Thạch trái cây", Price = 7000 },
            new Topping { Name = "Kem cheese", Price = 10000 }
        };

        //public OrderPageViewModel()
        //{
        //    // Danh sách sản phẩm
        //    Products.Add(new Product { Id = 1, Name = "Trà sữa", Price = 30000, ImageUrl = "milk_tea.png", Category = "Trà", Status = true });
        //    Products.Add(new Product { Id = 2, Name = "Cà phê", Price = 25000, ImageUrl = "coffee.png", Category = "Cà phê", Status = true });
        //    Products.Add(new Product { Id = 3, Name = "Trà đào", Price = 28000, ImageUrl = "peach_tea.png", Category = "Trà", Status = false });

        //    // Tạo danh mục từ sản phẩm
        //    Categories.Add("Tất cả");
        //    foreach (var cat in Products.Select(p => p.Category).Distinct())
        //        Categories.Add(cat);
        //    SelectedCategory = "Tất cả";

        //    // Mã giảm giá mẫu
        //    AvailableDiscounts.Add(new DiscountCode
        //    {
        //        Id = Guid.Empty,
        //        Name = "Không áp dụng",
        //        Description = "",
        //        IsActive = true
        //    });
        //    AvailableDiscounts.Add(new DiscountCode
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Giảm 10%",
        //        Description = "Giảm 10% cho đơn trên 100k",
        //        Percentage = 10,
        //        MinInvoiceTotal = 100000,
        //        StartDate = DateTime.Now.AddMonths(-1),
        //        EndDate = DateTime.Now.AddMonths(6),
        //        IsActive = true
        //    });
        //    AvailableDiscounts.Add(new DiscountCode
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Giảm 5.000đ",
        //        Description = "Giảm 5.000đ cho mọi đơn hàng",
        //        FixedAmount = 5000,
        //        StartDate = DateTime.Now.AddMonths(-1),
        //        EndDate = DateTime.Now.AddMonths(6),
        //        IsActive = true
        //    });

        //    UpdateTotal();
        //}
        private readonly IProductService _productService;

        public OrderPageViewModel(IProductService productService)
        {
            _productService = productService;

            Categories.Add("Tất cả");
            LoadProductsAsync(); // Gọi luôn khi khởi tạo
            /// 
            // Mã giảm giá mẫu
            AvailableDiscounts.Add(new DiscountCode
            {
                Id = Guid.Empty,
                Name = "Không áp dụng",
                Description = "",
                IsActive = true
            });
            AvailableDiscounts.Add(new DiscountCode
            {
                Id = Guid.NewGuid(),
                Name = "Giảm 10%",
                Description = "Giảm 10% cho đơn trên 100k",
                Percentage = 10,
                MinInvoiceTotal = 100000,
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now.AddMonths(6),
                IsActive = true
            });
            AvailableDiscounts.Add(new DiscountCode
            {
                Id = Guid.NewGuid(),
                Name = "Giảm 5.000đ",
                Description = "Giảm 5.000đ cho mọi đơn hàng",
                FixedAmount = 5000,
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now.AddMonths(6),
                IsActive = true
            });

            UpdateTotal();
        }

        // ✅ Lọc sản phẩm theo danh mục + tên (không dấu)
        //public IEnumerable<Product> FilteredProducts =>
        //    Products.Where(p =>
        //        (SelectedCategory == "Tất cả" || p.ProductCategory.CategoryId == SelectedCategory) &&
        //        (string.IsNullOrWhiteSpace(SearchText) ||
        //         RemoveDiacritics(p.Name).Contains(RemoveDiacritics(SearchText), StringComparison.OrdinalIgnoreCase)));
        public IEnumerable<Product> FilteredProducts => Products;
        partial void OnSearchTextChanged(string value) => OnPropertyChanged(nameof(FilteredProducts));
        partial void OnSelectedCategoryChanged(string value) => OnPropertyChanged(nameof(FilteredProducts));

        // ✅ Cập nhật tổng tiền & giảm giá
        private void UpdateTotal()
        {
            var subtotal = OrderItems.Sum(x => x.Subtotal);
            int discount = 0;

            if (SelectedDiscount != null &&
                SelectedDiscount.Id != Guid.Empty &&
                SelectedDiscount.IsValidNow &&
                SelectedDiscount.IsValidForTotal(subtotal))
            {
                if (SelectedDiscount.FixedAmount != null)
                    discount = SelectedDiscount.FixedAmount.Value;
                else if (SelectedDiscount.Percentage != null)
                    discount = (int)(subtotal * SelectedDiscount.Percentage.Value / 100.0);
            }

            DiscountAmountFormatted = discount > 0 ? $"-{discount:N0} đ" : "";
            TotalPriceFormatted = $"{(subtotal - discount):N0} đ";
        }

        partial void OnSelectedDiscountChanged(DiscountCode value) => UpdateTotal();

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
                Toppings = new ObservableCollection<Topping>(selectedToppings)
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

        [RelayCommand]
        private async Task ShowDiscountPopupAsync()
        {
            double subtotal = OrderItems.Sum(x => x.Subtotal);
            var popup = new SelectDiscountPopup(AvailableDiscounts.ToList(), subtotal);
            var result = await Application.Current.MainPage.ShowPopupAsync(popup);

            if (result is DiscountCode selected)
            {
                SelectedDiscount = selected;
                UpdateTotal();
            }
        }



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
        /////// Call api
        [ObservableProperty]
        bool isLoading;

        [RelayCommand]
        public async Task LoadProductsAsync()
        {
            IsLoading = true;

            try
            {
                Products.Clear();
                var response = await _productService.GetProductsAsync();

                foreach (var p in response.Items)
                {
                    Products.Add(new Product
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        ProductPrice = p.ProductPrice,
                        ProductImgUrl = p.ProductImgUrl,
                        ProductCategory = p.ProductCategory,
                        ProductStatus = p.ProductStatus
                    });

                    if (!string.IsNullOrEmpty(p.ProductCategory?.CategoryName) &&
                        !Categories.Contains(p.ProductCategory.CategoryName))
                    {
                        Categories.Add(p.ProductCategory.CategoryName);
                    }
                }
                Console.WriteLine(Products);
                OnPropertyChanged(nameof(FilteredProducts));
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Lỗi", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }
        }

    }
}
