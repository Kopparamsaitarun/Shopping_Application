$('#btnSave').click(function () {
    var UserObj = {
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        phoneNumber: $('#phoneNumber').val(),
        policyFlag: $('#policyFlag').val()
    };
    console.log(UserObj);
    UserOperations.SaveUserDetails(UserObj);
});
   
var UserOperations = {
    SaveUserDetails: function (Userdata) {
        $.ajax({
            url: "/User/SignUp",
            type: "POST",
            data: JSON.stringify(Userdata),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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
};
