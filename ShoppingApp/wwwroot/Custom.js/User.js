function Signup() {
    var UserObj = {
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        ConfirmPassword: $('#ConfirmPassword').val(),
        phoneNumber: $('#phoneNumber').val(),
        policyFlag: $('#policyFlag').val(),
        Role: $('#role').val()
    };


    $.ajax({
        type: "POST",
        dataType: "JSON",
        data: UserObj,
        url: "/User/Register",
        success: function (response) {
            if (response.success) {
                ShowSuccess(UserObj.firstName);
            } else {
                alert(response.message);
            }
        },
        error: function (errormessgae) {
            alert(errormessgae);
        }
    });
    return false;
}

function ShowSuccess(userName) {
    window.location.href = '/User/RegistrationSuccess/';
}

function EmailExists() {
    if (document.getElementById("lblEmailError").innerHTML.length <= 0) {
        var inputEmail = {
            email: $('#email').val(),
        };

        $.ajax({
            type: "GET",
            dataType: "JSON",
            data: inputEmail,
            url: "/User/EmailExists",
            success: function (response) {
                if (response.success) {
                    document.getElementById("IdEmailExists").innerHTML = "Email already exists";
                    document.getElementById("IdEmailExists").classList.remove("text-success");
                    document.getElementById("IdEmailExists").classList.add("text-danger");
                    document.getElementById("signup").disabled = true;
                } else {
                    document.getElementById("IdEmailExists").innerHTML = "Email available";
                    document.getElementById("IdEmailExists").classList.remove("text-danger");
                    document.getElementById("IdEmailExists").classList.add("text-success");
                    document.getElementById("signup").disabled = false;
                }
            },
            error: function (errormessgae) {
                alert(errormessgae);
            }
        });
    }
}



