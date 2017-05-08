using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Assistant;
public partial class SignUp : System.Web.UI.Page
{
    public string Message = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST") //מטרת פעולה זו היא להכניס את הנתונים שהתקבלו מדף ההרשמה למסד נתונים
        {
            int bit; // מין הנרשם
            // קליטת נתונים מהטופס
            string FirstName = Request.Form["firstname"];
            string LastName = Request.Form["lastname"];
            string Gender = Request.Form["gender"];
            string Email = Request.Form["email"];
            string Password = Request.Form["password"];
            string UserName = Request.Form["username"];
            if (Gender == "male") bit = 0; // הגדרת מין הנרשם
            else bit = 1;
            if (!checkingsignup(Email, UserName)) // אם הנתונים לא קיימים במסד נתונים
            {
                //הכנסת הנתונים למסד נתונים
                myAdoHelper.DoQuery("Database.mdf", "INSERT INTO Users (FirstName, LastName, Gender, Email, Password, UserName, Admin) VALUES ( '" + FirstName + "', '" + LastName + "', " + bit + ", '" + Email + "', '" + Password + "', '" + UserName + "',0);");
                Session["Email"] = Email; // אפשור חיבור של המשתמש לאתר לאורך זמן
                Session.Timeout = 30; // התנתקות לאחר 30 דקות של חוסר פעילות
                Response.Redirect("Default.aspx"); // מעבר לדף הבית
            }
            else Message = "  <h1 style=\" color: #ff9999\">  User Already Exists  </h1>";// אם הנתונים  קיימים תודיע על כך
        }
    }
    protected bool checkingsignup(string Email, string UserName) // האם נתונים קיימים במסד הנתונים
    {
       return myAdoHelper.IsExist("Database.mdf", "SELECT * FROM Users WHERE Email='"+Email+"' OR UserName='"+UserName+"'; ");
    }

}