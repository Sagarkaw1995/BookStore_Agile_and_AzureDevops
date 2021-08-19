using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace BookStoreLIB
{
    [TestClass]
    public class BookstoreFilterUnitTest
    {
        DALBookFilter dalbooktitle = new DALBookFilter();
        string inputTitle, inputMinPrice, inputMaxPrice;

        [TestMethod]
            public void SearchTitle_Valid()
            {
                inputTitle = "Microsoft";
                Boolean expectedoutput = true;

                DataSet ds;
                DataTable dt;
                ds = dalbooktitle.GetBookInfoWithTitle(inputTitle);
                Boolean actualoutput;
                dt = ds.Tables["Books"];
                DataRow[] rows = dt.Select();

                if (rows[0]["Title"].ToString() == "Microsoft Visual C# 2012: An Introduction to Object-Oriented Programming")
                    actualoutput = true;
                else
                    actualoutput = false;

                Assert.AreEqual(expectedoutput, actualoutput);
            }
        [TestMethod]
        public void SearchTitle_InValid()
        {
            inputTitle = "Hello";
            Boolean expectedoutput = false;

            DataSet ds;
            DataTable dt;
            ds = dalbooktitle.GetBookInfoWithTitle(inputTitle);
            Boolean actualoutput;
            dt = ds.Tables["Books"];

            if (dt.Rows.Count > 0)
                actualoutput = true;
            else
                actualoutput = false;

            Assert.AreEqual(expectedoutput, actualoutput);
        }
        [TestMethod]
        public void SearchPrice_Valid()
        {
            inputMinPrice = "100";
            inputMaxPrice = "200";
            Boolean expectedoutput = true;

            DataSet ds;
            DataTable dt;
            ds = dalbooktitle.GetBookInfoWithPrice(inputMinPrice, inputMaxPrice);
            Boolean actualoutput;
            dt = ds.Tables["Books"];
            DataRow[] rows = dt.Select();

            if (100 <= Convert.ToDouble(rows[0]["Price"].ToString()) && Convert.ToDouble(rows[0]["Price"].ToString()) <= 200)
                actualoutput = true;
            else
                actualoutput = false;

            Assert.AreEqual(expectedoutput, actualoutput);
        }
        [TestMethod]
        public void SearchPrice_InValid()
        {
            inputMinPrice = "500";
            inputMaxPrice = "1000";
            Boolean expectedoutput = false;

            DataSet ds;
            DataTable dt;
            ds = dalbooktitle.GetBookInfoWithPrice(inputMinPrice, inputMaxPrice);
            Boolean actualoutput;
            dt = ds.Tables["Books"];

            if (dt.Rows.Count > 0)
                actualoutput = true;
            else
                actualoutput = false;

            Assert.AreEqual(expectedoutput, actualoutput);
        }

        [TestMethod]
        public void SearchTitlePrice_Valid()
        {
            inputTitle = "Extreme";
            inputMinPrice = "20";
            inputMaxPrice = "50";
            Boolean expectedoutput = true;

            DataSet ds;
            DataTable dt;
            ds = dalbooktitle.GetBookInfoWithTitlePrice(inputTitle,inputMinPrice, inputMaxPrice);
            Boolean actualoutput;
            dt = ds.Tables["Books"];
            DataRow[] rows = dt.Select();

            if (20 <= Convert.ToDouble(rows[0]["Price"].ToString()) && Convert.ToDouble(rows[0]["Price"].ToString()) <=50 && rows[0]["Title"].ToString() == "Extreme Programming Explained: Embrace Change")
                actualoutput = true;
            else
                actualoutput = false;

            Assert.AreEqual(expectedoutput, actualoutput);
        }

        [TestMethod]
        public void SearchTitlePrice_InValid()
        {
            inputTitle = "ABCDEFG";
            inputMinPrice = "1000";
            inputMaxPrice = "1500";
            Boolean expectedoutput = false;

            DataSet ds;
            DataTable dt;
            ds = dalbooktitle.GetBookInfoWithPrice(inputMinPrice, inputMaxPrice);
            Boolean actualoutput;
            dt = ds.Tables["Books"];

            if (dt.Rows.Count > 0)
                actualoutput = true;
            else
                actualoutput = false;

            Assert.AreEqual(expectedoutput, actualoutput);
        }


    }

}
