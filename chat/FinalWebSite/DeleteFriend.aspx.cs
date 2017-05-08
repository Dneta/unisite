using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Assistant;


public partial class DeleteFriend : System.Web.UI.Page
{
    public string CB = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.RequestType == "POST" && Session["Email"] != null)
        {
            for (int i = 0; i < UserNamesnum(); i++) //  רצה על כל השמות של המשתמשים מהטופס 
            {
                string username = "username" + i; 
                if (Request.Form[username] != null) // בדיקת סימון
                {
                    DeleteContact(Request.Form[username]); // מחיקת משתמש
                }
            }
        }
        if (Session["Email"] != null) 
        {
            PrintUserNames(); // הדפסת צ'אקבוקס בהתאם לאנשי הקשר
        }

    }

    public void DeleteContact(string UserName) //מחיקת איש קשר
    {
        SqlConnection conn = myAdoHelper.ConnectToDb("Database.mdf");
        conn.Open();
        string email = Session["Email"].ToString(); // מייל המשתמש
        string GetUserNames = "SELECT (FriendsList) FROM Users WHERE Email='" + email + "';";//שאילתה לקבלת אנשי הקשר של המשתמש
        SqlCommand com = new SqlCommand(GetUserNames, conn);
        SqlDataReader data = com.ExecuteReader();
        if (!data.Read()) return;
        string usernames = data.GetString(0); // אנשי קשר
        data.Close();
        if (usernames.Length == UserName.Length) usernames = ""; // מחיקת איש קשר יחיד
        else if (usernames.IndexOf(UserName) == 0) usernames=usernames.Remove(usernames.IndexOf(UserName), UserName.Length + 1); // מחיקת איש הקשר הראשון
        else usernames=usernames.Remove(usernames.IndexOf(UserName) -1, UserName.Length); // מחיקת איש קשר במיקום אחר
        string updateFriendsList = " UPDATE Users SET FriendsList='" + usernames + "' WHERE Email='" + email + "';"; // שאילתת עידכון רשימה חדשה
        com = new SqlCommand(updateFriendsList, conn);
        com.ExecuteNonQuery();

        // מחיקת היסטוריית שיחות
        int code1; // קוד המשתמש
        int code2; // קוד איש הקשר הנמחק
        string GetCode1 = "SELECT (Code) FROM Users WHERE Email='" + email + "';"; // שאילתה לקבלת קוד המשתמש
        string GetCode2 = "SELECT (Code) FROM Users WHERE UserName='" + UserName + "';"; //שאילתה לקבלת קוד איש הקשר

        
         com = new SqlCommand(GetCode1, conn);
         data = com.ExecuteReader();
         if (!data.Read()) return;

        code1 = data.GetInt32(0);
        data.Close();
        com = new SqlCommand(GetCode2, conn);
        data = com.ExecuteReader();
        data.Read();
        code2 = data.GetInt32(0);
        data.Close();
        string GetUserName = "SELECT (UserName) FROM Users WHERE Code='" + code1 + "';"; // שאילתה לקבלת שם המשתמש של המשתמש
        com = new SqlCommand(GetUserName, conn);
        data = com.ExecuteReader();
        if (!data.Read()) return;
        string userName1 = data.GetString(0);
        data.Close();
        string GetFriendsList2 = "SELECT (FriendsList) FROM Users WHERE Code='" + code2 + "';"; // שאילתה לקבלת רשימת אנשי קשר של האיש הקשר הנמחק
        com = new SqlCommand(GetFriendsList2, conn);
        data = com.ExecuteReader();
        if (!data.Read()) return;
        string friendsList2 = data.GetString(0);
        data.Close();
        string deleteChat = "DELETE FROM Chats WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";"; // שאילתה למחיקת היסטוריית שיחות
        if (friendsList2.IndexOf(userName1) == -1) // אם המשתמש לא קיים ברשימת אנשי הקשר
        { 
            com = new SqlCommand(deleteChat, conn);
            com.ExecuteNonQuery();
        }
        conn.Close();
        
    }
    public void PrintUserNames() //הדפסת צ'אקבוקס
    {
        SqlConnection conn = myAdoHelper.ConnectToDb("Database.mdf"); 
        conn.Open();
        string email = Session["Email"].ToString();
        string GetUserNames = "SELECT (FriendsList) FROM Users WHERE Email='" + email + "';"; //שאילתה לקבלת אנשי קשר של המשתמש
        SqlCommand com = new SqlCommand(GetUserNames, conn); 
        SqlDataReader data = com.ExecuteReader();
        if (!data.Read()) return;
        string usernames = data.GetString(0); //רשימת אנשי הקשר של המשתמש
        data.Close();
        string[] userNamesList = usernames.Split(','); //  מערך אנשי קשר
        if (usernames != "")
        {
            for (int i = 0; i < userNamesList.Length; i++)
            {
                CB += "<input type=\"CheckBox\" name=\"username" + i + "\" value=\"" + userNamesList[i] + "\"/> " + userNamesList[i] + " <br/>";
            }
        }
        
        conn.Close();
    }

    public int UserNamesnum()
    {
        SqlConnection conn = myAdoHelper.ConnectToDb("Database.mdf");
        conn.Open();
        string email = Session["Email"].ToString();
        string GetUserNames = "SELECT (FriendsList) FROM Users WHERE Email='" + email + "';";
        SqlCommand com = new SqlCommand(GetUserNames, conn);
        SqlDataReader data = com.ExecuteReader();
        if (!data.Read()) return -1;
        string usernames = data.GetString(0);
        string[] userNamesList = usernames.Split(',');
        data.Close();
        conn.Close();
        return userNamesList.Length;
    }


     

}