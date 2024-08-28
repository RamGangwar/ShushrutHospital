///////////////////////////---------------------------------------form validation functionality -----------------------------------------------/////////////////////////////
var val = $('#frmLogin').validate({
    rules: {
        txtemail: {
            required: true
        },
        txtpassword: {
            required: true
        }
    },
    messages: {
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

    if ($('#frmLogin').valid()) {
        LoginUser();
    }
    else {
        val.focusInvalid();
    }
});

///////////////////---------------------------------------------reset button click functionality-----------------------------------------------------///////////////////////
$('#btnReset').on('click', function () {
    $('#frmLogin').validate().resetForm();
    $('#frmLogin').trigger('reset');
});

////////////////////////////---------------------------after validation submit form data to the controller---------------------------///////////////////////////////////
$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });
});
function LoginUser() {
    var email = $('#txtemail').val();
    var password = $('#txtpassword').val();
    $.ajax({
        type: "POST",
        dataType: "json",
        data: { 'email': email, 'password': password },
        url: GetRoute("/Authorization/Index"),
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "User LoggedIn",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                redirectPage(GetRoute(data.url));
            }
            else {
                swal({
                    title: "User Not Login",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function () {
            swal({
                title: "User login failed",
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