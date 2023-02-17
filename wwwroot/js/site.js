// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(".close-alert").click(function () {
    $(".alert").hide('hide');
});

$(document).ready(function () {
    getDataTable("#user-table");
    getDataTable("#product-table");
    formatMoney("productPrice");
})

function formatMoney(id) {
    document.getElementById(id).innerText = "$".concat(document.getElementById(id).innerText.replace(".", ","));
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