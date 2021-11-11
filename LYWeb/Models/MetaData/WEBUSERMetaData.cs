using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LYWeb.Models
{
    [MetadataType(typeof(WEBUSERMetaData))]
    public partial class WEBUSER
    {
    }

    public class WEBUSERMetaData
    {
        [Key]
        public string ID { get; set; }
        [DisplayName("用户名")]
        public string NAME { get; set; }
        [DisplayName("真实姓名")]
        public string REAL_NAME { get; set; }
        [DisplayName("铁路工作证号")]
        public string EMPLOYEE_NUMBER { get; set; }
        [DisplayName("所属局名")]
        public string BUREAU_ID { get; set; }
        [DisplayName("所属段名")]
        public string DEPOT_NAME { get; set; }
        [DisplayName("注册时间")]
        public Nullable<System.DateTime> REGISTER_TIME { get; set; }
        [DisplayName("最后登录时间")]
        public Nullable<System.DateTime> LATEST_LOGIN_TIME { get; set; }
        public Nullable<byte> LOGINFAILED_COUNTER { get; set; }
        [DisplayName("密码")]
        public string PASSWORD { get; set; }
        public Nullable<System.DateTime> LOCKOUT_TIME { get; set; }
        public Nullable<bool> ENABLED { get; set; }
        public string ROLE { get; set; }
        [DisplayName("单位名")]
        public string UNIT_NAME { get; set; }
    }

}