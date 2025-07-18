

using MauiAppUIDemo.Helopper;
using MauiAppUIDemo.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;


namespace MauiAppUIDemo.Views;

public partial class ProductListView : ContentView
{
 

    public ProductListView()
    {
        InitializeComponent();
        var vm = ServiceHelper.GetService<OrderPageViewModel>();
        BindingContext = vm;

        // Dữ liệu mẫu
        // Dữ liệu mẫu với ảnh có sẵn trong MAUI

    }
}