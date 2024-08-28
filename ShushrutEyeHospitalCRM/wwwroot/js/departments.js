$(function () {
    $.getJSON(GetRoute("/Department/Index"), function (departments) {
        $('#tblDepartment tbody').empty();
        var tr;
        $.each(departments, function (index, department) {
            tr = $('<tr/>');
            tr.append(`<td>${(index + 1)}</td>`);
            tr.append(`<td>${department.name}</td>`);
            tr.append(`<td>${department.description}</td>`);
            tr.append(`<td>${department.status}</td>`);
            tr.append(`<td>${department.status}</td>`);
            tr.append(`<td>
                   <button class="btn btn-info btn-sm" data-toggle="tooltip" data-placement="left" title="Update"><i class="fa fa-pencil" aria-hidden="true"></i></button>
                   <button class="btn btn-danger btn-sm" data-toggle="tooltip" data-placement="right" title="Delete "><i class="fa fa-trash-o" aria-hidden="true"></i></button>
                   </td>`);
            $('#tblDepartment').append(tr);
        });
    })
});
function GetDepartments() {
   /* $.ajax({
        type: "Get",
        dataType: "json",
        url: GetRoute("/Department/Index"),
        processData: false,
        contentType: "application/json",
        success: function (data) {
            BindDepartments(data);
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
    });*/
}