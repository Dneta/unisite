<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="DeleteFriend.aspx.cs" Inherits="DeleteFriend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" type="text/css" href="Styles/deletefriend.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container">
        <div class="font">
            <!-- מטרת דף זה היא למחוק אנשי קשר לא רצויים -->
            <h1>
                Delete Friend:
                <!-- כותרת -->
            </h1>
            <br />
            <!-- טופס של צ'אקבוקס עם אנשי קשר של המשתמש -->
            <form action="DeleteFriend.aspx" method="post">
            <!-- הדפסת צ'אקבוקס -->
            <%=CB %>
            <!-- כפתור לשליחת המידע המתקבל -->
            <button type="submit" class="btn btn-info"> Delete</button>  
            </form>
        </div>
    </div>
</asp:Content>
