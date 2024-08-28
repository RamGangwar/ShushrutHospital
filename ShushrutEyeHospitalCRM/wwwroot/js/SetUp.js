
$(document).on('submit', '#frmAddEditEyeProblems', function (e) {
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
                        title: "Added",
                        type: "success",
                        text: data.message,
                        confirmButtonColor: "#00B4B4",
                    });
                    window.location.href = "/Setup/ProblemList";
                }
                else {
                    swal({
                        title: "Failed to add",
                        type: "warning",
                        text: data.message,
                        confirmButtonColor: "#00B4B4",
                    });
                    
                }

            }
        });
    }
});


function DeleteProblem(Id) {
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
            url: '/SetUp/DeleteProblem',
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
                    window.location.href = "/Setup/ProblemList";
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


$(document).on('submit', '#frmAddEditMedicine', function (e) {
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
                        title: "Added",
                        type: "success",
                        text: data.message,
                        confirmButtonColor: "#00B4B4",
                    });
                    window.location.href = "/Setup/MedicineList";
                }
                else {
                    swal({
                        title: "Failed to add",
                        type: "warning",
                        text: data.message,
                        confirmButtonColor: "#00B4B4",
                    });

                }

            }
        });
    }
});


function DeleteMedicine(Id) {
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
            url: '/SetUp/DeleteMedicine',
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
                    window.location.href = "/Setup/MedicineList";
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