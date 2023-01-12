function CustomerLogin()
{
    var validResult = validateLoginCredentials();
    if (validResult == false) {
        return false;
    }
   

    var customer = {
        EmailId: $("#email").val(),
        Password: $("#password").val(),
    //    Role:$("#role").val()
    };

    $.ajax
    ({
        type: 'POST',
        dataType: 'JSON',
        data: customer,
        url: "/CustomerLogin/LoginCustomer",
        success: function (result)
        {
            if (result!=null && result.success == true)
            {
                swal({
                    title: "Congratulations",
                    text: "LoggedIn Successfully",
                    icon: "success",
                    button:"OK"
                });
                window.location.href = '/Dashboard/GetProduct';               
            }               
        },
         error: function (errormessage) {
             swal({
                 title: "OOPS !",
                 text: "Login Failed",
                 icon: "error",
                 button: "OK"
             });
            }               
    });
}


function validateLoginCredentials()
{
    var isValid = true;
    if ($("#email").val().trim() == "") {
        $("#email").css('border-color', 'Red');
        isValid = false;
    }
    

    if ($("#password").val().trim() == "") {
        $("#password").css('border-color', 'Red');
        isValid=false
    }
    return isValid;
}