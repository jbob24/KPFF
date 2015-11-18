$(document).ready(function () {
    $('[id$="txtSearch"]').keyup(function () {
        var hoursGrid = $('#hoursGridDiv .hoursGrid')
        $("tbody tr", hoursGrid).removeClass('searchHidden').show();

        if (this.value.length > 2)
            grid = $("tbody tr:not(:contains('" + this.value.toUpperCase() + "'))", hoursGrid).addClass('searchHidden').hide();

        setupAlternateRows(hoursGrid);
    });
});