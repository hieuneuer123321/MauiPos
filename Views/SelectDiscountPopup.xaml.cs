using CommunityToolkit.Maui.Views;
using MauiAppUIDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MauiAppUIDemo.Views
{
    public partial class SelectDiscountPopup : Popup
    {
        private DiscountCode selectedDiscount;

        public SelectDiscountPopup(List<DiscountCode> discounts, double currentTotal)
        {
            InitializeComponent();

            // Đánh dấu mã hợp lệ
            foreach (var d in discounts)
                d.IsValidForTotalCached = d.IsValidForTotal(currentTotal);

            DiscountList.ItemsSource = discounts;
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            Close(); // Không trả về mã nào
        }

        private void OnApplyClicked(object sender, EventArgs e)
        {
            Close(selectedDiscount); // Trả về mã đã chọn
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDiscount = e.CurrentSelection.FirstOrDefault() as DiscountCode;
        }
    }
}
