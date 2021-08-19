using System.Windows;
using System.Data;
using BookStoreLIB;


namespace BookStoreGUI
{
    /// <summary>
    /// GUI class to handle book return in an order
    /// </summary>
    public partial class OrderBookReturn : Window
    {
        public int OrderID { set; get; }
        public int UserID { set; get; }
        public OrderBookReturn()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // load the books present in a given OrderID
            UserOrderList userOrdersIntance = new UserOrderList();
            this.OrderBooksTable.ItemsSource = userOrdersIntance.FetchBooks(OrderID).DefaultView;
        }

        private void refundButton_Click(object sender, RoutedEventArgs e)
        {   
            // check if the return requested has valid quantities for each book
            if(isValidBookReturn())
            {
                double total_return_price = 0.0;
                UserOrderList order_books = new UserOrderList();
                foreach (DataRowView row in this.OrderBooksTable.Items)
                {
                    int return_quantity = int.Parse(row["returnQuantity"].ToString());
                    double total_book_price = double.Parse(row["perBookPrice"].ToString());
                    total_return_price += return_quantity * total_book_price;
                    if (return_quantity > 0)
                    {
                        int remaining_quantity = int.Parse(row["Quantity"].ToString()) - return_quantity;
                        // update returned quantity in the database
                        order_books.returnBook(OrderID, row["ISBN"].ToString(), remaining_quantity);

                    }
                }

                // increment user balance
                UserBalance userBalance = new UserBalance();
                userBalance.incBalance(UserID, total_return_price);
                MessageBox.Show("A total of " + total_return_price.ToString() + " has been refunded to your Account Wallet.");
                // close to avoid database inconsistency
                this.Close();
            }
        }
        private void returnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool isValidBookReturn()
        {
            bool valid_return = true;
            foreach (DataRowView row in this.OrderBooksTable.Items)
            {
                int return_quantity = int.Parse(row["returnQuantity"].ToString());
                int available_quantity = int.Parse(row["Quantity"].ToString());
                if (return_quantity < 0 || return_quantity > available_quantity)
                {
                    valid_return = false;
                    MessageBox.Show("Quantity to return for " + row["Title"].ToString() + " must be between 0 and " + row["Quantity"].ToString());
                }
            }
            return valid_return;
        }
    }
}
