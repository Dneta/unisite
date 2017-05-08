using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
/// <summary>
/// Summary description for myAdoHelper
/// </summary>
/// 

namespace Assistant
{
    public class myAdoHelper
    {
        public static SqlConnection ConnectToDb(string fileName)
        {
            string path = HttpContext.Current.Server.MapPath("App_Data/");//מיקום מסד בפורוייקט
            path += fileName;
            //string path = HttpContext.Current.Server.MapPath("App_Data/" + fileName);//מאתר את מיקום מסד הנתונים מהשורש ועד התקייה בה ממוקם המסד
            string connString = @"Data Source=.\SQLEXPRESS;AttachDbFilename=" +
                                 path +
                                 ";Integrated Security=True;User Instance=True";
            SqlConnection conn = new SqlConnection(connString);
            return conn;

        }

        /// <summary>
        /// To Execute update / insert / delete queries
        ///  הפעולה מקבלת שם קובץ ומשפט לביצוע ומבצעת את הפעולה על המסד
        /// </summary>

        public static void DoQuery(string fileName, string sql)//הפעולה מקבלת שם מסד נתונים ומחרוזת מחיקה/ הוספה/ עדכון
        //ומבצעת את הפקודה על המסד הפיזי
        {

            SqlConnection conn = ConnectToDb(fileName);
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            com.ExecuteNonQuery();
            com.Dispose();
            conn.Close();

        }


        /// <summary>
        /// To Execute update / insert / delete queries
        ///  הפעולה מקבלת שם קובץ ומשפט לביצוע ומחזירה את מספר השורות שהושפעו מביצוע הפעולה
        /// </summary>
        public static int RowsAffected(string fileName, string sql)//הפעולה מקבלת מסלול מסד נתונים ופקודת עדכון
        //ומבצעת את הפקודה על המסד הפיזי
        {

            SqlConnection conn = ConnectToDb(fileName);
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            int rowsA = com.ExecuteNonQuery();
            conn.Close();
            return rowsA;
        }

        /// <summary>
        /// הפעולה מקבלת שם קובץ ומשפט לחיפוש ערך - מחזירה אמת אם הערך נמצא ושקר אחרת
        /// </summary>
        public static bool IsExist(string fileName, string sql)//הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה אמת אם הנתונים קיימים ושקר אחרת
        {

            SqlConnection conn = ConnectToDb(fileName);
            conn.Open();
            SqlCommand com = new SqlCommand(sql, conn);
            SqlDataReader data = com.ExecuteReader();
            bool found;
            found = (bool)data.Read();// אם יש נתונים לקריאה יושם אמת אחרת שקר - הערך קיים במסד הנתונים
            conn.Close();
            return found;

        }

        public static DataTable ExecuteDataTable(string fileName, string sql)
        {
            SqlConnection conn = ConnectToDb(fileName);
            conn.Open();
            SqlDataAdapter tableAdapter = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            tableAdapter.Fill(dt);
            return dt;
        }
    }
}