using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Assistant;
public partial class ChatRoom : System.Web.UI.Page
{
    public string messages = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.RequestType == "POST" && Session["Email"]!=null)
        {
            string NewResp = Request.Form["newresp"];//קליטת הודעה
            addRespToDB(Session["Email"].ToString(), Session["activeChat"].ToString(),NewResp); // הוספת תגובה להיסטוריית השיחות

        }
        if (Session["Email"] != null) // הדפסת הודעה
        {
            messages=outputConv(Session["Email"].ToString(), Session["activeChat"].ToString());
        }
        
    }
    public string outputConv(string email, string activeChat) //הדפסת הודעה
    {
        if (activeChat == "None") return "";
        int code1; // קוד המשתמש
        int code2;// קוד איש הקשר
        string talk; //היסטוריית השיחות
        string GetCode1 = "SELECT (Code) FROM Users WHERE Email='" + email + "';"; //שאילתה לקבלת קוד המשתמש
        string GetCode2 = "SELECT (Code) FROM Users WHERE UserName='" + activeChat + "';"; //שאליתה לקבלת קוד איש הקשר
        
        SqlConnection conn = myAdoHelper.ConnectToDb("Database.mdf");
        conn.Open();
        SqlCommand com = new SqlCommand(GetCode1, conn);
        SqlDataReader data = com.ExecuteReader();
        if (!data.Read()) return "";
        
        code1 = data.GetInt32(0);
        data.Close();
        com = new SqlCommand(GetCode2, conn);
        data = com.ExecuteReader();
        data.Read();
        code2 = data.GetInt32(0);
        data.Close();
        string GetTalk = "SELECT (Talk) FROM Chats WHERE  MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";"; //שאילתה לקבלת היסטוריית השיחות
        
        com = new SqlCommand(GetTalk, conn);
        data = com.ExecuteReader();
        if (!data.Read()) return "";
        talk = data.GetString(0);
        data.Close();
        if (talk == "") return "";
        List<string> messageList = new List<string>();
        int lastpsik=-1; // מיקום הפסיק האחרון
        for (int i = 1; i < talk.Length; i++)
        {
            if (talk[i - 1] != '\\' && talk[i] == ',') // 
            {
                messageList.Add(talk.Substring(lastpsik + 1, i - lastpsik - 1)); // הוספת הודעה בין שני פסיקים מפרידים
                lastpsik = i; 
                
            }
        }
        messageList.Add(talk.Substring(lastpsik + 1, talk.Length - lastpsik - 1));// הוספת הודעה אחרונה
        string conversation = "";
        for (int i = 0; i < messageList.Count; i++)
        {
            int mcode = int.Parse(messageList[i].Split(':')[0]); // קוד כותב ההודעה 

            conversation += makeMessage(messageList[i], mcode != code1); // מוסיף  קוד של הודעה בודדת למכלול השיחה המודפסת וכיוונה
        }
        return conversation;

    }
    public string makeMessage(string inputFromDb,bool dir)// הדפסת קוד של הודעה
    {

        string direction = ""; // קוד קביעת כיוון
        if (dir) direction = "pull-right"; // על פי קלט dir
        string mess = inputFromDb+"";
        mess=mess.Replace("\\,",","); // הסרת האינדיקטור סלאש

        for (int i = 0; i < mess.Length; i++)// לולאה שרצה עד זיהוי נקודותיים
        {
            if (mess[i] == ':')
            {
                mess = mess.Substring(i+1); // הפרדת תוכן ההודעה בלבד ללא שם כותב
                break;
            }
        }
        // קוד ההודעה עם קביעת מיקום ועיצוב גוף ההודעה
        string ret = "<div class=\"row\">" +
            "<div class=\"col-sm-4 " + direction + "\">" +
                "<div class=\"panel panel-default\">" +
                    "<div class=\"panel-body\">" +
                        mess+ 
                "</div>" +
            "</div>" +
        "</div></div>";
        return ret;
    }

    public void addRespToDB(string email,string activeChat,string resp) // 
    {
        int code1; // קוד המשתמש
        int code2; // קוד איש הקשר
        string GetCode1 = "SELECT (Code) FROM Users WHERE Email='" + email + "';";
        string GetCode2 = "SELECT (Code) FROM Users WHERE UserName='" + activeChat + "';";

        resp=resp.Replace(",","\\,");
        SqlConnection conn = myAdoHelper.ConnectToDb("Database.mdf");
        conn.Open();
        SqlCommand com = new SqlCommand(GetCode1, conn);
        SqlDataReader data = com.ExecuteReader();
        data.Read();
        code1 = data.GetInt32(0);
        data.Close();
        resp = code1 + ":" + resp;
        com = new SqlCommand(GetCode2, conn);
        data = com.ExecuteReader();
        data.Read();
        code2 = data.GetInt32(0);
        data.Close();
        //אם היסטוריה קיימת עדכן היסטוריה
        if (myAdoHelper.IsExist("Database.mdf", "SELECT (Talk) FROM Chats WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + " AND Talk='';"))
        {
            string oldtalk = "SELECT (Talk) FROM Chats WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";";
            com = new SqlCommand(oldtalk, conn);
            data = com.ExecuteReader();
            data.Read();
            string talk = data.GetString(0); // היסטוריית השיחות
            data.Close();
            string update = "UPDATE Chats SET Talk= '"+ resp + "' WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";";
            com = new SqlCommand(update, conn);
            com.ExecuteNonQuery();
        }
        if (myAdoHelper.IsExist("Database.mdf", "SELECT (Talk) FROM Chats WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + " ;"))
        {
            string oldtalk = "SELECT (Talk) FROM Chats WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";";
            com = new SqlCommand(oldtalk, conn);
            data = com.ExecuteReader();
            data.Read();
            string talk = data.GetString(0); // היסטוריית השיחות
            data.Close();
            string update = "UPDATE Chats SET Talk= '" + talk + "," + resp + "' WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";";
            com = new SqlCommand(update, conn);
            com.ExecuteNonQuery();
        }
        else // אם לא קיימת היסטוריית שיחות הכנס הודעה
        {
            string insert = "INSERT INTO Chats (MinCode, MaxCode, Talk) VALUES ("+Math.Min(code1, code2)+","+ Math.Max(code1, code2)+",'"+resp+"');";
            com = new SqlCommand(insert, conn);
            com.ExecuteNonQuery();
        }

        conn.Close();
    }
}