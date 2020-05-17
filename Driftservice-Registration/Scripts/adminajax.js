$(document).ready(function () {
    GetContacts();
    GetServiceTypes();
});

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

function GetContacts(val = 0) {
    $.get("/Admin/GetContacts/", { filterByServiceId: val })
        .done(function (result) {
            var contactTable = '';
            $.each(result, function (key, item) {
                contactTable += '<tr>';
                contactTable += '<td>' + item.FirstName + '</td>';
                contactTable += '<td>' + item.LastName + '</td>';
                contactTable += '<td>' + item.Business + '</td>';
                contactTable += '<td>' + item.Email + '</td>';
                contactTable += '<td>' + (item.PhoneNumber === null ? "" : item.PhoneNumber) + '</td>';
                contactTable += '<td>' + (item.NotificationType === 1 ? "Email" : item.NotificationType === 2 ? "Sms" : "Email & Sms") + '</td>';
                contactTable += '<td>' + (item.Language === 1 ? "Svenska" : item.Language === 2 ? "Engelska" : item.Language === 3 ? "Tyska" : "") + '</td>';
                contactTable += '<td>' + ConvertDate(item.RegDate) + '</td>';
                contactTable += '<td>' + item.ServiceTypeID + '</td>';
                contactTable += '<td><a href="#" title="Ändra" onclick="GetContactById(' + item.ContactID + ')"><i class="fa fa-edit" id="editIcon"></i></a> <a href="#" title="Ta bort" onclick="DeleteContact(' + item.ContactID + ')"><i class="fa fa-trash-alt" id="delIcon"></i></a></td>';
                contactTable += '</tr>';
            });
            $('.cTable').html(contactTable);
        })
        .fail(function () {
            toastr.error("Kontakter kunde inte hämtas!");
        });
}

function ConvertDate(obj) {
    if (obj !== null) {
        var date = new Date(parseInt(obj.substr(6)));
        return date.toLocaleString("sv-SE").substr(0, 10);
    }
    return "";
}

function CreateContactModal() {
    $.get("/Admin/NewContact", function (result) {
        $('#createContactModal').modal('show');
        $('#createContactModalContent').html(result);
        setTimeout(function () {
            $('#createDescription').focus();
        }, 500);
        $('#createContact').blur();
    });
}

function CreateContact() {
    var service = [];
    $('.list-group-item :checkbox:checked').each(function () {
        service.push({ ServiceTypeID: $(this).attr('id') });
    });
    var contact = {
        FirstName: $('#editContactName').val(),
        LastName: $('#LastName').val(),
        Business: $('#Business').val(),
        Email: $('#Email').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        EmailChecked: $('#EmailChecked').val(),
        SmsChecked: $('#SmsChecked').val(),
        NotificationType: $('#NotificationType').val(),
        ContactGuid: $('#ContactGuid').val(),
        Language: $('#Language').val(),
        RegDate: $('#RegDate').val(),
        ServiceTypes: service
    };
    var validateService = ValidateServices();
    var validateFields = ValidateFields();
    if (validateFields && validateService) {
        $.post("/Admin/CreateContact", { contact })
            .done(function () {
                $('#createContactModal').modal('hide');
                GetContacts();
                toastr.success("Kontakt skapad!");
            })
            .fail(function () {
                toastr.error("Kontakten kunde inte skapas!");
            });
    }
    
}

function GetContactById(id) {
    $.get("/Admin/GetContactById/" + id, function (result) {
        $('#editContactModal').modal('show');
        $('#editContactModalContent').html(result);
        setTimeout(function () {
            $('#editContactName').focus();
        }, 500);
        $('a').blur();
    });
}

function EditContact() {
    var ContactID = $('#ContactID').val();
    var FirstName = $('#editContactName').val();
    var LastName = $('#LastName').val();
    var Business = $('#Business').val();
    var Email = $('#Email').val();
    var PhoneNumber = $('#PhoneNumber').val();
    var EmailChecked = $('#EmailChecked').is(":checked");
    var SmsChecked = $('#SmsChecked').is(":checked");
    var NotificationType = $('#NotificationType').val();
    var ContactGuid = $('#ContactGuid').val();
    var Language = $('#Language').val();
    var RegDate = $('#RegDate').val();
    var ServiceTypes = [];
    $('.list-group-item :checkbox:checked').each(function () {
        ServiceTypes.push({ ServiceTypeID: $(this).attr('id') });
    });
    var validateService = ValidateServices();
    var validateFields = ValidateFields();
    if (validateFields && validateService) {
        $.post("/Admin/EditContact", {
            "ContactID": ContactID, "FirstName": FirstName, "LastName": LastName,
            "Business": Business, "Email": Email, "PhoneNumber": PhoneNumber,
            "EmailChecked": EmailChecked, "SmsChecked": SmsChecked, "NotificationType": NotificationType,
            "ContactGuid": ContactGuid, "Language": Language, "RegDate": RegDate, "ServiceTypes": ServiceTypes
        })
            .done(function () {
                $('#editContactModal').modal('hide');
                GetContacts();
                toastr.success("Kontakt uppdaterad!");
            })
            .fail(function () {
                toastr.error("Kontakten kunde inte ändras!");
            });
    } 
}

