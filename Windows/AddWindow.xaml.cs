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
using LessonADO.EF;

namespace LessonADO.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        private Client useredit;
        AdminWindow admin = new AdminWindow();
        public AddWindow()
        {
            InitializeComponent();
            ComboRole.ItemsSource = AppData.context.Role.ToList();
            ComboRole.DisplayMemberPath = "Role1";
            ComboRole.SelectedIndex = 2;
            ComboGender.ItemsSource = AppData.context.Gender.ToList();
            ComboGender.DisplayMemberPath = "Gender1";
            ComboGender.SelectedIndex = 0;
           
        }
        public AddWindow(Client user)
        {
            InitializeComponent();
            TxtLogin.Text = user.Login;
            TxtPassword.Text = user.Password;
            TxtLName.Text = user.LName;
            TxtFName.Text = user.FName;
            TxtMName.Text = user.MName;
            ComboRole.SelectedIndex = Int32.Parse(user.IdRole.ToString()) - 1;
            ComboGender.SelectedIndex = Int32.Parse(user.IdGender.ToString()) - 1;
        }


        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Client usertb = new Client();

            useredit.IdRole = ComboRole.SelectedIndex + 1;
            useredit.IdGender = ComboGender.SelectedIndex + 1;
            useredit.MName = TxtMName.Text;
            useredit.FName= TxtFName.Text;
            useredit.LName= TxtLName.Text;
            useredit.Login= TxtLogin.Text;
            useredit.Password= TxtPassword.Text;
            if (!string.IsNullOrWhiteSpace(TxtAge.Text))
            {
                useredit.Age = Convert.ToInt32(TxtAge.Text);
            }
            useredit.PhoneNumber = TxtPhone.Text;
            
            AppData.context.Client.Add(useredit);
            
            
            AppData.context.SaveChanges();
            

            MessageBox.Show("Пользователь сохранён");
            Close();



        }
    }
}
