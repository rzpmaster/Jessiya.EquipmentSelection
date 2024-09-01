using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AvaloniaUI.Pages.Dashboard
{
    public partial class InvoiceViewModel : ObservableObject
    {
        [ObservableProperty] private int _id;
        [ObservableProperty] private string? _billingName;
        [ObservableProperty] private int _amountPaid;
        [ObservableProperty] private bool _paid;

        public InvoiceViewModel(int id, string? billingName, int amountPaid, bool paid)
        {
            Id = id;
            BillingName = billingName;
            AmountPaid = amountPaid;
            Paid = paid;
        }
    }
}
