function Signup() {
    var UserObj = {
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        ConfirmPassword: $('#ConfirmPassword').val(),
        phoneNumber: $('#phoneNumber').val(),
        policyFlag: $('#policyFlag').val(),
        Role: 'Admin'
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



