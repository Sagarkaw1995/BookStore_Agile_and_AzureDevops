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
     /// Interaction logic for CreditCardWindow.xaml
     /// </summary>
     public partial class CreditCardWindow : Window
     {
          int userId;
          CreditCard cc;
          public CreditCardWindow(int id)
          {
               userId = id;
               cc = new CreditCard(userId);
               InitializeComponent();
          }
          private void addButton_Click(object sender, RoutedEventArgs e)
          {
               string cnumber = cardnumberBoc.Text;
               string cname = nameBox.Text;
               string ctype = cardType.Text;
               string cdate = dataBox.ToString();

               if(cc.new_card(cnumber, cname, ctype, cdate) > 0)
               {
                    MessageBox.Show("Card Added");
               }
               else
               {
                    MessageBox.Show("Error Card Not Added");
               }

               this.Close();
          }

          private void removeButton_Click(object sender, RoutedEventArgs e)
          {
               cc.remove_card();
               this.Close();
          }

          private void cancelButton_Click(object sender, RoutedEventArgs e)
          {
               this.Close();
          }
     }
}
