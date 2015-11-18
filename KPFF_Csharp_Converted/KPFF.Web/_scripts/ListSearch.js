var sourceList = $("#projectsByName");
var listFilter = "";
var doFilter = false;


$(document).ready(function () {
    sourceList = $("#projectsByName");
    $("#projectsByName").hide();
    $("#projectsByNumber").hide();

    $(".rbByName").click(function () {
        sourceList = $("#projectsByName");
        buildSelectList();
    });

    $(".rbByNumber").click(function () {
        sourceList = $("#projectsByNumber");
        buildSelectList();
    });

    $("#projectList").show();
    buildSelectList();
});

function filterList(filter) {
    if (doFilter || filter.length > 3) {
        doFilter = true;
        listFilter = filter;
        buildSelectList();
    }

    if (filter.length == 0)
        doFilter = false;
}

function buildSelectList() {
    var dropdownList = $("#projectSelect")[0];
    $(dropdownList).empty();

    if (listFilter && listFilter != "")
        source = $(sourceList).find('option:Contains("' + listFilter + '")');
    else
        source = $(sourceList).find('option');

    $.each(source,
            function () {
                var option = new Option(this.text, this.value);

                if ($.browser.msie) {
                    dropdownList.add(option);
                }
                else {
                    dropdownList.add(option, null);
                }
            });
}

function modifySelection() {
    var selectedValue = $("#projectSelect option:selected")[0];

    $(".projectsByName option[selected]").removeAttr("selected");
    $(".projectsByName option[value='" + selectedValue.value + "']").attr("selected", "selected");

    $(".projectsByNumber option[selected]").removeAttr("selected");
    $(".projectsByNumber option[value='" + selectedValue.value + "']").attr("selected", "selected");
}

jQuery.expr[':'].Contains = function (a, i, m) {
    return jQuery(a).text().toUpperCase().indexOf(m[3].toUpperCase()) >= 0;
};
