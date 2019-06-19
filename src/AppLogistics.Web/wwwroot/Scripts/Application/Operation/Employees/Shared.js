$(function () {
    $("#BornDate").datepicker({
        changeMonth: true,
        changeYear: true,
        defaultDate: "-28y",
        firstDay: 0,
        maxDate: "-18y",
        showOtherMonths: true,
        selectOtherMonths: true,
        yearRange: "c-8:c+8"
    });
});