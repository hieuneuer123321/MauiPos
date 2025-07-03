using CommunityToolkit.Maui.Views;
using MauiAppUIDemo.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiAppUIDemo.Views
{
    public partial class SelectToppingPopup : Popup
    {
        public ObservableCollection<ToppingOption> Options { get; set; }

        public ICommand ToggleToppingCommand { get; }

        public SelectToppingPopup(List<Topping> availableToppings)
        {
            InitializeComponent();

            Options = new ObservableCollection<ToppingOption>(
                availableToppings.Select(t => new ToppingOption { Name = t.Name, Price = t.Price })
            );

            ToggleToppingCommand = new Command<ToppingOption>(ToggleTopping);

            BindingContext = this;
            ToppingList.ItemsSource = Options;
        }

        private void ToggleTopping(ToppingOption topping)
        {
            if (topping != null)
                topping.IsSelected = !topping.IsSelected; // Gọi SetProperty bên dưới
        }

        private void OnDoneClicked(object sender, EventArgs e)
        {
            var selected = Options
                .Where(x => x.IsSelected)
                .Select(x => new Topping { Name = x.Name, Price = x.Price })
                .ToList();

            Close(selected);
        }

     
    }
}
