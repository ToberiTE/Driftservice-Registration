$('#searchContact').keyup(function () {
    var result = $(this).val().trim().toLowerCase().split(' ');

    $('tr:not(:first-of-type)').hide().filter(function () {
        var text = $(this).text().toLowerCase();

        for (var i = 0; i < result.length; i++) {
            if (text.indexOf(result[i]) >= 0) {
                return true;
            }
        }
    }).show();
});