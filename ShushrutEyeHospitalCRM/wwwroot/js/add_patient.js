///////////////////////////---------------------------------------form validation extension method--------------------------------------------//////////////////////////////
/*$.validator.addMethod("genderExt", function (gender) {
    return gender.match(/^\d*(?:\.\d{1,2})?$/);
});*/

// email validator regex
$.validator.addMethod("emailExt", function (txtEmail) {
    return txtEmail.match(/^[a-zA-Z0-9_\.%\+\-]+@[a-zA-Z0-9\.\-]+\.[a-zA-Z]{2,}$/);
});
document.getElementById('txtMobile').addEventListener('input', function (event) {
    event.target.value = event.target.value.replace(/\D/g, '');
});
document.getElementById('txtConsultancyFee').addEventListener('input', function (event) {
    event.target.value = event.target.value.replace(/\D/g, '');
});

///////////////////////////---------------------------------------form validation functionality -----------------------------------------------/////////////////////////////
var val = $('#frmAddPatient').validate({
    rules: {
        txtFirstName: {
            required: true,
        },
        txtLastName: {
            required: true,
        },
        //txtEmail: {
        //    email: true,
        //    required: true,
        //    emailExt: true
        //},
        txtMobile: {
            required: true,
            minlength: 10,
            maxlength: 10,
        },
        txtConsultancyFee: {
            required: true,
        },
        txtDOB: {
            required: true,
        },
        optDoctors: {
            required: true,
        },
        optRefraction: {
            required: true,
        },
        txtProblem: {
            required: true,
        },
        txtAddress: {
            required: true,
        },
        gender: {
            required: true,
        },
        status: {
            required: true,
        }
    },
    messages: {
        txtFirstName: {
            required: "Please enter first name",
        },
        txtLastName: {
            required: "Please enter last name",
        },
        //txtEmail: {
        //    required: "Please enter email address",
        //    emailExt: "Email address is incorrect"
        //},
        txtMobile: {
            required: "Please enter mobile number",
            minlength: "Mobile number must be 10 digits",
            maxlength: "Mobile number is incorrect"
        },
        txtConsultancyFee: {
            required: "Please enter consultancy fee",
        },
        txtDOB: {
            required: "Please enter date of birth",
        },
        optDoctors: {
            required: "Please select a doctor",
        },
        optRefraction: {
            required: "Please select a refraction",
        },
        txtProblem: {
            required: "Please enter problem",
        },
        txtAddress: {
            required: "Please enter address",
        },
        gender: {
            required: "Please select gender",
        },
        status: {
            required: "Please select status",
        }
    },
    errorElement: 'span',
    errorPlacement: function (error, element) {
        error.addClass('invalid-feedback');
        element.closest('.form-group').append(error);
    },
    highlight: function (element, errorClass, validClass) {
        $(element).addClass('is-invalid');
    },
    unhighlight: function (element, errorClass, validClass) {
        $(element).removeClass('is-invalid');
    }
});

///////////////////////////---------------------------------------submit button click functionality ----------------------------------------///////////////////////////
$('#btnSave').on("click", function () {

    if ($('#frmAddPatient').valid()) {
        var selectedval = $('#txtProblem').val();
        var otherval = $('#txtOtherProblem').val();
        if (selectedval == "Other" && (otherval == "" || otherval == null || otherval == undefined)) {
            $('#txtOtherProblem').focus();
            $('#othrProb').show();
            return false;
        }
        CreatePatient();
    }
    else {
        val.focusInvalid();
    }
});

