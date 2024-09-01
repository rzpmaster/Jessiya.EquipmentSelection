using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaUI.Pages.Dashboard;
using AvaloniaUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;

namespace AvaloniaUI.Pages.Welcome
{
    public partial class WelcomeViewModel(PageNavigationService nav) : PageBase("Welcome", MaterialIconKind.Hand, int.MinValue)
    {
        [ObservableProperty] private bool _dashBoardVisited;

        [RelayCommand]
        private void OpenDashboard()
        {
            DashBoardVisited = true;
            nav.RequestNavigation<DashboardViewModel>();
        }
    }
}
