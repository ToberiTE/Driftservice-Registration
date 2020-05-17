var loader = $('.spinner-overlay');
var timer;
$(document).ajaxStart(function () {
    clearTimeout(timer);
    timer = setTimeout(function () {
        loader.fadeIn();
    }, 300);
})
    .ajaxStop(function () {
        clearTimeout(timer);
        loader.fadeOut();
    })
    .ajaxError(function () {
        clearTimeout(timer);
        loader.fadeOut();
    });

function RegisterContact() {
    var captcha = $("#g-recaptcha-response").val();
    var service = [];
    $('.list-group-item :checkbox:checked').each(function () {
        service.push({ ServiceTypeID: $(this).attr('id') });
    });
    var contact = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Business: $('#Business').val(),
        Email: $('#Email').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        EmailChecked: $('#EmailChecked').val(),
        SmsChecked: $('#SmsChecked').val(),
        NotificationType: $('#NotificationType').val(),
        Language: $('#Language').val(),
        ServiceTypes: service
    };
    var validateService = ValidateServices();
    var validateFields = ValidateFields();
    if (validateFields && validateService) {
        $.post("/Contacts/Create", { contact, captcha })
            .done(function () {
                toastr.success("Registrering slutförd!");
                setTimeout(function () {
                    $("form").trigger("reset");
                    window.location.reload();
                    history.go(0);
                }, 5000);
                
            })
            .fail(function () {
                toastr.error("Fel! Försök igen.");
            });
    }
}

function ValidateFields() {
    var isValid = true;
    $('.required').each(function () {
        if ($.trim($(this).val()) === '') {
            isValid = false;
            $(this).addClass('error-border');
            toastr.error("Fel i formuläret hittades.");
            setTimeout(function () {
                $('.required').removeClass('error-border');
            }, 5000);
        }
        else {
            $(this).removeClass('error-border');
        }
    });
    return isValid;
}

function ValidateServices() {
    if ($('.list-group-item :checkbox:checked').length === 0) {
        $('.list-group').addClass('error-border');
        toastr.error('Välj minst en tjänst.');
        return false;
    }
    else {
        $('.list-group').removeClass('error-border');
    }
    return true;
}

toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": true,
    "progressBar": false,
    "positionClass": "toast-bottom-center",
    "preventDuplicates": true,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "4000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut",
    "closeOnHover": false
};