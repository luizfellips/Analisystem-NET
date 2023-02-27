// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".close-alert").click(function () {
    $(".alert").hide('hide');
});

$(document).ready(function () {
    getDataTable("#user-table");
    getDataTable("#product-table");
    formatMoney();
    
    
})

$(document).ready(function () {
    $(".btn-informations").click(function () {
        var userId = $(this).attr('user-id');
        console.log(userId);

        $.ajax({
            type: 'GET',
            url: '/User/UserProducts/' + userId,
            success: function (result) {
                $("#userProductList").html(result);
                getDataTable("#product-table-user");
                formatMoney();
                $("#ProductUserModal").modal("show");
            },
            error: function (result) {
                console.log(result);
            }
        })
    })
})





function formatMoney() {
    $("td.productPrice").each(function () {
        $(this).text("$".concat($(this).text().replace(".", ",")));
    })
}




function getDataTable(id) {
    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "No registers found.",
            "sInfo": "Show _START_ until _END_ of _TOTAL_ registers",
            "sInfoEmpty": "Show 0 until 0 of 0 Registers",
            "sInfoFiltered": "(Filter from _MAX_ total registers)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Show _MENU_ registers per page",
            "sLoadingRecords": "Loading...",
            "sProcessing": "Processing...",
            "sZeroRecords": "No registers found.",
            "sSearch": "Search",
            "oPaginate": {
                "sNext": "Next",
                "sPrevious": "Previous",
                "sFirst": "First",
                "sLast": "Last"
            },
            "oAria": {
                "sSortAscending": ": Order columns by ascending",
                "sSortDescending": ": Order columns by descending"
            }
        }
    });
}

$(document).ready(function () {
    $("#phoneNumber").keypress(function () {
        mask(this, maskPhone);
    })
    $("#phoneNumber").blur(function () {
        mask(this, maskPhone);
    })

    limitText("#CPFNumber", 9);

    $("#CPFNumber").keypress(function () {
        mask(this, formatCPFinput);
    })
    $("#CPFNumber").blur(function () {
        mask(this, formatCPFinput);
    })
})

function mask(o, f) {
    setTimeout(function () {
        var value = f(o.value);
        if (value != o.value) {
            o.value = value;
        }
    }, 1);
}

function formatCPFinput(input) {
    var changedInput = input.replace(/\D/g, "");
    changedInput = changedInput.replace(/^0/, "");
    return changedInput;
}

function maskPhone(object) {
    var maskedValue = object.replace(/\D/g, "");
    maskedValue = maskedValue.replace(/^0/, "");
    if (maskedValue.length > 10) {
        maskedValue = maskedValue.replace(/^(\d{2})(\d{5})(\d{4}).*/, "($1) $2-$3");
    }
    else if (maskedValue.length > 5) {
        maskedValue = maskedValue.replace(/^(\d{2})(\d{4})(\d{0,4}).*/, "($1) $2-$3");
    }
    else if (maskedValue.length > 2) {
        maskedValue = maskedValue.replace(/^(\d{2})(\d{0,5})/, "($1) $2");
    }
    else {
        maskedValue = maskedValue.replace(/^(\d*)/, "($1")
    }
    return maskedValue;
}


function limitText(field, maxChar) {
    $(field).attr('maxlength', maxChar);
}