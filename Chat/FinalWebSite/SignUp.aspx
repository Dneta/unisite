<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
       SignUp
    </title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"
        crossorigin="anonymous">
         <link rel="stylesheet" type="text/css" href="formcss.css"/>
        <!-- Optional theme -->
        <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp"
            crossorigin="anonymous">

            <!-- Latest compiled and minified JavaScript -->
            <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js" integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
                crossorigin="anonymous"></script>
                <script src="Scripts/CheckSignUpIn.js" type="text/javascript"></script>
</head>
<body>
    <div class="container">
                <div class="font">
        <h1 style=" font-size: 300%"> sign Up</h1>
        <form id="signup" onsubmit="return checkup();" action="SignUp.aspx" method="post">
        <label for="username">User Name:</label>
                <input type="text" class="form-control" id="username" name = "username">
            <label for="firstname">First Name:</label>
                <input type="text" class="form-control" id="firstname" name = "firstname">
                <label for="lasttname">Last Name:</label>
                <input type="text" class="form-control" id="lastname" name = "lastname">
            <input type="radio" name="gender" id="male" value="male" checked> Male<br>
            <input type="radio" name="gender" id="female" value="female"> Female<br>
            <div class="form-group">
                <label for="email">Email address:</label>
                <input type="email" class="form-control" id="emailup" name="email">
            </div>
            <div class="form-group">
                <label for="pwd">Password:</label>
                <input type="password" class="form-control" id="pwdup " name="password">
            </div>
  <div class="form-group">
                <label for="pwdv">Password validation:</label>
                <input type="password" class="form-control" id="pwdv">
            </div>
            <button type="submit" class="btn btn-success">Submit</button>
        </form>
        </div>
    </div>
    <div>
    
    </div>
    </form>
</body>
</html>
