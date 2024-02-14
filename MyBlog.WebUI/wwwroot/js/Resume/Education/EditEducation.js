$(document).ready(function () {
    $("#educateAdd").click(function () {
        AddEducatelProcess()
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
        url: '/Resume/EditEducation',
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