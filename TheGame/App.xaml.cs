using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System.Windows;
using TheGame.Database;
using TheGame.DependencyInjection;

namespace TheGame;

/// <summary> Логика взаимодействия для App.xaml </summary>
public partial class App : Application
{
    private ServiceCollection _services;

    public App()
    {
        _services = new ServiceCollection();
        ConfigureServices(_services);
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        const string DB_PATH = "GameStore.db";

        services.AddDbContext<GameDbContext>(options =>
        {
            options.UseSqlite($"Data Source={DB_PATH}");
        });

        var dbSets = typeof(GameDbContext)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x =>
                x.PropertyType.IsGenericType &&
                x.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>));
        
        foreach (var dbSet in dbSets)
            services.AddScoped(dbSet.PropertyType, provider => dbSet.GetValue(provider.GetService<GameDbContext>()));

        services.AddSingleton<AuthorizedUser>();
    }

    public void OnStartup(object sender, StartupEventArgs e)
    {
        var switcher = new WindowSwitcher(_services);
        switcher.Open<MainWindow>();
    }

}
public sealed class AuthorizedUser
{
    public User Instance { get; set; }
}