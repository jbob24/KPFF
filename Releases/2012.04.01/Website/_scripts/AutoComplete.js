
var ACServiceURL = "AutoComplete.svc/";
var ACServiceProxy = new serviceProxy(ACServiceURL);
var ProjectsResponse;

$(document).ready(function () {
    SetupProjectsAC();

    $('.sortName').click(function () {
        //$(ProjectsResponse).sort(sortByProjectName);
        ProjectsResponse = projectsListByName;
        ProcessProjectsResponse();
    });

    $('.sortNumber').click(function () {
        //$(ProjectsResponse).sort(sortByProjectNo);
        ProjectsResponse = projectsListByNumber;
        ProcessProjectsResponse();
    });

    $('#ACTextBox').focus(function () {
        if (this.value == "")
            $(this).trigger('keydown.autocomplete');
    });
});

function SetupProjectsAC() {
    //ProjectsResponse = projectsList;
    ProjectsResponse = projectsListByName;
    //$(ProjectsResponse).sort(sortByProjectNo);
    ProcessProjectsResponse();


    //    ACServiceProxy.invoke({ serviceMethod: "GetProjects",
    //        callback: function (response) {
    //            ProjectsResponse = response;
    //            $(ProjectsResponse).sort(sortByProjectName);
    //            ProcessProjectsResponse();
    //        },
    //        error: function (xhr, errorMsg, thrown) {
    //            postError(xhr, errorMsg, thrown);
    //        }
    //    });
}

function ProcessProjectsResponse() {
    var data = $.map(ProjectsResponse, function (item) {
        return {
            value: item.ProjectNumber + " " + item.ProjectName,
            id: item.ID
        };
    });

    //$("#ACTextBox").autocomplete({ source: data });

        $("#ACTextBox").autocomplete({
            source: data,
            minLength: 0,
            delay: 0,
            scrollHeight: 100,
            select: function (event, ui) {
                log(ui.item ?
    							"Selected: " + ui.item.value + ", ID: " + ui.item.id :
    							"Nothing selected, input was " + this.value);
            }
        }); 

    //    .focus(function () {
    //        if (this.value == "")
    //            $(this).trigger('keydown.autocomplete');
    //    });
}

//Error Handling
function postError(xhr, errorMsg, thrown) {
    alert(errorMsg);
}

function log(message) {
    $("<div/>").text(message).prependTo("#log");
    $("#log").attr("scrollTop", 0);
}


function serviceProxy(wjOrderServiceURL) {
    var _I = this;
    this.ServiceURL = wjOrderServiceURL;

    // *** Call a wrapped object
    this.invoke = function (options) {

        // Default settings
        var settings = {
            serviceMethod: '',
            data: null,
            callback: null,
            error: null,
            type: "POST",
            processData: false,
            contentType: "application/json",
            timeout: 120000, //Not Preferable, but needed for now.
            dataType: "text",
            bare: false
        };

        if (options) {
            $.extend(settings, options);
        }

        // *** Convert input data into JSON - REQUIRES Json2.js
        var json = JSON2.stringify(settings.data);

        // *** The service endpoint URL
        var url = _I.ServiceURL + settings.serviceMethod;

        $.ajax({
            url: url,
            data: json,
            type: settings.type,
            processData: settings.processData,
            contentType: settings.contentType,
            timeout: settings.timeout,
            error: settings.error,
            dataType: settings.dataType,
            success:
                    function (res) {
                        if (!settings.callback) { return; }

                        // *** Use json library so we can fix up MS AJAX dates
                        var result = JSON2.parse(res);

                        if (result.ExceptionDetail) {
                            OnPageError(result.Message);
                            return;
                        }

                        // *** Bare message IS result
                        if (settings.bare)
                        { settings.callback(result); return; }

                        //http://encosia.com/2009/07/21/simplify-calling-asp-net-ajax-services-from-jquery/
                        if (result.hasOwnProperty('d'))
                        { return settings.callback(result.d); }
                        else
                        { return result; }
                    }
        });
    };
}

//$.fn.sort = function () {
//    return this.pushStack([].sort.apply(this, arguments), []);
//};

//function sortByProjectName(a, b) {
//    return a.ProjectName > b.ProjectName ? 1 : -1;
//};

//function sortByProjectNo(a, b) {
//    return parseFloat(a.ProjectNumber) > parseFloat(b.ProjectNumber) ? 1 : -1;
//};




(function ($) {
    $.widget("ui.combobox", {
        _create: function () {
            var self = this,
					select = this.element.hide(),
					selected = select.children(":selected"),
					value = selected.val() ? selected.text() : "";
            var input = $("<input>")
					.insertAfter(select)
					.val(value)
					.autocomplete({
					    delay: 0,
					    minLength: 0,
					    source: function (request, response) {
					        var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
					        response(select.children("option").map(function () {
					            var text = $(this).text();
					            if (this.value && (!request.term || matcher.test(text)))
					                return {
					                    label: text.replace(
											new RegExp(
												"(?![^&;]+;)(?!<[^<>]*)(" +
												$.ui.autocomplete.escapeRegex(request.term) +
												")(?![^<>]*>)(?![^&;]+;)", "gi"
											), "<strong>$1</strong>"),
					                    value: text,
					                    option: this
					                };
					        }));
					    },
					    select: function (event, ui) {
					        ui.item.option.selected = true;
					        self._trigger("selected", event, {
					            item: ui.item.option
					        });
					    },
					    change: function (event, ui) {
					        if (!ui.item) {
					            var matcher = new RegExp("^" + $.ui.autocomplete.escapeRegex($(this).val()) + "$", "i"),
									valid = false;
					            select.children("option").each(function () {
					                if (this.value.match(matcher)) {
					                    this.selected = valid = true;
					                    return false;
					                }
					            });
					            if (!valid) {
					                // remove invalid value, as it didn't match anything
					                $(this).val("");
					                select.val("");
					                return false;
					            }
					        }
					    }
					})
					.addClass("ui-widget ui-widget-content ui-corner-left");

            input.data("autocomplete")._renderItem = function (ul, item) {
                return $("<li></li>")
						.data("item.autocomplete", item)
						.append("<a>" + item.label + "</a>")
						.appendTo(ul);
            };

            $("<button>&nbsp;</button>")
					.attr("tabIndex", -1)
					.attr("title", "Show All Items")
					.insertAfter(input)
					.button({
					    icons: {
					        primary: "ui-icon-triangle-1-s"
					    },
					    text: false
					})
					.removeClass("ui-corner-all")
					.addClass("ui-corner-right ui-button-icon")
					.click(function () {
					    // close if already visible
					    if (input.autocomplete("widget").is(":visible")) {
					        input.autocomplete("close");
					        return;
					    }

					    // pass empty string as value to search for, displaying all results
					    input.autocomplete("search", "");
					    input.focus();
					});
        }
    });
})(jQuery);

$(function () {
    $("select[id$='cboProjectName']").combobox();
});
