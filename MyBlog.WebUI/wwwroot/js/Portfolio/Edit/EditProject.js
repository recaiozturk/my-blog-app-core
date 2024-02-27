$(document).ready(function () {
    $("#imageAdd").click(function () {
        AddPortfolioImageProcess();
    });
});

$('body').on('click', '.opening_modal_delete', function () {
    let id = this.id;
    $("#portfolioImageDelete").click(function () {
        DeleteImageFromPortfolio(id);
    });
});

function DeleteImageFromPortfolio(imageId) {

    $.ajax({
        type: 'POST',
        url: '/Portfolio/DeleteImageFromPortfolio',
        dataType: 'json',
        data: { ImageId: imageId },
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

function AddPortfolioImageProcess() {

    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");

    var fileInput = $('#imageInput')[0];
    var checkBoxInput = $('#checkBoxInput')[0];
    var pId = $('#portfId').val();

    if (fileInput.files.length > 0) {

        var formData = new FormData();
        formData.append('ImageFile', fileInput.files[0]);
        formData.append('IsDefault', checkBoxInput.checked);
        formData.append('PortfolioId', pId);

        $.ajax({
            type: 'POST',
            url: '/Portfolio/AddImageForPortfolio',
            dataType: 'json',
            data: formData,
            contentType: false,
            processData: false,

            success: function (res) {

                $("#spinner").removeClass("d-block");
                $("#spinner").addClass("d-none");

                if (!res.isValid) {
                    $('#add-image-error').empty();
                    $.each(res.errorMessages, function (key, value) {
                        $("#add-image-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                    });
                    $('#add-image-error').text(res.errorMessage);
                } else {

                    $(".skillDissmisClick").click();
                    $('#add-image-error').empty();

                    MyToast.fire({
                        icon: 'success',
                        title: "added successufuly",
                    })
                    location.reload();
                }
            }
        });

        return false;
    } else {

        $("#spinner").removeClass("d-block");
        $("#spinner").addClass("d-none");

        $('#add-image-error').text("Please choose a image file");
    }
}
