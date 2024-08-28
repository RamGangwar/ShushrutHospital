
///////////////////---------------------------------------------reset button click functionality-----------------------------------------------------///////////////////////
$('#btnReset').on('click', function () {
    $('#frmPatientHistory').resetForm();
});

///////////////////---------------------------------------------save button click functionality-----------------------------------------------------///////////////////////
$('#btnSave').on('click', function () {
    CreatePatientHistory();
});

///////////////////---------------------------------------------save button click functionality-----------------------------------------------------///////////////////////
$('#btnSubmit').on('click', function () {
    UpdatePatientHistory();
});
$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });

});
////////////////////////////---------------------------after validation save form data to the controller---------------------------///////////////////////////////////
function CreatePatientHistory() {
    //here implement the ajax functionality for submit (POST) form data to controller
    var patientHistoryViewModel = {};
    patientHistoryViewModel.PatientId = $('#PatientId').val();
    patientHistoryViewModel.MethodName = $('#MethodName').val();
    patientHistoryViewModel.IOPTime = $('#IOPTime').val();
    patientHistoryViewModel.IOPRE = $('#IOPRE').val();
    patientHistoryViewModel.IOPLE = $('#IOPLE').val();
    patientHistoryViewModel.PRE = $('#PRE').val();
    patientHistoryViewModel.PLE = $('#PLE').val();
    patientHistoryViewModel.Remarks = $('#Remarks').val();
    patientHistoryViewModel.DistanceVisionREWithGlass = $('#DistanceVisionREWithGlass').val();
    patientHistoryViewModel.DistanceVisionREWithPinhole = $('#DistanceVisionREWithPinhole').val();
    patientHistoryViewModel.DistanceVisionLEWithGlass = $('#DistanceVisionLEWithGlass').val();
    patientHistoryViewModel.DistanceVisionLEWithPinhole = $('#DistanceVisionLEWithPinhole').val();
    patientHistoryViewModel.NearVisionREWithGlass = $('#NearVisionREWithGlass').val();
    patientHistoryViewModel.NearVisionREWithPinhole = $('#NearVisionREWithPinhole').val();
    patientHistoryViewModel.NearVisionLEWithGlass = $('#NearVisionLEWithGlass').val();
    patientHistoryViewModel.NearVisionLEWithPinhole = $('#NearVisionLEWithPinhole').val();
    patientHistoryViewModel.DistanceVisionREUnAided = $("#DistanceVisionREUnAided").val();
    patientHistoryViewModel.DistanceVisionLEUnAided = $("#DistanceVisionLEUnAided").val();
    patientHistoryViewModel.NearVisionREUnAided = $("#NearVisionREUnAided").val();
    patientHistoryViewModel.NearVisionLEUnAided = $("#NearVisionLEUnAided").val();
    var AdvGlasses = {};
    if ($("#DistanceRE_Sph").val() != "" ||
        $("#DistanceRE_Cy").val() != "" ||
        $("#DistanceRE_Axis").val() != "" ||
        $("#DistanceRE_Prism").val() != "" ||
        $("#DistanceRE_VA").val() != "" ||
        $("#DistanceRE_NV").val() != "" ||
        $("#DistanceLE_Sph").val() != "" ||
        $("#DistanceLE_Cy").val() != "" ||
        $("#DistanceLE_Axis").val() != "" ||
        $("#DistanceLE_Prism").val() != "" ||
        $("#DistanceLE_VA").val() != "" ||
        $("#DistanceLE_NV").val() != ""
        ) {

        AdvGlasses.PatientId = $('#PatientId').val();
        AdvGlasses.DistanceRE_Sph = $("#DistanceRE_Sph").val();
        AdvGlasses.DistanceRE_Cy = $("#DistanceRE_Cy").val();
        AdvGlasses.DistanceRE_Axis = $("#DistanceRE_Axis").val();
        AdvGlasses.DistanceRE_Prism = $("#DistanceRE_Prism").val();
        AdvGlasses.DistanceRE_VA = $("#DistanceRE_VA").val();
        AdvGlasses.DistanceRE_NV = $("#DistanceRE_NV").val();
        AdvGlasses.DistanceLE_Sph = $("#DistanceLE_Sph").val();
        AdvGlasses.DistanceLE_Cy = $("#DistanceLE_Cy").val();
        AdvGlasses.DistanceLE_Axis = $("#DistanceLE_Axis").val();
        AdvGlasses.DistanceLE_Prism = $("#DistanceLE_Prism").val();
        AdvGlasses.DistanceLE_VA = $("#DistanceLE_VA").val();
        AdvGlasses.DistanceLE_NV = $("#DistanceLE_NV").val();
    }
    if ($("#AddRE_Sph").val() != "" ||
        $("#AddRE_Cy").val() != "" ||
        $("#AddRE_Axis").val() != "" ||
        $("#AddRE_Prism").val() != "" ||
        $("#AddRE_VA").val() != "" ||
        $("#AddRE_NV").val() != "" ||
        $("#AddLE_Sph").val() != "" ||
        $("#AddLE_Cy").val() != "" ||
        $("#AddLE_Axis").val() != "" ||
        $("#AddLE_Prism").val() != "" ||
        $("#AddLE_VA").val() != "" ||
        $("#AddLE_NV").val() != "") {
        AdvGlasses.AddRE_Sph = $("#AddRE_Sph").val();
        AdvGlasses.AddRE_Cy = $("#AddRE_Cy").val();
        AdvGlasses.AddRE_Axis = $("#AddRE_Axis").val();
        AdvGlasses.AddRE_Prism = $("#AddRE_Prism").val();
        AdvGlasses.AddRE_VA = $("#AddRE_VA").val();
        AdvGlasses.AddRE_NV = $("#AddRE_NV").val();
        AdvGlasses.AddLE_Sph = $("#AddLE_Sph").val();
        AdvGlasses.AddLE_Cy = $("#AddLE_Cy").val();
        AdvGlasses.AddLE_Axis = $("#AddLE_Axis").val();
        AdvGlasses.AddLE_Prism = $("#AddLE_Prism").val();
        AdvGlasses.AddLE_VA = $("#AddLE_VA").val();
        AdvGlasses.AddLE_NV = $("#AddLE_NV").val();

        patientHistoryViewModel.PatientAdvGlasses = AdvGlasses;
    }

    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(patientHistoryViewModel),
        url: GetRoute("/Patient/PatientCheckup"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "Patient Checkup Details Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                $('#frmPatientHistory').trigger("reset");
            }
            else {
                swal({
                    title: "Patient Checkup Details Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function (error) {
            console.log(error);
            swal({
                title: "Patient checkup details creating failed",
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



////////////////////////////---------------------------after validation submit form data to the controller---------------------------///////////////////////////////////
function UpdatePatientHistory() {
    //here implement the ajax functionality for submit (POST) form data to controller

    var editorPrescriptions = tinymce.get('Prescriptions');
    var editorDocAdvice = tinymce.get('DocAdvice');
    var editorDiagnosis = tinymce.get('Diagnosis');

    if (editorPrescriptions) {
        var contentPrescriptions = editorPrescriptions.getContent();        
    }
    if (editorDocAdvice) {
        var contentDocAdvice = editorDocAdvice.getContent();
    }
    if (editorDiagnosis) {
        var contentDiagnosis = editorDiagnosis.getContent();
    }
   
    var patientHistoryViewModel = {};
    patientHistoryViewModel.PatientId = $('#PatientId').val();
    patientHistoryViewModel.Prescriptions = contentPrescriptions;// $('#Prescriptions').val();
    patientHistoryViewModel.DoctorAdvice = contentDocAdvice;// $('#DocAdvice').val();
    patientHistoryViewModel.Diagnosis = contentDiagnosis;// $('#DocAdvice').val();
    patientHistoryViewModel.IsCounseling = $("input[name='IsCounseling']:checked").val() == 1 ? true : false;

    var myArray = [];
    var trcount = $('#tblExaminationDetail tbody tr').length;
    for (var i = 1; i <= trcount; i++) {
        if ($('#txtFindingName_' + i).val() != "" || $('#txtRightEye_' + i).val() != "" || $('#txtLeftEye_' + i).val() != "") {
            var ExaminationDetail = {
                PatientId: $('#PatientId').val(),
                FindingName: $('#txtFindingName_' + i).val(),
                RightEye: $('#txtRightEye_' + i).val(),
                LeftEye: $('#txtLeftEye_' + i).val(),
            }
            myArray.push(ExaminationDetail);
        }

    }
    patientHistoryViewModel.PatientExamination = myArray;

    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(patientHistoryViewModel),
        url: GetRoute("/Patient/PatientCheckup"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "Patient Checkup Details Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                $('#frmPatientHistory').trigger("reset");
            }
            else {
                swal({
                    title: "Patient Checkup Details Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function (error) {
            console.log(error);
            swal({
                title: "Patient checkup details creating failed",
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

$("#btnAddLine").on("click", function () {
    var lastIndex = $("#tblExaminationDetail tbody tr:last").index();

    var newRow = $("#tblExaminationDetail tbody tr:last").clone();

    var newIndex = lastIndex + 2;

    newRow.find("input").each(function () {
        var currentId = $(this).attr("id");
        //var newIndx = parseInt(currentId.split('_')[1]) + newIndex;// (0, currentId.length - 1) + newIndex;
        var newId = currentId.split('_')[0] + "_" + parseInt(newIndex);
        $(this).attr("id", newId);
        $(this).attr("name", newId);
        $(this).val("");
    });

    $("#tblExaminationDetail tbody").append(newRow);
});