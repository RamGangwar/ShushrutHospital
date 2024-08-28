function getReceptiondata() {
    if (document.getElementById('PId').value == "" || document.getElementById('PId').value == null) {
        var str = "Please Enter Patient No";
        document.getElementById('PId').focus();
        alert(str);
        return false;
    }

    let data = "";



    var Pcode = document.getElementById('PId').value;
    $.post(GetRoute("/Reports/CheckPatientDetails?id=" + Pcode + ""), function (data) {
        if (data != null) {
            document.getElementById('hideRtData').value = data;
            if (data != "Error") {
                var RPT = document.getElementById('reports').value;
                if (RPT == "Patient") {
                    data = "GetPatientDetails";
                }
                else {
                    data = "OPDReports";
                }
                $.post(window.open("/Reports/" + data + "?id=" + Pcode + ""), {
                }, 'json');
                return false;
            }
            else {
                alert("Please Enter correct Patient No!");
            }
        }
    }, 'json');
    return false;
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}


function DownloadPatient(patientId) {
    window.location.href = "/Reports/PatientCompleteDetails?Id=" +patientId;
}
function PatientCounseling(patientId) {
    
    window.location.href = "/Reports/PatientCounseling?Id=" +patientId;
}

function PatientCounselor(patientId) {

    window.location.href = "/Patient/PatientCounseling?Id=" + patientId;
}
function PatientDischarge(patientId) {

    window.location.href = "/Patient/DischargePatient?Id=" + patientId;
}
function DownloadPatientDetail(patientId) {
    window.location.href = "/Reports/PatientDetailsById?Id=" + patientId;
}
function DownloadDischargedPatient(patientId) {
    window.location.href = "/Reports/DischargedPatient?Id=" + patientId;
}

