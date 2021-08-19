using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BookStoreLIB
{
     [TestClass]
     public class CreditCardTest
     {
          [TestMethod]
          public void TestMethod_add_card()
          {
               var cc = new CreditCard(7);

               Assert.IsTrue(cc.new_card( "4012888888881881", "Test Card", "Visa", DateTime.Today.ToString("dd/MM/yyyy")) > 0);
          }

          [TestMethod]
          public void TestMethod_add_card_invalid()
          {
               var cc = new CreditCard(5);

               Assert.AreEqual(-1, cc.new_card( "", "", "", DateTime.Today.ToString("dd/MM/yyyy")));
          }

          [TestMethod]
          public void TestMethod_edit_card()
          {
               var cc = new CreditCard(7);

               Assert.IsTrue(cc.edit_card("4012888888881881", "Test Card", "Visa", DateTime.Today.ToString("dd/MM/yyyy")) > 0);
          }

          [TestMethod]
          public void TestMethod_edit_card_invalid()
          {
               var cc = new CreditCard(5);

               Assert.AreEqual(-1, cc.edit_card("", "", "", DateTime.Today.ToString("dd/MM/yyyy")));
          }


          [TestMethod]
          public void TestMethod_get_card()
          {
               var cc = new CreditCard(8);

               Assert.AreEqual(1, cc.get_card_id());
          }

          [TestMethod]
          public void TestMethod_get_card_invalid()
          {
               var cc = new CreditCard(-2);

               Assert.AreEqual(-1, cc.get_card_id());
          }

          [TestMethod]
          public void TestMethod_get_cid()
          {
               var cc = new CreditCard(8);
               cc.get_card_info();
               Assert.AreEqual(1, cc.get_cid());
          }

          [TestMethod]
          public void TestMethod_get_cid_invalid()
          {
               var cc = new CreditCard(-2);
               cc.get_card_info();
               Assert.AreEqual(-1, cc.get_cid());
          }

          [TestMethod]
          public void TestMethod_get_uid()
          {
               var cc = new CreditCard(8);
               cc.get_card_info();
               Assert.AreEqual(8, cc.get_uid());
          }

          [TestMethod]
          public void TestMethod_get_uid_invalid()
          {
               var cc = new CreditCard(-2);
               cc.get_card_info();
               Assert.AreEqual(-1, cc.get_uid());
          }

          [TestMethod]
          public void TestMethod_get_cardnumber()
          {
               var cc = new CreditCard(8);
               cc.get_card_info();
               Assert.AreEqual("4012888888881881", cc.get_cardnumber());
          }

          [TestMethod]
          public void TestMethod_get_cardnumber_invalid()
          {
               var cc = new CreditCard(-2);
               cc.get_card_info();
               Assert.AreEqual("", cc.get_cardnumber());
          }
          [TestMethod]
          public void TestMethod_get_cardname()
          {
               var cc = new CreditCard(8);
               cc.get_card_info();

               Assert.AreEqual("Test Card", cc.get_cardname());
          }

          [TestMethod]
          public void TestMethod_get_cardname_invalid()
          {
               var cc = new CreditCard(-2);
               cc.get_card_info();
               Assert.AreEqual("", cc.get_cardname());
          }

          [TestMethod]
          public void TestMethod_get_cardtype()
          {
               var cc = new CreditCard(8);
               cc.get_card_info();
               Assert.AreEqual("Visa", cc.get_cardtype());
          }

          [TestMethod]
          public void TestMethod_get_cardtype_invalid()
          {
               var cc = new CreditCard(-2);
               cc.get_card_info();
               Assert.AreEqual("", cc.get_cardtype());
          }

          [TestMethod]
          public void TestMethod_get_carddate()
          {
               var cc = new CreditCard(8);
               cc.get_card_info();
               Assert.AreEqual("2021-08-02", cc.get_carddate());
          }

          [TestMethod]
          public void TestMethod_get_carddate_invalid()
          {
               var cc = new CreditCard(-2);
               cc.get_card_info();
               Assert.AreEqual("", cc.get_carddate());
          }

          [TestMethod]
          public void TestMethod_get_cardinfo()
          {
               var cc = new CreditCard(8);

               Assert.IsTrue(cc.get_card_info());
          }

          [TestMethod]
          public void TestMethod_get_cardinfo_invalid()
          {
               var cc = new CreditCard(-2);
               Assert.IsFalse(cc.get_card_info());
          }

          [TestMethod]
          public void TestMethod_remove_card()
          {
               var cc = new CreditCard(6);
               cc.new_card( "4012888888881881", "Test Card", "Visa", DateTime.Today.ToString("dd/MM/yyyy"));
               Assert.AreEqual(1, cc.remove_card());
          }

          [TestMethod]
          public void TestMethod_remove_card_invlaid()
          {
               var cc = new CreditCard(-1);
               Assert.AreEqual(1, cc.remove_card());
          }
     }
}
