function ValidateEmail(email) {
    var x = email.value;
    if (x == '') return true;
    var atpos = x.indexOf("@");
    var dotpos = x.lastIndexOf(".");
    if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
        alert("Not a valid e-mail address");
        email.focus();
        return false;
    }
    return true;
}

function ValidateBlank(text, field) {
    var x = text.value;
    if (x == null || x.trim() == "") {
        alert(field + " can not be blank.");
        window.setTimeout(function() { text.focus(); }, 200);
        return false;
    }
    return true;
}

function ValidatePasswordLength(pass) {
    if (pass.value.length < 6) {
        alert("Password must be 6 character long");
        pass.focus();
        return false;
    }
    return true;
}

function ConfirmPass(pass, pass2) {
    if (pass.value != pass2.value) {
        alert("Confirm password does not match");
        return false;
    }
    return true;
}
function IsValidDate(myDate) {
    var filter = /^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][u]l|[aA][Uu][gG]|[Ss][eE][pP]|[oO][Cc]|[Nn][oO][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/
    
    if (filter.test(myDate) == false) {
        alert('Invalid Date');
        return false;
    }
    else
        return true;
}