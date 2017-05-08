using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Assistant;
public partial class SiteMaster : System.Web.UI.MasterPage
{
    public string friendpills = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        // סימון עמוד במצב אקטיבי
        string webAddress = this.Page.Request.FilePath;
        if (webAddress == "/FinalWebSite/Default.aspx") Default.Attributes.Add("class", "active");
        else if (webAddress == "/FinalWebSite/DeleteFriend.aspx") DeleteFriend.Attributes.Add("class", "active");
 
        if (Session["email"] != null)
        {
            string[] list = getFriendsList(Session["email"].ToString()).Split(','); //מערך אנשי קשר
            for (int i = 0; i < list.Length; i++) //הדפסת אנשי קשר בסרגל הימיני
            {
                friendpills += "<li runat=\"server\"><a href=\"ChangeActiveness.aspx?user=" + list[i] + "\">" + list[i] + "</a></li>";
            }
        }
    }


    public static string getFriendsList(string email)
    {
        string sql = "SELECT (FriendsList) FROM Users WHERE Email='" + email + "';"; //שאילתה לקבלת אנשי קשר
        SqlConnection conn = myAdoHelper.ConnectToDb("Database.mdf");
        conn.Open();
        SqlCommand com = new SqlCommand(sql, conn);
        SqlDataReader data = com.ExecuteReader();
        data.Read();
        string list = data.GetString(0); //רשימת אנשי קשר
        data.Close();
        conn.Close();
        return list;


    }
}
