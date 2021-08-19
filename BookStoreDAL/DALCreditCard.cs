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
     public class DALCreditCard
     {

          public int get_card(int uid)
          {
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select CardID from CardData where "
                    + " UserID = @UserID";
                    cmd.Parameters.AddWithValue("@UserID", uid);
                    conn.Open();
                    int cid = (int)cmd.ExecuteScalar();
                    if (cid != null)
                    {
                         return cid;
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


          public string get_cardnumber(int cid)
          {
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select CardNumber from CardData where "
                    + " CardID = @CardID";
                    cmd.Parameters.AddWithValue("@CardID", cid);
                    conn.Open();
                    return (string)cmd.ExecuteScalar();
                    
               }
               catch (Exception ex)
               {
                    Debug.WriteLine(ex.ToString());
                    return null;
               }
               finally
               {
                    if (conn.State == ConnectionState.Open)
                         conn.Close();
               }
          }

          public string get_cardname(int cid)
          {
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select CardName from CardData where "
                    + " CardID = @CardID";
                    cmd.Parameters.AddWithValue("@CardID", cid);
                    conn.Open();
                    return (string)cmd.ExecuteScalar();

               }
               catch (Exception ex)
               {
                    Debug.WriteLine(ex.ToString());
                    return "";
               }
               finally
               {
                    if (conn.State == ConnectionState.Open)
                         conn.Close();
               }
          }

          public string get_cardtype(int cid)
          {
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select CardType from CardData where "
                    + " CardID = @CardID";
                    cmd.Parameters.AddWithValue("@CardID", cid);
                    conn.Open();
                    return (string)cmd.ExecuteScalar();

               }
               catch (Exception ex)
               {
                    Debug.WriteLine(ex.ToString());
                    return "";
               }
               finally
               {
                    if (conn.State == ConnectionState.Open)
                         conn.Close();
               }
          }

          public String get_carddate(int cid)
          {
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select CardDate from CardData where "
                    + " CardID = @CardID";
                    cmd.Parameters.AddWithValue("@CardID", cid);
                    conn.Open();
                    return (string)cmd.ExecuteScalar();

               }
               catch (Exception ex)
               {
                    Debug.WriteLine(ex.ToString());
                    return null;
               }
               finally
               {
                    if (conn.State == ConnectionState.Open)
                         conn.Close();
               }
          }


          public int add_card(int uid, string cnumber, string cname, string ctype, string cdate)
          {
               int cid = get_card(uid);
               if (cid > 0)
               {
                    remove_card(cid);
               }
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO CardData (UserID, CardNumber, CardName, CardType, CardDate) VALUES (@UserID,@CardNumber,@CardName,@CardType,@CardDate)";
                    cmd.Parameters.AddWithValue("@UserID", uid);
                    cmd.Parameters.AddWithValue("@CardNumber", cnumber);
                    cmd.Parameters.AddWithValue("@CardName", cname);
                    cmd.Parameters.AddWithValue("@CardType", ctype);
                    cmd.Parameters.AddWithValue("@CardDate", cdate);
                    if (uid < 1 || cnumber == "" || cname == "" || ctype == "" )
                    {
                         throw new Exception();
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return 1;
               }
               catch
               {
                    return -1;
               }
               finally
               {
                    if (conn.State == ConnectionState.Open)
                         conn.Close();
               }
          }

          public int remove_card(int cid)
          {
               var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
               try
               {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE from CardData where "
                    + " CardID = @CardID";
                    cmd.Parameters.AddWithValue("@CardID", cid);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return 1;
               }
               catch
               {
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
