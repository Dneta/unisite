<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignIn.aspx.cs" Inherits="SingUpIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SignIn </title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css"
        integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"
        crossorigin="anonymous">
    <link rel="stylesheet" type="text/css" href="Styles/formcss.css" />
    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css"
        integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp"
        crossorigin="anonymous">
    <!-- Latest compiled and minified JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"
        integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa"
        crossorigin="anonymous"></script>
    <script src="Scripts/CheckSignUpIn.js" type="text/javascript"></script>
</head>
<body>
    <!-- מטרת דף זה היא לאפשר כניסה לאתר עם חשבון קיים -->
    <div class="container">
        <div class="font">
            <!-- כותרת  -->
            <h1 style="font-size: 300%">
                sign in</h1>
            <form id="signin" onsubmit="return checkin()" action="SignIn.aspx" method="post">
            <div class="form-group">
                <!-- שדה עבור מייל -->
                <label for="email">
                    Email address:</label>
                <input type="email" class="form-control" id="emailin" name="email">
            </div>
            <div class="form-group">
                <!-- שדה עבור סיסמה -->
                <label for="pwd">
                    Password:</label>
                <input type="password" class="form-control" id="pwdin" name="password">
            </div>
            <!-- כפתור לשליחת המידע -->
            <button type="submit" class="btn btn-success">
                Submit</button>
            </form>
            <br />
            <%=message %> 
            <!--אם הנתונים לא קיימים במסד-תתקבל על כך הודעה-->
            <br />
            <br />
            <!-- קישור להרשמה -->
            <h1>
                If you don't have a user-sign up
            </h1>
            <a href="SignUp.aspx">
                <button class="btn btn-success">
                    Click here!
                </button>
            </a>
        </div>
    </div>
</body>
</html>
