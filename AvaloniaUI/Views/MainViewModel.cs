using System.Collections.Generic;
using System.Linq;
using Avalonia.Collections;
using AvaloniaUI.Pages;
using AvaloniaUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using SukiUI.Dialogs;
using SukiUI.Toasts;

namespace AvaloniaUI.Views;

public partial class MainViewModel : ObservableObject
{
    public string Greeting => "Welcome to Avalonia!";
    public ISukiToastManager ToastManager { get; }
    public ISukiDialogManager DialogManager { get; }

    public MainViewModel(IEnumerable<PageBase> demoPages, PageNavigationService pageNavigationService, ISukiToastManager toastManager, ISukiDialogManager dialogManager)
    {
        ToastManager = toastManager;
        DialogManager = dialogManager;
        Pages = new AvaloniaList<PageBase>(demoPages.OrderBy(x => x.Index).ThenBy(x => x.DisplayName));
        //_theming = (ThemingViewModel)DemoPages.First(x => x is ThemingViewModel);
        //_theming.BackgroundStyleChanged += style => BackgroundStyle = style;
        //_theming.BackgroundAnimationsChanged += enabled => AnimationsEnabled = enabled;
        //_theming.CustomBackgroundStyleChanged += shader => CustomShaderFile = shader;
        //_theming.BackgroundTransitionsChanged += enabled => TransitionsEnabled = enabled;

        //BackgroundStyles = new AvaloniaList<SukiBackgroundStyle>(Enum.GetValues<SukiBackgroundStyle>());
        //_theme = SukiTheme.GetInstance();

        // Subscribe to the navigation service (when a page navigation is requested)
        pageNavigationService.NavigationRequested += pageType =>
        {
            var page = Pages.FirstOrDefault(x => x.GetType() == pageType);
            if (page is null || ActivePage?.GetType() == pageType) return;
            ActivePage = page;
        };

        //Themes = _theme.ColorThemes;
        //BaseTheme = _theme.ActiveBaseTheme;

        //// Subscribe to the base theme changed events
        //_theme.OnBaseThemeChanged += variant =>
        //{
        //    BaseTheme = variant;
        //    ToastManager.CreateSimpleInfoToast()
        //        .WithTitle("Theme Changed")
        //        .WithContent($"Theme has changed to {variant}.")
        //        .Queue();
        //};

        //// Subscribe to the color theme changed events
        //_theme.OnColorThemeChanged += theme => ToastManager.CreateSimpleInfoToast()
        //    .WithTitle("Color Changed")
        //    .WithContent($"Color has changed to {theme.DisplayName}.")
        //    .Queue();
    }

    public IAvaloniaReadOnlyList<PageBase> Pages { get; }

    [ObservableProperty]
    private PageBase? _activePage;
}
