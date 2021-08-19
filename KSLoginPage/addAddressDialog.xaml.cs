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
    /// Interaction logic for addAddressDialog.xaml
    /// </summary>
    public partial class addAddressDialog : Window
    {
        UserAddress userAds;
        int useid;
        public addAddressDialog()
        {
            InitializeComponent();
        }

            private void addAddressButton_Click(object sender, RoutedEventArgs e)
            {
                string fname = this.nameFullName.Text;
                string Country = this.country.Text;
                string housenum = this.housenumTextBox.Text;
                string city = this.cityTextBox.Text;
                string pincode = this.pincodeTextBox.Text;
                if (fname == "" || Country == "" || housenum == "" || city == "" || pincode == "")
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
        }
    }
