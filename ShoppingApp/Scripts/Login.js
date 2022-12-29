$("#loginForm").click(function () {
    event.preventDefault();
    $.ajax({
        type: "POST",
        datatype: "json",
        contentType: "application/json; charset=utf-8",
        url: "https://localhost:44316/api/CustomerLogin/LoginCustomer",
        data: ({
            email: $('#username').val(),
            password: $('#pswd').val()
        }),
        success: function (result) {
            if (result && result.auth_token.length > 1) // you should do your checking here
            {
                window.location = 'http://www.google.com/'; //just to show that it went through
            }
            else {
                $('#result').empty().addClass('error')
                    .append('Something is wrong.');
            }
        }
    });
    return false;
}