using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assistant;

public partial class SingUpIn : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Request.RequestType == "POST")//this code handles signup
        {
            string Email = Request.Form["email"];
            string Password = Request.Form["password"];
            if (checkingsignin(Email, Password))
            {
               Session["Email"]=Email;
               Session.Timeout = 30;
               Response.Redirect("Default.aspx");
            }

        }
    }

    protected bool checkingsignin(string Email, string Password)
    {
        return myAdoHelper.IsExist("Database.mdf", "SELECT * FROM Users WHERE Email='" + Email + "' AND Password='" + Password + "'; ");
    }
}