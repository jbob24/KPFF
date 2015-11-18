

$(document).ready(function () {
    $(".ValidateInt").blur(
    function () {
        $(this).val(ValidateInteger($(this).val()));
    });
});

function ValidateInteger(value) {
    if (isNaN(parseInt(value)))
        return 0;
    else
        return parseInt(value);
}