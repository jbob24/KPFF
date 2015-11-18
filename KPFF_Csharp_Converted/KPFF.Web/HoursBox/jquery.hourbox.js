(function ($) {

    $.fn.hourbox = function (options) {

        var defaults = {
            height: null,
            width: null,
            sortable: false,
            sortedColId: null,
            sortType: [],
            colresizable: false,
            colratio: [],
            zebra: false,
            zebraClass: 'ui-state-active',
            wrapper: true
        };

        var options = $.extend(defaults, options);

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


        return this.each(function () {
            var _table = $(this);
            var main_wrapper = null;
            var nbcol = $('thead th', this).length;
            var _initialWidth = $(this).width();
            var _wrapper = null;
            var _headerscontainer = null;
            var _footercontainer = null;
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

            function buildColgroup(nbcol) {
                var colgroup = $('<colgroup />');

                if (options.colratio.length == 0) {
                    var temp = null;

                    for (var i = 0; i < nbcol; i++) {
                        temp = $('<col style="width : ' + (100 / nbcol) + '%" id="' + (i + 1) + '" />');
                        colgroup.append(temp);
                        temp = null;
                    }
                } else if (options.colratio.length == nbcol) {
                    var cw = 0;

                    for (var i = 0; i < nbcol; i++) {
                        temp = $('<col style="width : ' + options.colratio[i] + 'px" id="' + (i + 1) + '" />');
                        colgroup.append(temp);
                        temp = null;
                        cw += parseInt(options.colratio[i]);
                    }
                    $(_table).css('width', cw + 'px');
                }
                return colgroup;
            }

            function buildHeaders(table) {
                _headers = $('<table class="hoursGrid"/>').css('border-spacing', '0px').css('border-padding', '0px').append(_colgroup); //.append($('thead', table));
                _headerscontainer.append(_headers);
                _headers.wrap('<div></div>');

                var tab_headers = $('th', _headers);
                //tab_headers.addClass('ui-widget-content ui-state-default');
            }


            function buildFooters(table) {

                _footercontainer = $('<div class="foottable"></div>');
                _footercontainer.insertAfter(table);
                _footers = $('<table class="hoursGrid"/>').css('border-spacing', '0px').css('border-padding', '0px').append(_colgroup).append($('tfoot', table));
                _footercontainer.append(_footers);
                _footers.wrap('<div></div>');



                //                _headers = $('<table class="hoursGrid"/>').css('border-spacing', '0px').css('border-padding', '0px').append(_colgroup).append($('tfoot', table));
                //                _headerscontainer.append(_headers);
                //                _headers.wrap('<div></div>');

                //                var tab_headers = $('th', _headers);
                //tab_headers.addClass('ui-widget-content ui-state-default');
            }



            function buildBody(table) {
                _body = $('<div class="body "></div>').insertBefore(table).append(table);

                if (options.height != null && !isNaN(parseInt(options.height))) {
                    _body.css('height', options.height + 'px');
                }

                _colgroup_body = _colgroup.clone();
                $(table).prepend(_colgroup_body);
                //$('td', table).addClass('ui-widget-content');
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

            function objectToTable(objectArray, table) {

                var body = $('tbody', table);

                body.children().remove();

                if (options.zebra) {

                    for (var i = 0; i < objectArray.length; i++) {

                        (i % 2) ? (tr = $('<tr class="' + options.zebraClass + '"></tr>')) : (tr = $('<tr></tr>'));

                        for (var j in objectArray[i]) {

                            tr.append($('<td></td>').html(objectArray[i][j]));
                        }

                        body.append(tr);
                    }

                } else {

                    for (var i = 0; i < objectArray.length; i++) {

                        tr = $('<tr></tr>');

                        for (var j in objectArray[i]) {

                            tr.append($('<td></td>').html(objectArray[i][j]));
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

                        data[j] = $(this).html();
                    })

                    objectArray.push(data);
                });

                return objectArray;
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

            /***********************/
            /********* MAIN ********/
            /***********************/

            _wrapper = $('<div/>').addClass('t_fixed_header default ' + options.theme).insertBefore(this).append(this);
            _wrapper.css('border', 'none').css('font-weight', 'normal');
            _main_wrapper = $('<div class="t_fixed_header_main_wrapper ' + options.theme + '"></div>');

            if (options.whiteSpace == 'normal') {
                _wrapper.addClass('t_fixed_header_wrap');
            }

            buildTop(this);
            buildHeaders(this);
            buildBody(this);
            //buildFooters(this);

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

                _objectTable = tableToObject(this);
            }

            if (options.pager) {

                buildPager(this);
            }

            if (options.sortable && !isNaN(parseInt(options.sortedColId))) {

                var cur_th = $('th', _headers)[options.sortedColId];

                //$(cur_th).addClass('ui-state-hover')

                //$('div.ui-sort', cur_th).click();
            }

            if (options.resizeCol && (options.colratio.length == nbcol)) {

                _resizeGhost = $('<div style="height : ' + _main_wrapper.parent().height() + 'px"></div>');

                _wrapper.append(_resizeGhost);
            }

            _body.css('overflow-y', adaptScroll(this));

            if (options.minWidth != null && !isNaN(parseInt(options.minWidth)) && options.minWidth > 0) {

                var minWidth = options.minWidth + 'px';

            } else if (options.minWidthAuto) {

                if (options.colratio.length == nbcol) {

                    var minWidth = $(this).width() + 'px';

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
        });
    };

})(jQuery);