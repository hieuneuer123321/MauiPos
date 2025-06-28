using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiAppUIDemo.Models;

public partial class ToppingOption : ObservableObject
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private int price;

    [ObservableProperty]
    private bool isSelected;
}
