using CommunityToolkit.Maui.Views;
using MauiAppUIDemo.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace MauiAppUIDemo.Views
{
    public partial class SelectDiscountPopup : Popup
    {
        public ObservableCollection<DiscountCode> DiscountCodes { get; set; }

        public static readonly BindableProperty SelectedDiscountIdProperty =
            BindableProperty.Create(nameof(SelectedDiscountId), typeof(Guid), typeof(SelectDiscountPopup), Guid.Empty);

        public Guid SelectedDiscountId
        {
            get => (Guid)GetValue(SelectedDiscountIdProperty);
            set => SetValue(SelectedDiscountIdProperty, value);
        }

        public ICommand SelectDiscountCommand { get; }

        private DiscountCode selected;

        public SelectDiscountPopup(List<DiscountCode> discounts, double currentTotal)
        {
            InitializeComponent();

            foreach (var d in discounts)
                d.IsValidForTotalCached = d.IsValidForTotal(currentTotal);

            DiscountCodes = new ObservableCollection<DiscountCode>(discounts);

            SelectDiscountCommand = new Command<DiscountCode>((d) =>
            {
                if (d != null && d.IsValidForTotalCached) // Chỉ cho chọn nếu hợp lệ
                {
                    selected = d;
                    SelectedDiscountId = d.Id;
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
}
