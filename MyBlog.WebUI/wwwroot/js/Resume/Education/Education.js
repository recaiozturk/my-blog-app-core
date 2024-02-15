$(document).ready(function () {
    $("#educateAdd").click(function () {
        AddEducatelProcess()
    });
});
$('body').on('click', '.opening_modal_delete', function () {
    let id = this.id;
    $("#eduDelete").click(function () {
        DeleteEducationProcess(id);
    });
});



function AddEducatelProcess() {

    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");

    let Title = $('#Title').val();
    let DateBetween = $('#DateBetween').val();
    let UniversityName = $('#UniversityName').val();
    let Adress = $('#Adress').val();
    let Description = $('#Description').val();

    $.ajax({
        type: 'POST',
        url: '/Resume/CreateEducation',
        dataType: 'json',
        data: {
            Title: Title,
            DateBetween: DateBetween,
            UniversityName: UniversityName,
            Adress: Adress,
            Description: Description,
        },
        success: function (res) {

            $("#spinner").removeClass("d-block");
            $("#spinner").addClass("d-none");
            if (!res.isValid) {
                $('#add-education-error').empty();
                $.each(res.errorMessages, function (key, value) {
                    $("#add-education-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                });
                $('#add-education-error').text(res.errorMessage);
            } else {

                $(".skillDissmisClick").click();

                $('#add-education-error').empty();
                ClearFields();
                $("#eduTbody").append(`

                        <tr id="tr_${res.education.id}">
                            <td>
                                <div class="main__table-text">${res.education.id}</div>
                            </td>
                            <td>
                                <div id="" class="main__table-text">${res.education.title}</div>
                            </td>
                            <td>
                                <div id="" class="main__table-text">${res.education.universityName}</div>
                            </td>
                            <td>
                                <div class="main__table-text">1</div>
                            </td>

                            <td>
                                <div class="main__table-btns">
                                    <a href="#modal-edit-skill" class="main__table-btn main__table-btn--edit opening_modal_edit open-modal">
                                        <i class="icon ion-ios-create "></i>
                                    </a>
                                    <a href="#modal-delete" id="${res.education.id}" class="main__table-btn main__table-btn--delete opening_modal_delete open-modal">
                                        <i class="icon ion-ios-trash"></i>
                                    </a>
                                </div>
                            </td>
                        </tr>
                 `);

                MyToast.fire({
                    icon: 'success',
                    title: "added successufuly2",
                })

                $('.open-modal').magnificPopup({
                    fixedContentPos: true,
                    fixedBgPos: true,
                    overflowY: 'auto',
                    type: 'inline',
                    preloader: false,
                    focus: '#username',
                    modal: false,
                    removalDelay: 300,
                    mainClass: 'my-mfp-zoom-in',
                });
            }
        }
    });
    return false;
}

function DeleteEducationProcess(id) {
    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");
    $.ajax({
        type: 'POST',
        url: '/Resume/DeleteEducation',
        dataType: 'json',
        data: {
            EduId: id
        },
        success: function (res) {

           
            if (!res.isValid) {
                $('#delete-edu-error').empty();
                $.each(res.errorMessages, function (key, value) {
                    $("#delete-edu-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                });
                $('#delete-edu-error').text(res.errorMessage);
            } else {
                $(".skillDissmisClick").click();
                $("#tr_" + id).remove();

                $("#spinner").removeClass("d-block");
                $("#spinner").addClass("d-none");

                MyToast.fire({
                    icon: 'success',
                    title: "skill deleted"
                })
            }
        }
    });
    return false;

}

function ClearFields() {
    $('#Title').val("");
    $('#DateBetween').val("");
    $('#UniversityName').val("");
    $('#Adress').val("");
    $('#Description').val("");
}