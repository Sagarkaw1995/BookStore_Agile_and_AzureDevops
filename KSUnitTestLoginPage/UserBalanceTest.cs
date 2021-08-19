using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookStoreLIB
{
     [TestClass]
     public class UserBalanceTest
     {
          [TestMethod]
          public void TestMethod_Get_balance() //get balance normal case
          {
               int userid = 8;
               var userBalance = new UserBalance();

               Assert.AreEqual(userBalance.GetBalance(userid),1.00 );


          }
          [TestMethod]
          public void TestMethod_Get_balance_null() //get balance user will null balance
          {
               int userid = 7;
               var userBalance = new UserBalance();

               Assert.AreEqual(userBalance.GetBalance(userid), -1);


          }
          [TestMethod]
          public void TestMethod_Get_balance_invalid() //get balance invalid user
          {
               int userid = -1;
               var userBalance = new UserBalance();

               Assert.AreEqual(userBalance.GetBalance(userid), -1);


          }
          [TestMethod]
          public void TestMethod_Inc_balance() //increase balance normal case
          {
               int userid = 8;
               var userBalance = new UserBalance();

               double bal = userBalance.GetBalance(userid) + 2.5;
               Assert.AreEqual(userBalance.incBalance(userid,2.5), bal);
               userBalance.decBalance(userid, 2.5);

          }
          [TestMethod]
          public void TestMethod_Inc_balance_invalid() //increase balance invalid user
          {
               int userid = -1;
               var userBalance = new UserBalance();

               Assert.AreEqual(userBalance.incBalance(userid,1.0), -1);


          }

          [TestMethod]
          public void TestMethod_Dec_balance() //decrease balance normal case
          {
               int userid = 8;
               var userBalance = new UserBalance();

               double bal = userBalance.incBalance(userid, 2.5) - 2.5;
               Assert.AreEqual(userBalance.decBalance(userid, 2.5), bal);

          }

          [TestMethod]
          public void TestMethod_Dec_balance_invalid() //decrease balance invalid user
          {
               int userid = -1;
               var userBalance = new UserBalance();

               Assert.AreEqual(userBalance.decBalance(userid, 1.0), -2);


          }

          [TestMethod]
          public void TestMethod_Dec_balance_negitive() //decrease balance normal case to negtive value
          {
               int userid = 8;
               var userBalance = new UserBalance();

               double bal = userBalance.GetBalance(userid) + 1;
               Assert.AreEqual(userBalance.decBalance(userid, bal), -1);

          }

     }
}
