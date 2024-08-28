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
var val = $('#frmAddRefraction').validate({
    rules: {
        txtFirstName: {
            required: true,
        },
        txtLastName: {
            required: true,
        },
        txtMobile: {
            required: true,
            minlength: 10,
            maxlength: 10,
        },
        txtEmail: {
            email: true,
            required: true,
            emailExt: true
        },
        txtDOB: {
            required: true,
        },
        txtPassword: {
            required: true,
        },
        //fileProfilePic: {
        //    required: true,
        //},
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
        txtMobile: {
            required: "Please enter mobile number",
            minlength: "Mobile number must be 10 digits",
            maxlength: "Mobile number is incorrect"
        },
        txtEmail: {
            required: "Please enter email address",
            emailExt: "Email address is incorrect"
        },
        txtDOB: {
            required: "Please enter date of birth",
        },
        txtPassword: {
            required: "Please enter password",
        },
        //fileProfilePic: {
        //    required: "Please choose the profile pic",
        //},
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

    if ($('#frmAddRefraction').valid()) {
        CreateRefraction();
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
    $('#frmAddRefraction').validate().resetForm();
});
$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });
    document.getElementById('txtDOB').setAttribute('max', new Date().toISOString().split('T')[0])
});
////////////////////////////---------------------------after validation submit form data to the controller---------------------------///////////////////////////////////
function CreateRefraction() {
    //here implement the ajax functionality for submit (POST) form data to controller
    var refractionViewModel = {};
    refractionViewModel.Address = $('#txtAddress').val();
    refractionViewModel.ProfilePic = $('#previewProfile').attr('src');
    refractionViewModel.DOB = $('#txtDOB').val()
    refractionViewModel.Gender = $("input[name='gender']:checked").val();
    refractionViewModel.Status = $("input[name='status']:checked").val() == 1 ? true : false;
    var user = {};
    user.FirstName = $('#txtFirstName').val();
    user.LastName = $('#txtLastName').val();
    user.PhoneNumber = $('#txtMobile').val();
    user.Email = $('#txtEmail').val();
    user.UserName = $('#txtEmail').val();
    user.Password = $('#txtPassword').val();
    refractionViewModel.ApplicationUser = user;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(refractionViewModel),
        url: GetRoute("/Refraction/AddRefraction"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "Refraction Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                $('#frmAddRefraction').trigger("reset");
                $('#previewProfile').attr('src', '');
            }
            else {
                swal({
                    title: "Refraction Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function (error) {
            console.log(error);
            swal({
                title: "Refraction creating failed",
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
/// Image to Base64 Bit Conversion

$('#fileProfilePic').on('change', function () {
    var fileInput = document.getElementById('fileProfilePic');
    var reader = new FileReader();
    reader.onload = function (e) {
        $('#previewProfile').attr('src', e.target.result);
    };
    reader.readAsDataURL(fileInput.files[0]);
    $('#previewProfile').attr('style', 'width:100%;height:100px;')

});