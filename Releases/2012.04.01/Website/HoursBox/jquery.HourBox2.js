(function ($) {

    $.fn.outerHTML = function () {

        // IE, Chrome & Safari will comply with the non-standard outerHTML, all others (FF) will have a fall-back for cloning
        return (!this.length) ? this : (this[0].outerHTML || (
      function (el) {
          var div = document.createElement('div');
          div.appendChild(el.cloneNode(true));
          var contents = div.innerHTML;
          div = null;
          return contents;
      })(this[0]));

    }

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
            dateFormat: 'm-d-y',
            maxColWidth: 250,
            startingParentId: 0,
            scrollable: true,
            multicolsort: false
        };

        var options = $.extend(defaults, options);
        var parentId = options.startingParentId;

        var header = null;
        var body = null;

        return this.each(function (i, e) {
            setTableParent(e);

            if (options.scrollable) {
                var scrollableTable = setupScrolling(e);

                header = $('.headtable .hoursGrid', scrollableTable);
                body = $('.body .hoursGrid', scrollableTable);
            } else {
                body = e;
                header = e;
            }

            if (options.editable) {
                $(body).EditHourbox({ parentId: parentId });
            }
            else {
                setCurrentWeekStyle(header, body);
            }

        });

        function setTableParent(hoursGrid) {
            var hoursGridDiv = $(hoursGrid).parent();
            hoursGridDiv[0].id = 'hoursGridDiv' + parentId;
            hoursGridDiv.addClass("hoursGridDiv");

            parentId += 1;
        }

        function setCurrentWeekStyle(headerTable, body) {
            var header = $("th", headerTable).each(function (i, e) {
                var headerStyled = $(".currentWeekHeader", e);
                if (headerStyled.length > 0) {
                    $(body).find('td').filter(':nth-child(' + (i + 1) + ')').addClass(options.curweekstyle)
                }
            });
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
            var _minWrapperWidth = 0;
            var _colgroup = buildColgroup(nbcol);
            var _colgroup_body = null;
            var _nbRowsPerPage = 10;
            var _new_nbRowsPerPage = null;
            var _nbpages = null;
            var _nbpagesDiv = null;
            var _currentpage = null;
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
                    var maxColWidth = options.maxColWidth;
                    var width = this.offsetWidth < maxColWidth ? this.offsetWidth : maxColWidth;
                    _minWrapperWidth += width;
                    colgroup.append('<col style="width : ' + width + 'px; min-width: ' + width + 'px;" />');
                });

                return colgroup;
            }

            function buildFooters(table) {
                var footerColGroup = _colgroup.clone();
                var footer = $('tfoot', table);

                if (footer && footer.length > 0) {
                    if ($('td:has(img)', footer)) {
                        var img = $('img', footer);

                        $('img', footer).remove();

                        // merge first 3 cols in colgroup
                        var cols = $('col', footerColGroup);
                        var colWidth = getColWidth(cols[0]) + getColWidth(cols[1]) + getColWidth(cols[2]) + 9;

                        // merger first 3 cols in footer
                        $('td:lt(2)', footer).remove();
                        $('td:first', footer).attr('colspan', '3').append(img);

                        var col1Width = 0;

                        $('col', footerColGroup).each(function (i, e) {
                            if (i <= 2) {
                                col1Width += getColWidth(this);
                            }

                            if (i == 2) {
                                $('td:eq(0)', footer).attr('width', (col1Width - 7) + 'px');
                            }

                            if (i > 2) {
                                var width = getColWidth(this);
                                $('td:eq(' + (i - 2) + ')', footer).attr('width', (width - 7) + 'px');
                            }
                        });
                    }

                    _footers = $('<table class="hoursGrid" cellspacing="0" border="1" style="border-collapse:collapse;" />').append(footer);
                    _footerscontainer.append(_footers);
                    _footers.wrap('<div></div>');
                }
            }

            function getColWidth(col) {
                return parseInt($(col).css('width').replace('px', ''));
            }

            function buildHeaders(table) {
                _headers = $('<table class="hoursGrid" cellspacing="0" border="1" style="border-collapse:collapse;" />').append(_colgroup).append($('thead', table));
                _headerscontainer.append(_headers);
                _headers.wrap('<div></div>');
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
            _wrapper.css('border', 'none').css('font-weight', 'normal').css('min-width', (_minWrapperWidth + 20) + "px"); //(_initialWidth + 20) + "px"); // (_wrapper[0].offsetWidth + _scrollWidth) + "px");
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

            var res = _wrapper.detach();
            var main_wrapper_child = $('<div class="t_fixed_header_main_wrapper_child"></div>');

            _main_wrapper.append(main_wrapper_child);
            main_wrapper_child.append(res);
            tampon.append(_main_wrapper);

            if (isIE6_7()) {
                _body.css('margin-bottom', 17 + 'px');
            }

            if (options.sortable && options.sortType.length != 0 && options.sortType.length == nbcol) {
                $(_headers).sortable({ sortType: options.sortType, table: hoursGrid, dateFormat: options.dateFormat, editable: options.editable, zebraClass: options.zebraClass, zebra: options.zebra, parentId: parentId, multicolsort: options.multicolsort });
            }

            _body.css('overflow-y', adaptScroll(hoursGrid));

            _body.scroll(function () {
                _headerscontainer[0].scrollLeft = _body[0].scrollLeft;
            });

            if (options.colratio.length == nbcol) {
                _wrapper.removeClass('default');
                $(_headers).css('width', getColratioWidth() + 'px');
            }

            return _main_wrapper;
        }
    };

})(jQuery);








