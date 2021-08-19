using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookStoreDAL
{
     [TestClass]
     public class BalanceTest
     {
          [TestMethod]
          public void TestMethod_Set_balance() //set balance normal case
          {
               int userid = 8;
               var userBalance = new DALUserBalance();

               Assert.IsTrue(userBalance.set_balance(userid,100) > 0);


          }

          [TestMethod]
          public void TestMethod_Set_balance_invalid_user() //set balance with invalid user
          {
               int userid = -1;
               var userBalance = new DALUserBalance();

               Assert.AreEqual(-1, userBalance.set_balance(userid, 100));


          }


          [TestMethod]
          public void TestMethod_Get_Balance() //get balance normal case
          {
               int userid = 8;
               var userBalance = new DALUserBalance();
               userBalance.set_balance(userid, 100);
               Assert.AreEqual(100, userBalance.get_balance(userid));


               
          }

          [TestMethod]
          public void TestMethod_Get_Balance_Null() //get the balance of a user with null dalance
          {
               int userid = 7;
               var userBalance = new DALUserBalance();
               Assert.AreEqual(-1, userBalance.get_balance(userid));



          }

          [TestMethod]
          public void TestMethod_Get_invalid_user() //get the balance of a invalid user
          {
               int userid = -1;
               var userBalance = new DALUserBalance();
               Assert.AreEqual(-1, userBalance.get_balance(userid));



          }
     }
}
