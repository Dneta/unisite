using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DeleteFriend : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.RequestType == "POST" && Session["Email"] != null)
        {
            string UserName = Request.Form["un"];

    }

         public void makeMessage(string UserName)
    {
             "DELETE FROM 
    }

}