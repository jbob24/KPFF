$.widget("kpff.engineerdetails", {

    // These options will be used as defaults
    options: {
        //clear: null
    },

    // Set up the widget
    _create: function () {
        //alert('hello');
        this._setEvents();
        $('.weekCal').datepicker();
    },

    _setEvents: function () {
        var self = this;
        $('#linkCurrentWeek').live('click', self._thisWeek);
        $('#linkNextWeek').live('click', self._nextClick);
        $('#linkPrevious').live('click', self._prevClick);
    },

    _nextClick: function () {
        var dateData = $('.weekCal').val();
        $.ajax
        (
            {
                type: 'POST',
                url: '../Service/Service1.svc/GetNextWeekDate',
                dataType: 'json',
                contentType: 'application/json',
                data: '{ "request": { "Date": "' + dateData + '"} }',
                success: function (response, type, xhr) {
                    $('.weekCal').val(response.GetNextWeekDateResult.Date);
                },
                error: function (xhr) {
                    window.alert('error: ' + xhr.statusText);
                }
            }
        );
    },

    _prevClick: function () {
        var dateData = $('.weekCal').val();
        $.ajax
        (
            {
                type: 'POST',
                url: '../Service/Service1.svc/GetPreviousWeekDate',
                dataType: 'json',
                contentType: 'application/json',
                data: '{ "request": { "Date": "' + dateData + '"} }',
                success: function (response, type, xhr) {
                    $('.weekCal').val(response.GetPreviousWeekDateResult.Date);
                },
                error: function (xhr) {
                    window.alert('error: ' + xhr.statusText);
                }
            }
        );
    },

    _thisWeek: function () {
        $.ajax
        (
            {
                type: 'GET',
                url: '../Service/Service1.svc/GetThisWeekDate',
                dataType: 'json',
                data: '',
                success: function (response, type, xhr) {
                    $('.weekCal').val(response);
                },
                error: function (xhr) {
                    window.alert('error: ' + xhr.statusText);
                }
            }
        );
    },

    // Use the _setOption method to respond to changes to options
    _setOption: function (key, value) {
        //            switch (key) {
        //                case "clear":
        //                    // handle changes to clear option
        //                    break;
        //            }

        //            // In jQuery UI 1.8, you have to manually invoke the _setOption method from the base widget
        //            $.Widget.prototype._setOption.apply(this, arguments);
        //            // In jQuery UI 1.9 and above, you use the _super method instead
        //            this._super("_setOption", key, value);
    },

    // Use the destroy method to clean up any modifications your widget has made to the DOM
    destroy: function () {
        // In jQuery UI 1.8, you must invoke the destroy method from the base widget
        $.Widget.prototype.destroy.call(this);
        // In jQuery UI 1.9 and above, you would define _destroy instead of destroy and not call the base method
    }//,

    //_updateResults(
});
