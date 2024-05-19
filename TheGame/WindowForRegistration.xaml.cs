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
    /// Логика взаимодействия для WindowForRegistration.xaml
    /// </summary>
    public partial class WindowForRegistration : Window
    {
        WindowSwitcher _sw;
        GameDbContext _db;
        public WindowForRegistration(GameDbContext db, WindowSwitcher open)
        {

            _sw = open;
            _db = db;

            InitializeComponent();
        }


        private void BtnRegistration_Click(object sender, RoutedEventArgs e)
        {
            if (!DateTime.TryParse(TbBirthday.Text, out var datebirthday))
            {
                MessageBox.Show("Вы вводите дату неправильно!");
                return;
            }


            var user = new User()
            {
                NikName = TbNikname.Text,
                Birthday = datebirthday,
                Email = TbEmail.Text,
                Password = TbPass.Text,
            };

            if (string.IsNullOrWhiteSpace(user.NikName))
            {
                MessageBox.Show("Введите Ник-Нейм!");
                return;
            }
            if (string.IsNullOrWhiteSpace(user.Email))
            {
                MessageBox.Show("Введите вашу почту!");
                return;
            }
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                MessageBox.Show("Введите пароль!");
                return;
            }
            if (_db.Users.Any(x => x.Email == user.Email))
            {
                MessageBox.Show("Такой пользователь уже существует");
                return;
            }

            try
            {
                _db.Users.Add(user);
                _db.SaveChanges();
                MessageBox.Show("Вы успешно зарегестрировали пользователя");            
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
