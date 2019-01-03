$(function () {
    $("#datepicker").datepicker({
        dateFormat: "dd/mm/yy"
    });

    $("#datetoggle").click(function () {
        $("#datepicker").datepicker('show');
    });
});