$(document).ready(function () {
    setTableParent();

    var hoursGrid = $('#hoursGridDiv .hoursGrid');

    setupResizing(hoursGrid);
    setupSorting(hoursGrid);
    setCurrentWeekStyle(hoursGrid);
});

function setCurrentWeekStyle(hoursGrid) {
    var header = $("th", hoursGrid).each(function (i, e) {
        var headerStyled = $(".currentWeekHeader", e);
        if (headerStyled.length > 0) {
            $(hoursGrid).find('td').filter(':nth-child(' + (i + 1) + ')').addClass('currentWeekCell')
        }
    });
}

function setTableParent() {
    var hoursGridDiv = $('.hoursGrid').parent();
    hoursGridDiv[0].id = 'hoursGridDiv';
    hoursGridDiv.addClass("hoursGridDiv");
}

function getGrid(gridDiv) {
    return $('.hoursGrid', gridDiv);
}

function setupResizing(hoursGrid) {
    var header = $('th', hoursGrid);
    //$('.weekHeader', header).resizable({ stop: handleResizeStop, resize: handleResize, handles: 'e', minWidth: 28 });
    $('.weekHeader', header).resizable({ handles: 'e', minWidth: 28 });
    $('.projectHeader', header).resizable({ handles: 'e', minWidth: 200 });
    $('.pocHeader', header).resizable({ handles: 'e', minWidth: 60 });
}

function setupSorting(hoursGrid) {
    $(hoursGrid).tablesorter({
        textExtraction: extractValue
    }).bind("sortEnd", { hoursGrid: hoursGrid }, handleSortEnd);
}

function handleSortEnd(hoursGrid) {
    setupAlternateRows(hoursGrid.data.hoursGrid);
    //resetCellIDs(hoursGrid.data.hoursGrid);
}


function handleResizeStop(event, ui) {
    var tstr = "";
}

function handleResize(event, ui) {
    ui.element[0].innerHTML = ui.size.width;

}

function extractValue(node) {
    var retVal = "";
    var link = $('a', node);

    if (link && link.length > 0)
        retVal = $('a', node)[0].title;
    else {
        if (node.children.length <= 0) {
            if (node.childNodes.length > 0) {
                retVal = node.childNodes[0].data;
            }
            else {
                retVal = 0;
            }
        }
        else {
            if (node.children[0].children.length > 0) {
                retVal = node.children[0].children[0].value;
            }
            else {
                var type = node.children[0].type

                if (type == "") {
                    retVal = node.children[0].text
                }
                else {
                    switch (type) {
                        case "checkbox":
                            retVal = node.children[0].checked.toString();
                            break;
                        case "radio":
                            retVal = node.children[0].checked.toString();
                            break;
                        case "text":
                            retVal = node.children[0].value;
                            break;
                        default: retVal = "";
                    }
                }
            }
        }
    }

    return retVal;
}

function setupAlternateRows(hoursGrid) {
    $("tr", hoursGrid).removeClass("alternateRow");

    $("tr:not('.searchHidden')", hoursGrid).each(function (i, row) {
        if (i % 2 == 0) {
            $(row).addClass("alternateRow");
        }
    });
}

