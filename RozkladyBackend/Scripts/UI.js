$(document).ready(function () {
    $(document).on('change', 'input[name="validOnDay"]', {}, function() {
        $("input[name='rawDeparture']").focus();
    });
});