<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="DeleteFriend.aspx.cs" Inherits="DeleteFriend" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
 <link rel="stylesheet" type="text/css" href"=Styles/deletefriend.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="container">

        <div class="font">
            <h1>
                Delete Friend:
            </h1>
            <br/>
            <form>
                <label for="UserName">Username:</label>
                <input type="text" class="form-control" id="Username" name="un">
                <button type="submit" class="btn btn-info">Delete</button>
            </form>
        </div>
    </div>
</asp:Content>

