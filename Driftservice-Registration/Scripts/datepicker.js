$("#datepicker").datepicker({
    dateFormat: 'yy-mm-dd'
});

$(".input-group-addon").click(function () {
    $("#datepicker").datepicker("show");
});