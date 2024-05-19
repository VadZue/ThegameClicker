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
using System.Windows.Shapes;
using TheGame.Database;

namespace TheGame
{
    /// <summary>
    /// Логика взаимодействия для WindowRecord.xaml
    /// </summary>
    public partial class WindowRecord : Window
    {

        public WindowRecord(GameDbContext db)
        {
            InitializeComponent();
            TopGamers.ItemsSource = db.Users.Select(x => new { x.Score, x.Money, x.NikName }).OrderByDescending(x => x.Score).ToList();
        }

        private void TopGamers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
