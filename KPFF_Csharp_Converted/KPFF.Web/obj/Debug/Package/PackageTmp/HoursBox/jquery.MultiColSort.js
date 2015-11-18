(function ($) {

    $.fn.MultiColSort = function (options) {
        var defaults = {};

        $('.weekHeader', this).parent().parent().each(function (i, c) {
            $(c).addClass('dragsorter');
            $(c).droppable({ scope: "dragsorter", drop: handleDrop }).draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "dragsorter" });
            //$(cell).droppable({ drop: handleDrop, over: handleHover, scope: "Hours" }).draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "Hours", start: startDrag, stop: RemoveHoverClass });
        });

        function handleDrop(event, ui) {
            var startId = $($(ui.draggable)[0]).attr('colid');
            var endId = $(this).attr('colid');

            if (startId && endId && !isNaN(parseInt(startId)) && !isNaN(parseInt(endId))) {
                //call sort method here passing in startid and endid
                // the method should sort by startid, then by startid + 1, then by startid + 1 + 1, etc. all the way to end id
            }

//            var sortCols = new Array();
//            sortCols[0] = $(ui.draggable[0]).attr('colid');
            //            RemoveHoverClass();
            //            var draggable = $(ui.draggable[0]);
            //            var startFieldIDArray = draggable[0].id.split("_");
            //            var endFieldIDArray = $($('.divBox', this)[0]).parent()[0].id.split("_");
            //            var hourBox = $('.hourBox', draggable);

            //            var tableId = parseInt(startFieldIDArray[0]);
            //            var startrow = parseInt(startFieldIDArray[1]);
            //            var startcol = parseInt(startFieldIDArray[2]);
            //            var endrow = parseInt(endFieldIDArray[1]);
            //            var endcol = parseInt(endFieldIDArray[2]);
            //            var value = hourBox.val()

            //            if (endrow < startrow) {
            //                var temp = startrow;
            //                startrow = endrow;
            //                endrow = temp;
            //            }

            //            if (endcol < startcol) {
            //                var temp = startcol;
            //                startcol = endcol;
            //                endcol = temp;
            //            }

            //            var id;
            //            for (var i = startrow; i <= endrow; i++) {
            //                for (var j = startcol; j <= endcol; j++) {
            //                    id = "#" + tableId + "_" + i + "_" + j + " .hourBox";
            //                    if ($(id).val() != value) {
            //                        $(id).val(value).addClass("textBoxChanged");
            //                    }
            //                }
            //            }

            //            hourBox.removeClass("dragging");
        }

    };
})(jQuery);