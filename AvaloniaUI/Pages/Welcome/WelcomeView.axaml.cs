using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using SukiUI;

namespace AvaloniaUI.Pages.Welcome;

public partial class WelcomeView : UserControl
{
    public WelcomeView()
    {
        SukiTheme.GetInstance().OnBaseThemeChanged += _ =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                var TextBlockWithInlines = TipPanel.Children.Where(x => x is TextBlock);
                foreach (var TextBlockWithInline in TextBlockWithInlines)
                {
                    TextBlockWithInline.InvalidateVisual();
                }
            });
        };
        InitializeComponent();
    }
}