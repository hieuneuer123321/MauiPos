using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiAppUIDemo.Models;
using MauiAppUIDemo.Views;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MauiAppUIDemo.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    MenuItemModel selectedMenuItem;

    [ObservableProperty]
    View currentView;

    public ObservableCollection<MenuItemModel> MenuItems { get; } = new()
    {
        new MenuItemModel
        {
            Title = "Đơn Hàng",
            Icon = "🛒",
            TargetViewType = typeof(ProductListView)
        },
        new MenuItemModel
        {
            Title = "Sản Phẩm",
            Icon = "🏠",
            SubMenuItems = new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel { Title = "Menu", Icon = "📄", TargetViewType = typeof(ProductListView) },
                new MenuItemModel { Title = "Phân loại", Icon = "➕", TargetViewType = typeof(SettingsView) }
            }
        },
          new MenuItemModel
        {
            Title = "Báo Cáo",
            Icon = "📊",
            SubMenuItems = new ObservableCollection<MenuItemModel>
            {
                new MenuItemModel { Title = "Sản phẩm", Icon = "👤", TargetViewType = typeof(SettingsView) },
                new MenuItemModel { Title = "Doanh thu", Icon = "⚙️", TargetViewType = typeof(ProductListView) }
            }
        },
        new MenuItemModel
        {
            Title = "Thông tin",
            Icon = "ℹ️",
            TargetViewType = typeof(AboutView)
        },
    };

    public MainViewModel()
    {
        foreach (var item in MenuItems)
        {
            if (item.SubMenuItems != null)
            {
                foreach (var sub in item.SubMenuItems)
                {
                    sub.Parent = item;
                }
            }
        }

        SelectedMenuItem = MenuItems[0];
        CurrentView = Activator.CreateInstance(SelectedMenuItem.TargetViewType) as View;
    }

    [RelayCommand]
    public async Task MenuItemSelected(MenuItemModel menu)
    {
        if (menu == null) return;

        if (menu.HasSubMenus)
        {
            foreach (var item in MenuItems)
            {
                if (item != menu)
                {
                    await AnimateSubMenu(item, false);
                    item.IsSubMenuVisible = false;
                }
            }

            menu.IsSubMenuVisible = !menu.IsSubMenuVisible;
            await AnimateSubMenu(menu, menu.IsSubMenuVisible);
            return;
        }

        // Nếu là submenu con
        if (menu.Parent != null)
        {
            foreach (var item in MenuItems)
            {
                if (item != menu.Parent)
                {
                    await AnimateSubMenu(item, false);
                    item.IsSubMenuVisible = false;
                }
            }

            menu.Parent.IsSubMenuVisible = true;
            await AnimateSubMenu(menu.Parent, true);
        }

        SelectedMenuItem = menu;

        if (menu.TargetViewType != null)
            CurrentView = Activator.CreateInstance(menu.TargetViewType) as View;
    }

    private async Task AnimateSubMenu(MenuItemModel menu, bool expand)
    {
        if (menu.IsAnimating) return;
        menu.IsAnimating = true;

        double from = menu.SubMenuHeight;
        double to = expand ? menu.SubMenuItems.Count * 50 : 0; // 50 là chiều cao 1 item submenu

        int steps = 8;
        double stepValue = (to - from) / steps;
        int delay = 8;

        for (int i = 0; i < steps; i++)
        {
            menu.SubMenuHeight += stepValue;
            await Task.Delay(delay);
        }
        menu.SubMenuHeight = to;

        menu.IsAnimating = false;
    }
}
