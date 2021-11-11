<%@ WebHandler Language="C#" Class="data" %>

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Linq;
//using System.Data.OracleClient;
using System.Collections;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Net;
//using System.Windows.Forms;
using System.ComponentModel;
using Newtonsoft.Json;
using LYWeb.Models;

public class data : IHttpHandler {

    //private string myConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
    public void ProcessRequest (HttpContext context) {
        try
        {
            string key = context.Request.QueryString["type"].ToString();
            switch (key)
            {
                case "int": Get_Int(context); break;
                //case "qx": Get_QX(context); break;
            }
        }
        catch (Exception e)
        {
            context.Response.ContentType = "json/plain";
            context.Response.Write("");
        }
        finally
        {
            context.Response.ContentType = "application/json";
            //context.Response.Write("");
        }
    }

    public void Get_Int(HttpContext context)
    {
        INSPECTION_DEFECT_DATA inspection_defect_data;
        using (TudsEntities db = new TudsEntities())
        {
            inspection_defect_data = db.INSPECTION_DEFECT_DATA.FirstOrDefault();
        }
        //OracleConnection myConnection = new OracleConnection(myConnectionString);
        //DataTable dt = new DataTable();
        //string str = "select t.*,to_char(t.detection_time,'yyyy-mm-dd hh24:mi:ss') as detection_time1 from inspection_defect_data t where rownum=1 order by t.detection_time desc";
        //OracleDataAdapter oda = new OracleDataAdapter(str, myConnection);
        //DataSet ds = new DataSet();
        //oda.Fill(ds, "qx");
        context.Response.Write(JsonConvert.SerializeObject(inspection_defect_data));
    }

    //public void Get_QX(HttpContext context)
    //{
    //    OracleConnection myConnection = new OracleConnection(myConnectionString);
    //    DataTable dt = new DataTable();
    //    string time = context.Request.QueryString["time"].ToString();
    //    string ttlx = context.Request.QueryString["ttlx"].ToString();
    //    string level = context.Request.QueryString["level"].ToString();
    //    string where = "";
    //    if (ttlx != "")
    //    {
    //        string[] arr_t = ttlx.Split(',');
    //        string[] type = new string[] { "直探头", "斜探头", "斜探头-轮缘" };
    //        where = where + " and (";
    //        for (int i = 0; i < arr_t.Length; i ++ )
    //        {
    //            if (i == 0)
    //            {
    //                where = where + " probe_type='" + type[int.Parse(arr_t[i])] + "'";
    //            }
    //            else
    //            {
    //                where = where + " or probe_type='" + type[int.Parse(arr_t[i])] + "'";
    //            }
    //        }
    //        where = where + ")";
    //    }
    //    if (level != "")
    //    {
    //        string[] arr_l = level.Split(',');
    //        where = where + " and (";
    //        for (int i = 0; i < arr_l.Length; i++)
    //        {
    //            if (i == 0)
    //            {
    //                where = where + " alarm_level=" + arr_l[i] + "";
    //            }
    //            else
    //            {
    //                where = where + " or alarm_level=" + arr_l[i] + "";
    //            }
    //        }
    //        where = where + ")";
    //    }
    //    else
    //    {
    //        where = where + " and alarm_level=0";
    //    }
    //    string str = "select * from inspection_defect_data where detection_time=to_date('" + time + "','yyyy/mm/dd hh24:mi:ss')" + where;
    //    OracleDataAdapter oda = new OracleDataAdapter(str, myConnection);
    //    DataSet ds = new DataSet();
    //    oda.Fill(ds, "qx");
    //    context.Response.Write(JsonConvert.SerializeObject(ds.Tables["qx"]));
    //}

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}