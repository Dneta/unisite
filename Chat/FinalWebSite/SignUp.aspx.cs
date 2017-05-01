using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assistant;
public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST")//this code handles signup
        {
            int bit;
            string FirstName = Request.Form["firstname"];
            string LastName = Request.Form["lastname"];
            string Gender = Request.Form["gender"];
            string Email = Request.Form["email"];
            string Password = Request.Form["password"];
            string UserName = Request.Form["username"];
            if (Gender == "male") bit = 0;
            else bit = 1;
            if (!checkingsignup(Email, UserName))
            {
                myAdoHelper.DoQuery("Database.mdf", "INSERT INTO Users (FirstName, LastName, Gender, Email, Password, UserName, Admin) VALUES ( '" + FirstName + "', '" + LastName + "', " + bit + ", '" + Email + "', '" + Password + "', '" + UserName + "',0);");
            }
        }
    }
    protected bool checkingsignup(string Email, string UserName)
    {
       return myAdoHelper.IsExist("Database.mdf", "SELECT * FROM Users WHERE Email='"+Email+"' OR UserName='"+UserName+"'; ");
    }

}