///////////////////---------------------------------------------reset button click functionality-----------------------------------------------------///////////////////////
$('#btnReset').on('click', function () {
    $('#frmAddPatient').validate().resetForm();
});
$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });
    document.getElementById('txtDOB').setAttribute('max', new Date().toISOString().split('T')[0])
});
////////////////////////////---------------------------after validation submit form data to the controller---------------------------///////////////////////////////////
function CreatePatient() {
    //here implement the ajax functionality for submit (POST) form data to controller
    var patientViewModel = {};
    patientViewModel.BloodGroup = $('#optBloodGroup option:selected').val();
    patientViewModel.DoctorId = $('#optDoctors option:selected').val();
    patientViewModel.RefractionId = $('#optRefraction option:selected').val();
    patientViewModel.ConsultancyFee = $('#txtConsultancyFee').val();
    patientViewModel.Problem = $('#txtProblem').val();
    patientViewModel.OtherProblem = $('#txtOtherProblem').val();
    patientViewModel.Address = $('#txtAddress').val();
    patientViewModel.DOB = $('#txtDOB').val()
    patientViewModel.IsBySearch = $('#hdnbySearch').val()
    patientViewModel.MRDNo = $('#hdnMRDNo').val()
    patientViewModel.Gender = $("input[name='gender']:checked").val();
    patientViewModel.Status = $("input[name='status']:checked").val() == 1 ? true : false;

    var user = {};
    user.FirstName = $('#txtFirstName').val();
    user.LastName = $('#txtLastName').val();
    user.PhoneNumber = $('#txtMobile').val();
    user.Email = $('#txtEmail').val();
    user.UserName = $('#txtEmail').val();
    user.Password = "SP@123";
    patientViewModel.ApplicationUser = user;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(patientViewModel),
        url: GetRoute("/Patient/AddPatient"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "Patient Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                $('#frmAddPatient').trigger("reset");
            }
            else {
                swal({
                    title: "Patient Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function (error) {
            console.log(error);
            swal({
                title: "Patient creating failed",
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



$('#txtProblem').on("change", function () {
    var selectedval = $('#txtProblem').val();
    if (selectedval == "Other") {
        $('#othrProb').show();
    }
    else {
        $('#othrProb').hide();
    }
});


$('#txtMRDNo').on("change", function () {
    var selectedval = $('#txtMRDNo').val();
    if (selectedval != "" && selectedval != null && selectedval != undefined) {
        searchPatient(selectedval, "");
    }
});

$('#txtContactNo').on("change", function () {
    var selectedval = $('#txtContactNo').val();
    if (selectedval != "" && selectedval != null && selectedval != undefined) {
        searchPatient(0, selectedval);
    }
});

function searchPatient(mrdno, mobileno) {

    $.ajax({
        type: "POST",
        dataType: "json",
        data: {
            //PatientName: $("#txtPatientName").val(),
            MobileNo: mobileno,
            MRDNo: mrdno,
        },
        url: GetRoute("/Patient/GetPatientByFilter"),
        success: function (response) {
            debugger;
            var cont = response.data.length - 1;
            $("#overlay").hide();
            $("#txtFirstName").val(response.data[cont].applicationUser.firstName);
            $("#txtLastName").val(response.data[cont].applicationUser.lastName);
            $("#txtMobile").val(response.data[cont].applicationUser.phoneNumber);
            //$("#txtEmail").val(response.data[cont].applicationUser.email);
            var dat = moment(response.data[cont].dob).format("YYYY-MM-DD");
            $("#txtDOB").val(dat);
            $("#txtConsultancyFee").val(response.data[cont].consultancyFee);
            $("#optDoctors").val(response.data[cont].doctorId);
            $("#optRefraction").val(response.data[cont].refractionId);
            $("#optBloodGroup").val(response.data[cont].bloodGroup);

            let radioOption = $("input:radio[name='gender'][value=" + response.data[cont].gender + "]");
            radioOption.prop("checked", true);
            let radioOptionsts = $("input:radio[name='status'][value=" + response.data[cont].status + "]");
            radioOptionsts.prop("checked", true);
            $("#hdnMRDNo").val(response.data[cont].mrdNo);
            $("#hdnbySearch").val("1");
            //$("#txtProblem").val(response.data[cont].problem);
            //if (response.data[cont].otherProblem != "") {
            //    $("#txtOtherProblem").val(response.data[cont].otherProblem);
            //    $("#othrProb").show();
            //}
            $("#txtAddress").val(response.data[cont].address);
        },
        error: function (error) {
            console.log(error);
            $("#overlay").hide();
            swal({
                title: "Patient creating failed",
                type: "warning",
                text: "Please try again later!",
                confirmButtonColor: "#00B4B4",
            });

        }
    });
}
