using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangeActiveness : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType == "GET")
        {
            System.Diagnostics.Debug.WriteLine("got into change activeness");
            Session["activeChat"] = Request.QueryString["user"];
            Response.Redirect("ChatRoom.aspx");
        }
    }
}