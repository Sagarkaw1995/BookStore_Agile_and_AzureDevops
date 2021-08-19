/* ************************************************************
 * For students to work on assignments and project.
 * Permission required material. Contact: xyuan@uwindsor.ca 
 * ************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BookStoreDAL;


namespace BookStoreLIB
{
    public class BookCatalog
    {

        const string DEFAULT_MIN = "0";
        const string DEFAULT_MAX = "1000";
        public DataSet GetBookInfo()
        {
            
            DALBookCatalog bookCatalog = new DALBookCatalog();
            return bookCatalog.GetBookInfo();
        }
        public DataSet GetBookInfoWithTitle(string keyword)
        {    string Title = keyword;
            
            DALBookFilter bookTitle = new DALBookFilter();
            return bookTitle.GetBookInfoWithTitle(Title);
        }

        
        public DataSet GetBookInfoWithPrice(string min, string max)
        {
           DALBookFilter priceFilter = new DALBookFilter();
            if (min == "")
                min = DEFAULT_MIN; //set default min price if none set
            if (max == "")
                max = DEFAULT_MAX; //set default max price if none set 

            return priceFilter.GetBookInfoWithPrice(min, max);
        }

        public DataSet GetBookInfoWithTitlePrice(string title, string min, string max)
        {
           
            DALBookFilter bookFitler = new DALBookFilter();
            if (min == "")
                min = DEFAULT_MIN; //set default min price if none set
            if (max == "")
                max = DEFAULT_MAX; //set default max price if none set 

            return bookFitler.GetBookInfoWithTitlePrice(title, min, max);
        }

        public bool AddNewBook(string isbn, string title, string author, string price, string category,
           string year, string edition, string publisher, string supplierID)
        {
            DALBookCatalog dalBookCatalog = new DALBookCatalog();
            bool result = false; 
            if (dalBookCatalog.CheckBookExists(isbn))
               result = false; 
           else result = dalBookCatalog.AddNewBook(isbn, title, author, price, category, year, edition, publisher, supplierID);

            return result;

        }

        public bool EditBookPrice(string isbn, string price)
        {
            DALBookCatalog dalBookCatalog = new DALBookCatalog();
            int result = dalBookCatalog.EditBookPrice(isbn, price);
            if (result != 1)
                return false;
            else
                return true;

        }

        public DataSet GetBookCategories()
        {
            DALBookCatalog bookCatalog = new DALBookCatalog();
            return bookCatalog.GetBookCategories();
        }

        public DataSet GetBookSuppliers()
        {
            DALBookCatalog bookCatalog = new DALBookCatalog();
            return bookCatalog.GetBookSuppliers();
        }

        public bool DeleteBook(string isbn)
        {
            DALBookCatalog dalBookCatalog = new DALBookCatalog();
            int result = dalBookCatalog.DeleteBook(isbn);
            if (result == 1)
                return true;
            return false;

        }
    }
}
