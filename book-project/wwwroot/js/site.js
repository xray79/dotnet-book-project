// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// product
$(document).ready(function() {
    loadDataTable();
})

let datatable;

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": { "url": "/admin/product/getAll"},
        "columns": [
            {"data": "title", "width": "15%"},
            {"data": "isbn", "width": "15%"},
            {"data": "listPrice", "width": "15%"},
            {"data": "author", "width": "15%"},
            {"data": "category.name", "width": "15%"},
            {
                "data": "id", 
                "render": function (data) {
                return `
                <a href="/admin/product/upsert?id=${data}" class="text-blue-500 hover:underline mr-3">
                     Edit
                 </a>
                 <a onclick=Delete("/admin/product/delete/${data}") class="text-red-500 cursor-pointer hover:underline">
                     Delete
                 </a>`;
            }, "width": "15%"},
        ]
    })
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {
                    datatable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}