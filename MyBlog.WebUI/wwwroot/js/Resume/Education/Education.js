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

$('body').on('click', '.opening_modal_edit', function () {
    let id = this.id;
    let eduTitleVal = $("#edu_title_" + id).text();
    let uniNameVal = $("#edu_uniName_" + id).text();
    let eduDateVal = $("#edu_date_" + id).val();
    let eduAdrsVal = $("#edu_adrss_" + id).val();
    let eduDescVal = $("#edu_desc_" + id).val();

    $('#EduTitle').val(eduTitleVal);
    $('#EduDateBetween').val(eduDateVal);
    $('#EduUniversityName').val(uniNameVal);
    $('#EduAdress').val(eduAdrsVal);
    $('#EduDescription').val(eduDescVal);


    $("#edit-educate-error").empty();

    $("#educateEdit").click(function () {
        EditEducationProcess(id);
    });
});

function EditEducationProcess(eduId) {

    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");
    let skillNameVal = $('#SkillEditName').val();
    let skilValueVal = $('#SkillEditValue').val();


    $.ajax({
        type: 'POST',
        url: '/Resume/EditEducation',
        dataType: 'json',
        data: {
            EducationId: eduId,
            Title: $('#EduTitle').val(),
            DateBetween: $('#EduDateBetween').val(),
            Adress: $('#EduAdress').val(),
            Description: $('#EduDescription').val() ,
            UniversityName: $('#EduUniversityName').val()
        },
        success: function (res) {

            $("#spinner").removeClass("d-block");
            $("#spinner").addClass("d-none");
            if (!res.isValid) {
                $('#edit-skill-error').empty();
                $.each(res.errorMessages, function (key, value) {
                    $("#edit-educate-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                });
                $('#edit-educate-error').text(res.errorMessage);
            } else {
                //simdilik inputlare güncellenen entity yi setliyoruz,append ile de yapabiliriz
                $("#edu_title_" + res.education.id).text(res.education.title);
                $("#edu_uniName_"+ res.education.id).text(res.education.universityName);
                $("#edu_date_" + res.education.id).text(res.education.dateBetween);                
                $("#edu_adrss_" + res.education.id).text(res.education.adress);
                $("#edu_desc_" + res.education.id).text(res.education.description);

                $(".eduDissmisClick").click();

                

                MyToast.fire({
                    icon: 'success',
                    title: "educate edited"
                })
            }


        }
    });
    return false;
}



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

                            <input type="hidden" id="edu_date_${res.education.id}" value="${res.education.dateBetween}" />
                            <input type="hidden" id="edu_adrss_${res.education.id}" value="${res.education.adress}" />
                            <input type="hidden" id="edu_desc_${res.education.id}" value="${res.education.description}" />

                            <td>
                                <div class="main__table-text">${res.education.id}</div>
                            </td>
                            <td>
                                <div id="edu_title_${res.education.id}" class="main__table-text">${res.education.title}</div>
                            </td>
                            <td>
                                <div id="edu_uniName_${res.education.id}" class="main__table-text">${res.education.universityName}</div>
                            </td>
                            <td>
                                <div class="main__table-text">1</div>
                            </td>

                            <td>
                                <div class="main__table-btns">
                                    <a href="#modal-edit-education" id="${res.education.id}"  class="main__table-btn main__table-btn--edit opening_modal_edit open-modal">
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