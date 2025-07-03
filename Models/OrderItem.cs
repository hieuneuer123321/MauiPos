using CommunityToolkit.Mvvm.ComponentModel;
using MauiAppUIDemo.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace MauiAppUIDemo.Models
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

        private ObservableCollection<Topping> toppings = new();
        public ObservableCollection<Topping> Toppings
        {
            get => toppings;
            set
            {
                if (toppings != null)
                    toppings.CollectionChanged -= Toppings_CollectionChanged;

                toppings = value;
                OnPropertyChanged(nameof(Toppings));
                OnPropertyChanged(nameof(Subtotal));
                OnPropertyChanged(nameof(ToppingSummary));

                if (toppings != null)
                    toppings.CollectionChanged += Toppings_CollectionChanged;
            }
        }

        public double Subtotal => Quantity * (Price + Toppings.Sum(t => t.Price));

        public string ToppingSummary =>
            Toppings != null && Toppings.Any()
            ? $"Topping: {string.Join(", ", Toppings.Select(t => t.Name))}"
            : string.Empty;

        partial void OnQuantityChanged(int oldValue, int newValue)
        {
            OnPropertyChanged(nameof(Subtotal));
        }

        private void Toppings_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Subtotal));
            OnPropertyChanged(nameof(ToppingSummary));
        }
    }
}
