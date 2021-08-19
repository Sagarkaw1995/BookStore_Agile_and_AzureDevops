using BookStoreLIB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
     /// Interaction logic for Checkout.xaml
     /// </summary>
     public partial class Checkout : Window
     {
          ObservableCollection<OrderItem> orderitems;
          double total;
          int orderId;
          int userId;
          BookOrder bookOrder;
          UserAddress userAds;
          UserData userData;

        public Checkout(ObservableCollection<OrderItem> items, BookOrder books, int id)
          {
               InitializeComponent();
               orderitems = items;
               bookOrder = books;
               total = bookOrder.GetOrderTotal();
               userId = id;
          }

          private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
          {

          }



          private void PlaceOrderClick(object sender, RoutedEventArgs e)
          {

               orderId = bookOrder.PlaceOrder(userId);
               OrderInvoice orderinvoice = new OrderInvoice();
               orderinvoice.orderid.Text = "OrderID: " + orderId.ToString();
               orderinvoice.userid.Text = "UserID: " + userId.ToString();
               orderinvoice.toatlprice.Text = "Total: " + total.ToString();
               orderinvoice.date.Text = DateTime.UtcNow.ToString("MM-dd-yyyy");
               orderinvoice.orderListView.ItemsSource = orderitems;

            orderinvoice.Show();
            this.Close();

          }

        private void addAddressbutton_click(object sender, RoutedEventArgs e)
        {
            addAddressDialog dlg = new addAddressDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                userAds = new UserAddress();
                if (userAds.checkExistingAddress(userId))
                {
                    int CheckedAddress = userAds.checkAds;
                    MessageBox.Show("Your address already exists in database. Please update Address if it needs to be changed.");
                }
                else
                {
                    if (userAds.AddAddress(userId, dlg.nameFullName.Text, dlg.country.Text, dlg.housenumTextBox.Text, dlg.cityTextBox.Text, dlg.pincodeTextBox.Text) == true)
                    {
                        MessageBox.Show("You have successfully Added Address.");
                    }
                    else
                    {
                        MessageBox.Show("Registration Failed.");
                    }
                }
            }

        
    }

        private void updateAddressButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateAddress ua = new UpdateAddress();
            userAds = new UserAddress();
            ua.Owner = this;
            ua.ShowDialog();
            if (!userAds.checkExistingAddress(userId))
            {
                MessageBox.Show("No address exists in database. Please click on add Address.");
            }
            else
            {
                if (ua.DialogResult == true && userAds.updateExistingAddress(userId, ua.nameFullName.Text, ua.country.Text, ua.housenumTextBox.Text, ua.cityTextBox.Text, ua.pincodeTextBox.Text) == true)
                {
                    MessageBox.Show("Address successfully Updated");
                }
                else
                {
                    MessageBox.Show("Failed to update address. Please contact Admin");
                }
            }
        }
    }
}
