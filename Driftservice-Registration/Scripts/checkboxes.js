$(".check").click(function () {
    $(this).closest('.list-group-item').find('.pending').css('display', this.checked ? 'none' : 'block');
    $(this).closest('.list-group-item').find('.done').css('display', this.checked ? 'block' : 'none');
    $(this).closest('.list-group-item').css('background-color', this.checked ? '#0275D8' : '');
    $(this).closest('.list-group-item').css('color', this.checked ? '#fff' : '');
});

$(".check").before(function () {
    $(this).closest('.list-group-item').find('.pending').css('display', this.checked ? 'none' : 'block');
    $(this).closest('.list-group-item').find('.done').css('display', this.checked ? 'block' : 'none');
    $(this).closest('.list-group-item').css('background-color', this.checked ? '#0275D8' : '');
    $(this).closest('.list-group-item').css('color', this.checked ? '#fff' : '');
});
