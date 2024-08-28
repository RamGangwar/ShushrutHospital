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

///////////////////////////---------------------------------------form validation functionality -----------------------------------------------/////////////////////////////
var val = $('#frmAddDoctor').validate({
    rules: {
        txtFirstName: {
            required: true,
        },
        txtLastName: {
            required: true,
        },
        txtEmail: {
            email: true,
            required: true,
            emailExt: true
        },
        txtPassword: {
            required: true,
        },
        txtDesignation: {
            required: true,
        },
        optDepartment: {
            required: true,
        },
        txtAddress: {
            required: true,
        },
        txtSpecialist: {
            required: true,
        },
        txtMobile: {
            required: true,
            minlength: 10,
            maxlength: 10,
        },
        //fileProfilePic: {
        //    required: true,
        //},
        //txtBiography: {
        //    required: true,
        //},
        txtDOB: {
            required: true,
        },
        //optBloodGroup: {
        //    required: true,
        //},
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
        txtEmail: {
            required: "Please enter email address",
            emailExt: "Email address is incorrect"
        },
        txtPassword: {
            required: "Please enter password",
        },
        txtDesignation: {
            required: "Please enter designation",
        },
        optDepartment: {
            required: "Please select department",
        },
        txtAddress: {
            required: "Please enter address",
        },
        txtSpecialist: {
            required: "Please enter specialist",
        },
        txtMobile: {
            required: "Please enter mobile number",
            minlength: "Mobile number must be 10 digits",
            maxlength: "Mobile number is incorrect"
        },
        //fileProfilePic: {
        //    required: "Please choose profile picture",
        //},
        //txtBiography: {
        //    required: "Please enter biography",
        //},
        txtDOB: {
            required: "Please enter date of birth",
        },
        //optBloodGroup: {
        //    required: "Please select blood group",
        //},
        gender: {
            required: "Please choose gender",
        },
        status: {
            required: "Please choose status",
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
    if ($('#frmAddDoctor').valid()) {
        CreateDoctor()
        //var gender = $('input[name="gender"]:checked').val();
        //var status = $('input[name="status"]:checked').val();
        //if (gender != '') {
        //    if (status != '') {

        //    }
        //    else {
        //        var element = $('input[name="status"]');
        //    }
        //}
        //else {
        //   var element = $('input[name="gender"]');
        //}
    }
    else {
        val.focusInvalid();
    }
});

///////////////////---------------------------------------------reset button click functionality-----------------------------------------------------///////////////////////
$('#btnReset').on('click', function () {
    $('#frmAddDoctor').validate().resetForm();
});

////////////////////////////---------------------------after validation submit form data to the controller---------------------------///////////////////////////////////

$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });
});
function CreateDoctor() {
    var doctorViewModel = {};
    doctorViewModel.Designation = $('#txtDesignation').val();
    doctorViewModel.DepartmentId = parseInt($('#optDepartment option:selected').val());
    doctorViewModel.Address = $('#txtAddress').val();
    doctorViewModel.Specialist = $('#txtSpecialist').val();
    doctorViewModel.ProfilePic = $('#previewProfile').attr('src');
    doctorViewModel.Signature = $('#previewSignature').attr('src');
    doctorViewModel.DOB = $('#txtDOB').val()//GetDate($('#txtDOB').val());
    doctorViewModel.Biography = $('#txtBiography').val();
    doctorViewModel.Gender = $("input[name='gender']:checked").val();
    doctorViewModel.BloodGroup = $('#optBloodGroup option:selected').val();
    doctorViewModel.Status = $("input[name='status']:checked").val() == 1 ? true : false;
    var user = {};
    user.FirstName = $('#txtFirstName').val();
    user.LastName = $('#txtLastName').val();
    user.PhoneNumber = $('#txtMobile').val();
    user.Email = $('#txtEmail').val();
    user.UserName = $('#txtEmail').val();
    user.Password = $('#txtPassword').val();
    doctorViewModel.ApplicationUser = user;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(doctorViewModel),
        url: GetRoute("/Doctor/AddDoctor"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "Doctor Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                $('#frmAddDoctor').trigger("reset");
                $('#previewProfile').attr('src', '');
                $('#previewProfile').hide();
            }
            else {
                swal({
                    title: "Doctor Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function (error) {
            console.log(error);
            swal({
                title: "Doctor creating failed",
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

//$('#btnSaveEdit').on("click", function () {
//    if ($('#frmEditDoctor').valid()) {
//        UpdateDoctor()

//    }
//    else {
//        val.focusInvalid();
//    }
//});

$(document).on('submit', '#frmEditDoctor', function (e) {
    debugger;
    e.preventDefault();
    if ($(this).valid()) {
        console.log($(this).serialize());
        $.ajax({
            type: $(this).attr("method"),
            url: $(this).attr("action"),
            data: $(this).serialize(),
            datatype: "json",
            async: false,
            success: function (data) {
                if (data.status == true) {
                    swal({
                        title: "Doctor Updated",
                        type: "success",
                        text: data.message,
                        confirmButtonColor: "#00B4B4",
                    });
                    window.location.href = "/Doctor/index";
                }
                else {
                    swal({
                        title: "Doctor Not Created",
                        type: "warning",
                        text: data.message,
                        confirmButtonColor: "#00B4B4",
                    });
                    window.location.href = "/Doctor/index";
                }
               
            }
        });
    }
});

function UpdateDoctor() {
    debugger;
    var doctorModel = {};
    doctorModel.Id = $('#hdnDocId').val();
    doctorModel.Designation = $('#txtDesignation').val();
    doctorModel.DepartmentId = parseInt($('#optDepartment option:selected').val());
    doctorModel.Address = $('#txtAddress').val();
    doctorModel.Specialist = $('#txtSpecialist').val();
    doctorModel.ProfilePic = $('#hdnpreviewProfile').val();// $('#previewProfile').attr('src');
    doctorModel.Signature = $('#hdnpreviewSignature').val();// $('#previewSignature').attr('src');
    doctorModel.DOB = $('#txtDOB').val()//GetDate($('#txtDOB').val());
    doctorModel.Biography = $('#txtBiography').val();
    doctorModel.Gender = $("input[name='Item1.Gender']:checked").val();
    doctorModel.BloodGroup = $('#optBloodGroup option:selected').val();
    doctorModel.Status = $("input[name='Item1.Status']:checked").val();// == 1 ? true : false;
    var user = {};
    user.FirstName = $('#txtFirstName').val();
    user.LastName = $('#txtLastName').val();
    user.PhoneNumber = $('#txtMobile').val();
    user.Email = $('#txtEmail').val();
    user.UserName = $('#txtEmail').val();
    doctorModel.ApplicationUser = user;
    console.log(doctorViewModel);
    $.ajax({
        type: "Post",
        dataType: "json",
        data: { doctorViewModel: doctorModel },//JSON.stringify(doctorViewModel),
        url: GetRoute("/Doctor/DoctorUpdate"),//?Id=" + $('#hdnDocId').val()
        //processData: false,
        contentType: "application/json",
        success: function (data) {
            debugger;
            if (data.status == true) {
                swal({
                    title: "Doctor Updated",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                window.location.href = "/Doctor/index";
            }
            else {
                swal({
                    title: "Doctor Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                //window.location.href = "/Doctor/index";
            }
        },
        error: function (error) {
            debugger;
            console.log(error);
            swal({
                title: "Doctor creating failed",
                type: "warning",
                text: "Please try again later!",
                confirmButtonColor: "#00B4B4",
            });
            setTimeout(function () {
                $("#overlay").fadeOut(0);
            }, 0);
        }
    }).done(function () {
        setTimeout(function () {
            $("#overlay").fadeOut(0);
        }, 0);
    });
}


function GetDate(formatedDate) {
    var month = {
        "Jan": 1,
        "Feb": 3,
        "Mar": 3,
        "Apr": 4,
        "May": 5,
        "Jun": 6,
        "Jul": 7,
        "Aug": 8,
        "Sep": 9,
        "Oct": 10,
        "Nov": 11,
        "Dec": 12,
    }
    var dateArray = formatedDate.split('-');
    var date = dateArray[2] + "-" + month[dateArray[1]] + "-" + dateArray[0];
    return date;
}
// date picker
$(function () {
    document.getElementById('txtDOB').setAttribute('max', new Date().toISOString().split('T')[0])
    $(".datepicker").datepicker({
        dateFormat: "dd-M-yy",
        changeMonth: true,
        changeYear: true,
        //minDate: 0,
        maxDate: 0,
        //maxDate: "+1M +5D"
        //maxDate: "1M",
        //showOn: 'button',
        //buttonImageOnly: true,
        //buttonImage: '../images/calender.png'
    });

    $('.datepicker').on('change', function (event) {
        $(this).removeClass('is-invalid');
        $(this).closest('.form-group').remove('span');
    });

    $('.datepicker').on('input', function (event) {
        if (!GetDate($(this).val()).match(/^\d{4}[\/\-](0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])$/)) {
            $(this).val('');
        }
    });

    $('.ui-datepicker-trigger').removeAttr('title');
});

function DeleteDoctor(Id) {
    swal({
        title: "Are you sure, You want to delete?",
        icon: "warning",
        buttons: ["No", "Yes"],
        dangerMode: true,
        confirmButtonColor: "#00B4B4",
        closeOnClickOutside: false,
    }).then(val => {

        if (!val) throw null;
        $.ajax({
            type: 'DELETE',
            url: '/Doctor/DeleteDoctor',
            data: { Id: Id },
            datatype: "json",
            success: function (response) {
                if (response.status) {
                    swal({
                        title: "Success",
                        icon: "success",
                        text: response.message,
                        type: 'success',
                        confirmButtonColor: "#00B4B4",
                        closeOnClickOutside: false
                    });
                }
                else {
                    swal("Error!", "Please try again", "error");
                }
            },
            error: function () {
            }
        });
    });
}

/// Image to Base64 Bit Conversion

$('#fileProfilePic').on('change', function () {
    var fileInput = document.getElementById('fileProfilePic');
    var reader = new FileReader();
    reader.onload = function (e) {
        $('#previewProfile').attr('src', e.target.result);
        $('#hdnpreviewProfile').val(e.target.result);
    };
    reader.readAsDataURL(fileInput.files[0]);
    $('#previewProfile').attr('style', 'width:50px;height:50px;')

});
$('#fileSignature').on('change', function () {
    var fileInput = document.getElementById('fileSignature');
    var reader = new FileReader();
    reader.onload = function (e) {
        $('#previewSignature').attr('src', e.target.result);
        $('#hdnpreviewSignature').val(e.target.result);
    };
    reader.readAsDataURL(fileInput.files[0]);
    $('#previewSignature').attr('style', 'width:50px;height:50px;')

});