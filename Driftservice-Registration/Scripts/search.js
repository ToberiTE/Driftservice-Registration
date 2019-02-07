$('#searchContact').keyup(function () {
    var result = $(this).val().trim().toLowerCase().split(' ');

    $('.cTable tr').hide().filter(function () {
        var text = $(this).text().toLowerCase();

        for (var i = 0; i < result.length; i++) {
            if (text.indexOf(result[i]) >= 0) {
                return true;
            }
        }
    }).show();
});