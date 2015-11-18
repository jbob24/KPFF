(function ($) {

    $.fn.hourbox2 = function (options) {
        var defaults = {
            editable: false,
            sortable: true,
            colresizable: true,
            height: null,
            colratio: [],
            curweekstyle: 'currentWeekCell',
            zebra: true,
            zebraClass: 'alternateRow',
            dateFormat: 'd-m-y'
        };

        var options = $.extend(defaults, options);

        var header = null;
        var body = null;

        return this.each(function () {
            setTableParent(this);

            setupScrolling(this);

            header = $('.headtable .hoursGrid');
            body = $('.body .hoursGrid');

            if (options.editable) {
                enableEditing(body);
            }
            else {
                setCurrentWeekStyle(header, body);
            }

        });

        function enableEditing(hoursGrid) {
            resetCellIDs(hoursGrid);
            setupDragDrop(hoursGrid);
            $('#btnCancelEdit').click(function () { CancelEdit(); });


            textboxes = $("input.textBox");
            // now we check to see which browser is being used
            if ($.browser.mozilla) {
                $(textboxes).keypress(checkForEnter);
            } else {
                $(textboxes).keydown(checkForEnter);
            }
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

        function setupDragDrop(hoursGrid) {
            var cells = $('.hours', hoursGrid);

            for (var cell, i = -1; cell = cells[++i]; ) {
                $(cell).droppable({ drop: handleDrop, over: handleHover, scope: "Hours" }).draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "Hours", start: startDrag, stop: RemoveHoverClass });
            }
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

        function setTableParent(hoursGrid) {
            var hoursGridDiv = $(hoursGrid).parent();
            hoursGridDiv[0].id = 'hoursGridDiv';
            hoursGridDiv.addClass("hoursGridDiv");
        }

        function setCurrentWeekStyle(headerTable, body) {
            var header = $("th", headerTable).each(function (i, e) {
                var headerStyled = $(".currentWeekHeader", e);
                if (headerStyled.length > 0) {
                    $(body).find('td').filter(':nth-child(' + (i + 1) + ')').addClass(options.curweekstyle)
                }
            });
        }

        function setupResizing(hoursGrid) {
            var header = $('th', hoursGrid);
            $('.weekHeader', header).resizable({ handles: 'e', minWidth: 28 });
            $('.projectHeader', header).resizable({ handles: 'e', minWidth: 200 });
            $('.pocHeader', header).resizable({ handles: 'e', minWidth: 60 });
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

            if (options.editable) {
                $("tr:nth-child(even)", hoursGrid).addClass("alternateRow");
            } else {
                $("tr:not('.searchHidden')", hoursGrid).each(function (i, row) {
                    if (i % 2 == 0) {
                        $(row).addClass(options.alternaterowclass);
                    }
                });
            }
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

        function setupScrolling(hoursGrid) {
            var _table = $(hoursGrid);
            var main_wrapper = null;
            var nbcol = $('thead th', hoursGrid).length;
            var _initialWidth = $(hoursGrid).width();
            var _wrapper = null;
            var _headerscontainer = null;
            var _footerscontainer = null;
            var _fillScrollbar = null;
            var _body = null;
            var _headers = null;
            var _footers = null;
            var _scrollWidth = util_getScrollbarWidth();
            var _colgroup = buildColgroup(nbcol);
            var _colgroup_body = null;
            var _nbRowsPerPage = 10;
            var _new_nbRowsPerPage = null;
            var _nbpages = null;
            var _nbpagesDiv = null;
            var _currentpage = null;
            var _pager = null;
            var _objectTable = null;
            var _stripNum = /[\$,%]/g;
            var _resizeInfo = null;
            var _resizeGhost = null;
            
            function buildTop(table) {
                _fillScrollbar = $('<div class="headtable" style="margin-right : 0px"></div>');
                _headerscontainer = _fillScrollbar;
                _headerscontainer.insertBefore(table);
            }

            function buildBottom(table) {
                _footerscontainer = $('<div class="foottable" style="margin-right : 0px"></div>');
                _footerscontainer.insertAfter(table);
            }

            function buildColgroup(nbcol) {
                var colgroup = $('<colgroup />');

                $('thead th', _table).each(function (i) {
                    var width = this.offsetWidth;
                    colgroup.append('<col style="width : ' + width + 'px" />');
                });

                return colgroup;
            }

            function sortColumn(table, number, sens, th) {
                if ((options.sortType.length != 0) && (options.sortType.length == nbcol)) {
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
                            return util_parseDate(options.dateFormat, cell).getTime();
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

                _objectTable.sort(function (a, b) {
                    var x = getSortKey($(a[number])[0].innerHTML);
                    var y = getSortKey($(b[number])[0].innerHTML);
                    return ((x < y) ? -1 : ((x > y) ? 1 : 0));
                })

                if (sens == 'DESC') {
                    _objectTable.reverse();
                }
                (options.pager) ? moveToPage(table) : objectToTable(_objectTable, table);


                if (options.editable) {
                    resetCellIDs(table);
                }
                else {
                    var header = $('.headtable .hoursGrid');
                    var body = $('.body .hoursGrid');
                    setCurrentWeekStyle(header, body);
                }
            }

            function objectToTable(objectArray, table) {
                var body = $('tbody', table);
                body.children().remove();
                var isEditable = options.editable;

                if (options.zebra) {
                    for (var i = 0; i < objectArray.length; i++) {
                        (i % 2) ? (tr = $('<tr class="' + options.zebraClass + '"></tr>')) : (tr = $('<tr></tr>'));

                        for (var j in objectArray[i]) {
                            tr.append(objectArray[i][j]);
                        }

                        body.append(tr);

                        if (isEditable) {
                            $('.hours', tr).droppable({ drop: handleDrop, over: handleHover, scope: "Hours" }).draggable({ opacity: 0.7, helper: "clone", revert: true, scope: "Hours", start: startDrag, stop: RemoveHoverClass });
                        }
                    }
                } else {
                    for (var i = 0; i < objectArray.length; i++) {
                        tr = $('<tr></tr>');

                        for (var j in objectArray[i]) {
                            tr.append(objectArray[i][j]);
                        }

                        body.append(tr);
                    }
                }
            }

            function tableToObject(table) {
                var objectArray = [];

                $('tr', table).each(function (i) {
                    var data = {};

                    $('td', this).each(function (j) {
                        data[j] = this.outerHTML; 
                    })

                    objectArray.push(data);
                });

                return objectArray;
            }

            function buildFooters(table) {
                _footers = $('<table class="hoursGrid" cellspacing="0" border="1" style="border-collapse:collapse;" />').append(_colgroup.clone()).append($('tfoot', table));
                _footerscontainer.append(_footers);
                _footers.wrap('<div></div>');
            }

            function buildHeaders(table) {
                _headers = $('<table class="hoursGrid" cellspacing="0" border="1" style="border-collapse:collapse;" />').append(_colgroup).append($('thead', table));
                _headerscontainer.append(_headers);
                _headers.wrap('<div></div>');

                var tab_headers = $('th', _headers);

                if (options.sortable) {
                    var th_div_sort = null;

                    tab_headers.each(function (i) {
                        $(this).contents().wrapAll('<div class="sorter"></div>');
                        th_div_sort = $('div.sorter', this);
                        th_div_sort.click(function () {

                            if (options.sortType[i] == "integer") {
                                if ($(this).hasClass('sortedDown')) {
                                    sortColumn(table, i, "ASC", this);
                                    $(this).removeClass('sortedDown').addClass('sortedUp');
                                    $(this).append('<span style="display : inline-block; vertical-align : middle"></span>');
                                } else {
                                    sortColumn(table, i, "DESC", this);
                                    $(this).removeClass('sortedUp').addClass('sortedDown');
                                    $(this).append('<span style="display : inline-block; vertical-align : middle"></span>');
                                }
                            }
                            else {
                                if ($(this).hasClass('sortedUp')) {
                                    sortColumn(table, i, "DESC", this);
                                    $(this).removeClass('sortedUp').addClass('sortedDown');
                                    $(this).append('<span style="display : inline-block; vertical-align : middle"></span>');
                                } else {
                                    sortColumn(table, i, "ASC", this);
                                    $(this).removeClass('sortedDown').addClass('sortedUp');
                                    $(this).append('<span style="display : inline-block; vertical-align : middle"></span>');
                                }
                            }
                            _headerscontainer[0].scrollLeft = _body[0].scrollLeft;
                        })
                    });

                    $('div.sorter', tab_headers).addClass('hover');
                }
            }

            function isIE6_7() {
                if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                    var ieversion = new Number(RegExp.$1);

                    if (ieversion == 7 || ieversion == 6) {
                        return true;
                    } else {
                        return false;
                    }
                }
            }

            function isIE8() {
                if (/MSIE (\d+\.\d+);/.test(navigator.userAgent)) {
                    var ieversion = new Number(RegExp.$1);

                    if (ieversion == 8) {
                        return true;
                    } else {
                        return false;
                    }
                }
            }

            function buildBody(table) {
                _body = $('<div class="body"></div>').insertBefore(table).append(table);

                if (options.height != null && !isNaN(parseInt(options.height))) {
                    var bodyHeight = _body[0].offsetHeight;
                    if (bodyHeight > options.height) {
                        _body.css('height', options.height + 'px');
                    }
                }

                _colgroup_body = _colgroup.clone();
                $(table).prepend(_colgroup_body);
                $(table).wrap('<div></div>');

                if (options.addTitles == true) {
                    $('td', table).each(function () {
                        $(this).attr('title', $(this).text());
                    });
                }

                if (options.zebra) {
                    $('tr:odd', table).addClass(options.zebraClass);
                }
            }

            function adaptScroll(table) {
                var scrollwidth = _scrollWidth;

                if (isIE6_7()) {
                    scrollwidth = 0;
                }

                var width = 0;

                if (parseInt($(table).height()) > parseInt(options.height)) {
                    width = scrollwidth;
                    overflow = 'scroll';
                } else {
                    width = 0;
                    overflow = 'auto';
                }

                if ($.browser.msie && options.height) {
                    width = scrollwidth;
                    overflow = 'scroll';
                }

                _fillScrollbar.css('margin-right', width);

                return overflow;
            }

            function getColratioWidth() {
                var tw = 0;

                for (var i = 0; i < options.colratio.length; i++) {
                    tw += parseInt(options.colratio[i]);
                }
                return tw;
            }

            function util_getComputedStyle(element, property) {
                if (element.currentStyle) {
                    var y = x.currentStyle[property];
                } else if (window.getComputedStyle) {
                    var y = document.defaultView.getComputedStyle(element, null).getPropertyValue(property);
                }
                return y;
            }

            function util_getScrollbarWidth() {
                var inner = $('<p/>').addClass('t_fixed_header_scroll_inner');
                var outer = $('<div/>').addClass('t_fixed_header_scroll_outer');

                outer.append(inner);
                $(document.body).append(outer);
                var w1 = inner[0].offsetWidth;
                outer.css('overflow', 'scroll');
                var w2 = inner[0].offsetWidth;
                if (w1 == w2) w2 = outer[0].clientWidth;
                outer.remove();
                return (w1 - w2);
            }

            function util_parseDate(format, date) {
                var tsp = { m: 1, d: 1, y: 1970, h: 0, i: 0, s: 0 }, k, hl, dM;

                if (date && date !== null && date !== undefined) {
                    date = $.trim(date);
                    date = date.split(/[\\\/:_;.\t\T\s-]/);
                    format = format.split(/[\\\/:_;.\t\T\s-]/);

                    var dfmt = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    var afmt = ["am", "pm", "AM", "PM"];
                    var h12to24 = function (ampm, h) {
                        if (ampm === 0) { if (h == 12) { h = 0; } }
                        else { if (h != 12) { h += 12; } }
                        return h;
                    };

                    for (k = 0, hl = format.length; k < hl; k++) {
                        if (format[k] == 'M') {
                            dM = $.inArray(date[k], dfmt);
                            if (dM !== -1 && dM < 12) { date[k] = dM + 1; }
                        }

                        if (format[k] == 'F') {
                            dM = $.inArray(date[k], dfmt);
                            if (dM !== -1 && dM > 11) { date[k] = dM + 1 - 12; }
                        }

                        if (format[k] == 'a') {
                            dM = $.inArray(date[k], afmt);
                            if (dM !== -1 && dM < 2 && date[k] == afmt[dM]) {
                                date[k] = dM;
                                tsp.h = h12to24(date[k], tsp.h);
                            }
                        }

                        if (format[k] == 'A') {
                            dM = $.inArray(date[k], afmt);

                            if (dM !== -1 && dM > 1 && date[k] == afmt[dM]) {
                                date[k] = dM - 2;
                                tsp.h = h12to24(date[k], tsp.h);
                            }
                        }

                        if (date[k] !== undefined) {
                            tsp[format[k].toLowerCase()] = parseInt(date[k], 10);
                        }
                    }

                    tsp.m = parseInt(tsp.m, 10) - 1;

                    var ty = tsp.y;

                    if (ty >= 70 && ty <= 99) { tsp.y = 1900 + tsp.y; }
                    else if (ty >= 0 && ty <= 69) { tsp.y = 2000 + tsp.y; }
                }

                return new Date(tsp.y, tsp.m, tsp.d, tsp.h, tsp.i, tsp.s, 0);
            }
            
            _wrapper = $('<div/>').addClass('t_fixed_header default ' + options.theme).insertBefore(hoursGrid).append(hoursGrid);
            _wrapper.css('border', 'none').css('font-weight', 'normal');
            _main_wrapper = $('<div class="t_fixed_header_main_wrapper ' + options.theme + '"></div>');

            if (options.whiteSpace == 'normal') {
                _wrapper.addClass('t_fixed_header_wrap');
            }

            _wrapper = $('<div/>').addClass('t_fixed_header default ' + options.theme).insertBefore(hoursGrid).append(hoursGrid);
            _wrapper.css('border', 'none').css('font-weight', 'normal');
            _main_wrapper = $('<div class="t_fixed_header_main_wrapper ' + options.theme + '"></div>');

            if (options.whiteSpace == 'normal') {
                _wrapper.addClass('t_fixed_header_wrap');
            }

            buildTop(hoursGrid);
            buildHeaders(hoursGrid);

            buildBottom(hoursGrid);
            buildFooters(hoursGrid);

            buildBody(hoursGrid);

            if (options.wrapper) {
                var tampon = _wrapper.wrap('<div style="padding : 5px; font-size : 1em;"></div>').parent();
            } else {
                var tampon = _wrapper.wrap('<div></div>').parent();
            }

            if (options.width != null && !isNaN(parseInt(options.width)) && options.width > 0) {
                tampon.css('width', options.width + 'px');
            }

            var res = _wrapper.detach();
            var main_wrapper_child = $('<div class="t_fixed_header_main_wrapper_child"></div>');

            _main_wrapper.append(main_wrapper_child);
            main_wrapper_child.append(res);
            tampon.append(_main_wrapper);

            if (isIE6_7()) {
                _body.css('margin-bottom', 17 + 'px');
            }

            if (options.sortable || options.pager) {
                _objectTable = tableToObject(hoursGrid);
            }

            if (options.sortable && !isNaN(parseInt(options.sortedColId))) {
                var cur_th = $('th', _headers)[options.sortedColId];
                $(cur_th).addClass('ui-state-hover')
                $('div.sorter', cur_th).click();
            }

            _body.css('overflow-y', adaptScroll(hoursGrid));

            if (options.minWidth != null && !isNaN(parseInt(options.minWidth)) && options.minWidth > 0) {
                var minWidth = options.minWidth + 'px';
            } else if (options.minWidthAuto) {
                if (options.colratio.length == nbcol) {
                    var minWidth = $(hoursGrid).width() + 'px';
                } else {
                    var minWidth = (_initialWidth + 150) + 'px';
                }
            }

            _headerscontainer.children().first().css('min-width', minWidth);
            _body.children().first().css('min-width', minWidth);

            _body.scroll(function () {
                _headerscontainer[0].scrollLeft = _body[0].scrollLeft;
            });

            if (options.colratio.length == nbcol) {
                _wrapper.removeClass('default');
                $(_headers).css('width', getColratioWidth() + 'px');
            }
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







