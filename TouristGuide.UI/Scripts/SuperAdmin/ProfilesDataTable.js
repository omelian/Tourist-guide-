var passedId;
$(document).ready(function () {
    // —-— OPEN 
    $('[data-popup-open]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-open');
        passedId = jQuery(this).attr('data-profile-id');
        $('[data-popup="' + targeted_popup_class + '"]').fadeIn(350);
        e.preventDefault();
    });
    // —--UNBAN 
    $("#unban-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-unban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        $.get("/SuperAdmin/UnbanProfileById", { Id: passedId }, function () {
        });
    });

    // —--BAN 
    $("#ban-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-ban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        $.get("/SuperAdmin/BanProfileById", { Id: passedId }, function () {
        });
    });

    // —-— CLOSE 
    $('[data-popup-close]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-close');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        e.preventDefault();
    });
    $(document).ready(function () {
        $('#ProfilesTable').DataTable({
            "pagingType": "full_numbers",
            "aLengthMenu": [[10, 15, 20, -1], [10, 15, 20, "All"]],
            "iDisplayLength": 10, "columnDefs": [{ "targets": [1], "searchable": false, "visible": true },
            { "targets": [2], "searchable": false, "visible": true }
            ]
        });
    });
});