function DeleteContact(id) {
    bootbox.confirm({
        message: '<span><i class="fas fa-exclamation-triangle" id="bootboxIcon"></i><h4>Radera kontakten?</h4></span>',
        closeButton: false,
        backdrop: true,
        buttons: {
            confirm: {
                label: '<i class="fa fa-check"></i> Ta bort',
                className: 'btn-success'
            },
            cancel: {
                label: '<i class="fa fa-times"></i> Avbryt',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                $.post("/Admin/DeleteContact/" + id)
                    .done(function () {
                        GetContacts();
                        toastr.info("Kontakt raderad!");
                    })
                    .fail(function () {
                        toastr.error("Kontakten kunde inte raderas!");
                    });
            }
        }
    });
}

function GetServiceTypes() {
    $.get("/Admin/GetServiceTypes", function (result) {
        var serviceTable = '';
        $.each(result, function (key, item) {
            serviceTable += '<tr>';
            serviceTable += '<td>' + item.Description + '</td>';
            serviceTable += '<td>' + "<input type='checkbox' disabled='true' style='cursor: default;' " + (item.PublicServiceType ? 'checked' : '') + " > " + '</td>';
            serviceTable += '<td><a href="#" title="Ändra" onclick="GetServiceById(' + item.ServiceTypeID + ')"><i class="fa fa-edit" id="editIcon"></i></a> <a href="#" title="Ta bort" onclick="DeleteServiceType(' + item.ServiceTypeID + ')"><i class="fa fa-trash-alt" id="delIcon"></i></a></td>';
            serviceTable += '</tr>';
        });
        $('.sTable').html(serviceTable);
    });
}

function CreateServiceModal() {
    $.get("/Admin/NewServiceType", function (result) {
        $('#createServiceModal').modal('show');
        $('#createServiceModalContent').html(result);
        setTimeout(function () {
            $('#createDescription').focus();
        }, 500);
        $('#createService').blur();
    });
}

function CreateServiceType() {
    var service = {
        ServiceTypeID: $('#ServiceTypeID').val(),
        Description: $('#createDescription').val(),
        PublicServiceType: $('#PublicServiceType').val()
    };
    $.post("/Admin/CreateServiceType", { service })
        .done(function () {
            $('#createServiceModal').modal('hide');
            GetServiceTypes();
            toastr.success('Tjänst skapad!');
        })
        .fail(function () {
            toastr.error("Tjänsten kunde inte skapas!");
        });
}

function GetServiceById(id) {
    $.get("/Admin/GetServiceById/" + id, function (result) {
        $('#editServiceModal').modal('show');
        $('#editServiceModalContent').html(result);
        setTimeout(function () {
            $('#editDescription').focus();
        }, 500);
        $('a').blur();
    });
}

function EditServiceType() {
    var ServiceTypeID = $('#ServiceTypeID').val();
    var Description = $('#editDescription').val();
    var PublicServiceType = $('#editPublic').val();
    $.post("/Admin/EditServiceType", {
        "ServiceTypeID": ServiceTypeID, "Description": Description,
        "PublicServiceType": PublicServiceType
    })
        .done(function () {
            $('#editServiceModal').modal('hide');
            GetServiceTypes();
            toastr.success('Tjänst uppdaterad!');
        })
        .fail(function () {
            toastr.error("Tjänsten kunde inte ändras!");
        });
}

function DeleteServiceType(id) {
    bootbox.confirm({
        message: '<span><i class="fas fa-exclamation-triangle" id="bootboxIcon"></i><h4>Radera tjänsten?</h4></span>',
        closeButton: false,
        backdrop: true,
        buttons: {
            confirm: {
                label: '<i class="fa fa-check"></i> Ta bort',
                className: 'btn-success'
            },
            cancel: {
                label: '<i class="fa fa-times"></i> Avbryt',
                className: 'btn-danger'
            }
        },
        callback: function (result) {
            if (result) {
                $.post("/Admin/DeleteServiceType/" + id)
                    .done(function () {
                        clearTimeout(timer);
                        loader.fadeOut();
                        GetServiceTypes();
                        toastr.info('Tjänst Borttagen!');
                    })
                    .fail(function () {
                        toastr.error("Tjänsten kunde inte raderas!");
                    });
            }
        }
    });
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
        setTimeout(function () {
            $('.list-group').removeClass('error-border');
        }, 5000);
        return false;
    }
    return true;
}

$("#reloadContacts").click(function () {
    this.blur();
});

$("#reloadServices").click(function () {
    this.blur();
});

$("#createContact").click(function () {
    this.blur();
});

$("#createService").click(function () {
    this.blur();
});

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