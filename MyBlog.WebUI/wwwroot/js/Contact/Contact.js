function SendMessage() {

    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");

    $.ajax({
        type: 'POST',
        url: '/Contact/SendMessage',
        dataType: 'json',
        data: {
            Name: $("#ContactFormViewModel_Name").val(),
            Email: $("#ContactFormViewModel_Email").val(),
            Subject: $("#ContactFormViewModel_Subject").val(),
            Message: $("#ContactFormViewModel_Message").val()
        },
        success: function (res) {

            if (!res.isValid) {
                $('#contact-error').empty();
                $.each(res.errorMessages, function (key, value) {
                    $("#contact-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                });
                $('#contact-error').text(res.errorMessage);
                $("#spinner").removeClass("d-block");
                $("#spinner").addClass("d-none");
            } else {

                $("#spinner").removeClass("d-block");
                $("#spinner").addClass("d-none");

                MyToast.fire({
                    icon: 'success',
                    title: "mesajınız iletildi"
                })
            }
        }
    });
    return false;

}