using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDAL
{
    public class DALUpdateOrderItemQuantity
    {
        public bool update_quantity(int orderID, string bookISBN, int quantity)
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE OrderItem SET Quantity = @Quantity WHERE OrderID = @OrderID AND ISBN = @ISBN";
                cmd.Parameters.AddWithValue("@Quantity", quantity);
                cmd.Parameters.AddWithValue("@ISBN", bookISBN);
                cmd.Parameters.AddWithValue("@OrderID", orderID);
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
