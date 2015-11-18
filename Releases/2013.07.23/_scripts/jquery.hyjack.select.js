/**
*	Hyjack Select plugin for jQuery
*	
*	@version 
*		1.0.1
*
*	@author
*		Copyright (c) 2010 Brant Wills
*		August 31, 2010
*	
*	@requires 
*		jQuery 1.4.2+
*		Dual licensed under MIT or GPL Version 2 licenses
*
*	summary
*		Hijack the select control and replace with one which incorporates searching
*		through a textbox similar to a google suggest.
*
*	@returns 
*		the hidden original jQuery matched set 
*		along with the visible hyjacked version of the select control appeneded using after().
*
*	remarks
*		The selector control (this) is hidden from the page and the new hyjack select control is appended after.
*		Any change() to select val() are stored and triggered back to the select control (this) preventing code breaks.
*		Any modification to display needs to be handled through CSS not hardcoded (see sample css below).
*/

(function ($) {
    var 
	    version = 'v1.0.0',
	    hyjackable = 'select';

    // Extend Contains: CaSe INsensative 
    $.expr[':'].hjsel_contains = function (a, i, m) { return jQuery(a).text().toUpperCase().indexOf(m[3].toUpperCase()) >= 0; };

    // Hyjack Select
    $.fn.hyjack_select = function (settings) {
        try {

            // Default settings
            settings = jQuery.extend({
                ddImage: 'arrow_down.png',
                ddCancel: 'cancel.png',
                ddImageClass: 'hjsel_ddImage',
                ddCancelClass: 'hjsel_ddCancel',
                emptyMessage: 'No Items to Display',
                restrictSearch: false,
                checkScroll: false
            }, settings);

            // Return hyjack control if allowed/hyjackable
            return this.filter(hyjackable).each(function (index, value) {

                // Igonor if already hyjacked
                if ($(this).attr('hyjacked') == 'select') { return; }
                $(this).attr('hyjacked', 'select');

                // Inject index and settings
                hjsel = {
                    selector: $(value),
                    container: $('<div id="hjsel_' + index + '"/>'),
                    select: $('<div id="hjsel' + index + '_select" class="hjsel_select"/>'),
                    options: $('<div id="hjsel' + index + '_options" class="hjsel_options"/>'),
                    txtbox: $('<input id="hjsel' + index + '_txtbox" type="text" class="hjsel_txtbox" />'),
                    ddImage: $('<img id="hjsel' + index + '_ddImage" class="' + settings.ddImageClass + '" />'),
                    ddCancel: $('<img id="hjsel' + index + '_ddCancel" class="' + settings.ddCancelClass + '"/>')
                };

                $(value)
				    .hide()
				    .after(hyjack(value, hj = hjsel));
            });
        }
        catch (error) { alert('Hyjack Error ' + version + ':\n' + error.description); }


        function hyjack(value, hj) {

            // Build textbox and inject items
            _txtbox(hj);
            _items($(value), hj);

            // Build ddImage and ddCancel
            hj.ddImage.attr('src', settings.ddImage);
            hj.ddCancel.attr('src', settings.ddCancel);

            // Inject hyjacked control
            hj.select
			    .append(hj.txtbox)
			    .append(hj.ddCancel)
			    .append(hj.ddImage);

            // Adjust Position and inject
            _scroll(hj);
            hj.container
			    .append(hj.select)
			    .append(hj.options);

            // Hookup Away click functions
            $(window)
				.scroll(function () { _scroll(hj); });
            $(document)
				.bind('click', function (e) { _away(e, hj); });
            return hj.container;
        }

        // Determin Key press
        function _keyup(hj, e) {
            switch (e.keyCode) {
                case 37: break; case 39: break; case 16: break; // unused
                case 17: break; case 18: break; case 19: break; // unused
                case 20: break; case 33: break; case 34: break; // unused
                case 35: break; case 36: break; case 45: break; // unused        
                case 38: // up
                    _dirSelect($('option:selected', hj.selector).prev(), hj);
                    break;
                case 40: // down
                    _dirSelect($next = $('option:selected', hj.selector).next(), hj);
                    break;
                case 13: // return

                    break;
                case 9:  // tab canceled out

                    break;
                case 27: // escape
                    options.hide();
                    break;
                default: // keyup
                    var i = 0, j = 0;
                    $('li', hj.options).remove('.hjsel_noitems').hide();
                    $('li:hjsel_contains("' + hj.txtbox.val() + '")', hj.options).show();
                    $('li', hj.options).each(function () { if ($(this).is(':hidden')) { i++; } j++; });
                    if (i == j) {
                        hj.options.append(
							$('<li/>')
								.append(settings.emptyMessage)
								.addClass('hjsel_noitems')
						);
                    }
                    hj.options
						.scrollTop(0)
						.show();
                    break;
            }
        }


        // Direction Select Change
        function _dirSelect($dir, hj) {
            if ($dir != null) {
                hj.txtbox.val($dir.text());
                hj.selector.val($dir.val());
            }
            $('li', hj.options).each(function (index, value) {
                $(value).removeClass('hjsel_options_hover');
                if (hj.selector.val() == $(value).attr('val')) {
                    $(value).addClass('hjsel_options_hover')

                    // options scroll algorithm
                    var pos = $(value).position().top;
                    hj.options.scrollTop(pos);

                }
            });
        }


        // Clear text from textbox
        // Clear any previous option items
        // Display options and focus textbox
        function _clear(hj) {
            $('.hjsel_options').hide();
            hj.txtbox.val('').focus();
            hj.options.show();

            _keyup(hj, 0);
        }


        // Display all option items except noitems
        function _reset(hj) {
            $('li', hj.options).show();
            $('.hjsel_noitems', hj.options).hide();
            _dirSelect(null, hj);
        }

        // Away click algorithms
        function _away(e, hj) {
            if ($(e.target).attr('id') == hj.ddImage.attr('id')) { hj.options.toggle(); }                   // Dropdown Arrow Clicked
            else if ($(e.target).attr('id') == hj.ddCancel.attr('id')) { _clear(hj); }                      // Cancel Button Clicked
            else if ($(e.target).attr('id') == hj.txtbox.attr('id')) { hj.options.show(); }                 // Textbox Area Clicked
            else if ($(e.target).parents().attr('id') == hj.container.attr('id')) { hj.options.show(); }    // Control Clicked
            else {
                hj.options.hide(); // Avoid leaving textbox text empty
                if (hj.txtbox.val() === '') { hj.txtbox.val($('option:selected', hj.selector).text()); }
            }

            // Determine fate of textbox text
            if (settings.restrictSearch) { hj.txtbox.val($('option:selected', hj.selector).text()); }
            _reset(hj);
        }

        // Scroll algorithms
        function _scroll(hj) {
            var 
				pos = $(hj.container).position(),
				offset = hj.options.innerHeight() + hj.select.innerHeight();

            if (pos.top + hj.options.innerHeight() > $(window).height() && settings.checkScroll) {
                hj.options.css('margin-top', '-' + offset);
            }
            else {
                hj.options.css('margin-top', '0');
            }
        }

        // Inject the textbox values and events
        function _txtbox(hj) {
            hj.txtbox
                .bind('click', function () { _clear(hj); })
			    .bind('keyup', function (e) { _keyup(hj, e); })
                .val($('option:selected', hj.selector).text());
        }

        // Inject the listitems values and events
        function _items($value, hj) {
            var listitems = $('<ul/>');
            $('option', $value).each(function (index, value) {
                listitems.append(
				    $('<li/>')
                        .attr('val', $(value).val())
					    .append($(value).text())
					    .bind('mouseover', function () {
					        $('li', listitems).removeClass('hjsel_options_hover');
					        $(this).addClass('hjsel_options_hover');
					    })
					    .bind('click', function () {
					        $(this).addClass('hjsel_options_hover');
					        hj.txtbox
                                .focus()
								.val($(value).text());

					        hj.selector
                                .val($(value).val())
                                .change();
					        _reset(hj);
					    })
			    );
            });

            hj.options.append(listitems);
        }
    };
})(jQuery);



