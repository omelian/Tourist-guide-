var passedId;
var passedReason;
$(document).ready(function () {

    //Open popup 
    $('[data-popup-open]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-open');
        passedId = jQuery(this).attr('data-profile-id');
        $('[data-popup="' + targeted_popup_class + '"]').fadeIn(350);
        e.preventDefault();
    });

    //Ban profile popup button
    $("#ban-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-ban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        passedReason = document.getElementById("ban-reason").value;
        $.get("/SuperAdmin/BanProfileById", { Id: passedId, banReason: passedReason }, function () {
        });
    });

    //Unban profile popup button 
    $("#unban-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-unban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        passedReason = document.getElementById("unban-reason").value;
        $.get("/SuperAdmin/UnbanProfileById", { Id: passedId, unbanReason : passedReason }, function () {
        });
    });

    //Close popup
    $('[data-popup-close]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-close');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        e.preventDefault();
    });

    //Table with profiles data
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
