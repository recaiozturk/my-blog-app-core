$('body').on('click', '.opening_modal_delete', function () {
    let id = this.id;
    $("#expDelete").click(function () {
        DeleteExpProcess(id);
    });
});


function DeleteExpProcess(id) {
    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");
    $.ajax({
        type: 'POST',
        url: '/Resume/DeleteExperience',
        dataType: 'json',
        data: {
            ExpId: id
        },
        success: function (res) {


            if (!res.isValid) {
                $('#delete-exp-error').empty();
                $.each(res.errorMessages, function (key, value) {
                    $("#delete-exp-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                });
                $('#delete-exp-error').text(res.errorMessage);
            } else {
                $(".skillDissmisClick").click();
                $("#tr_" + id).remove();

                $("#spinner").removeClass("d-block");
                $("#spinner").addClass("d-none");

                MyToast.fire({
                    icon: 'success',
                    title: "experience deleted"
                })
            }
        }
    });
    return false;

}