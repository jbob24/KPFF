$(document).ready(function () {
    //setTableParent();

    //var hoursGrid = $('#hoursGridDiv .hoursGrid')

    $('.hoursGrid').each(function (i, e) {

        //    $(hoursGrid).tableScroll({ height: 200 });
        //    var headerTable = $('.tablescroll_head').addClass('hoursGrid');

        setupResizing(e);   
        setupSorting(e);
        resetCellIDs(e);

        setupDragDrop(e);
    });

});

function setTableParent() {
    var hoursGridDiv = $('.hoursGrid').parent();
    hoursGridDiv[0].id = 'hoursGridDiv';
    hoursGridDiv.addClass("hoursGridDiv");
}

function getGrid(gridDiv) {
    return $('.hoursGrid', gridDiv);
}

function setupDragDrop(hoursGrid) {
    var cells = $('.hours', hoursGrid);

    for (var cell, i = -1; cell = cells[++i]; ) {
        $(cell).droppable({ drop: handleDrop, over: handleHover, scope: "Hours" })
        .draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "Hours", start: startDrag, stop: RemoveHoverClass });
    }

    //    $('.hours', hoursGrid).each(function () {
    //        $(this).droppable({ drop: handleDrop, over: handleHover, scope: "Hours" }).draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "Hours", start: startDrag, stop: RemoveHoverClass });
    //    });
    //    //$('.hours', hoursGrid).draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "Hours", start: startDrag, stop: RemoveHoverClass });
}

function setupResizing(hoursGrid) {
    var header = $('th', hoursGrid);
    //$('.weekHeader', header).resizable({ stop: handleResizeStop, resize: handleResize, handles: 'e', minWidth: 28 });
    $('.weekHeader', header).resizable({ handles: 'e', minWidth: 28 });
    $('.projectHeader', header).resizable({ handles: 'e', minWidth: 200 });
}

function setupSorting(hoursGrid) {
    $(hoursGrid).tablesorter({
        textExtraction: extractValue
    }).bind("sortEnd", { hoursGrid: hoursGrid }, handleSortEnd);
}

function handleSortEnd(hoursGrid) {
    setupAlternateRows(hoursGrid.data.hoursGrid);
    resetCellIDs(hoursGrid.data.hoursGrid);
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

function resetCellIDs(hoursGrid) {
    var rows = $('tbody tr', hoursGrid);

    $(rows).each(function (rowID, row) {
        var cells = $('td', row);

        $(cells).each(function (cellID, cell) {
            cell.id = rowID + "_" + cellID;
        });
    });
}

function setupAlternateRows(hoursGrid) {
    $("tr", hoursGrid).removeClass("alternateRow");
    $("tr:nth-child(even)", hoursGrid).addClass("alternateRow");
}

function textChanged(field) {
    if (!Number(field.value) || Number(field.value) < 0)
        $(field).val("0");

    if (field.value != $(field).attr("OriginalValue"))
        $(field).addClass("textBoxChanged");
    else
        $(field).removeClass("textBoxChanged");
}

function handleHover(event, ui) {
    RemoveHoverClass();

    var draggable = $(ui.draggable[0]);
    var startFieldIDArray = draggable[0].id.split("_");
    var endFieldIDArray = $($('.divBox', this)[0]).parent()[0].id.split("_");
    var hourBox = $('.hourBox', draggable);

    var startrow = parseInt(startFieldIDArray[0]);
    var startcol = parseInt(startFieldIDArray[1]);
    var endrow = parseInt(endFieldIDArray[0]);
    var endcol = parseInt(endFieldIDArray[1]);
    var value = hourBox.val()

    if (endrow < startrow) {
        var temp = startrow;
        startrow = endrow;
        endrow = temp;
    }

    if (endcol < startcol) {
        var temp = startcol;
        startcol = endcol;
        endcol = temp;
    }

    var id;
    for (var i = startrow; i <= endrow; i++) {
        for (var j = startcol; j <= endcol; j++) {
            id = "#" + i + "_" + j;
            var cell = $(id);
            cell.addClass("droppableHovered");

            if (i == startrow)
                cell.addClass("droppableHoveredTop");

            if (i == endrow)
                cell.addClass("droppableHoveredBottom");

            if (j == startcol)
                cell.addClass("droppableHoveredLeft");

            if (j == endcol)
                cell.addClass("droppableHoveredRight");
        }
    }
}

function RemoveHoverClass() {
    $(".droppableHovered").removeClass("droppableHovered").removeClass("droppableHoveredTop").removeClass("droppableHoveredBottom").removeClass("droppableHoveredLeft").removeClass("droppableHoveredRight");
}

function handleDrop(event, ui) {
    RemoveHoverClass();
    var draggable = $(ui.draggable[0]);
    //var startFieldIDArray = draggable.parent()[0].id.split("_");
    var startFieldIDArray = draggable[0].id.split("_");
    var endFieldIDArray = $($('.divBox', this)[0]).parent()[0].id.split("_");
    //var endFieldIDArray = $($('.divBox', this)[0])[0].id.split("_");
    var hourBox = $('.hourBox', draggable);

    var startrow = parseInt(startFieldIDArray[0]);
    var startcol = parseInt(startFieldIDArray[1]);
    var endrow = parseInt(endFieldIDArray[0]);
    var endcol = parseInt(endFieldIDArray[1]);
    var value = hourBox.val()

    if (endrow < startrow) {
        var temp = startrow;
        startrow = endrow;
        endrow = temp;
    }

    if (endcol < startcol) {
        var temp = startcol;
        startcol = endcol;
        endcol = temp;
    }

    var id;
    for (var i = startrow; i <= endrow; i++) {
        for (var j = startcol; j <= endcol; j++) {
            //id = "#cphMainContent_gridHours_" + i + "_" + j + "_" + (i - 1) + " .hourBox";
            id = "#" + i + "_" + j + " .hourBox";
            if ($(id).val() != value) {
                $(id).val(value).addClass("textBoxChanged");
            }
        }
    }

    hourBox.removeClass("dragging");
}

function startDrag(event, ui) {
    var draggable = $(ui.helper[0]);
    var hourBox = $('.hourBox', draggable);
    hourBox.addClass("dragging");
}

function CancelEdit() {
    $("#hoursGridDiv .hoursGrid .textBoxChanged").removeClass("textBoxChanged").each(function () {
        this.value = $(this).attr("OriginalValue");
    });
}

function getScrollBottom(p_oElem) {
    return p_oElem.scrollHeight - p_oElem.scrollTop - p_oElem.clientHeight;
}


function setDroppable(cell) {
    $(this).droppable({ drop: handleDrop, over: handleHover, scope: "Hours" })
}

