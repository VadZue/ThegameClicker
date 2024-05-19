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
    /// Логика взаимодействия для WindowFight.xaml
    /// </summary>
    public partial class WindowFight : Window
    {
    
        GameDbContext _db;
        User _user;

        public WindowFight(GameDbContext db, AuthorizedUser user)
        {
            _db = db;
            _user = user.Instance;
            InitializeComponent();
            UpdateStats();
        }

        private void UpdateStats() 
        {
           TbForScore.Text = _user.Score.ToString();
           TbForMoney.Text = _user.Money.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _user.Click();
                _db.SaveChanges();
                UpdateStats();
            }
            catch (Exception v)
            {
                MessageBox.Show(v.Message);
            }
        }

        private void BtnTawern_Копировать_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
