//Users logic
$(function () {
    //----- OPEN
    var passedId;
    $('[data-popup-open]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-open');
        passedId = jQuery(this).attr('data-user-id');
        $('[data-popup="' + targeted_popup_class + '"]').fadeIn(350);
        e.preventDefault();
    });
    //----UNBAN
    $('[data-popup-unban]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-unban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        $.get("/SuperAdmin/UnbanUserById", { Id: passedId }, function () {
        });
        location.reload();
        e.preventDefault();
    });

    //----BAN
    $('[data-popup-ban]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-ban');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);
        $.get("/SuperAdmin/BanUserById", { Id: passedId }, function () {
        });
        location.reload();
        e.preventDefault();
    });

    //----- CLOSE
    $('[data-popup-close]').on('click', function (e) {
        var targeted_popup_class = jQuery(this).attr('data-popup-close');
        $('[data-popup="' + targeted_popup_class + '"]').fadeOut(350);

        e.preventDefault();
    });
});