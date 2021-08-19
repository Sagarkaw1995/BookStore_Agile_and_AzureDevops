using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Data;


namespace BookStoreLIB
{
     [TestClass]
     public class UnitTest1
     {
          UserData userData = new UserData();
          string inputName, inputPassowrd;
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
    }
}
