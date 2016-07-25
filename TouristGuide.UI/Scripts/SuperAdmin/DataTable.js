var passedId;
var passedReason;
$(document).ready(function () {
    //OPEN
    $('[data-popup-open]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-open');
        passedId = jQuery(this).attr('data-user-id');
        passedReason = jQuery(this).attr('data-user-reason');
        $('[data-popup="' + targeted_popup_class + '"]').fadeIn(350);
        e.preventDefault();
    });

    //----UNBAN 

    $("#unban-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-unban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        passedReason = document.getElementById("unban-reason").value;
        $.get("/SuperAdmin/UnbanUserById", { Id: passedId, unbanReason: passedReason }, function () {
        });
    });

    //----UNDELETE 

    $("#undelete-form").submit(function () {
            var targeted_popup_class = jQuery(this).attr('data-popup-undelete');
            $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
            $.get("/SuperAdmin/UndeleteUserById", { Id: passedId, unbanReason : passedReason }, function () {
            });
    });

    //----BAN 

    $("#ban-form").submit(function () {
            var targeted_popup_class = jQuery(this).attr('data-popup-ban');
            $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
            passedReason = document.getElementById("ban-reason").value;
            $.get("/SuperAdmin/BanUserById", { Id: passedId, banReason : passedReason }, function () {
            });
    });

    //---DELETE 

    $("#delete-form").submit(function () {
            var targeted_popup_class = jQuery(this).attr('data-popup-delete');
            $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
            $.get("/SuperAdmin/DeleteUserById", { Id: passedId }, function () {
            });
    });

    //---— CLOSE 
    $('[data-popup-close]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-close');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        e.preventDefault();
    });
});
$(document).ready(function () {
    $('#UsersTable').DataTable({
        "pagingType": "full_numbers",
        "aLengthMenu": [[10, 15, 20, -1], [10, 15, 20, "All"]],
        "iDisplayLength": 10,
        "columnDefs": [{ "targets": [2], "searchable": false,  "visible": true },
        { "targets": [3], "searchable": false, "visible": true },
        { "targets": [4], "searchable": false,  "visible": true }
        ]
    });
});
