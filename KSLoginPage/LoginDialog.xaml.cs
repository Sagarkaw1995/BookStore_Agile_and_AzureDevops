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
using BookStoreLIB;

namespace BookStoreGUI
{
     /// <summary>
     /// Interaction logic for LoginDialog.xaml
     /// </summary>
     public partial class LoginDialog : Window
     {
          public LoginDialog()
          {
               InitializeComponent();
          }

          private void okButton_Click(object sender, RoutedEventArgs e)
          {
            /*

            string username = this.nameTextBox.Text;
            string password = this.passwordTextBox.Password;
            var userData = new UserData();
            if (username != "" && password != "") //Check if boxes are empty

                 if (userData.LogIn(username, password) == true)
                 {//Check the password
                      //MessageBox.Show("Thank you for providing the input");
                      this.DialogResult = true;
                 }
                 else
                 {
                      //MessageBox.Show("A valid password needs to have at least six characters with both letters and numbers");
                      this.DialogResult = false;
                 }
            else
            {
                 MessageBox.Show("Please fill in all slots.");
                 //this.DialogResult = false;
            }
            */
            if (this.nameTextBox.Text == "" || this.passwordTextBox.Password == "")
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else
            {
                this.DialogResult = true;
            }
          }

          private void cancelButton_Click(object sender, RoutedEventArgs e)
          {
               this.DialogResult = false;
          }

        private void forgotPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePassword cp = new ChangePassword();
            cp.Owner = this;
            cp.ShowDialog();
            if (cp.DialogResult == true)
            {
                MessageBox.Show("Password changed successfully.");
            }
        }
    }
}
