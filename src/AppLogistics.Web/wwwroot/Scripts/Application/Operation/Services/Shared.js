$(function () {
    // Disable elements on loading
    $("button[data-for$='VehicleTypeId']").prop('disabled', true);
    $("button[data-for$='VehicleTypeId']").hide();

    $("div.mvc-lookup-control[data-for$='VehicleTypeId'] > input").val('');
    $("div.mvc-lookup-control[data-for$='VehicleTypeId'] > input").prop('disabled', true);

    // Add event to checkbox
    $("#SpecifyVehicleType").click(function () {

        var allowSelectingVehicle = $('#SpecifyVehicleType').is(":checked");

        if (allowSelectingVehicle) {
            $("button[data-for$='VehicleTypeId']").prop('disabled', false);
            $("button[data-for$='VehicleTypeId']").show();

            $("div.mvc-lookup-control[data-for$='VehicleTypeId'] > input").prop('disabled', false);
        } else {
            $("button[data-for$='VehicleTypeId']").prop('disabled', true);
            $("button[data-for$='VehicleTypeId']").hide();

            $("div.mvc-lookup-control[data-for$='VehicleTypeId'] > input").val('');
            $("div.mvc-lookup-control[data-for$='VehicleTypeId'] > input").prop('disabled', true);
        }
    });
});

