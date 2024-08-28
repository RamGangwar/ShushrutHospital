///////////////////////////---------------------------------------form validation functionality -----------------------------------------------/////////////////////////////
var val = $('#frmDepartment').validate({
    rules: {
        txtDepartment: {
            required: true,
        },
        txtDescription: {
            required: true,
        },
        status: {
            required: true
        }
    },
    messages: {
        txtDepartment: {
            required: "Please enter department name"
        },
        txtDescription: {
            required: "Please enter description"
        },
        status: {
            required: "Please select the status"
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

    if ($('#frmDepartment').valid()) {
        CreateDepartment();
    }
    else {
        val.focusInvalid();
    }
});

///////////////////---------------------------------------------reset button click functionality-----------------------------------------------------///////////////////////
$('#btnReset').on('click', function () {
    $('#frmDepartment').validate().resetForm();
});

////////////////////////////---------------------------after validation submit form data to the controller---------------------------///////////////////////////////////
$(function () {
    $(document).ajaxSend(function () {
        $("#overlay").fadeIn(300);
    });
});
function CreateDepartment() {
    var departmentViewModel = {};
    departmentViewModel.Name = $('#txtDepartment').val();
    departmentViewModel.Description = $('#txtDescription').val();
    departmentViewModel.Status = $("input[name='status']:checked").val() == 1 ? true : false;
    $.ajax({
        type: "POST",
        dataType: "json",
        data: JSON.stringify(departmentViewModel),
        url: GetRoute("/Department/AddDepartment"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            if (data.status == true) {
                swal({
                    title: "Department Created",
                    type: "success",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
                //redirectPage(GetRoute(data.url));
                $('#frmDepartment').trigger("reset");
            }
            else {
                swal({
                    title: "Department Not Created",
                    type: "warning",
                    text: data.message,
                    confirmButtonColor: "#00B4B4",
                });
            }
        },
        error: function () {
            swal({
                title: "Department creating failed",
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