using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SukiUI.Dialogs;

namespace AvaloniaUI.Pages.Dashboard
{
    public partial class DialogViewModel(ISukiDialog dialog) : ObservableObject
    {
        [RelayCommand]
        private void CloseDialog()
        {
            dialog.Dismiss();
        }
    }
}
