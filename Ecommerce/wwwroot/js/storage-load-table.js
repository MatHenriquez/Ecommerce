let dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#storageTable').DataTable({
        "ajax": {
            "url": "/Admin/Storage/GetAll"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "description", "width": "40%" },
            { "data": "status", 
                "render": function (data) {
                    if (data) {
                        return "Active";
                    }
                    else {
                        return "Inactive";
                    }
            },
                "width": "20%"
            },
            {
                "data": "id", "render": function (data) {
                    return `<div class="text-center">
                                <a href="/Admin/Storage/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer">
                                    <i class="bi bi-pencil-square"></i>
                                </a>
                                <a onclick=Delete("/Admin/Storage/Delete/${data}") class="btn btn-danger text-white" style="cursor:pointer">
                                    <i class="bi bi-trash3-fill"></i>
                                </a>
                            </div>`;
                },
                "width": "20%"
            }
        ]
    });
}