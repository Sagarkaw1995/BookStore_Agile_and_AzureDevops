using System;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreDAL
{
    public class DALUserOrders
    {
        DataTable userOrders;
        public DataTable Fetch(int userID)
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                conn.Open();
                string ordersListSQL = "Select Orders.OrderID, Orders.OrderDate, SUM(OrderItemPrice.TotalPrice) AS 'TotalPrice'"
                    + " from Orders "
                    + " INNER JOIN ( "
                    + " Select OrderItem.OrderID, SUM(OrderItem.Quantity * BookData.Price) as 'TotalPrice' "
                    + "	from OrderItem"
                    + "	INNER JOIN BookData ON OrderItem.ISBN = BookData.ISBN"
                    + "	GROUP BY OrderItem.OrderID"
                    + " ) AS OrderItemPrice ON Orders.OrderID = OrderItemPrice.OrderID"
                    + " where UserID = @UserID"
                    + " GROUP BY Orders.OrderID, Orders.OrderDate"
                    + " ORDER BY Orders.OrderDate DESC";
                SqlCommand cmdSelCategory = new SqlCommand(
                    ordersListSQL,
                    conn
                );
                cmdSelCategory.Parameters.AddWithValue("@UserID", userID);
                SqlDataAdapter ordersSqlAdapter = new SqlDataAdapter(cmdSelCategory);
                DataTable userOrders = new DataTable("Orders");
                ordersSqlAdapter.Fill(userOrders);
                return userOrders;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
            return userOrders;
        }
    }
}