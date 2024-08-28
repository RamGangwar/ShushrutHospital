
///////////////////---------------------------------------------reset button click functionality-----------------------------------------------------///////////////////////
$('#btnReset').on('click', function () {
    $('#frmPatientCounseling').resetForm();
});


///////////////////---------------------------------------------save button click functionality-----------------------------------------------------///////////////////////
$('#btnSubmit').on('click', function () {
    CreatePatientHistory();
});
$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });

});
////////////////////////////---------------------------after validation save form data to the controller---------------------------///////////////////////////////////
function CreatePatientHistory() {
    //here implement the ajax functionality for submit (POST) form data to controller
    var counselingModel = {};
    counselingModel.PatientId = $("#txtPatientId").val();
    counselingModel.MRNO = $("#txtMRNO").val();
    counselingModel.Diagnosis = $("#txtDiagnosis").val();
    counselingModel.OperatedEye = $("input[name='OperatedEye']:checked").val();
    counselingModel.ProcedureName = $("#txtProcedureName").val();
    counselingModel.PackageName = $("#txtPackageName").val();
    counselingModel.AdditionalProcedure = $("#txtAdditionalProcedure").val();
    counselingModel.AnesthesiaType = $("#txtAnesthesiaType").val();
    counselingModel.PatientOrParty = $("#txtPatientOrParty").val();
    counselingModel.Remarks = $("#txtRemarks").val();
    counselingModel.ApproxCharge = $("#txtApproxCharge").val();
    counselingModel.DoctorName = $("#DoctorName").val();
    //counselingModel.CounsellorName = $("#txtCounsellorName").val();
    counselingModel.Status = $("input[name='Status']:checked").val() == 1 ? true : false;
    counselingModel.BookingDate = $("#txtBookingDate").val();
    //var datetimme = $("#txtSurgeryDateTime").val() + " " + $("#txtSurgeryTime").val()+":00";
    counselingModel.SurgeryDateTime =  $("#txtSurgeryDateTime").val();

    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(counselingModel),
        url: GetRoute("/Patient/PatientCounseling"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "Patient Counseling Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                $('#frmPatientCounseling').trigger("reset");
            }
            else {
                swal({
                    title: "Patient Counseling Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function (error) {
            console.log(error);
            swal({
                title: "Patient Counseling creating failed",
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
