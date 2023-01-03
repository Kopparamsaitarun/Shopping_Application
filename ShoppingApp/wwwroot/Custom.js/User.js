$('#btnSave').click(function () {
    var UserObj = {
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        phoneNumber: $('#phoneNumber').val(),
        ConfirmPassword: $('#ConfirmPassword').val(),
        policyFlag: $('#policyFlag').val(),
        role: 'Admin'
    };

    $.ajax({
        type: "POST",
        dataType: "JSON",
        data: UserObj,
            url: "/User/Register",            
            contentType: "application/json; charset=utf-8",
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
    
});


