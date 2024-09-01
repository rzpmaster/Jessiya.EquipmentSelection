using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaloniaUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;

namespace AvaloniaUI.Pages.Welcome
{
    public partial class WelcomeViewModel(PageNavigationService nav) : PageBase("Welcome", MaterialIconKind.Hand, 0)
    {
        [RelayCommand]
        private void OpenSettings()
        {
            nav.RequestNavigation<Settings.SettingViewModel>();
        }

        [RelayCommand]
        private void OpenDashboard()
        {
            nav.RequestNavigation<Dashboard.DashboardViewModel>();
        }

        [RelayCommand]
        private void OpenEquipments()
        {
            nav.RequestNavigation<Equipments.EquipmentViewModel>();
        }

        [RelayCommand]
        private void OpenRules()
        {
            nav.RequestNavigation<Rules.RuleViewModel>();
        }
    }
}
