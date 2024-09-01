﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Collections;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using SukiUI.Enums;
using SukiUI.Models;
using SukiUI;

namespace AvaloniaUI.Pages.Settings
{
    public partial class SettingViewModel : PageBase
    {
        public Action<SukiBackgroundStyle>? BackgroundStyleChanged { get; set; }
        public Action<bool>? BackgroundAnimationsChanged { get; set; }
        public Action<bool>? BackgroundTransitionsChanged { get; set; }
        public Action<string?>? CustomBackgroundStyleChanged { get; set; }

        public IAvaloniaReadOnlyList<SukiColorTheme> AvailableColors { get; }
        public IAvaloniaReadOnlyList<SukiBackgroundStyle> AvailableBackgroundStyles { get; }
        public IAvaloniaReadOnlyList<string> CustomShaders { get; } = new AvaloniaList<string> { "Space", "Weird", "Clouds" };

        private readonly SukiTheme _theme = SukiTheme.GetInstance();

        [ObservableProperty] private bool _isLightTheme;
        [ObservableProperty] private SukiBackgroundStyle _backgroundStyle;
        [ObservableProperty] private bool _backgroundAnimations;
        [ObservableProperty] private bool _backgroundTransitions;

        private string? _customShader = null;

        public SettingViewModel() : base("Settings", MaterialIconKind.Settings, 100)
        {
            AvailableBackgroundStyles = new AvaloniaList<SukiBackgroundStyle>(Enum.GetValues<SukiBackgroundStyle>());
            AvailableColors = _theme.ColorThemes;
            IsLightTheme = _theme.ActiveBaseTheme == ThemeVariant.Light;
            _theme.OnBaseThemeChanged += variant =>
                IsLightTheme = variant == ThemeVariant.Light;
            _theme.OnColorThemeChanged += theme =>
            {
                // TODO: Implement a way to make this correct, might need to wrap the thing in a VM, this isn't ideal.
            };
        }

        partial void OnIsLightThemeChanged(bool value) =>
            _theme.ChangeBaseTheme(value ? ThemeVariant.Light : ThemeVariant.Dark);

        [RelayCommand]
        private void SwitchToColorTheme(SukiColorTheme colorTheme) =>
            _theme.ChangeColorTheme(colorTheme);

        partial void OnBackgroundStyleChanged(SukiBackgroundStyle value) =>
            BackgroundStyleChanged?.Invoke(value);

        partial void OnBackgroundAnimationsChanged(bool value) =>
            BackgroundAnimationsChanged?.Invoke(value);

        partial void OnBackgroundTransitionsChanged(bool value) =>
            BackgroundTransitionsChanged?.Invoke(value);

        [RelayCommand]
        private void TryCustomShader(string shaderType)
        {
            _customShader = _customShader == shaderType ? null : shaderType;
            CustomBackgroundStyleChanged?.Invoke(_customShader);
        }
    }
}
