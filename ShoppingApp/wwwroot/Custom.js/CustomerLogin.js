function CustomerLogin() {
    var validResult = validateLoginCredentials();
    if (validResult == false) {
        return false;
    }


    var customer = {
        EmailId: $("#email").val(),
        Password: $("#password").val(),
        Role: $("#role").val()
    };

    $.ajax
        ({
            type: 'POST',
            dataType: 'JSON',
            data: JSON.stringify(customer),
            url: "/CustomerLogin/LoginCustomer",
            success: function (result) {
                if (result.success == true) {
                    alert("logged in successfully");
                }
            },
            error: function (errormessage) {
                alert("Please Enter Valid Credentials");
            }
        });
}


function validateLoginCredentials() {
    var isValid = true;
    if ($("#email").val().trim() == "") {
        $("#email").css('border-color', 'Red');
        isValid = false;
    }


    if ($("#password").val().trim() == "") {
        $("#password").css('border-color', 'Red');
        isValid = false
    }
    return isValid;
}