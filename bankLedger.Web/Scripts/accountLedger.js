const CreateLedger = function() {
    $("#badAmount, #badRequest").addClass("d-none");
    const TransactionType = $("#transactionType").val();
    const Amount = $("#amount").val();

    if(!Amount || isNaN(parseFloat(Amount))) {
        $("#badAmount").removeClass("d-none");
        return;
    }

    const dto = {
        "TransactionType": TransactionType,
        "Amount": Amount,
        };

    const postData = JSON.stringify(dto);
    const ajaxObject = {
            cache: false,
            method: "POST",
            url: "/Account/CreateAccountLedger/",
            contentType: "application/json",
            data: postData,
            success: function (data) {
                ShowNewLedger(data);
                $("#amount").val("");
                $("#addLedgerModal").modal("hide");
                
            },
            error: function(data) {
                $("#badRequest").removeClass("d-none");
            }
        };
    $.ajax(ajaxObject);
}

const ShowNewLedger = function(ledgerView) {
    $("#ledgerBody").append(ledgerView);
}

$(document).ready(function() {
    const $newLedgerBtn = $("#addLedger");
    if($newLedgerBtn.length)
        $newLedgerBtn.on("click", CreateLedger);
});