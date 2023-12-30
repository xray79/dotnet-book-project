// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// product
$(document).ready(function() {
    loadDataTable();
})

function loadDataTable() {
    let datatable = $('#tblData').DataTable({
        "ajax": { "url": "admin/product/getAll"},
        "colums": [
            {"data": "name", "width": "15%"},
            {"data": "position"},
            {"data": "salary"},
            {"data": "office"},
        ]
    })
}