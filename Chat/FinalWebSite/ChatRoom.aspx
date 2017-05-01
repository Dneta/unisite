﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ChatRoom.aspx.cs" Inherits="ChatRoom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link rel="stylesheet" type="text/css" href="Styles/chatroom.css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <%=messages %>
    </div>
    <div class="container-fluid">
     <div class="navbar navbar-fixed-buttom">
        <form id="respond" onsubmit="return true;" action="ChatRoom.aspx" method="post">
        <div class="row">
            <div class="form-group col-sm-8">
                <input type="text" class="form-control" name="newresp">
            </div>
            <div class="form-group col-sm-4">
                <button class="btn">
                    Push Me Buttons</button>
            </div>
        </div>
        </div>
</asp:Content>
