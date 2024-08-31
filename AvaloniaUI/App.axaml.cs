using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using AvaloniaUI.ViewModels;
using AvaloniaUI.Views;
using Microsoft.Extensions.DependencyInjection;

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

        // ViewModels
        services.AddSingleton<MainViewModel>();
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => !p.IsAbstract && typeof(IViewModel).IsAssignableFrom(p));
        foreach (var type in types)
            services.AddSingleton(typeof(IViewModel), type);

        return services.BuildServiceProvider();
    }
}
