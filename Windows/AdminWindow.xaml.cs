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
using System.Data.SqlClient;
using System.Data;
using LessonADO.EF;
using LessonADO.Windows;

namespace LessonADO
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private List<Role> roles = new List<Role>();
        public bool b;
        public AdminWindow()
        {
            InitializeComponent();
            lvDataAdmin.ItemsSource = AppData.context.Client.ToList();


            roles = AppData.context.Role.ToList();
            roles.Insert(0, new Role { Role1 = "Все роли" });
            cmbRole.ItemsSource = roles;
            cmbRole.DisplayMemberPath = "Role1";
            cmbRole.SelectedIndex = 0;
        }

        private void Filter()
        {
            var userlist = AppData.context.Client.ToList();
            if (cmbRole.SelectedIndex != 0)
            {
                userlist = userlist.Where(i => i.IdRole == cmbRole.SelectedIndex).ToList();
            }


            userlist = userlist.
            Where(i => i.LName.Contains(txtSortFIO.Text) || i.FName.Contains(txtSortFIO.Text) || i.MName.Contains(txtSortFIO.Text)).ToList();
            lvDataAdmin.ItemsSource = userlist;
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            b = false;
            AddWindow addwin = new AddWindow();
            Hide();
            addwin.ShowDialog();
            lvDataAdmin.ItemsSource = AppData.context.Client.ToList();
            Show();
        }


        public void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lvDataAdmin.SelectedItem is Client user)
            {
                AddWindow addwin = new AddWindow(user);
                addwin.ShowDialog();
                Hide();
                lvDataAdmin.ItemsSource = EF.AppData.context.Client.ToList();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно удалить выделенного пользователя?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (lvDataAdmin.SelectedItem is Client client)
                {
                    AppData.context.Client.Remove(client);
                    AppData.context.SaveChanges();

                    lvDataAdmin.ItemsSource = AppData.context.Client.ToList();
                }
            }
            else
            {
                return;
            }

        }

        private void txtSortFIO_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }
    }
}
