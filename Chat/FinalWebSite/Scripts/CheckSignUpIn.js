function checkin() {
    var formin = document.forms["signin"];
    var check = true;
    if (formin["emailin"].value.length < 6) {
        alert("wrong email");
        check = false;
    }
    if (formin["pwdin"].value.length < 8) {
        alert("wrong password");
        check = false;
    }
    if (check) alert("signin completed successfully");
    return check;

}

function checkup() {
    var formup = document.forms["signup"];
    var check = true;
    if (formup["emailup"].value.length < 6) {
        alert("wrong email");
        check = false;
    }
    if (formup["pwdup"].value.length < 8) {
        alert("wrong password");
        check = false;
    }
    if (formup["pwdup"].value != formup["pwdv"].value) {
        alert("password does not match the password validation");
        check = false;
    }

    if (check) alert("signup completed successfully");

    return check;



}
