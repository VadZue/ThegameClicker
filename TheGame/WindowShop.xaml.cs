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
using TheGame.DependencyInjection;

namespace TheGame
{
    /// <summary>
    /// Логика взаимодействия для WindowShop.xaml
    /// </summary>
    public partial class WindowShop : Window
    {
        WindowSwitcher _sw;

        public WindowShop(WindowSwitcher open)
        {
            _sw = open;
            InitializeComponent();
        }

        private void BtnForFight_Click(object sender, RoutedEventArgs e)
        {           
            _sw.Open<WindowFight>(this);
        }

        private void BtnBuy_Click(object sender, RoutedEventArgs e)
        {
            _sw.Open<WindowBuy>(this);
        }
    }
}
