
$(document).ready(function GetContacts() {
    $.ajax({
        url: "/Admin/GetContacts",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var contactTable = '';
            $.each(result, function (key, item) {
                contactTable += '<tr>';
                contactTable += '<td>' + item.ContactID + '</td>';
                contactTable += '<td>' + item.FirstName + '</td>';
                contactTable += '<td>' + item.LastName + '</td>';
                contactTable += '<td>' + item.Business + '</td>';
                contactTable += '<td>' + item.Email + '</td>';
                contactTable += '<td>' + FormatPhone(item.PhoneNumber) + '</td>';
                contactTable += '<td>' + item.NotificationType + '</td>';
                contactTable += '<td>' + item.ContactGuid + '</td>';
                contactTable += '<td>' + item.Language + '</td>';
                contactTable += '<td>' + ConvertDate(item.RegDate) + '</td>';
                contactTable += '<td><a href="#" onclick="return GetContactById(' + item.ContactID + ')">Ändra</a> | <a href="#" onclick="DeleteContact(' + item.ContactID + ')">Ta bort</a></td>';
                contactTable += '</tr>';
            });
            $('.cTable').html(contactTable);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
});

function ConvertDate(obj) {
    if (obj !== null) {
        var date = new Date(parseInt(obj.substr(6)));
        return date.toISOString().substr(0,10);
    }
    return "";  
}

function FormatPhone(obj) {
    return obj === null ? "" : obj;
}

function GetServiceTypes() {
    $.ajax({
        url: "/Admin/GetServiceTypes",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var serviceTable = '';
            $.each(result, function (key, item) {
                serviceTable += '<tr>';
                serviceTable += '<td>' + item.ServiceTypeID + '</td>';
                serviceTable += '<td>' + item.Description + '</td>';
                serviceTable += '<td>' + item.PublicServiceType + '</td>';
                serviceTable += '<td><a href="#" onclick="DeleteServiceType(' + item.ServiceTypeID + ')">Ta bort</a></td>';
                serviceTable += '</tr>';
            });
            $('.sTable').html(serviceTable);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function EditContact() {
    var contact = {
        FirstName: $('#FirstName').val(),
        LastName: $('#LastName').val(),
        Business: $('#Business').val(),
        Email: $('#Email').val(),
        PhoneNumber: $('#PhoneNumber').val(),
        NotificationType: $('#NotificationType').val(),
        Language: $('#Language').val()
    };
    $.ajax({
        url: "/Admin/EditContact",
        data: JSON.stringify(contact),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function () {
            GetContacts();
            $('#editModal').modal('hide');
            $('#FirstName').val("");
            $('#LastName').val("");
            $('#Business').val("");
            $('#Email').val("");
            $('#PhoneNumber').val("");
            $('#NotificationType').val("");
            $('#Language').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteContact(id) {
    var c = confirm("Kontakten raderas permanent!");
    if (c) {
        $.ajax({
            url: "/Admin/DeleteContact" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function () {
                GetContacts();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function CreateServiceType() {
    var service = {
        ServiceTypeID: $('#ServiceTypeID').val(),
        Description: $('#Description').val(),
        PublicServiceType: $('#PublicServiceType').val()
    };
    $.ajax({
        url: "/Admin/CreateServiceType",
        data: JSON.stringify(service),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function () {
            GetServiceTypes();
            $('#editModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

function DeleteServiceType(id) {
    var c = confirm("Vill du ta bort tjänsten?");
    if (c) {
        $.ajax({
            url: "/Admin/DeleteServiceType" + id,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function () {
                GetServiceTypes();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

function GetContactById(id) {
    $.ajax({
        url: "/Admin/GetContactById" + id,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#FirstName').val(result.FirstName);
            $('#LastName').val(result.LastName);
            $('#Business').val(result.Business);
            $('#Email').val(result.Email);
            $('#PhoneNumber').val(result.PhoneNumber);
            $('#NotificationType').val(result.NotificationType);
            $('#Language').val(result.Language);
            $('#editModal').modal('show');
            $('#btnSave').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function clearServiceTypeFields() {
    $('#ServiceTypeID').val("");
    $('#Description').val("");
    $('#PublicServiceType').val("");
    $('#btnSave').hide();
    $('#btnAdd').show();
    $('#ServiceTypeID').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
}