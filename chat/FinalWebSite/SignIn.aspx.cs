using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assistant;

public partial class SingUpIn : System.Web.UI.Page
{
    public string message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (Request.RequestType == "POST")
        {
            //קליטת נתונים מהטופס
            string Email = Request.Form["email"];
            string Password = Request.Form["password"];
            if (checkingsignin(Email, Password)) //אם הנתונים קיימים במסד נתונים
            {
                Session["Email"] = Email; // אפשור חיבור של המשתמש לאתר לאורך זמן
                Session.Timeout = 30; // התנתקות לאחר 30 דקות של חוסר פעילות
                Response.Redirect("Default.aspx"); // מעבר לדף הבית
            }
            else message = "  <h1 style=\" color: #ff9999\">  User Does Not Exist  </h1>";// אם הנתונים לא קיימים תודיע על כך

        }
    }

    protected bool checkingsignin(string Email, string Password) // האם הנתונים קיימים במסד נתונים
    {
        return myAdoHelper.IsExist("Database.mdf", "SELECT * FROM Users WHERE Email='" + Email + "' AND Password='" + Password + "'; ");
    }
}