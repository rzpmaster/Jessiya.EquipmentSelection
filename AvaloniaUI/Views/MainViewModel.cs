using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Styling;
using AvaloniaUI.Pages;
using AvaloniaUI.Pages.Settings;
using AvaloniaUI.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using SukiUI;
using SukiUI.Dialogs;
using SukiUI.Enums;
using SukiUI.Models;
using SukiUI.Toasts;

namespace AvaloniaUI.Views;

public partial class MainViewModel : ObservableObject
{
    public IAvaloniaReadOnlyList<PageBase> Pages { get; }
    public IAvaloniaReadOnlyList<SukiColorTheme> Themes { get; }
    //public IAvaloniaReadOnlyList<SukiBackgroundStyle> BackgroundStyles { get; }

    public ISukiToastManager ToastManager { get; }
    public ISukiDialogManager DialogManager { get; }

    private readonly SukiTheme _theme;
    //private readonly SettingViewModel _setting;

    public MainViewModel(IEnumerable<PageBase> demoPages, PageNavigationService pageNavigationService, ISukiToastManager toastManager, ISukiDialogManager dialogManager)
    {
        ToastManager = toastManager;
        DialogManager = dialogManager;
        Pages = new AvaloniaList<PageBase>(demoPages.OrderBy(x => x.Index).ThenBy(x => x.DisplayName));
        //_setting = (SettingViewModel)Pages.First(x => x is SettingViewModel);
        //_setting.BackgroundStyleChanged += style => BackgroundStyle = style;
        //_setting.BackgroundAnimationsChanged += enabled => AnimationsEnabled = enabled;
        //_setting.CustomBackgroundStyleChanged += shader => CustomShaderFile = shader;
        //_setting.BackgroundTransitionsChanged += enabled => TransitionsEnabled = enabled;

        //BackgroundStyles = new AvaloniaList<SukiBackgroundStyle>(Enum.GetValues<SukiBackgroundStyle>());
        _theme = SukiTheme.GetInstance();

        // Subscribe to the navigation service (when a page navigation is requested)
        pageNavigationService.NavigationRequested += pageType =>
        {
            var page = Pages.FirstOrDefault(x => x.GetType() == pageType);
            if (page is null || ActivePage?.GetType() == pageType) return;
            ActivePage = page;
        };

        Themes = _theme.ColorThemes;
        BaseTheme = _theme.ActiveBaseTheme;

        // Subscribe to the base theme changed events
        _theme.OnBaseThemeChanged += variant =>
        {
            BaseTheme = variant;
            ToastManager.CreateSimpleInfoToast()
                .WithTitle("Theme Changed")
                .WithContent($"Theme has changed to {variant}.")
                .Queue();
        };

        // Subscribe to the color theme changed events
        _theme.OnColorThemeChanged += theme => ToastManager.CreateSimpleInfoToast()
            .WithTitle("Color Changed")
            .WithContent($"Color has changed to {theme.DisplayName}.")
            .Queue();
    }

    [ObservableProperty] private ThemeVariant _baseTheme;
    [ObservableProperty] private PageBase? _activePage;
    //[ObservableProperty] private SukiBackgroundStyle _backgroundStyle = SukiBackgroundStyle.Bubble;
    //[ObservableProperty] private bool _animationsEnabled;
    //[ObservableProperty] private string? _customShaderFile;
    //[ObservableProperty] private bool _transitionsEnabled;
}
