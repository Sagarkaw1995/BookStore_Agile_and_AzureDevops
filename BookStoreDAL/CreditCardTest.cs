using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookStoreDAL
{
     [TestClass]
     public class CreditCardTest
     {
          [TestMethod]
          public void TestMethod_add_card()
          {
               var cc = new DALCreditCard();

               Assert.IsTrue(cc.add_card(7, "4012888888881881","Test Card","Visa", DateTime.Today.ToString("dd/MM/yyyy")) > 0);
          }

          [TestMethod]
          public void TestMethod_add_card_invalid()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual(-1, cc.add_card(5, "", "", "", DateTime.Today.ToString("dd/MM/yyyy")));
          }

          [TestMethod]
          public void TestMethod_get_card()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual(1, cc.get_card(8));
          }

          [TestMethod]
          public void TestMethod_get_card_invalid()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual(-1, cc.get_card(-2));
          }


          [TestMethod]
          public void TestMethod_get_cardnumber()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual("4012888888881881", cc.get_cardnumber(1));
          }

          [TestMethod]
          public void TestMethod_get_cardnumber_invalid()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual(null, cc.get_cardnumber(-2));
          }

          [TestMethod]
          public void TestMethod_get_cardname()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual("Test Card", cc.get_cardname(1));
          }

          [TestMethod]
          public void TestMethod_get_cardname_invalid()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual(null, cc.get_cardname(-2));
          }

          [TestMethod]
          public void TestMethod_get_cardtype()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual("Visa", cc.get_cardtype(1));
          }

          [TestMethod]
          public void TestMethod_get_cardtype_invalid()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual(null, cc.get_cardtype(-2));
          }

          [TestMethod]
          public void TestMethod_get_carddate()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual("2021-08-02", cc.get_carddate(1));
          }

          [TestMethod]
          public void TestMethod_get_carddate_invalid()
          {
               var cc = new DALCreditCard();

               Assert.AreEqual(null, cc.get_carddate(-2));
          }


          [TestMethod]
          public void TestMethod_remove_card()
          {
               var cc = new DALCreditCard();
               cc.add_card(6, "4012888888881881", "Test Card", "Visa", DateTime.Today.ToString("dd/MM/yyyy"));
               Assert.AreEqual(1, cc.remove_card(cc.get_card(6)));
          }

          [TestMethod]
          public void TestMethod_remove_card_invlaid()
          {
               var cc = new DALCreditCard();
               Assert.AreEqual(1, cc.remove_card(-1));
          }
     }
}
