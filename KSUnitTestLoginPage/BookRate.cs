using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data;
using BookStoreDAL;


namespace BookStoreLIB
{
    public class BookRate
    {
        public Byte Rating { set; get; }

        DALBookRate dalbookrate;
        public Boolean CheckExistingRating(string isbn, int userid)
        {
            dalbookrate = new DALBookRate();
            Rating = dalbookrate.CheckExistRating(isbn, userid);
            if (Rating > 0)
                return true;
            else
                return false;


        }
        public Boolean InsertRating(string isbn, int userid, Byte rating)
        {  
            dalbookrate = new DALBookRate();
            if (dalbookrate.InsertRating(isbn, userid, rating) == 1)
                return true;
            else          
               return false;
        }
        public Boolean RemoveRating(string isbn, int userid)
        {
            dalbookrate = new DALBookRate();
            if (dalbookrate.RemoveTestRating(isbn, userid))
                return true;
            else
                return false;
        }
    }
}
  
