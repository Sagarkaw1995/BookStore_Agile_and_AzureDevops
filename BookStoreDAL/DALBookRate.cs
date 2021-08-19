using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace BookStoreDAL
{
    public class DALBookRate
    {

        public Byte CheckExistRating(string isbn, int userid)
        {  //creat sql connection to connect to the database
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                //Create sql command to check if user rated this book before
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "Select Rating from Ratings where UserID = " + userid + "and ISBN ='" + isbn + "'";

                conn.Open();

                //store the output  of the sql command in the variable rating
                if (cmd.ExecuteScalar() == null)
                {
                    return 0;
                }
                else
                {
                    Byte Rating = (Byte)cmd.ExecuteScalar();

                    return Rating;
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

        public int InsertRating(string isbn, int userid, Byte rating)
        {
            //creat sql connection to connect to the database
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);

            try
            {
                //Create sql command to check if user rated this book before
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Ratings (ISBN,UserID,Rating) VALUES (@ISBN,@UserID,@Rating)";
                cmd.Parameters.AddWithValue("@ISBN", isbn);
                cmd.Parameters.AddWithValue("@UserID", userid);
                cmd.Parameters.AddWithValue("@Rating", rating);
                conn.Open();
                cmd.ExecuteNonQuery();

                //Update Average Ratings in BookData
                SqlCommand cmd2 = new SqlCommand();
                cmd2.Connection = conn;
                cmd2.CommandText = "Update BookData Set BookData.Average_Rating = (Select AVG(Rating) from Ratings where BookData.ISBN = Ratings.ISBN)";
                cmd2.ExecuteNonQuery();
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
        public Boolean RemoveTestRating(string isbn, int userid)
        {
            //creat sql connection to connect to the database
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);

            try
            {
                //Create sql command to check if user rated this book before
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM Ratings WHERE ISBN = '" + isbn + "' AND UserID =" + userid;
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