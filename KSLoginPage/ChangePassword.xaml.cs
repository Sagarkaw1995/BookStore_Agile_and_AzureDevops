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
using BookStoreLIB;


namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        UserData userData = new UserData();
        public ChangePassword()
        {
            InitializeComponent();
        }
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string uname = this.nameTextBox.Text;
            string pwd = this.passwordTextBox.Password;

            if (uname == "" || pwd == "")
            {
                MessageBox.Show("Please enter both username and password.");
            }
            else if (!(pwd.Length >= 6 && char.IsLetter(pwd[0]) && pwd.Any(c => char.IsLetter(c)) && pwd.Any(c => char.IsDigit(c)) && pwd.All(c => char.IsLetterOrDigit(c))))
            {
                MessageBox.Show("A valid password needs to have at least six characters with both letters and numbers");
            }
            else
            {
                if (userData.checkUsername(uname) && userData.UpdatePassword(uname, pwd))
                {
                    this.DialogResult = true;
                }
                else
                {
                    MessageBox.Show("Username not found in database.");
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
