const SubmitLogin = function()
{

    const password = $("#password").val();
    const dto = {
        "UserName": $("#userName").val(),
        "DecryptedPassword": password
        };

    const postData = JSON.stringify(dto);
    const ajaxObject = {
            cache: false,
            method: "POST",
            url: "/Login/AttemptLogin/",
            contentType: "application/json",
            data: postData,
            success: function (data) {
                if(data.data)
                    location.href = "/Account/AccountLedger/";
                else 
                    $("#badLogin").removeClass("d-none");
            },
            error: function(data) {
                $("#badLogin").removeClass("d-none");
            }
        };
    $.ajax(ajaxObject);
}

$(document).ready(function() {
    const $loginBtn = $("#Login");
    if($loginBtn.length > 0)
        $loginBtn.on("click", SubmitLogin);
});
