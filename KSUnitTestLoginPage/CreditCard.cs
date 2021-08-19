using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreDAL;


namespace BookStoreLIB
{
     public class CreditCard
     {
          int cid;
          int uid;
          string cardnumber;
          string name;
          string ctype;
          string cdate;

          Boolean flag = false;

          public CreditCard(int userid)
          {
               uid = userid;
          }

          public int new_card( string cnumber, string cname, string cardtype, string carddate)
          {
               cardnumber = cnumber;
               name = cname;
               ctype = cardtype;
               cdate = carddate;

               DALCreditCard cc = new DALCreditCard();
               cid = cc.add_card(uid, cardnumber, name, ctype, cdate);
               if (cid > 0)
               {
                    flag = true;
               }

               return cid;
          }

          public int edit_card(string cnumber, string cname, string cardtype, string carddate)
          {
               return new_card(cnumber, cname, cardtype, carddate);
          }

          public int get_card_id()
          {
               DALCreditCard cc = new DALCreditCard();
               cid = cc.get_card(uid);
               return cid;
          }

          public int remove_card()
          {
               get_card_id();
               DALCreditCard cc = new DALCreditCard();

               return cc.remove_card(cid);

          }

          public bool get_card_info()
          {
               DALCreditCard cc = new DALCreditCard();
               cid = cc.get_card(uid);

               if (cid> 0)
               {
                    cardnumber = cc.get_cardnumber(cid);
                    name = cc.get_cardname(cid);
                    ctype = cc.get_cardtype(cid);
                    cdate = cc.get_carddate(cid);
                    if (cardnumber == null || name == null || ctype == null || cdate == null)
                    {
                         flag = false;
                    }
                    else
                    {
                         flag = true;
                    }
               }

               return flag;
          }

          public int get_cid()
          {
               if (flag)
               {
                    return cid;
               }
               else
               {
                    return -1;
               }
          }

          public int get_uid()
          {
               if (flag)
               {
                    return uid;
               }
               else
               {
                    return -1;
               }
          }

          public string get_cardnumber()
          {
               if (flag)
               {
                    return cardnumber;
               }
               else
               {
                    return "";
               }
          }

          public string get_cardname()
          {
               if (flag)
               {
                    return name;
               }
               else
               {
                    return "";
               }
          }

          public string get_cardtype()
          {
               if (flag)
               {
                    return ctype;
               }
               else
               {
                    return "";
               }
          }

          public string get_carddate()
          {
               if (flag)
               {
                    return cdate;
               }
               else
               {
                    return "";
               }
          }



     }
}
