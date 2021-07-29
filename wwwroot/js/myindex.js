

var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function Delete(id) {
    $.post(" /Citizen/Delete ", { id: id })
        .done(function (data) {
            toastr.success("Deleted Successfully");

            setInterval('location.reload()', 1000);
            toastr.options.timeOut = 1000;
            toastr.options.fadeOut = 1000;

        });
}

function loadDataTable() {
    dataTable = $('#tbl').DataTable({
        "ajax": {
            "url": "Citizen/GetAll",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "15%" },
            { "data": "fatherName", "width": "15%" },
            { "data": "age", "width": "10%" },
            { "data": "sex", "width": "10%" },
            { "data": "address", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {

                    return `<div class="text-center">
                        <a href="/Citizen/CreateEdit?id=${data}" class='btn btn-success text-white' style='cursor:pointer; width:70px;'>
                            Edit
                        </a>
                        &nbsp;
                        <a href="/Citizen/Detail?id=${data}" class="btn btn-secondary text-white btn-sm">Detail</a>
                        &nbsp;
                
                        <a class='btn btn-danger text-white' style='cursor:pointer; width:70px;'
                            onclick=Delete(${data})>
                            Delete
                        </a>
                        </div>`;
                }, "width": "35%"
            }
        ],
        "language": {
            "emptyTable": "no data found"
        },
        "width": "100%"
    });


}

