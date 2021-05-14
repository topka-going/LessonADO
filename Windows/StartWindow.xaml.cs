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
using LessonADO.EF;
namespace LessonADO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            var user = AppData.context.Client.ToList().
            Where(i => i.Login == txtLogin.Text && i.Password == txtPassword.Password).FirstOrDefault();

            if (user != null)
            {
                int role = user.IdRole;

                switch (role)
                {
                    case 1:
                        AdminWindow adminka = new AdminWindow();
                        adminka.ShowDialog();

                        break;
                    case 2:
                        ManagerWindow manager = new ManagerWindow();
                        manager.ShowDialog();
                        break;
                    case 3:
                        UserWindow userwindow = new UserWindow(user);
                        userwindow.ShowDialog();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неправильно введены данные");
            }
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
