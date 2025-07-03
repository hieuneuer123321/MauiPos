using CommunityToolkit.Mvvm.ComponentModel;
namespace MauiAppUIDemo.Models
{
    public partial class DiscountCode : ObservableObject
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Percentage { get; set; }
        public int? FixedAmount { get; set; }
        public int? MinInvoiceTotal { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        [ObservableProperty]
        private bool isValidForTotalCached;

        public string DisplayText =>
            IsValidForTotalCached
            ? $"{Name} - {Description}"
            : $"❌ {Name} - (Không đủ điều kiện)";

        public bool IsValidNow =>
            IsActive &&
            (StartDate == null || StartDate <= DateTime.Now) &&
            (EndDate == null || EndDate >= DateTime.Now);

        public bool IsValidForTotal(double total) =>
            IsValidNow &&
            (MinInvoiceTotal == null || total >= MinInvoiceTotal);
    }

}

