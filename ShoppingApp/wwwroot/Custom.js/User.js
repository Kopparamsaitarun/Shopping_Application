$('#btnSave').click(function () {
    var UserObj = {
        firstName: $('#firstName').val(),
        lastName: $('#lastName').val(),
        email: $('#email').val(),
        password: $('#password').val(),
        phoneNumber: $('#phoneNumber').val(),
        ConfirmPassword: $('#ConfirmPassword').val(),
        policyFlag: $('#policyFlag').val(),
        role: '"Admin"'
    };

    //var UserObj1 = "{"
    //    + '"firstName" : "' + $('#firstName').val() + '",'
    //    + '"lastName" : "' + $('#lastName').val() + '",'
    //    + '"email" : "' + $('#email').val() + '",'
    //    + '"password" : "' + $('#password').val() + '",'
    //    + '"phoneNumber" : "' + $('#phoneNumber').val() + '",'
    //    + '"policyFlag" : "' + $('#policyFlag').val() + '",'
    //    + '"role" : "Admin"'
    //    + "}";
    //console.log(UserObj);
    //console.log(UserObj);
    //var SendInfo = [];
    //SendInfo.push(UserObj);

    UserOperations.SaveUserDetails(UserObj);
    //UserOperations.SaveUserDetails(UserObj);
});

$('#btnSample').click(function () {
    var SampleObj = {
        Id: 100
    };
    console.log(SampleObj);
    UserOperations.SaveSample(SampleObj);
});
   
var UserOperations = {
    SaveUserDetails: function (Userdata) {
        $.ajax({
            url: "/User/Register",
            type: "post",
            data: Userdata,
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
    } ,   
    
    SaveSample: function (SampleData) {
        $.ajax({
            url: "/User/Sample",
            type: "POST",
            data: SampleData,
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
