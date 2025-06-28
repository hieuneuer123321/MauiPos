using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MauiAppUIDemo.Models;

public partial class MenuItemModel : ObservableObject
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public Type TargetViewType { get; set; }

    public ObservableCollection<MenuItemModel> SubMenuItems { get; set; }

    public bool HasSubMenus => SubMenuItems != null && SubMenuItems.Count > 0;

    [ObservableProperty]
    bool isSubMenuVisible;

    [ObservableProperty]
    double subMenuHeight;

    [ObservableProperty]
    MenuItemModel parent;

    [ObservableProperty]
    bool isAnimating;
}
