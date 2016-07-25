var passedId;
var passedReason;
$(document).ready(function () {

    //Open popup  
    $('[data-popup-open]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-open');
        passedId = jQuery(this).attr('data-admin-id');
        $('[data-popup="' + targeted_popup_class + '"]').fadeIn(350);
        e.preventDefault();
    });

    //Approve request popup button
    $("#approve-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-approve');
        $.get("/SuperAdmin/ApproveRequestFromAdmin", { Id: passedId }, function () {
        });
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);

    });
    //Reject request popup button
    $("#reject-form").submit(function () {
        var targeted_popup_class = jQuery(this).attr('data-popup-reject');
        passedReason = document.getElementById("reject-reason").value;
        $.get("/SuperAdmin/RejectRequestFromAdmin", { Id: passedId, rejectReason : passedReason }, function () {
        });
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
    });

    //Close popup
    $('[data-popup-close]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-close');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        e.preventDefault();
    });

    //Table with requests data
    $(document).ready(function () {
        $('#RequestsTable').DataTable({
            "pagingType": "full_numbers",
            "aLengthMenu": [[10, 15, 20, -1], [10, 15, 20, "All"]],
            "iDisplayLength": 10, "columnDefs": [
            { "targets": [2], "searchable": false, "orderable": false, "visible": true },
            { "targets": [3], "searchable": false, "orderable": false, "visible": true }
            ]
        });
    });
});
