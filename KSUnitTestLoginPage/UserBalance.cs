using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using BookStoreDAL;

namespace BookStoreLIB
{
     public class UserBalance
     {
          public double GetBalance(int userid)
          {
               DALUserBalance dub = new DALUserBalance();
               double bal = (double)dub.get_balance(userid) / 100.0;
               if (bal > 0)
               {
                    return bal ;
               }
               else
               {
                    return -1;
               }
          }

          public double incBalance(int userid, double amount)
          {
               DALUserBalance dub = new DALUserBalance();
               double bal = (double)dub.get_balance(userid) / 100.0;
               if (bal > 0)
               {
                    bal = bal + amount;
               }
               else
               {
                    bal = amount;
               }

               int val = (int)(bal * 100);
               if (dub.set_balance(userid, val)> 0)
               {
                    return bal; //return final balance
               }
               else
               {
                    return -1; //error from set balance
               }

          }

          public double decBalance(int userid, double amount)
          {
               DALUserBalance dub = new DALUserBalance();
               double bal = (double)dub.get_balance(userid) / 100.0;
               if (bal > 0)
               {
                    bal = bal - amount;
               }
               else
               {
                    return -2; //user not valid
               }
               if (bal < 0) //balance can not be negtive
               {
                    return -1;
               }
               int val = (int)(bal * 100);
               if (dub.set_balance(userid, val) > 0)
               {
                    return bal; //return final balance
               }
               else
               {
                    return -2; //error from set balance
               }

          }
     }
}
