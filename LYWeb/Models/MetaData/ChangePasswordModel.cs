using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LYWeb.Models
{
    public class ChangePasswordModel
    {
        [DisplayName("用户ID")]
        public string ID { get; set; }
        [DisplayName("用户姓名")]
        public string REALNAME { get; set; }
        [DisplayName("原密码")]
        public string OLD_PASSWORD { get; set; }
        [DisplayName("新密码")]
        public string NEW_PASSWORD { get; set; }
        [DisplayName("确认新密码")]
        public string CONFIRM_PASSWORD { get; set; }

        
    }
}