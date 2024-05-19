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
    /// Логика взаимодействия для WindowBuy.xaml
    /// </summary>
    public partial class WindowBuy : Window
    {
        GameDbContext _db;
        User _user;
        public WindowBuy(GameDbContext db, AuthorizedUser user)
        {
            _db = db;
            _user = user.Instance;
            InitializeComponent();
            UpdateStats();
        }
        private void UpdateStats()
        {
            TbForScore.Text = $"bonus: {_user.ScoreSkill.Up}, cost: {_user.ScoreSkill.UpgradeCost}"; 
            TbForMoney.Text = $"bonus: {_user.MoneySkill.Up}, cost: {_user.MoneySkill.UpgradeCost}"; 
        }

        private void TbByScore_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_user.Score < _user.ScoreSkill.UpgradeCost)
                {
                    MessageBox.Show("У вас не хватает денег на покупку");
                    return;
                }
                _user.UpgradeScoreSkill();
                _db.SaveChanges();
                UpdateStats();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);
                
            }
        }

        private void TbByMoney_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_user.Money < _user.MoneySkill.UpgradeCost)
                {
                    MessageBox.Show("У вас не хватает денег на покупку");
                    return;
                }
                _user.UpgradeMoneySkill();
                _db.SaveChanges();
                UpdateStats();
            }
            catch (Exception a)
            {
                MessageBox.Show(a.Message);

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
