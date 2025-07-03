using CommunityToolkit.Maui.Views;
using MauiAppUIDemo.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace MauiAppUIDemo.Views;

public partial class SelectDiscountPopup : Popup
{
    public ObservableCollection<DiscountCode> DiscountCodes { get; set; }
    public ICommand SelectDiscountCommand { get; }

    private DiscountCode selected;

    // ➕ Thêm thuộc tính này để binding đổi màu
    public Guid SelectedDiscountId { get; set; } = Guid.Empty;

    public SelectDiscountPopup(List<DiscountCode> discounts, double currentTotal)
    {
        InitializeComponent();
        foreach (var d in discounts)
        {
            d.IsValidForTotalCached = d.IsValidForTotal(currentTotal);
        }

        DiscountCodes = new ObservableCollection<DiscountCode>(discounts);

        SelectDiscountCommand = new Command<DiscountCode>((d) =>
        {
            if (d != null)
            {
                selected = d;
                SelectedDiscountId = d.Id;
                OnPropertyChanged(nameof(SelectedDiscountId)); // ⚠️ thông báo cập nhật binding
            }
        });

        BindingContext = this;
    }

    private void OnCancelClicked(object sender, EventArgs e) => Close();

    private void OnApplyClicked(object sender, EventArgs e)
    {
        if (selected != null)
            Close(selected);
    }
}
