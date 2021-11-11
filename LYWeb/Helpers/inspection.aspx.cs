using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OracleClient;
using System.Data;
using Newtonsoft.Json;

public partial class _Default : System.Web.UI.Page
{
    private string myConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString1"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        string time = Request.QueryString["time"].ToString();
        string ttlx = Request.QueryString["ttlx"].ToString();
        string level = Request.QueryString["level"].ToString();
        OracleConnection myConnection = new OracleConnection(myConnectionString);
        DataTable dt = new DataTable();
        string where = "";
        if (ttlx != "")
        {
            string[] arr_t = ttlx.Split(',');
            string[] type = new string[] { "直探头", "斜探头", "斜探头-轮缘" };
            where = where + " and (";
            for (int i = 0; i < arr_t.Length; i++)
            {
                if (i == 0)
                {
                    where = where + " a.probe_type='" + type[int.Parse(arr_t[i])] + "'";
                }
                else
                {
                    where = where + " or a.probe_type='" + type[int.Parse(arr_t[i])] + "'";
                }
            }
            where = where + ")";
        }
        if (level != "")
        {
            string[] arr_l = level.Split(',');
            where = where + " and (";
            for (int i = 0; i < arr_l.Length; i++)
            {
                if (i == 0)
                {
                    where = where + " a.alarm_level=" + arr_l[i] + "";
                }
                else
                {
                    where = where + " or a.alarm_level=" + arr_l[i] + "";
                }
            }
            where = where + ")";
        }
        else
        {
            where = where + " and a.alarm_level=0";
        }
        string str = "select a.*,b.name from inspection_defect_data a left join train b on b.id=a.id where a.detection_time=to_date('" + time + "','yyyy/mm/dd hh24:mi:ss')" + where;
        OracleDataAdapter oda = new OracleDataAdapter(str, myConnection);
        DataSet ds = new DataSet();
        oda.Fill(ds, "qx");
        this.data.Value = JsonConvert.SerializeObject(ds.Tables["qx"]);
    }
}