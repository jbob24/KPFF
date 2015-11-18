(function ($) {

    $.fn.sortable = function (options) {
        var defaults = {};

        var tab_headers = $('th', this);
        var th_div_sort = null;
        var _stripNum = /[\$,%]/g;
        var _objectTable = tableToObject(options.table);

        tab_headers.each(function (i) {
            $(this).attr('colid', i);
            $(this).contents().wrapAll('<div class="sorter"></div>');
            th_div_sort = $('div.sorter', this);
            th_div_sort.click(function () {
                if ($(this).hasClass('sortedDown')) {
                    sortColumn(options.table, i, "ASC", false);
                    $(this).removeClass('sortedDown').addClass('sortedUp');
                    $(this).append('<span style="display : inline-block; vertical-align : middle"></span>');
                } else {
                    sortColumn(options.table, i, "DESC", false);
                    $(this).removeClass('sortedUp').addClass('sortedDown');
                    $(this).append('<span style="display : inline-block; vertical-align : middle"></span>');
                }
            })
        });

        if (options.multicolsort) {
            $('.weekHeader', this).parent().parent().each(function (i, c) {
                $(c).addClass('dragsorter');
                $(c).droppable({ scope: "dragsorter", drop: handleDrop }).draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "dragsorter" });
            });
        }

        $('div.sorter', tab_headers).addClass('hover');

        function handleDrop(event, ui) {
            var startId = $($(ui.draggable)[0]).attr('colid');
            var endId = $(this).attr('colid');

            if (startId && endId && !isNaN(parseInt(startId)) && !isNaN(parseInt(endId)) && parseInt(startId) < parseInt(endId)) {
                sortColumn(options.table, parseInt(startId), "DESC", true, parseInt(endId));
            }
        }

        function sortColumn(table, number, sens, multiSort, endNumber) {
            if (options.sortType.length != 0) {
                var type = options.sortType[number];

                if (type == 'float') {
                    getSortKey = function (cell) {
                        var key = parseFloat(String(cell).replace(_stripNum, ''));
                        return isNaN(key) ? 0.00 : key;
                    }
                } else if (type == 'integer') {
                    getSortKey = function (cell) {
                        cell = cell.indexOf('(') > -1 ? "-" + cell.replace('(', '').replace(')', '') : cell;
                        return cell && $.trim(cell).length > 0 ? parseFloat(String(cell).replace(_stripNum, '')) : -1;
                    }
                } else if (type == 'hourbox') {
                    getSortKey = function (cell) {
                        cell = cell.indexOf('(') > -1 ? "-" + cell.replace('(', '').replace(')', '') : cell;
                        var hourBox = cell && $.trim(cell).length > 0 ? $('.hourBox', cell) : null;
                        if (hourBox && hourBox.length > 0) {
                            var val = $(hourBox).val();
                            return $.trim(val).length > 0 ? parseFloat(String(val).replace(_stripNum, '')) : -1;
                        }
                    }
                } else if (type == 'date') {
                    getSortKey = function (cell) {
                        var date = util_parseDate(options.dateFormat, cell);

                        if (isNaN(date.valueOf()))
                            return 0

                        return date.valueOf();
                    }
                } else if (type == 'link') {
                    getSortKey = function (cell) {
                        return cell ? $.trim($(cell).text()) : '';
                    }
                } else {
                    getSortKey = function (cell) {

                        if (!cell) { cell = ""; }
                        return $.trim(String(cell).toLowerCase());
                    }
                }
            } else {
                getSortKey = function (cell) {
                    if (!cell) { cell = ""; }
                    return $.trim(String(cell).toLowerCase());
                }
            }

            var lt = sens == 'DESC' ? 1 : -1;
            var gt = -1 * lt;


            _objectTable.sort(function (a, b) {
                var x = getSortKey($('td', a)[number].innerHTML);
                var y = getSortKey($('td', b)[number].innerHTML);

                if (x < y)
                    return lt;

                if (x > y)
                    return gt;


                if (multiSort) {
                   // _objectTable.sort(function (a, b) {
                        var x1 = getSortKey($('td', a)[number].innerHTML);
                        var y1 = getSortKey($('td', b)[number].innerHTML);

                        if (x1 > y1) return 0;

                        for (var i = number + 1; i <= endNumber; i++) {
                            var m3 = getSortKey($('td', a)[i].innerHTML);
                            var n3 = getSortKey($('td', b)[i].innerHTML);

                            if (m3 < n3)
                                return 1;

                            if (m3 > n3)
                                return -1;
                        }

                        return 0;
                   // });
                }

                return 0;
                //return ((x < y) ? lt : ((x > y) ? gt : 0));
            });

            //            if (multiSort) {
            //                _objectTable.sort(function (a, b) {
            //                    var x1 = getSortKey($('td', a)[number].innerHTML);
            //                    var y1 = getSortKey($('td', b)[number].innerHTML);

            //                    if (x1 > y1) return 0;

            //                    for (var i = number + 1; i <= endNumber; i++) {
            //                        var m3 = getSortKey($('td', a)[i].innerHTML);
            //                        var n3 = getSortKey($('td', b)[i].innerHTML);

            //                        if (m3 < n3)
            //                            return 1;

            //                        if (m3 > n3)
            //                            return -1;
            //                    }

            //                    return 0;
            //                });
            //            }

            objectToTable(_objectTable, table);

            if (options.editable) {
                resetCellIDs(table);
            }
        }

        function objectToTable(objectArray, table) {
            var body = $('tbody', table);
            body.children().remove();
            var isEditable = options.editable;


            for (var i = 0; i < objectArray.length; i++) {
                var row = $(objectArray[i]).removeClass(options.zebraClass);

                if (options.zebra && i % 2 == 0) {
                    $(row).addClass(options.zebraClass);
                }

                body.append($(row[0]).outerHTML());
            }
        }

        function tableToObject(table) {
            var objectArray = [];

            $('tr', table).each(function (i) {
                var data = {};

                data = $(this).outerHTML();
                objectArray.push(data);
            });

            return objectArray;
        }

        function resetCellIDs(hoursGrid) {
            var rows = $('tbody tr', hoursGrid);

            $(rows).each(function (rowID, row) {
                var cells = $('td', row);

                $(cells).each(function (cellID, cell) {
                    cell.id = options.parentId + "_" + rowID + "_" + cellID;
                });
            });
        }
    };
})(jQuery);