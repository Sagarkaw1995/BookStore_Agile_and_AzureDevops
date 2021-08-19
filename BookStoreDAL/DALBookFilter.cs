using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;


namespace BookStoreDAL
{
        public class DALBookFilter
        {
            SqlConnection conn;
            DataSet dsBooks;
            public DALBookFilter()
            {
                conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            }

            public DataSet GetBookInfoWithTitle(string title)
            {
                string strSQL2 = "Select ISBN, CategoryID, Title," +
                       "Author, Price, Year, Edition, Publisher from BookData where Title lIKE '%" + title + "%'";
                SetFilteredCatDataset(strSQL2);
                return dsBooks;
            }


            public DataSet GetBookInfoWithPrice(string min, string max)
            {
                string strSQL2 = "Select ISBN, CategoryID, Title," +
                        "Author, Price, Year, Edition, Publisher from BookData where Price BETWEEN '" + min + "' AND '" + max + "'";
                SetFilteredCatDataset(strSQL2);
                return dsBooks;
            }

            public DataSet GetBookInfoWithTitlePrice(string title, string min, string max)
            {
                string strSQL2 = "Select ISBN, CategoryID, Title," +
                        "Author, Price, Year, Edition, Publisher from BookData where Title lIKE '%" + title + "%' AND Price BETWEEN '" + min + "' AND '" + max + "'";
                SetFilteredCatDataset(strSQL2);

                return dsBooks;
            }

            public void SetFilteredCatDataset(string query)
            {
                try
                {
                    string strSQL = "Select CategoryID, Name, Description from Category";
                    SqlCommand cmdSelCategory = new SqlCommand(strSQL, conn);
                    SqlDataAdapter daCatagory = new SqlDataAdapter(cmdSelCategory);
                    dsBooks = new DataSet("Books");
                    daCatagory.Fill(dsBooks, "Category");            //Get category info

                    //add category to show all books
                    /*var dr = dsBooks.Tables["Category"].NewRow();
                    dr["CategoryID"] = 0;
                    dr["Name"] = "All";
                    dr["Description"] = "N/A";
                    dsBooks.Tables["Category"].Rows.InsertAt(dr, 0);*/

                    SqlCommand cmdSelBook = new SqlCommand(query, conn);
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


            }


        }



}

