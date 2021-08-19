using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;


namespace BookStoreDAL
{
    public class DALUserInfo
    {
            public int LogIn(string userName, string password)
            {
                var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select UserID from UserData where "
                        + " UserName = @UserName and Password = @Password ";
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    object userId = cmd.ExecuteScalar();
                    if (userId != null && (int)userId > 0)
                    {
                        return (int)userId;
                    }
                else
                {
                    return -1;
                }
            }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return -1;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            public int checkusernameexists(string uname)
            {
                var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "Select UserID from UserData where " + " UserName = @UserName";
                    cmd.Parameters.AddWithValue("@UserName", uname);
                    conn.Open();
                    object userId = cmd.ExecuteScalar();
                    if (userId != null && (int)userId > 0)
                    {
                        return (int)userId;
                    }
                    else
                    {
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.ToString());
                    return -1;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            public int SignUp(string fname, string type, string mgr, string uname, string pwd)
            {
                var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "INSERT INTO UserData (FullName, Type, Manager, UserName, Password) VALUES (@fullname,@type,@manager,@username,@password)";
                    cmd.Parameters.AddWithValue("@fullname", fname);
                    cmd.Parameters.AddWithValue("@type", type);
                    cmd.Parameters.AddWithValue("@manager", mgr);
                    cmd.Parameters.AddWithValue("@username", uname);
                    cmd.Parameters.AddWithValue("@password", pwd);
                    if (fname == "" || type == "" || mgr == "" || uname == "" || pwd == "" || !(char.IsLetter(pwd[0]) && pwd.Any(c => char.IsLetter(c)) && pwd.Any(c => char.IsDigit(c))))
                    {
                        throw new Exception();
                    }
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return 1;
                }
                catch
                {
                    return -1;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                        conn.Close();
                }
            }
            public int updatepassword(string uname, string pwd)
            {
                if(uname == "" || pwd == "" || !(pwd.Length >= 6 && char.IsLetter(pwd[0]) && pwd.Any(c => char.IsLetter(c)) && pwd.Any(c => char.IsDigit(c)) && pwd.All(c => char.IsLetterOrDigit(c))))
            {
                return -1;
            }
                var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
                try
                {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE UserData SET Password = @password WHERE UserName = @username";
                cmd.Parameters.AddWithValue("@username", uname);
                cmd.Parameters.AddWithValue("@password", pwd);
                conn.Open();
                cmd.ExecuteNonQuery();
                return 1;
                }
                catch
                {
                return -1;
                }
                finally
                {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                }
        }

        public int GetManagerStatus(string username)
        {
            var conn = new SqlConnection(Properties.Settings.Default.DBconnection);
            try
            {
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandText = "Select Manager from UserData where " + " UserName = @username";
                cmd.Parameters.AddWithValue("@UserName", username);
                conn.Open();
                bool isManager = (bool)cmd.ExecuteScalar();
                if (isManager == true)
                    return 1;
                else return 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }


        }
    }
    }
