using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace TheGame.DependencyInjection;

public sealed class WindowSwitcher
{
    private ServiceProvider _provider;

    public WindowSwitcher(ServiceCollection services)
    {
        services.AddSingleton(this);
        _provider = services.BuildServiceProvider();
    }

    public Window Open<TWindow>() where TWindow : Window
    {
        var newWindow = ActivatorUtilities.GetServiceOrCreateInstance<TWindow>(_provider);
        newWindow.Show();

        return newWindow;
    }

    public Window Open<TWindow>(Window current) where TWindow : Window
    {
        var newWindow = ActivatorUtilities.GetServiceOrCreateInstance<TWindow>(_provider);

        newWindow.Closing += (_, _) => current.Show();

        current.Hide();
        newWindow.Show();

        return newWindow;
    }
}
