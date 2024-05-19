using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TheGame.Database;
using TheGame.DependencyInjection;

namespace TheGame;

/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    WindowSwitcher _sw;
    GameDbContext _db;
    AuthorizedUser _user;

    public MainWindow(GameDbContext db, WindowSwitcher open, AuthorizedUser user)
    {
       _sw = open;
       _db = db;
       _user = user;    
      
        InitializeComponent();
    }

    private void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var exists = _db.Users.SingleOrDefault(x => x.Email == BtnMail.Text && x.Password == BtnPassword.Text);
            if (exists != null)
            {             
                _user.Instance = exists;
                _sw.Open<WindowShop>(this);
                return;                
            }
            MessageBox.Show("Неверный логи или пароль!");
        }
        catch (Exception a)
        {
            MessageBox.Show(a.Message);
            throw;
        }

   
    }

    private void BtnForRegistration_Click(object sender, RoutedEventArgs e)
    {
        _sw.Open<WindowForRegistration>(this);
    }

    private void TbTop_Click(object sender, RoutedEventArgs e)
    {
        _sw.Open<WindowRecord>(this);
    }
}
