using System;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreDAL
{
    public class DALOrderBooks
    {
            DataTable orderBooks;
            public DataTable Fetch(int orderID)
            {
                var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
                try
                {
                    conn.Open();
                    string ordersListSQL = "Select OrderItem.ISBN, BookData.Title, OrderItem.Quantity, (OrderItem.Quantity * BookData.Price) AS 'unitPirce', BookData.Price AS 'perBookPrice', OrderItem.Quantity as 'returnQuantity'"
                    + " from OrderItem"
                    + " INNER JOIN BookData ON OrderItem.ISBN = BookData.ISBN"
                    + " WHERE OrderItem.OrderID = @OrderID";
                    SqlCommand cmdSelCategory = new SqlCommand(
                        ordersListSQL,
                        conn
                    );
                    cmdSelCategory.Parameters.AddWithValue("@OrderID", orderID);
                    SqlDataAdapter ordersSqlAdapter = new SqlDataAdapter(cmdSelCategory);
                    DataTable orderBooks = new DataTable();
                    ordersSqlAdapter.Fill(orderBooks);
                    return orderBooks;
                }
                catch (Exception ex) { Debug.WriteLine(ex.Message); }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
                return orderBooks;
            }
    }
}