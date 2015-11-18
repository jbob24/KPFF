function ConfirmUnassign() {
    if (confirm("Are you sure you want to unassign the selected projects?")) {
        return true;
    }
    else {
        return false;
    }
}

function ConfirmEmployeeUnassign() {
    if (confirm("Are you sure you want to unassign the selected employees?")) {
        return true;
    }
    else {
        return false;
    }
}

var displayUnassignError = false;
$(document).ready(function () {
    if (displayUnassignError == true) {
        displayUnassignError = false;
        alert("Unable to unassign a project with future hours assigned. Delete or reassign future hours before unassigning.");
        //$(location).attr('href', window.location.pathname);
    }
});

var displayEmployeeUnassignError = false;
$(document).ready(function () {
    if (displayEmployeeUnassignError == true) {
        displayEmployeeUnassignError = false;
        alert("Unable to unassign an emloyee with future hours assigned. Delete or reassign future hours before unassigning.");
        //$(location).attr('href', window.location.pathname);
    }
});