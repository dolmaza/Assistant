$(function () {
    $("#users-table").dataTable({
        "aaSorting": [],
        "columnDefs": [{
            "targets": [0, -1, -2],
            "orderable": false 
        }]
    });

    $(".delete-btn").click(function () {
        var url = $(this).attr("href");

        bootbox.confirm(globals.textConfirmDelete,
            function (result) {
                if (result) {
                    window.location = url;
                }
            });

        return false;
    });
});