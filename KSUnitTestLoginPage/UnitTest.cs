using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;


namespace BookStoreLIB
{
    [TestClass]
    public class UnitTest
    {
        UserData userData = new UserData();
        string inputName, inputPassowrd;
        BookCatalog bookcatalog = new BookCatalog();
        string inputTitle, inputMinPrice, inputMaxPrice;
        string inputISBN; 
        [TestMethod]
        public void TestMethod_Password_len() //password < 6
        {
            inputName = "jsmith";
            inputPassowrd = "a1234";
            Assert.IsFalse(userData.LogIn(inputName, inputPassowrd));
        }

        [TestMethod]
        public void TestMethod_Password_Char() //password has symbol
        {
            inputName = "jsmith";
            inputPassowrd = "js123@";
            Assert.IsFalse(userData.LogIn(inputName, inputPassowrd));
        }

        [TestMethod]
        public void TestMethod_Username_Invalid() //invalid username
        {
            inputName = "smith";
            inputPassowrd = "js1234";
            Assert.IsFalse(userData.LogIn(inputName, inputPassowrd));
        }

        [TestMethod]
        public void TestMethod_Valid() //valid password and username
        {
            inputName = "jsmith";
            inputPassowrd = "js1234";
            Assert.IsTrue(userData.LogIn(inputName, inputPassowrd));
        }

        [TestMethod]
        public void TestMethod_Signup_Valid() //valid FullName, Type, Manager, Username, Password
        {
            string fullname = "Test User";
            string type = "PA";
            string manager = "False";
            string username = "tuser";
            string password = "tu12345";
            Assert.IsTrue(userData.SignUp(fullname, type, manager, username, password));
        }
        [TestMethod]
        public void TestMethod_Signup_Invalid_EmptyField_fullname() //Empty Field
        {
            string fullname = "";
            string type = "SA";
            string manager = "False";
            string username = "tuser";
            string password = "tu12345";
            Assert.IsFalse(userData.SignUp(fullname, type, manager, username, password));
        }

        [TestMethod]
        public void TestMethod_Signup_Invalid_EmptyField_Type() //Empty Field
        {
            string fullname = "Test User";
            string type = "";
            string manager = "False";
            string username = "tuser";
            string password = "tu12345";
            Assert.IsFalse(userData.SignUp(fullname, type, manager, username, password));
        }
        [TestMethod]
        public void TestMethod_Signup_Invalid_EmptyField_Manager() //Empty Field
        {
            string fullname = "Test User";
            string type = "SA";
            string manager = "";
            string username = "tuser";
            string password = "tu12345";
            Assert.IsFalse(userData.SignUp(fullname, type, manager, username, password));
        }
        [TestMethod]
        public void TestMethod_Signup_Invalid_EmptyField_username() //Empty Field
        {
            string fullname = "Test User";
            string type = "SA";
            string manager = "False";
            string username = "";
            string password = "tu12345";
            Assert.IsFalse(userData.SignUp(fullname, type, manager, username, password));
        }
        [TestMethod]
        public void TestMethod_Signup_Invalid_EmptyField_Password() //Empty Field
        {
            string fullname = "Test User";
            string type = "SA";
            string manager = "False";
            string username = "tuser";
            string password = "";
            Assert.IsFalse(userData.SignUp(fullname, type, manager, username, password));
        }
        [TestMethod]
        public void TestMethod_Signup_Invalid_IncorrectPassword() //Empty Field
        {
            string fullname = "Test User";
            string type = "SA";
            string manager = "True";
            string username = "tuser";
            string password = "12345";
            Assert.IsFalse(userData.SignUp(fullname, type, manager, username, password));
        }
        [TestMethod]
        public void TestMethod_Signup_CorrectPassword() //Empty Field
        {
            string fullname = "Test User";
            string type = "RG";
            string manager = "True";
            string username = "tuser";
            string password = "tu123456";
            Assert.IsTrue(userData.SignUp(fullname, type, manager, username, password));
        }
        [TestMethod]
        public void TestMethod_Signup_DuplicateUsername() //Empty Field
        {
            string username = "skaw";
            Assert.IsTrue(userData.checkUsername(username));
        }
        [TestMethod]
        public void TestMethod_Signup_UniqueUsername() //Empty Field
        {
            string username = "tuser100";
            Assert.IsFalse(userData.checkUsername(username));
        }

