using DishesYulin4ISP9_7.EF;
using DishesYulin4ISP9_7.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Windows.Threading;

using static DishesYulin4ISP9_7.EF.HelperEF;

namespace DishesYulin4ISP9_7
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        private string _captcha;
        public AuthorizationWindow()
        {
            InitializeComponent();

            SpCaptcha.Visibility = Visibility.Collapsed;
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            #region Validation
            if (string.IsNullOrWhiteSpace(TbLogin.Text) || string.IsNullOrWhiteSpace(TbPassword.Text))
            {
                MessageBox.Show("Вы не ввели данные", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = DbContext.User.FirstOrDefault(u => u.UserLogin == TbLogin.Text && u.UserPassword == TbPassword.Text);

            if (user is null)
            {
                MessageBox.Show("Вы ввели неправильные данные!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                _captcha = GetCaptcha();
                UseCaptcha(_captcha);

                return;
            }

            if (SpCaptcha.Visibility == Visibility.Visible)
            {
                if (TbCaptcha.Text != _captcha)
                {
                    MessageBox.Show("Вы ввели неккоректно каптчу!\nПодождите 10 секунд и повторите попытку вновь.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                    DispatcherTimer dispatcherTimer = new DispatcherTimer();
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
                    dispatcherTimer.Start();

                    BtnLogin.IsEnabled = false;

                    _captcha = GetCaptcha();
                    UseCaptcha(_captcha);
                    return;
                }
            }
            #endregion

            MainWindow mainWindow = new MainWindow(user);
            mainWindow.Show();
            Close();
        }

        private void BtnGuest_Click(object sender, RoutedEventArgs e)
        {
            GuestWindow guestWindow = new GuestWindow();
            guestWindow.Show();
            Close();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            var timer = sender as DispatcherTimer;
            timer.Stop();

            BtnLogin.IsEnabled = true;
        }

        #region Captcha
        private void UseCaptcha(string capcha)
        {
            SpCaptcha.Visibility = Visibility.Visible;

            Txt1.Text = capcha[0].ToString();
            Txt2.Text = capcha[1].ToString();
            Txt3.Text = capcha[2].ToString();
            Txt4.Text = capcha[3].ToString();
        }

        private string GetCaptcha()
        {
            Random rand = new Random();

            string symbs = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            string captcha = string.Empty;

            for (int i = 0; i < 4; i++)
            {
                int newIndex = rand.Next(0, symbs.Length);

                char symb = symbs[newIndex];

                while (captcha.Contains(symb))
                {
                    newIndex = rand.Next(0, symbs.Length);

                    symb = symbs[newIndex];
                }

                captcha += symb;
            }

            return captcha;
        }
        #endregion
    }
}
