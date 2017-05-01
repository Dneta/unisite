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
            string NewResp = Request.Form["newresp"];
            addRespToDB(Session["Email"].ToString(), Session["activeChat"].ToString(),NewResp);

        }
        if (Session["Email"] != null)
        {
            messages=outputConv(Session["Email"].ToString(), Session["activeChat"].ToString());
        }
        
    }
    public string outputConv(string email, string activeChat)
    {
        if (activeChat == "None") return "";
        int code1;
        int code2;
        string talk;
        string GetCode1 = "SELECT (Code) FROM Users WHERE Email='" + email + "';";
        string GetCode2 = "SELECT (Code) FROM Users WHERE UserName='" + activeChat + "';";
        
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
        string GetTalk = "SELECT (Talk) FROM Chats WHERE  MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";";
        
        com = new SqlCommand(GetTalk, conn);
        data = com.ExecuteReader();
        if (!data.Read()) return "";
        talk = data.GetString(0);
        data.Close();
        List<string> messageList = new List<string>();
        int lastpsik=-1;
        for (int i = 1; i < talk.Length; i++)
        {
            if (talk[i - 1] != '\\' && talk[i] == ',')
            {
                messageList.Add(talk.Substring(lastpsik + 1, i - lastpsik - 1));
                lastpsik = i;
                
            }
        }
        messageList.Add(talk.Substring(lastpsik + 1, talk.Length - lastpsik - 1));
        string conversation = "";
        foreach (string s in messageList)
        {
            int mcode = int.Parse(s.Split(':')[0]);

            conversation += makeMessage(s, mcode != code1);
        }
        return conversation;

    }
    public string makeMessage(string inputFromDb,bool dir)//false is left
    {

        string direction = "";
        if (dir) direction = "pull-right";
        string mess = inputFromDb+"";
        mess=mess.Replace("\\,",",");

        for (int i = 0; i < mess.Length; i++)
        {
            if (mess[i] == ':')
            {
                mess = mess.Substring(i+1);
                break;
            }
        }
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

    public void addRespToDB(string email,string activeChat,string resp)
    {
        int code1;
        int code2;
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
        if (myAdoHelper.IsExist("Database.mdf", "SELECT (Talk) FROM Chats WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + " ;"))
        {
            string oldtalk = "SELECT (Talk) FROM Chats WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";";
            com = new SqlCommand(oldtalk, conn);
            data = com.ExecuteReader();
            data.Read();
            string talk = data.GetString(0);
            data.Close();
            string update = "UPDATE Chats SET Talk= '" + talk + "," + resp + "' WHERE MinCode=" + Math.Min(code1, code2) + "  AND MaxCode=" + Math.Max(code1, code2) + ";";
            com = new SqlCommand(update, conn);
            com.ExecuteNonQuery();
        }
        else
        {
            string insert = "INSERT INTO Chats (MinCode, MaxCode, Talk) VALUES ("+Math.Min(code1, code2)+","+ Math.Max(code1, code2)+",'"+resp+"');";
            com = new SqlCommand(insert, conn);
            com.ExecuteNonQuery();
        }

        conn.Close();
    }
}