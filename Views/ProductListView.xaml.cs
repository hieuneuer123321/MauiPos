

using MauiAppUIDemo.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace MauiAppUIDemo.Views;

public partial class ProductListView : ContentView
{
 

    public ProductListView()
    {
        InitializeComponent();
        BindingContext = new OrderPageViewModel();
        // Dữ liệu mẫu
        // Dữ liệu mẫu với ảnh có sẵn trong MAUI

    }
}