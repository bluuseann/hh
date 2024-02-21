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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace pereplet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void auto_Click(object sender, RoutedEventArgs e)
        {
            var login = log.Text;
            var password = pass.Text;

            var context = new AppDbContext();

            var user = context.Users.SingleOrDefault(x => (x.Login == login  && x.Password == password));
            log.BorderBrush = new SolidColorBrush(Colors.Black);
            pass.BorderBrush = new SolidColorBrush(Colors.Black);
            if (user is null)
            {
                //passTB.Password = pass.Text;
                //pass.Visibility = Visibility.Visible;
                //passTB.Visibility = Visibility.Hidden;
                var user_exists = context.Users.FirstOrDefault(x => x.Login == login);
                if (user_exists is null)
                {
                    log.BorderBrush = new SolidColorBrush(Colors.Red);
                    error.Text = "Такого пользователя не существует";

                }
                else
                {
                    pass.BorderBrush = new SolidColorBrush(Colors.Red);
                    error.Text = "Неверный пароль";
                }
                return;
            }

            MessageBox.Show("Вы успешно вошли в аккаунт!");
            katalog katalog = new katalog();
            katalog.Show();
            this.Close();

        }

        private void reg_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            this.Close();
        }
    }
}
