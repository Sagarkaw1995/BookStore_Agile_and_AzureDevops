/*
 */

using System;
using System.Linq;
using BookStoreDAL;

namespace BookStoreLIB
{


     public class UserData
     {
          public int UserID { set; get; }
          public string LoginName { set; get; }
           public bool IsManager { set; get; }
        public string Password { set; get; }
          public Boolean LoggedIn { set; get; }
          public int LogID { set; get; }
          public int checkusernameexist { set; get; }
          public int update_pwd { set; get; }

        public Boolean LogIn(string loginName, string passWord)
        {
            if (loginName == "" || passWord == "")
                return false;

            if (passWord.Length < 6) //check if password is less than 6
                return false;

            if (!Char.IsLetter(passWord[0])) //check if password starts with letter
                return false;

            if (!passWord.Any(char.IsDigit)) //check if password has number
                return false;

            if (passWord.Any(c => (int)c < 48 || (57 < (int)c && (int)c < 65) || (90 < (int)c && (int)c < 97) || (int)c > 122)) //check if password only has letters and numbers
                return false;

            var dbUser = new DALUserInfo();
            UserID = dbUser.LogIn(loginName, passWord);
            if (UserID > 0)
            {
                LoginName = loginName;
                Password = passWord;
                LoggedIn = true;
                if (dbUser.GetManagerStatus(LoginName) == 1)
                    IsManager = true;
                else IsManager = false;
                return true;
            }
            else
            {
                LoggedIn = false;
                return false;
            }

        }
        public Boolean checkUsername(string uname)
        {
            var userID = new DALUserInfo();
            checkusernameexist = userID.checkusernameexists(uname);
            if (checkusernameexist > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean SignUp(string fname, string type, string mgr, string uname, string pwd)
        {
            var logUser = new DALUserInfo();
            LogID = logUser.SignUp(fname, type, mgr, uname, pwd);
            if (LogID == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean UpdatePassword(string uname, string pwd)
        {
            var updatepwd = new DALUserInfo();
            update_pwd = updatepwd.updatepassword(uname, pwd);
            if (update_pwd == 1)
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
