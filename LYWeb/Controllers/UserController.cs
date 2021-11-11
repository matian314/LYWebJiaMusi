using LYWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LYWeb.Helpers;
using System.Web.Security;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace LYWeb.Controllers
{
    public class UserController : Controller
    {
        private TudsEntities db = new TudsEntities();
        // GET: User/Login
        public ActionResult Login()
        {
            ViewBag.Tips = "请输入用户名和密码";
            return View();
        }

        // POST : User/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login([Bind(Include = "NAME,PASSWORD")]WEBUSER user, string txtValiCode)
        {
            
            if (!string.Equals(txtValiCode, Session["VerifyCode"]?.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("failed", "验证码错误");
                return View();
            }
            string result = AccountHelper.ValidUser(user.NAME, user.PASSWORD);
            if(string.IsNullOrEmpty(result))
            {
                string name = user.NAME;
                var record = db.WEBUSER.Where(u => u.NAME == name).First();
                Session.Add("user", name);
                Session.Add("realname", record.REAL_NAME);
                Session.Add("role", record.ROLE);
                FormsAuthentication.RedirectFromLoginPage(name, false);
                if(Request.QueryString["ReturnUrl"] != null)
                {
                    return Redirect(Request.QueryString["ReturnUrl"]);
                }
                else
                {
                    return RedirectToAction("Index", "Monitor");
                }
                
            }
            else
            {
                ModelState.AddModelError("failed", result);
                return View();
            }
            
        }
        /// <summary>
        /// 获取验证码 FileContentResult
        /// </summary>
        /// <returns></returns>
        public FileResult VerifyCode()
        {
            string code = "";
            Bitmap bitmap =  VerifyCodeHelper.CreateVerifyCode(out code);
            base.HttpContext.Session["VerifyCode"] = code;
            using(MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Gif);
                return File(ms.ToArray(), "image/gif");
            }
        }
        // GET: User/Register
        public ActionResult Register()
        {

            return View();
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Clear();
            return RedirectToAction("Login", "User");
        }
        // POST: User/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "NAME,PASSWORD,REAL_NAME,BUREAU_ID,DEPOT_NAME,UNIT_NAME,EMPLOYEE_NUMBER")]WEBUSER user)
        {
            user.ID = Guid.NewGuid().ToString();
            user.ENABLED = false;
            user.LOGINFAILED_COUNTER = 0;
            string passwordMessage = AccountHelper.ValidPassword(user.PASSWORD);
            if (!string.IsNullOrEmpty(passwordMessage))
            {
                ViewBag.Tips = passwordMessage;
                return View();
            }
            user.PASSWORD = AccountHelper.EncryptPassword(user.PASSWORD);
            user.ROLE = "普通用户";
            user.REGISTER_TIME = DateTime.Now;
            try
            {
                db.WEBUSER.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "User");
            }
            catch (Exception ex)
            {
                ViewBag.Tips = ex.InnerException.Message;
                return View();
            }
        }
        public ActionResult UserEdit()
        {
            //管理员不能管理自己和其他管理员的权限
            var WEBUSER = db.WEBUSER.Where(w => w.ROLE != "管理员");
            //WEBUSER = SelectListHelper.GetWEBUSER(WEBUSER);
            return View(WEBUSER.ToList());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(string submit)
        {
            //存在对应的用户记录
            var user = db.WEBUSER.Find(submit);
            if(user != null)
            {
                try
                {
                    user.ROLE = Request.Form[$"ROLE{submit}"];
                    user.ENABLED = Request.Form[$"ENABLED{submit}"] == "0" ? false : true;
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    //log error here
                }

            }

            return UserEdit();
        }


        public ActionResult ChangePassword()
        {
            string name = Session["user"]?.ToString();
            ChangePasswordModel cpm = null;
            try
            {
                var user = db.WEBUSER.Where(t => t.NAME == name).First();
                cpm = new ChangePasswordModel();
                cpm.ID = user.ID;
                cpm.REALNAME = user.REAL_NAME;
            }
            catch
            {
                ModelState.AddModelError("failed", "加载错误");
            }
            return View(cpm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword([Bind(Include = "ID,REALNAME,OLD_PASSWORD,NEW_PASSWORD,CONFIRM_PASSWORD")]ChangePasswordModel cpm)
        {
            if(cpm.NEW_PASSWORD != cpm.CONFIRM_PASSWORD)
            {
                ModelState.AddModelError("failed", "两次输入新密码不一致");
                return View(cpm);
            }
            string validInfo = AccountHelper.ValidPassword(cpm.NEW_PASSWORD);
            if (String.Empty != validInfo)
            {
                ModelState.AddModelError("failed", validInfo);
                return View(cpm);
            }
            var user = db.WEBUSER.Find(cpm.ID);
            string oldPasswordMsg = AccountHelper.EncryptPassword(cpm.OLD_PASSWORD);
            if(oldPasswordMsg != user.PASSWORD)
            {
                ModelState.AddModelError("failed", "旧密码输入错误");
                return View(cpm);
            }
            string newPasswordMsg = AccountHelper.EncryptPassword(cpm.NEW_PASSWORD);
            user.PASSWORD = newPasswordMsg;
            db.SaveChanges();
            ModelState.AddModelError("ok", "修改成功");
            return View(cpm);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}