function SingupRegister() {
    var firstName = $('#firstName').val();

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
    var dummy = UserObj;

    $.ajax({
        type: "POST",
        dataType: "JSON",
        data: UserObj,
        url: "/User/Register",
        success: function (result) {
            if (result == true) {
                //Clearform();
                alert("Records added succesfully")
            }
        },
        error: function (errormessgae) {
            alert(errormessgae);
        }

    });

}




