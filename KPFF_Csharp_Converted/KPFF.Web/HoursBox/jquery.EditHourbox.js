(function ($) {

    $.fn.EditHourbox = function (options) {
        var defaults = {
            parentId: 0
        };

        var hoursGrid = this;
        var rows = $('tbody tr', hoursGrid);

        $(rows).each(function (rowID, row) {
            var cells = $('td', row);

            $(cells).each(function (cellID, cell) {
                cell.id = options.parentId + "_" + rowID + "_" + cellID;

                if ($(cell).hasClass('hours')) {
                    $(cell).droppable({ drop: handleDrop, over: handleHover, scope: "Hours" }).draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "Hours", start: startDrag, stop: RemoveHoverClass });
                }
            });
        });

        $('#btnCancelEdit').click(function () { CancelEdit(); });

        textboxes = $("input.textBox");
        // now we check to see which browser is being used
        if ($.browser.mozilla) {
            $(textboxes).keypress(checkForEnter);
        } else {
            $(textboxes).keydown(checkForEnter);
        }


        function checkForEnter(event) {
            if (event.keyCode == 13) {
                currentBoxNumber = textboxes.index(this);

                if (event.shiftKey) {
                    if (currentBoxNumber == 0) {
                        currentBoxNumber = textboxes.length;
                    }

                    if (textboxes[currentBoxNumber - 1] != null) {
                        nextBox = textboxes[currentBoxNumber - 1];
                    }
                }
                else {
                    if (currentBoxNumber == textboxes.length - 1) {
                        currentBoxNumber = -1;
                    }
                    if (textboxes[currentBoxNumber + 1] != null) {
                        nextBox = textboxes[currentBoxNumber + 1]
                    }
                }

                if (nextBox) {
                    nextBox.focus();
                    nextBox.select();
                    event.preventDefault();
                    return false;
                }
            }
        }

        function handleHover(event, ui) {
            RemoveHoverClass();

            var draggable = $(ui.draggable[0]);
            var startFieldIDArray = draggable[0].id.split("_");
            var endFieldIDArray = $($('.divBox', this)[0]).parent()[0].id.split("_");
            var hourBox = $('.hourBox', draggable);

            var tableId = parseInt(startFieldIDArray[0]);
            var startrow = parseInt(startFieldIDArray[1]);
            var startcol = parseInt(startFieldIDArray[2]);
            var endrow = parseInt(endFieldIDArray[1]);
            var endcol = parseInt(endFieldIDArray[2]);
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
                    id = "#" + tableId + "_" + i + "_" + j;
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
            var startFieldIDArray = draggable[0].id.split("_");
            var endFieldIDArray = $($('.divBox', this)[0]).parent()[0].id.split("_");
            var hourBox = $('.hourBox', draggable);

            var tableId = parseInt(startFieldIDArray[0]);
            var startrow = parseInt(startFieldIDArray[1]);
            var startcol = parseInt(startFieldIDArray[2]);
            var endrow = parseInt(endFieldIDArray[1]);
            var endcol = parseInt(endFieldIDArray[2]);
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
                    id = "#" + tableId + "_" + i + "_" + j + " .hourBox";
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
            $(".hoursGrid .textBoxChanged").removeClass("textBoxChanged").each(function () {
                this.value = $(this).attr("OriginalValue");
            });
        }
    };
})(jQuery);


function textChanged(field) {
    if (!Number(field.value) || Number(field.value) < 0)
        $(field).val("0");

    if (field.value != $(field).attr("OriginalValue"))
        $(field).addClass("textBoxChanged");
    else
        $(field).removeClass("textBoxChanged");
}

