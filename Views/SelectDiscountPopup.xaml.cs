using CommunityToolkit.Maui.Views;
using MauiAppUIDemo.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MauiAppUIDemo.Views
{
    public partial class SelectDiscountPopup : Popup
    {
        public ObservableCollection<DiscountCode> DiscountCodes { get; set; }
        public ICommand SelectDiscountCommand { get; }

        private DiscountCode selected;

        public Guid SelectedDiscountId { get; set; } = Guid.Empty;

        private readonly double currentTotal;

        public SelectDiscountPopup(List<DiscountCode> discounts, double currentTotal)
        {
            InitializeComponent();
            this.currentTotal = currentTotal;

            foreach (var d in discounts)
                d.IsValidForTotalCached = d.IsValidForTotal(currentTotal);

            DiscountCodes = new ObservableCollection<DiscountCode>(discounts);

            SelectDiscountCommand = new Command<DiscountCode>((d) =>
            {
                if (d == null)
                    return;

                if (d.Id == Guid.Empty || (d.IsValidNow && d.IsValidForTotal(currentTotal)))
                {
                    selected = d;
                    SelectedDiscountId = d.Id;
                    OnPropertyChanged(nameof(SelectedDiscountId));
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
