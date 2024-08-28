///////////////////////////---------------------------------------form validation functionality -----------------------------------------------/////////////////////////////
var val = $('#frmSignUp').validate({
    rules: {
        txtfirstName: {
            required: true,
        },
        txtlastName: {
            required: true,
        },
        txtmobile: {
            required: true,
            minlength: 10,
            maxlength: 10
        },
        txtemail: {
            required: true
        },
        txtpassword: {
            required: true
        }
    },
    messages: {
        txtfirstName: {
            required: "Please enter your first name",
        },
        txtlastName: {
            required: "Please enter your last name",
        },
        txtmobile: {
            required: "Please enter your mobile",
            minlength: "Please enter a valid mobile",
            maxlength: "Please enter a valid mobile"
        },
        txtemail: {
            required: "Please enter your email",
        },
        txtpassword: {
            required: "Please enter your password",
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
$('#btnSubmit').on("click", function () {
    if ($('#frmSignUp').valid()) {
        RegisterUser();
    }
    else {
        val.focusInvalid();
    }
});

///////////////////---------------------------------------------reset button click functionality-----------------------------------------------------///////////////////////
$('#btnReset').on('click', function () {
    $('#frmSignUp').validate().resetForm();
    $('#frmSignUp').trigger("reset");
});

////////////////////////////---------------------------after validation submit form data to the controller---------------------------///////////////////////////////////
$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });
});
function RegisterUser() {
    var applicationUserViewModel = {};
    applicationUserViewModel.FirstName = $('#txtfirstName').val();
    applicationUserViewModel.LastName = $('#txtlastName').val();
    applicationUserViewModel.PhoneNumber = $('#txtmobile').val();
    applicationUserViewModel.Email = $('#txtemail').val();
    applicationUserViewModel.UserName = $('#txtemail').val();
    applicationUserViewModel.Password = $('#txtpassword').val();
    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(applicationUserViewModel),
        url: GetRoute("/Authorization/SignUp"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "User Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                $('#frmSignUp').trigger("reset");
                redirectPage(GetRoute(data.url));
            }
            else {
                swal({
                    title: "User Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function () {
            swal({
                title: "User creating failed",
                type: "warning",
                text: "Please try again later!",
                confirmButtonColor: "#00B4B4",
            });
        }
    }).done(function () {
        setTimeout(function () {
            $("#overlay").fadeOut(300);
        }, 500);
    });
}