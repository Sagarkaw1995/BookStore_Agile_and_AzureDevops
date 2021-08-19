using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStoreDAL;

namespace BookStoreLIB
{  
        public class UserAddress
        {
            public int addAds { set; get; }
            public int checkAds { set; get; }
            public int updateAds { set; get; }

            DALUserAddress addAddress;
            public Boolean AddAddress(int userid, string fname, string country, string hnum, string city, string pin)
            {
                addAddress = new DALUserAddress();
                addAds = addAddress.Address(userid, fname, country, hnum, city, pin);
                if (addAds == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public Boolean checkExistingAddress(int userid)
            {
                addAddress = new DALUserAddress();
                checkAds = addAddress.checkExistAddress(userid);
                if (checkAds > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public Boolean updateExistingAddress(int userid, string fname, string country, string hnum, string city, string pin)
            {
                addAddress = new DALUserAddress();
                updateAds = addAddress.updateExistAddress(userid, fname, country, hnum, city, pin);

                if (updateAds > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
