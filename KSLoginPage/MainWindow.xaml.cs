/* ************************************************************
 * For students to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * ************************************************************/
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
using System.Data;
using BookStoreLIB;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Net.Mail;

namespace BookStoreGUI
{
    /// Interaction logic for MainWindow.xaml
    public partial class MainWindow : Window
    {
        DataSet dsBookCat;
        UserData userData;
        BookOrder bookOrder;

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog dlg = new LoginDialog();
            dlg.Owner = this;
            dlg.ShowDialog();

            // Process data entered by user if dialog box is accepted
            if (dlg.DialogResult == true)
            {
                if (userData.LogIn(dlg.nameTextBox.Text, dlg.passwordTextBox.Password) == true)
                {

                    if (userData.IsManager)
                    {
                        AdminWindow adminWin = new AdminWindow();
                        adminWin.adminStatusTextBlock.Text = "Welcome user #" +
                        userData.UserID;
                        adminWin.Show();
                        Close();

                    }
                    else
                        this.statusTextBlock.Text = "You are logged in as User #" +
                        userData.UserID;
                }
                else
                    this.statusTextBlock.Text = "Your login failed. Please try again.";
            }
        }
        private void exitButton_Click(object sender, RoutedEventArgs e) { this.Close(); }
        public MainWindow() { InitializeComponent(); }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            BookCatalog bookCat = new BookCatalog();
            dsBookCat = bookCat.GetBookInfo();
            // fill the combobox with Category
            this.DataContext = dsBookCat.Tables["Category"];

            bookOrder = new BookOrder();
            userData = new UserData();
                        
