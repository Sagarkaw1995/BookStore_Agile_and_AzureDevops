using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace BookStoreDAL
{
    public class DALBookCatalog
    {
        SqlConnection conn;
        DataSet dsBooks;

            public DALBookCatalog()
            {
                conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            }
            public DataSet GetBookInfo()
            {
                try
                {
                    String strSQL = "Select CategoryID, Name, Description from Category";
                    SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                    SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);

                    dsBooks = new DataSet("Books");
                    daCatagory.Fill(dsBooks, "Category");            //Get category info

                String strSQL2 = "Select ISBN, CategoryID, Title," +
                    "Author, Price, Year, Edition, Publisher, Average_Rating  from BookData";
                SqlCommand cmdSelBook = new SqlCommand(strSQL2, conn);
                    SqlDataAdapter daBook = new SqlDataAdapter(cmdSelBook);
                    daBook.Fill(dsBooks, "Books");                  //Get Books info

                    DataRelation drCat_Book = new DataRelation("drCat_Book",
                    dsBooks.Tables["Category"].Columns["CategoryID"],
                    dsBooks.Tables["Books"].Columns["CategoryID"], false);

                    dsBooks.Relations.Add(drCat_Book);       //Set up the table relation

                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }

                return dsBooks;
            }

        public bool AddNewBook(string isbn, string title, string author, string price, string categoryID,
            string year, string edition, string publisher, string supplierID)
        {

            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO BookData (ISBN, CategoryID, Title, Author, Price, SupplierId, Year, Edition, Publisher) " +
                    "VALUES (@isbn,@categoryID,@title,@author,@price, @supplierID, @year, @edition, @publisher)";
                cmd.Parameters.AddWithValue("@isbn", isbn);
                cmd.Parameters.AddWithValue("@categoryID", categoryID);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@supplierID", supplierID);
                cmd.Parameters.AddWithValue("@year", year);
                cmd.Parameters.AddWithValue("@edition", edition);
                cmd.Parameters.AddWithValue("@publisher", publisher);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


        }

        public int EditBookPrice(string isbn, string price)
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE BookData SET Price = @price WHERE ISBN = @isbn";
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@isbn", isbn);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
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

        public bool CheckBookExists(string isbn)
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT EXISTS(SELECT * FROM BookData WHERE ISBN = @isbn)";
                cmd.Parameters.AddWithValue("@isbn", isbn);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    return true;
                else return false; 
            }
            catch
            {
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public DataSet GetBookCategories()
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                String strSQL = "Select CategoryID, Name from Category";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmdSelCategory);

                da.Fill(ds);
                conn.Open();
                cmdSelCategory.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


            return ds;

        }

        public DataSet GetBookSuppliers()
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            DataSet ds = new DataSet();
            try
            {
                conn.Open();
                String strSQL = "Select SupplierID, Name from Supplier";
                SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                SqlDataAdapter da = new SqlDataAdapter(cmdSelCategory);

                da.Fill(ds);
                conn.Open();
                cmdSelCategory.ExecuteNonQuery();



            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


            return ds;


        }

        public int DeleteBook(string isbn)
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM BookData WHERE ISBN = @isbn";
                cmd.Parameters.AddWithValue("@isbn", isbn);

                conn.Open();
                int result = cmd.ExecuteNonQuery();
                return result;
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

