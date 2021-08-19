using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDAL
{
     public class DALUserBalance
     {
          //Get the balance of a user with the uid returns balance, -1 for exception
          public int get_balance(int uid)
          {
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select Balance from UserData where "
                    + " UserID = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", uid);
                    conn.Open();
                    int balance = (int)cmd.ExecuteScalar();
                    Console.WriteLine(balance);
                    if (balance != null)
                    {
                         return balance;
                    }
                    else
                    {
                         return -1;
                    }
               }
               catch (Exception ex)
               {
                    Debug.WriteLine(ex.ToString());
                    return -1;
               }
               finally
               {
                    if (conn.State == ConnectionState.Open)
                         conn.Close();
               }
          }

          //Set the balance of a user to a int amount
          public int set_balance(int uid, int balance)
          {
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);

               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select UserID from UserData where "
                        + " UserID = @UserID ";
                    cmd.Parameters.AddWithValue("@UserID", uid);
                    conn.Open();
                    object userId = cmd.ExecuteScalar();
                    if (userId != null && (int)userId > 0)
                    {
                         SqlCommand cmd2 = new SqlCommand();
                         cmd2.Connection = conn;
                         cmd2.CommandText = "UPDATE UserData SET Balance = @Balance WHERE UserID = @UserID";
                         cmd2.Parameters.AddWithValue("@Balance", balance);
                         cmd2.Parameters.AddWithValue("@UserID", uid);
                         cmd2.ExecuteNonQuery();
                         return 1;
                    }
                    else
                    {
                         return -1;
                    }
               }
               catch (Exception ex)
               {
                    Debug.WriteLine(ex.ToString());
                    return -1;
               }
               finally
               {
                    if (conn.State == ConnectionState.Open)
                         conn.Close();
               }

               
          }
     }
}
