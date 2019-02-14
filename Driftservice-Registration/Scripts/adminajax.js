$(document).ready(function () {
    GetContacts();
});


function GetContacts() {
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
                contactTable += '<td>' + (item.PhoneNumber === null ? "" : item.PhoneNumber) + '</td>';
                contactTable += '<td>' + item.NotificationType + '</td>';
                contactTable += '<td>' + item.ContactGuid + '</td>';
                contactTable += '<td>' + item.Language + '</td>';
                contactTable += '<td>' + ConvertDate(item.RegDate) + '</td>';
                contactTable += '<td><a href="#" title="Ändra" onclick="GetContactById(' + item.ContactID + ')"><i class="fa fa-edit" id="editIcon"></i></a> <a href="#" title="Ta bort" onclick="DeleteContact(' + item.ContactID + ')"><i class="fa fa-trash-alt" id="delIcon"></i></a></td>';
                contactTable += '</tr>';
            });
            $('.cTable').html(contactTable);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

setInterval(function () {
    GetContacts();
}, 60000);

function ConvertDate(obj) {
    if (obj !== null) {
        var date = new Date(parseInt(obj.substr(6)));
        return date.toISOString().substr(0,10);
    }
    return "";  
}

function GetContactById(id) {
    $.get("/Admin/GetContactById/" + id, function (result) {
        $('#editContactModal').modal('show');
        $('#modalContent').html(result);
    });
}


function EditContact() {
    //var contact = {
    //    FirstName: $('#FirstName').val(),
    //    LastName: $('#LastName').val(),
    //    Business: $('#Business').val(),
    //    Email: $('#Email').val(),
    //    PhoneNumber: $('#PhoneNumber').val(),
    //    NotificationType: $('#NotificationType').val(),
    //    Language: $('#Language').val()
    //};
    $.ajax({
        url: "/Admin/EditContact",
        data: JSON.stringify(contact),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function () {
            GetContacts();
            $('#editContactModal').modal('hide');
            // Success/info toast: Kontakt uppdaterad!
        },
        error: function () {
            // Error toast: Åtgärden kunde inte utföras!
        }
    });
}

function DeleteContact(id) {
    bootbox.confirm({
        message: "<h4>Kontakten raderas permanent!</h4>",
        backdrop: true,
        size: 'small',
        buttons: {
            cancel: {
                label: '<i class="fa fa-times"></i> Avbryt',
                className: 'btn-danger p-5'
            },
            confirm: {
                label: '<i class="fa fa-check"></i> Ta bort',
                className: 'btn-success'
            }
        },
        callback: function (result) {
            if (result) {
                // show spinner
                $.ajax({
                    url: "/Admin/DeleteContact/" + id,
                    type: "POST",
                    contentType: "application/json;charset=UTF-8",
                    dataType: "json",
                    success: function (result) {
                        // hide spinner
                        //toastr.success(result.Message);
                        GetContacts();
                    },
                    error: function (result) {
                        // toastr.error(result.Message);
                    }
                });
            }
        }
    });
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
                serviceTable += '<td><a href="#" title="Ta bort" onclick="DeleteServiceType(' + item.ServiceTypeID + ')"><i class="fa fa-trash-alt" id="delIcon"></i></a></td>';
                serviceTable += '</tr>';
            });
            $('.sTable').html(serviceTable);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
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
        error: function () {
            // Error toast: Åtgärden kunde inte utföras!
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
                // Success/info toast: Tjänst borttagen.
            },
            error: function () {
                // Error toast: Åtgärden kunde inte utföras!
            }
        });
    }
}