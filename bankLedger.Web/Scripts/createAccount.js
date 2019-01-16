const SubmitCreateAccount = function()
{
    $("#badRequest").addClass("d-none");
    $("#badPassword").addClass("d-none");
    $("#noMatch").addClass("d-none");
    $("#badUserName").addClass("d-none");

    const userName = $("#userName").val();
    const password = $("#password").val();
    const verifyPassword = $("#passwordVerification").val();


    if(!userName) {
        $("#badUserName").removeClass("d-none");
        return;
    }

    if(!password) {
        $("#badPassword").removeClass("d-none");
        return;
    }

    if(password !== verifyPassword || !password){
        $("#noMatch").removeClass("d-none");
        return;
    }

    const dto = {
        "UserName": userName,
        "Password": password
        };

    const postData = JSON.stringify(dto);
    const ajaxObject = {
            cache: false,
            method: "POST",
            url: "CreateAccountSubmit/",
            contentType: "application/json",
            data: postData,
            success: function (data) {
                location.href = "/Login/";
            },
            error: function(data) {
                $("#badRequest").removeClass("d-none");
            }
        };
    $.ajax(ajaxObject);
}

$(document).ready(function() {
    const $createBtn = $("#createUser");
    if($createBtn.length > 0)
        $createBtn.on("click", SubmitCreateAccount);
    $(".login-input").on("keyup", function (event) {
        event.preventDefault();
        if (event.keyCode === 13) {
            SubmitCreateAccount();
        }
    });
});
