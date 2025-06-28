using CommunityToolkit.Maui.Views;
using MauiAppUIDemo.Models;

namespace MauiAppUIDemo.Views
{
    public partial class SelectToppingPopup : Popup
    {
        private List<ToppingOption> options;

        public SelectToppingPopup(List<Topping> availableToppings)
        {
            InitializeComponent();
            options = availableToppings
                .Select(t => new ToppingOption { Name = t.Name, Price = t.Price })
                .ToList();
            ToppingList.ItemsSource = options;
        }

        private void OnDoneClicked(object sender, EventArgs e)
        {
            var selected = options
                .Where(x => x.IsSelected)
                .Select(x => new Topping { Name = x.Name, Price = x.Price })
                .ToList();

            Close(selected);
        }

        class ToppingOption : Topping
        {
            public bool IsSelected { get; set; }
        }
    }
}
