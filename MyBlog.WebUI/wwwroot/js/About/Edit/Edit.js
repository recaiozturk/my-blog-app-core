$(document).ready(function () {
    $("#skillAdd").click(function () {
        AddSkillProcess()
    });
});

$('body').on('click', '.opening_modal_edit', function () {
    let id = this.id;
    let sValue = $("#skillv_" + id).text();
    let sName = $("#skilln_" + id).text();

    $('#SkillEditName').val(sName);
    $('#SkillEditValue').val(sValue.replace(/%/g, ''));
    $("#edit-skill-error").empty();

    $("#skillEdit").click(function () {
        EditSkillProcess(id);
    });
});

$('body').on('click', '.opening_modal_delete', function () {
    let id = this.id;
    $("#skillDelete").click(function () {
        DeleteSkillProcess(id);
    });
});


function AddSkillProcess() {

    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");
    let idVal = $('#AboutId').val();
    let skillNameVal = $('#SkillName').val();
    let skilValueVal = $('#SkillValue').val();

    $.ajax({
        type: 'POST',
        /*url: '@Url.Action("AddSkillToAbout")',*/
        url: '/About/AddSkillToAbout',
        dataType: 'json',
        data: {
            AboutId: idVal,
            SkillName: skillNameVal,
            SkillValue: skilValueVal,
        },
        success: function (res) {

            $("#spinner").removeClass("d-block");
            $("#spinner").addClass("d-none");
            if (!res.isValid) {
                $('#add-skill-error').empty();
                $.each(res.errorMessages, function (key, value) {
                    $("#add-skill-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                });
                $('#add-skill-error').text(res.errorMessage);
            } else {

                $(".skillDissmisClick").click();
                $('#SkillName').val("");
                $('#SkillValue').val("");
                $("#skillTbody").append(`

                            <tr id="tr_${res.skill.id}">
                                <td>
                                    <div class="main__table-text">${res.skill.id}</div>
                                </td>

                                <td>
                                    <div id="skilln_${res.skill.id}" class="main__table-text">${res.skill.skillName}</div>
                                </td>
                                <td>
                                    <div id="skillv_${res.skill.id}" class="main__table-text">%${res.skill.skillValue}</div>
                                </td>
                                <td>
                                    <div class="main__table-text">1</div>
                                </td>

                                <td>
                                    <div class="main__table-btns">
                                        <a  href="#modal-edit-skill" id="${res.skill.id}" class="main__table-btn main__table-btn--edit opening_modal_edit open-modal">
                                            <i class="icon ion-ios-create" id="${res.skill.id}"></i>
                                        </a>
                                        <a href="#modal-delete"   id="${res.skill.id}" class="main__table-btn main__table-btn--delete opening_modal_delete open-modal">
                                            <i class="icon ion-ios-trash"></i>
                                        </a>
                                    </div>
                                </td>
                              </tr>

                        `);

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


function EditSkillProcess(skillID) {

    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");
    let skillNameVal = $('#SkillEditName').val();
    let skilValueVal = $('#SkillEditValue').val();


    $.ajax({
        type: 'POST',
        /*url: '@Url.Action("EditSkillForAbout")',*/
        url: '/About/EditSkillForAbout',
        dataType: 'json',
        data: {
            SkillId: skillID,
            SkillName: skillNameVal,
            SkillValue: skilValueVal,
        },
        success: function (res) {

            $("#spinner").removeClass("d-block");
            $("#spinner").addClass("d-none");
            if (!res.isValid) {
                $('#edit-skill-error').empty();
                $.each(res.errorMessages, function (key, value) {
                    $("#edit-skill-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                });
                $('#edit-skill-error').text(res.errorMessage);
            } else {
                $(".skillDissmisClick").click();
                $("#skillv_" + skillID).text("%" + res.skill.skillValue.toString());
                $("#skilln_" + skillID).text(res.skill.skillName);
            }


        }
    });
    return false;
}


function DeleteSkillProcess(id) {
    $("#spinner").removeClass("d-none");
    $("#spinner").addClass("d-block");


    $.ajax({
        type: 'POST',
        /*url: '@Url.Action("DeleteSkillForAbout")',*/
        url: '/About/DeleteSkillForAbout',

        dataType: 'json',
        data: {
            skillId: id
        },
        success: function (res) {

            $("#spinner").removeClass("d-block");
            $("#spinner").addClass("d-none");
            if (!res.isValid) {
                $('#delete-skill-error').empty();
                $.each(res.errorMessages, function (key, value) {
                    $("#delete-skill-error").append("<span class='model-danger mt-3'>" + value + '</span><br/>');
                });
                $('#delete-skill-error').text(res.errorMessage);
            } else {
                $(".skillDissmisClick").click();
                $("#tr_" + id).remove();
            }
        }
    });
    return false;

}


function showImagePreview(input) {
    if (input.files[0]) {
        var filerdr = new FileReader();
        filerdr.onload = function (e) {
            $('#form__img').attr('src', e.target.result);
        }
        filerdr.readAsDataURL(input.files[0]);
    }
}