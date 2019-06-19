Datepicker = {
    init: function () {
        var lang = document.documentElement.lang;

        if ($.fn.datepicker) {
            $.datepicker.setDefaults(window.cultures.datepicker[lang]);
            $('.datepicker').datepicker({
                changeMonth: true,
                changeYear: true,
                firstDay: 0,
                showOtherMonths: true,
                selectOtherMonths: true,
                yearRange: "c-4:c+2",
                beforeShow: function (element) {
                    return !element.readOnly;
                },
                onSelect: function (value, data) {
                    this.blur();

                    if (value != data.lastVal) {
                        $(this).change();
                    }
                }
            });
        }

        if ($.fn.timepicker) {
            $.timepicker.setDefaults(window.cultures.timepicker[lang]);
            $('.datetimepicker').datetimepicker({
                beforeShow: function (element) {
                    return !element.readOnly;
                },
                onSelect: function () {
                    this.blur();
                }
            });
        }
    }
};
