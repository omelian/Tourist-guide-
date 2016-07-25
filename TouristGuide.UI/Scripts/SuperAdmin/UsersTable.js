var passedId;
var passedReason;
$(document).ready(function () {

    //Open popup
    $('[data-popup-open]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-open');
        passedId = jQuery(this).attr('data-user-id');
        passedReason = jQuery(this).attr('data-user-reason');
        $('[data-popup="' + targeted_popup_class + '"]').fadeIn(350);
        e.preventDefault();
    });

    //Unban user popup button
    $("#unban-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-unban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        passedReason = document.getElementById("unban-reason").value;
        $.get("/SuperAdmin/UnbanUserById", { Id: passedId, unbanReason: passedReason }, function () {
        });
    });

    //Ban user popup button
    $("#ban-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-ban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        passedReason = document.getElementById("ban-reason").value;
        $.get("/SuperAdmin/BanUserById", { Id: passedId, banReason: passedReason }, function () {
        });
    });

    //Delete user popup button
    $("#delete-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-delete');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        passedReason = document.getElementById("delete-reason").value;
        $.get("/SuperAdmin/DeleteUserById", { Id: passedId, deleteReason : passedReason }, function () {
        });
    });

    //Undelete user popup button
    $("#undelete-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-undelete');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        passedReason = document.getElementById("undelete-reason").value;
        $.get("/SuperAdmin/UndeleteUserById", { Id: passedId, undeleteReason: passedReason }, function () {
        });
    });

    //Close popup
    $('[data-popup-close]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-close');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        e.preventDefault();
    });

    //Table with users data
    $(document).ready(function () {
        $('#UsersTable').DataTable({
            "pagingType": "full_numbers",
            "aLengthMenu": [[10, 15, 20, -1], [10, 15, 20, "All"]],
            "iDisplayLength": 10,
            "columnDefs": [{ "targets": [2], "searchable": false, "visible": true },
            { "targets": [3], "searchable": false, "visible": true },
            { "targets": [4], "searchable": false, "visible": true }
            ]
        });
    });
});

