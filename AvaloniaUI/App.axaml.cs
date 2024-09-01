using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaUI.Pages;
using AvaloniaUI.Services;
using AvaloniaUI.Views;
using Microsoft.Extensions.DependencyInjection;
using SukiUI.Dialogs;
using SukiUI.Toasts;

namespace AvaloniaUI;

public partial class App : Application
{
    private IServiceProvider? _provider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);

        _provider = ConfigureServices();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var mainViewModel = _provider?.GetRequiredService<MainViewModel>();
            var mainView = _provider?.GetRequiredService<MainWindow>();
            mainView!.DataContext = mainViewModel;
            desktop.MainWindow = mainView;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private IServiceProvider? ConfigureServices()
    {
        var services = new ServiceCollection();

        // Views
        services.AddSingleton<MainWindow>();

        // Services
        var viewLocator = Current?.DataTemplates.First(x => x is ViewLocator);
        if (viewLocator is not null)
            services.AddSingleton(viewLocator);
        services.AddSingleton<PageNavigationService>();
        services.AddSingleton<ISukiToastManager, SukiToastManager>();
        services.AddSingleton<ISukiDialogManager, SukiDialogManager>();

        // ViewModels
        services.AddSingleton<MainViewModel>();
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => !p.IsAbstract && typeof(PageBase).IsAssignableFrom(p));
        foreach (var type in types)
            services.AddSingleton(typeof(PageBase), type);

        return services.BuildServiceProvider();
    }
}
