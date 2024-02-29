$('body').on('click', '.opening_modal_delete', function () {
    let id = this.id;
    $("#messageDelete").click(function () {
        DeleteMessage(id);
    });
});

function DeleteMessage(messageId) {

    $.ajax({
        type: 'POST',
        url: '/Contact/DeleteMessage',
        dataType: 'json',
        data: { MessageId: messageId },
        success: function (res) {
            if (!res.isValid) {
                alert(res.errorMessage)
            }
            else {
                MyToast.fire({
                    icon: 'success',
                    title: "deleted successufuly",
                })

                location.reload();
            }
        }
    });
}