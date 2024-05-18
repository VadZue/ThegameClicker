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

namespace TheGame;

/// <summary>
/// Логика взаимодействия для MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{

    public MainWindow()
    {
        try
        {
            var db = GameDbContext.Db;
            var user = new User()
            {
                Birthday = new DateTime(2024, 11, 13),
                Email = "vad@gamil.com",
                Password = "12345",
                NikName = "Vadim"
            };
            db.Users.Add(user);
            db.SaveChanges();
            var b = db.Users.ToList();
            InitializeComponent();
        }
        catch(Exception ex)
        {

        }
       
    }
}
