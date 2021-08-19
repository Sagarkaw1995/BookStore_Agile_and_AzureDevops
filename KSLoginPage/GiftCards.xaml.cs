using BookStoreLIB;
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
     /// Interaction logic for GiftCards.xaml
     /// </summary>
     public partial class GiftCards : Window
     {
          public GiftCards()
          {
               InitializeComponent();
          }

          private void sendButton_Click(object sender, RoutedEventArgs e)
          {
               int userid;

               double amount;
               if (int.TryParse(IDBox.Text, out userid))
               {

                    if (double.TryParse(amountBox.Text, out amount))
                    {
                         if(amount > 0)
                         {
                              UserBalance ub = new UserBalance();

                              double  results = ub.incBalance(userid, amount);

                              if (results > 0)
                              {
                                   MessageBox.Show("New Amount: " + results.ToString());
                              }
                              else
                              {
                                   MessageBox.Show("Error!");
                              }

                         }
                         else //amount < 0
                         {
                              MessageBox.Show("Amount must be postive");
                         }

                    }
                    else //invalid amnount
                    {
                         MessageBox.Show("Invlid Amount");
                    }
               }
               else //invalid userid
               {
                    MessageBox.Show("Invlid Id");
               }
               this.Close();
          }

          private void cancelButton_Click(object sender, RoutedEventArgs e)
          {
               this.Close();
          }
     }
}
