using MauiAppUIDemo.Models;

namespace MauiAppUIDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MauiAppUIDemo.ViewModels.MainViewModel();
        }
        //private void OnToggleSubMenuClicked(object sender, EventArgs e)
        //{
        //    if (sender is Button btn && btn.CommandParameter is MenuItemModel menuItem)
        //    {
        //        menuItem.IsSubMenuVisible = !menuItem.IsSubMenuVisible;
        //    }
        //}
    }

}
