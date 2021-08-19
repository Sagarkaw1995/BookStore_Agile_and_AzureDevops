using System.Windows;
using System.Data;
using BookStoreLIB;
using System.Collections.ObjectModel;

namespace BookStoreGUI
{
    /// <summary>
    /// Interaction logic for OrderHistory.xaml
    /// Dialog for the orders of a user
    /// </summary>
    public partial class OrderHistory : Window
    {
        public int UserID { set; get; }
        public OrderHistory()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UserOrderList userOrdersInstance = new UserOrderList();
            this.OrderHistoryTable.ItemsSource = userOrdersInstance.FetchOrders(UserID).DefaultView;
        }

        private void orderExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void returnBook_Click(object sender, RoutedEventArgs e)
        {
            OrderBookReturn book_return = new OrderBookReturn();
            DataRowView selectedRow = (DataRowView)this.OrderHistoryTable.SelectedItems[0];
            int orderId = int.Parse(selectedRow.Row.ItemArray[0].ToString());
            book_return.OrderID = orderId;
            book_return.UserID = UserID;
            book_return.ShowDialog();
        }

        private void viewOrderInvoice_Click(object sender, RoutedEventArgs e)
        {
            // move to a better class
            DataRowView selectedRow = (DataRowView)this.OrderHistoryTable.SelectedItems[0];
            int orderId = int.Parse(selectedRow.Row.ItemArray[0].ToString());

            OrderInvoice orderinvoice = new OrderInvoice();
            orderinvoice.orderid.Text = "OrderID: " + orderId.ToString();
            orderinvoice.userid.Text = "UserID: " + UserID.ToString();
            orderinvoice.toatlprice.Text = "Total: " + selectedRow.Row.ItemArray[2].ToString();
            orderinvoice.date.Text = selectedRow.Row.ItemArray[1].ToString();
            BookOrder bookOrder = new BookOrder();
            UserOrderList userOrdersIntance = new UserOrderList();
            DataTable books = userOrdersIntance.FetchBooks(orderId);
            foreach (DataRow row in books.Rows)
            {
                bookOrder.AddItem(
                    new OrderItem(row["ISBN"].ToString(),
                    row["Title"].ToString(),
                    (double.Parse(row["unitPirce"].ToString()) / int.Parse(row["Quantity"].ToString())),
                    int.Parse(row["Quantity"].ToString()))
                );
            }
            ObservableCollection<OrderItem> orderitems = bookOrder.OrderItemList;
            orderinvoice.orderListView.ItemsSource = orderitems;
            orderinvoice.ShowDialog();
        }
    }
}
