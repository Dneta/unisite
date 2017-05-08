function checkin() // בדיקת תקינות קלטים בדף הכניסה
{
    var formin = document.forms["signin"]; 
    var check = true;
    if (formin["emailin"].value.length < 6) // האם אורך המייל קטן מ6 תווים
    {
        alert("wrong email");
        check = false;
    }
    if (formin["pwdin"].value.length < 8) // האם אורך הסיסמה קטן מ8 תווים
    {
        alert("wrong passwoרd ");
        check = false;
    }
    return check;

}

function checkup() // בדיקת תקינות קלטים בדף ההרשמה
{
    var formup = document.forms["signup"];
    var check = true;
    if (formup["emailup"].value.length < 6) // האם אורך המייל קטן מ6 תווים
    {
        alert("wrong email");
        check = false;
    }
    if (formup["pwdup"].value.length < 8) // האם אורך הסיסמה קטן מ8 תווים
    {
        alert("wrong password");
        check = false;
    }
    if (formup["pwdup"].value != formup["pwdv"].value) // האם ווידוא הסיסמה זהה להזנת הסיסמה המקורית
     {
        alert("password does not match the password validation");
        check = false;
    }
    return check;



}
