using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using LYWeb.Models;

namespace LYWeb.Helpers
{
    public sealed class AccountHelper
    {
        public static string EncryptPassword(string password)
        {
            string saultedPassword = "VeicTuds_2020" + password;
            byte[] stream = Encoding.ASCII.GetBytes(saultedPassword);
            System.Security.Cryptography.SHA256 sha256 = new SHA256CryptoServiceProvider();
            return BitConverter.ToString(sha256.ComputeHash(stream)).Replace("-","");
        }
        public static string ValidPassword(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (password.Length < 10 || password.Length > 16)
            {
                return "密码的长度应为10-16位";
            }
            if (!(Regex.IsMatch(password, "[0-9]")))
            {
                return "密码应包含数字";
            }
            if (!(Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[a-z]")))
            {
                return "密码应同时由大小写字母组成";
            }
            if (!Regex.IsMatch(password, "[^a-zA-Z0-9]"))
            {
                return "密码应包含特殊字符";
            }
            return string.Empty;
        }

        public static string ValidUser(string name, string password)
        {
            TudsEntities db = new TudsEntities();
            int MaxLoginFailTimes = 5;
            if(db.WEBUSER.Where(u => u.NAME == name).Count() != 1)
            {
                //log here
                return "您的用户名错误";
            }
            WEBUSER user = db.WEBUSER.Where(u => u.NAME == name).First();
            if(user.PASSWORD != EncryptPassword(password) && user.LOGINFAILED_COUNTER < 4)
            {
                user.LOGINFAILED_COUNTER++;
                db.SaveChanges();
                return $"您的密码错误(还有{MaxLoginFailTimes - user.LOGINFAILED_COUNTER}次机会)";
            }
            else if(user.PASSWORD != EncryptPassword(password) && user.LOGINFAILED_COUNTER == MaxLoginFailTimes - 1)
            {
                user.LOGINFAILED_COUNTER = 0;
                user.LOCKOUT_TIME = DateTime.Now.AddMinutes(10);
                db.SaveChanges();
                return $"您的密码错误(您的账户将被锁定至{user.LOCKOUT_TIME})";
            }
            else if(user.PASSWORD != EncryptPassword(password))
            {
                //不存在的
                //log here
                return "您的密码错误";
            }
            if(user.LOCKOUT_TIME >= DateTime.Now)
            {
                return $"您的账户已被锁定至{user.LOCKOUT_TIME?.ToString()}";
            }
            if(!user.ENABLED.HasValue || user.ENABLED.Value == false)
            {
                return "您的账户尚未激活，请与管理员进行联系";
            }
            user.LOGINFAILED_COUNTER = 0;
            user.LATEST_LOGIN_TIME = DateTime.Now;
            db.SaveChanges();
            return string.Empty;
        }
    }
}