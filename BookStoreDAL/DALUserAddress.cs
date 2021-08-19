using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace BookStoreDAL
{
    public class DALUserAddress
    {

        public int Address(int userid, string fname, string country, string hnum, string city, string pin)
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Address (UserID, FullName, Country, Housenum, City, Pincode) VALUES (@UserID,@fullname,@country,@hnum,@city,@pin)";
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@fullname", fname);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@hnum", hnum);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@pin", pin);

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

        public int checkExistAddress(int userid)
        {
            //create sql connection to connect to the database
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                //Create sql command to check if user added address before
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                //cmd.CommandText = "Select UserID from Address where UserID = " + userid + "and FullName ='" + fname + "'";
                cmd.CommandText = "Select UserID from Address where " + " UserID = @UserID";
                cmd.Parameters.AddWithValue("@UserID", userid);
                conn.Open();
                object userId = cmd.ExecuteScalar();
                if (userId != null && (int)userId > 0)
                {
                    return (int)userId;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return 0;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public int updateExistAddress(int userid, string fname, string country, string hnum, string city, string pin)
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Update Address SET FullName=@fullname, Country=@country, Housenum=@hnum, City=@city, Pincode=@pin where UserID=@UserID ";
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@fullname", fname);
                cmd.Parameters.AddWithValue("@country", country);
                cmd.Parameters.AddWithValue("@hnum", hnum);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Parameters.AddWithValue("@pin", pin);
                conn.Open();
                cmd.ExecuteNonQuery();
                return 1;
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