        [TestMethod]
        public void TestMethod_FetchOrders_ForCorrectUserID()
        {
            int userID = 2;
            UserOrderList userOrdersInstance = new UserOrderList();
            foreach (DataRow row in userOrdersInstance.FetchOrders(userID).Rows)
            {
                Assert.AreNotEqual(row["OrderID"], null);
            }
        }

        [TestMethod]
        public void TestMethod_FetchOrders_ForInCorrectUserID()
        {
            int userID = -1;
            UserOrderList userOrdersInstance = new UserOrderList();
            DataTable userOrders = userOrdersInstance.FetchOrders(userID);
            foreach (DataRow row in userOrdersInstance.FetchOrders(userID).Rows)
            {
                // unreachable code - since FetchOrders returns an empty DataTable for incorrect userID
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestMethod_FetchOrderBooks_ForCorrectOrderID()
        {
            int orderID = 1;
            UserOrderList userOrdersInstance = new UserOrderList();
            foreach (DataRow row in userOrdersInstance.FetchBooks(orderID).Rows)
            {
                Assert.IsTrue(double.Parse(row["unitPirce"].ToString()) > 0);

            }
        }

        [TestMethod]
        public void TestMethod_FetchOrderBooks_ForInCorrectOrderID()
        {
            int orderID = -1;
            UserOrderList userOrdersInstance = new UserOrderList();
            foreach (DataRow row in userOrdersInstance.FetchBooks(orderID).Rows)
            {
                // unreachable code - since FetchOrders returns an empty DataTable for incorrect userID
                Assert.IsTrue(false);
            }
        }
        [TestMethod]
        public void SearchTitle_Valid()
        {
            inputTitle = "Microsoft";
            Boolean expectedoutput = true;

            DataSet ds;
            DataTable dt;
            ds = bookcatalog.GetBookInfoWithTitle(inputTitle);
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
            ds = bookcatalog.GetBookInfoWithTitle(inputTitle);
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
            ds = bookcatalog.GetBookInfoWithPrice(inputMinPrice, inputMaxPrice);
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
            ds = bookcatalog.GetBookInfoWithPrice(inputMinPrice, inputMaxPrice);
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
            ds = bookcatalog.GetBookInfoWithTitlePrice(inputTitle, inputMinPrice, inputMaxPrice);
            Boolean actualoutput;
            dt = ds.Tables["Books"];
            DataRow[] rows = dt.Select();

            if (20 <= Convert.ToDouble(rows[0]["Price"].ToString()) && Convert.ToDouble(rows[0]["Price"].ToString()) <= 50 && rows[0]["Title"].ToString() == "Extreme Programming Explained: Embrace Change")
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
            ds = bookcatalog.GetBookInfoWithPrice(inputMinPrice, inputMaxPrice);
            Boolean actualoutput;
            dt = ds.Tables["Books"];

            if (dt.Rows.Count > 0)
                actualoutput = true;
            else
                actualoutput = false;

            Assert.AreEqual(expectedoutput, actualoutput);
        }

        [TestMethod]
        public void TestMethod_ChangePassword_emptypasswordfield() //Empty Field
        {
            string username = "Test User";
            string password = "";
            Assert.IsFalse(userData.UpdatePassword(username, password));
        }
        [TestMethod]
        public void TestMethod_ChangePassword_emptyusernamefield() //Empty Field
        {
            string username = "";
            string password = "tu123456";
            Assert.IsFalse(userData.UpdatePassword(username, password));
        }
        [TestMethod]
        public void TestMethod_ChangePassword_incorrectpasswordwithspecialcharacter() //Empty Field
        {
            string username = "Test User";
            string password = "tu@123";
            Assert.IsFalse(userData.UpdatePassword(username, password));
        }
        [TestMethod]
        public void TestMethod_ChangePassword_passwordlengthlessthan6() //Empty Field
        {
            string username = "Test User";
            string password = "tu12";
            Assert.IsFalse(userData.UpdatePassword(username, password));
        }
        [TestMethod]
        public void TestMethod_ChangePassword_passworddoesntstartswithalphabet() //Empty Field
        {
            string username = "Test User";
            string password = "12tu";
            Assert.IsFalse(userData.UpdatePassword(username, password));
        }
        [TestMethod]
        public void TestMethod_ChangePassword_correctpassword() //Empty Field
        {
            string username = "Test User";
            string password = "tu1234";
            Assert.IsTrue(userData.UpdatePassword(username, password));
        }
        [TestMethod]
        public void ExistingRating()
        {
            int inputUserId = 1;
            string inputISBN = "1852333944";
            BookRate bookrate = new BookRate();
            Assert.IsTrue(bookrate.CheckExistingRating(inputISBN, inputUserId));
        }
        [TestMethod]
        public void NotExistingRating()
        {
            int inputUserId = 1;
            string inputISBN = "1852333944NN";

            BookRate bookrate = new BookRate();
            Assert.IsFalse(bookrate.CheckExistingRating(inputISBN, inputUserId));
        }
        [TestMethod]
        public void ValidRating()
        {
            int inputUserId = 9;
            string inputISBN = "testisbn";
            Byte inputrating = 5;

            BookRate bookrate = new BookRate();
            Assert.IsTrue(bookrate.InsertRating(inputISBN, inputUserId, inputrating));
            Assert.IsTrue(bookrate.RemoveRating(inputISBN, inputUserId));

        }
        [TestMethod]
        public void InValidRating()
        {
            int inputUserId = 1;
            string inputISBN = "1554683831";
            Byte inputrating = 5;

            BookRate bookrate = new BookRate();
            Assert.IsFalse(bookrate.InsertRating(inputISBN, inputUserId, inputrating));

        }

        [TestMethod]
        public void AddNewBook_Duplicate()
        {
            string isbn = "0135974445";
            string title = "Agile Software Development, Principles, Patterns, and Practices";
            string author = "Robert C. Martin";
            string price = "70.40";
            string category = "2";
            string year = "2002";
            string edition = "1";
            string publisher = "Pearson";
            string supplierID = "1";

            BookCatalog bookCatalog = new BookCatalog();
            //if book isbn already exists in DB adding new book fails 
            Assert.IsFalse(bookCatalog.AddNewBook(isbn, title, author, price, category, year, edition, publisher, supplierID));

        }

        [TestMethod]
        public void AddNewBook_Valid()
        {
            BookCatalog bookCatalog = new BookCatalog();
            inputISBN = "0123456789";
            string title = "A Good Programming Book";
            string author = "Aisha Badi";
            string price = "60.50";
            string category = "1";
            string year = "2018";
            string edition = "1";
            string publisher = "Pearson";
            string supplierID = "1";
             
           //verify that book was added to DB 
           Assert.IsTrue(bookCatalog.AddNewBook(inputISBN, title, author, price, category, year, edition, publisher, supplierID));
         
        }

        [TestMethod]
        public void EditBookPrice_ValidBook()
        {
            string isbn = "0135974445";
            string newPrice = "89.90";
            BookCatalog bookCatalog = new BookCatalog();
            Assert.IsTrue(bookCatalog.EditBookPrice(isbn, newPrice));

        }

        [TestMethod]
        public void EditBookPrice_InvalidBook()
        {
            string isbn = "0000000000";
            string newPrice = "22.5";
            BookCatalog bookCatalog = new BookCatalog();
            Assert.IsFalse(bookCatalog.EditBookPrice(isbn, newPrice));

        }

        [TestMethod]
        public void DeleteBook_InvalidBook()
        {
            string isbn = "010101010100";
            BookCatalog bookCatalog = new BookCatalog();
            Assert.IsFalse(bookCatalog.DeleteBook(isbn));

        }

        [TestMethod]
        public void GetBookCategories_SuccessfulRetrieval()
        {
            DataSet ds;
            DataTable dt;
            ds = bookcatalog.GetBookCategories();
            bool actualoutput;
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
                actualoutput = true;
            else
                actualoutput = false;

            Assert.IsTrue(actualoutput);
           
        }

        [TestMethod]
        public void GetBookSuppliers_SuccessfulRetrieval()
        {
            DataSet ds;
            DataTable dt;
            ds = bookcatalog.GetBookSuppliers();
            bool actualoutput;
            dt = ds.Tables[0];

            if (dt.Rows.Count > 0)
                actualoutput = true;
            else
                actualoutput = false;

            Assert.IsTrue(actualoutput);

        }

        [TestCleanup]
        public void DeleteTestBookEntry()
        {
            BookCatalog bookCatalog = new BookCatalog();
            bookcatalog.DeleteBook(inputISBN);
        }

        [TestMethod]
        public void TestMethod_AddAddress() //Empty Field
        {
            UserAddress ua = new UserAddress();
            string fname = "Barun";
            string Country = "Canada";
            string housenum = "205A";
            string city = "Windsor";
            string pincode = "3964OA";
            Assert.IsFalse(ua.AddAddress(1,fname, Country, housenum, city, pincode));
        }
        [TestMethod]
        public void TestMethod_AddAddress_EmptyName() //Empty Field
        {
            UserAddress ua = new UserAddress();
            string fname = "";
            string Country = "Canada";
            string housenum = "205A";
            string city = "Windsor";
            string pincode = "3964OA";
            Assert.IsFalse(ua.AddAddress(1, fname, Country, housenum, city, pincode));
        }
        [TestMethod]
        public void TestMethod_AddAddress_Emptycity() //Empty Field
        {
            UserAddress ua = new UserAddress();
            string fname = "Varun";
            string Country = "Canada";
            string housenum = "205A";
            string city = "";
            string pincode = "3964OA";
            Assert.IsFalse(ua.AddAddress(1, fname, Country, housenum, city, pincode));
        }

        [TestMethod]
        public void TestMethod_UpdateAddress() //Empty Field
        {
            UserAddress ua = new UserAddress();
            string fname = "Barun";
            string Country = "Canada";
            string housenum = "205A";
            string city = "Windsor";
            string pincode = "3964OA";
            Assert.IsTrue(ua.updateExistingAddress(1, fname, Country, housenum, city, pincode));
        }

        [TestMethod]
        public void TestMethod_UpdateOrderBookQuantityInvalid() //Invalid ISBN
        {
            UserOrderList book_returner = new UserOrderList();
            string isbn = "";
            int orderId = 1;
            int quantity = 1;
            Assert.IsFalse(book_returner.returnBook(orderId, isbn, quantity));
        }

        [TestMethod]
        public void TestMethod_UpdateOrderBookQuantityInvalid2() //Invalid orderID
        {
            UserOrderList book_returner = new UserOrderList();
            string isbn = "161729232X";
            int orderId = -1;
            int quantity = 1;
            Assert.IsFalse(book_returner.returnBook(orderId, isbn, quantity));
        }

        [TestMethod]
        public void TestMethod_UpdateOrderBookQuantityInvalid3() //Invalid quantity
        {
            UserOrderList book_returner = new UserOrderList();
            string isbn = "161729232X";
            int orderId = 12;
            int quantity = -1;
            Assert.IsFalse(book_returner.returnBook(orderId, isbn, quantity));
        }

        [TestMethod]
        public void TestMethod_UpdateOrderBookQuantity()
        {
            UserOrderList book_returner = new UserOrderList();
            string isbn = "161729232X";
            int orderId = 12;
            int quantity = 1;
            Assert.IsTrue(book_returner.returnBook(orderId, isbn, quantity));
        }
    }
}
