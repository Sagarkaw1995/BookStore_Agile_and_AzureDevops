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

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for SignUpDialog.xaml
    /// </summary>
    public partial class SignUpDialog : Window
    {
        public SignUpDialog()
        {
            InitializeComponent();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            string fname = this.nameFullName.Text;
            string type = this.nameType.Text;
            string manager = this.nameManager.Text;
            string username = this.nameTextBox.Text;
            string password = this.passwordTextBox.Password;
            bool goodpassword = false;

            if (password.Length > 6)
            {
                goodpassword = char.IsLetter(password[0]) && password.Any(c => char.IsLetter(c)) && password.Any(c => char.IsDigit(c));
            }
            if (fname == "" || type == "" || manager == "" || username == "" || password == "")
            {
                MessageBox.Show("Please fill in all the fields");
            }
            else if (goodpassword)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("A valid password needs to have at least six characters with both letters and numbers.");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
