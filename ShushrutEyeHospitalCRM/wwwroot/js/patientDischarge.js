
$('#btnReset').on('click', function () {
    $('#frmDischargePatient').resetForm();
});


$('#btnSubmit').on('click', function () {
    CreatePatientDischarge();
});
$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });

});
function CreatePatientDischarge() {
    debugger;
    var editor = tinymce.get('txtDischargeDetail');

    if (editor) {
        var content = editor.getContent();
        console.log(content);
    } 
    if (content == "" || content == undefined) {
        $("#txtDischargeDetail").focus();
        return false;
    }

    var patientView = {};
    patientView.DischargeDetail = content; //$("#txtDischargeDetail").val();
    patientView.PatientId = $("#txtPatientId").val();


    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(patientView),
        url: GetRoute("/Patient/DischargePatient"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "Patient Discharg Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                $('#frmDischargePatient').trigger("reset");
            }
            else {
                swal({
                    title: "Discharge Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function (error) {
            console.log(error);
            swal({
                title: "Patient Discharge creating failed",
                type: "warning",
                text: "Please try again later!",
                confirmButtonColor: "#00B4B4",
            });
            setTimeout(function () {
                $("#overlay").fadeOut(300);
            }, 500);
        }
    }).done(function () {
        setTimeout(function () {
            $("#overlay").fadeOut(300);
        }, 500);
    });
}