            this.orderListView.ItemsSource = bookOrder.OrderItemList;
        }


        private void addButton_Click(object sender, RoutedEventArgs e)
        {   if (this.ProductsDataGrid.SelectedItem == null)
                MessageBox.Show("You didn't select any book to add to the Shopping Cart.Please select the desired book and try again.");
            else
            {
                OrderItemDialog orderItemDialog = new OrderItemDialog();
                DataRowView selectedRow;
                selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                orderItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
                orderItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
                orderItemDialog.priceTextBox.Text = selectedRow.Row.ItemArray[4].ToString();
                orderItemDialog.Owner = this;
                orderItemDialog.ShowDialog();
                if (orderItemDialog.DialogResult == true)
                {
                    string isbn = orderItemDialog.isbnTextBox.Text;
                    string title = orderItemDialog.titleTextBox.Text;
                    double unitPrice = double.Parse(orderItemDialog.priceTextBox.Text);
                    var quan = orderItemDialog.quantityTextBox.Text;
                    if ((quan == "") || (!quan.All(c => char.IsDigit(c))))
                    {
                        MessageBox.Show("Please enter integer value greater than 0 for Book quantity");
                    }
                    else
                    {
                        int quantity = int.Parse(orderItemDialog.quantityTextBox.Text);
                        if (quantity < 1)
                        {
                            MessageBox.Show("Minimum quantity needs to be greater than 1.");
                        }
                        else
                        {
                            bookOrder.AddItem(new OrderItem(isbn, title, unitPrice, quantity));

                        }
                    }
                }
            }
        }
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.orderListView.SelectedItem != null)
            {
                var selectedOrderItem = this.orderListView.SelectedItem as OrderItem;
                bookOrder.RemoveItem(selectedOrderItem.BookID);
            }
        }
        private void editButton_Click(object sender, RoutedEventArgs e)
        {
            EditQuantityDialog editQuantityDialog = new EditQuantityDialog();
            if (this.orderListView.SelectedItem != null)
            {
                var selectedOrderItem = this.orderListView.SelectedItem as OrderItem;
                editQuantityDialog.isbnValue.Text = selectedOrderItem.BookID;
                editQuantityDialog.titleValue.Text = selectedOrderItem.BookTitle;
                double unitPrice1 = selectedOrderItem.UnitPrice;
                editQuantityDialog.priceValue.Text = unitPrice1.ToString();
                editQuantityDialog.Owner = this;
                editQuantityDialog.ShowDialog();
                if (editQuantityDialog.DialogResult == true)
                {
                    string isbn = editQuantityDialog.isbnValue.Text;
                    string title = editQuantityDialog.titleValue.Text;
                    double unitPrice = double.Parse(editQuantityDialog.priceValue.Text);
                    var inputQuantity = editQuantityDialog.quantityTextBox.Text;
                    if ((inputQuantity == "") || (!inputQuantity.All(c => char.IsDigit(c))))
                    {
                        MessageBox.Show("Please enter integer value greater than 0 for Book quantity");
                    }
                    else
                    {
                        int quantity = int.Parse(editQuantityDialog.quantityTextBox.Text);
                        if (quantity < 1)
                        {
                            MessageBox.Show("Minimum quantity needs to be greater than 1.");
                        }
                        else
                        {
                            bookOrder.UpdateQuantity(new OrderItem(isbn, title, unitPrice, quantity));
                        }
                    }

                }

            }
        }
        private void contactUsButton_Click(object sender, RoutedEventArgs e)
        {
            ContactUsDialog dlg = new ContactUsDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                if (dlg.nameTextBox.Text != "" && dlg.emailTextBox.Text != "" && dlg.subjectTextBox.Text != "" && dlg.messageTextBox.Text != "")
                {
                    if (IsValidEmail(dlg.emailTextBox.Text) == true)
                    {
                        if (dlg.nameTextBox.Text.All(Char.IsLetter))
                        {
                            sendMessage(dlg);
                        }
                        else
                        {
                            MessageBox.Show("Please enter name containing only alphabets.");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Please enter valid email address.");
                    }

                }
                else
                {
                    MessageBox.Show("Please fill in all the slots.");
                }
            }
        }
        bool IsValidEmail(string emailId)
        {
            try
            {
                var email = new System.Net.Mail.MailAddress(emailId);
                return email.Address == emailId;
            }
            catch
            {
                return false;
            }
        }
        private void sendMessage(ContactUsDialog contactUsDialog)
        {
            try
            {
                MailMessage Msg = new MailMessage();
                Msg.From = new MailAddress(contactUsDialog.emailTextBox.Text);
                Msg.To.Add("bookstorecontactus2021@gmail.com");
                Msg.Subject = contactUsDialog.subjectTextBox.Text;
                Msg.Body = "Name : " + contactUsDialog.nameTextBox.Text + " , " + "From Email Id : " + Msg.From + " , " + "Message : " + contactUsDialog.messageTextBox.Text;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 25;
                smtp.Credentials = new System.Net.NetworkCredential("bookstorecontactus2021@gmail.com", "bookstore@123");
                smtp.EnableSsl = true;
                smtp.Send(Msg);
                MessageBox.Show("Thank you for contacting us. We will get back to you soon.");
                contactUsDialog.nameTextBox.Text = "";
                contactUsDialog.emailTextBox.Text = "";
                contactUsDialog.subjectTextBox.Text = "";
                contactUsDialog.messageTextBox.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught.", ex);
            }

        }
        private void RateBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.ProductsDataGrid.SelectedItem == null)
                MessageBox.Show("You didn't select any book. Please select the desired book and try again");
            else
            {
                if (userData.LoggedIn)
                {
                    RateItemDialog rateItemDialog = new RateItemDialog();
                    DataRowView selectedRow;
                    selectedRow = (DataRowView)this.ProductsDataGrid.SelectedItems[0];
                    rateItemDialog.isbnTextBox.Text = selectedRow.Row.ItemArray[0].ToString();
                    rateItemDialog.titleTextBox.Text = selectedRow.Row.ItemArray[2].ToString();
                    rateItemDialog.authorTextBox.Text = selectedRow.Row.ItemArray[3].ToString();
                    rateItemDialog.Owner = this;
                    rateItemDialog.ShowDialog();
                    if (rateItemDialog.DialogResult == true)
                    {
                        string isbn = rateItemDialog.isbnTextBox.Text;
                        string title = rateItemDialog.titleTextBox.Text;
                        string author = rateItemDialog.authorTextBox.Text;
                        Byte rating = Byte.Parse(rateItemDialog.ComboBox1.Text);
                        BookRate bookRate = new BookRate();

                        if (bookRate.CheckExistingRating(isbn, userData.UserID))
                        {
                            Byte CheckedRating = bookRate.Rating;
                            MessageBox.Show("You cannot rate this book again. You gave this book " + CheckedRating + "-stars review before.");
                        }
                        else
                        {
                            if (bookRate.InsertRating(isbn, userData.UserID, rating))
                            {
                                MessageBox.Show("Thank you very much for your " + rating + "-star review! ");
                                Window_Loaded(sender, e);
                            }
                            else
                                MessageBox.Show("Failed to Add your rating");


                        }
                    }
                }
                else
                    MessageBox.Show("Please Login to rate the selected book");

            }
        }
        private void chechoutButton_Click(object sender, RoutedEventArgs e)
        {
                 if (userData.UserID < 1 || bookOrder.OrderItemList.Count == 0)
            {
                MessageBox.Show("Please sign in and select book(s) before placing the order");
            }
            else
            {


                    ObservableCollection<OrderItem> orderitems = bookOrder.OrderItemList;
                    double total = bookOrder.GetOrderTotal();
                    int orderId;
                    //orderId = bookOrder.PlaceOrder(userData.UserID);
                    Checkout checkout = new Checkout(orderitems,bookOrder, userData.UserID);

                    checkout.userid.Text = "UserID: " + userData.UserID.ToString();
                    checkout.toatlprice.Text = "Total: " + total.ToString();
                    checkout.date.Text = DateTime.UtcNow.ToString("MM-dd-yyyy");
                    checkout.orderListView.ItemsSource = orderitems;

                    checkout.ShowDialog();
               }
        }

        private void signupButton_Click(object sender, RoutedEventArgs e)
        {
            SignUpDialog dlg = new SignUpDialog();
            dlg.Owner = this;
            dlg.ShowDialog();
            if (dlg.DialogResult == true)
            {
                if (userData.checkUsername(dlg.nameTextBox.Text) == true)
                {
                    MessageBox.Show("Sign up Failed. Username already exists in database. Please try a different username.");
                }
                else if (userData.SignUp(dlg.nameFullName.Text, dlg.nameType.Text, dlg.nameManager.Text, dlg.nameTextBox.Text, dlg.passwordTextBox.Password) == true)
                {
                    MessageBox.Show("You have successfully been registered.");
                }
                else
                {
                    MessageBox.Show("Registration Failed.");
                }
            }
        }

        private void logOut_Click(object sender, RoutedEventArgs e)
        {
            var mv = new MainWindow();
            mv.Show();
            MessageBox.Show("LoggedOut Successfully");
            this.Close();
        }

        private void orderHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (userData.UserID < 1)
            {
                MessageBox.Show("Please sign in to view your past orders");
            }
            else
            {
                OrderHistory order_history = new OrderHistory();
                order_history.UserID = userData.UserID;
                order_history.ShowDialog();
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            BookCatalog bookCat = new BookCatalog();
            string title = BookTitleTextBox.Text;
            string minPrice = MinPriceTextBox.Text;
            string maxPrice = MaxPriceTextBox.Text;


            if (minPrice == "" && maxPrice == "" && title == "")
                return;
            if (title == "" && minPrice != "" && maxPrice != "")
            {
                if (Convert.ToDouble(minPrice) > Convert.ToDouble(maxPrice))
                    MessageBox.Show("Minimum price should be less than maximum price");
                else
                    dsBookCat = bookCat.GetBookInfoWithPrice(minPrice, maxPrice);
            }
            else if (title != "" && minPrice == "" && maxPrice == "")
                dsBookCat = bookCat.GetBookInfoWithTitle(title);
            else
            {
                if (minPrice != "" && maxPrice != "")
                {
                    if (Convert.ToDouble(minPrice) > Convert.ToDouble(maxPrice))
                        MessageBox.Show("Minimum price should be less than maximum price");
                    else
                        dsBookCat = bookCat.GetBookInfoWithTitlePrice(title, minPrice, maxPrice);
                }
                else
                    dsBookCat = bookCat.GetBookInfoWithTitlePrice(title, minPrice, maxPrice);
            }
            this.DataContext = dsBookCat.Tables["Category"];
        }

        private void clearallButton_Click(object sender, RoutedEventArgs e)
        {
            BookTitleTextBox.Clear();
            MinPriceTextBox.Clear();
            MaxPriceTextBox.Clear();
            Remove_Filters(sender, e);
        }

        private void NumbericInputValidation(object sender, TextCompositionEventArgs e)
        {

            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);

        }

        private void Remove_Filters(object sender, RoutedEventArgs e)
        {
            BookCatalog bookCat = new BookCatalog();
            dsBookCat = bookCat.GetBookInfo();
            // fill the combobox with Category
            this.DataContext = dsBookCat.Tables["Category"];

            this.orderListView.ItemsSource = bookOrder.OrderItemList;
        }

        private void categoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //categoryChanger(categoriesComboBox.SelectedIndex);

        }

          private void giftcardButton_click(object sender, RoutedEventArgs e)
          {
               GiftCards giftcard = new GiftCards();
               giftcard.ShowDialog();
          }

          private void creditCardButton_Click(object sender, RoutedEventArgs e)
          {
               if (userData.UserID < 1)
               {
                    MessageBox.Show("Please sign in to edit credit cards");
               }
               else
               {
                    CreditCardWindow creditcard = new CreditCardWindow(userData.UserID);
                    creditcard.ShowDialog();

               }

          }
     }
}
