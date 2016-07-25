app.factory("CommonContentService", ['$http', function ($http) {
    var CommonContentService = {};



    CommonContentService.GetCommentsByRestaurantId = function (id) {
        return $http.get("/ListOfRestaurant/GetCommentsByRestaurantId", { params: { restaurantId: id } });
    };

    CommonContentService.editComment = function (comment, profileId, date_) {
        return $http.post("/ListOfRestaurant/UpdateRestaurantComment", { comment: comment, restaurantId: profileId, date: date_ });
    };

    CommonContentService.editcommentInManagePage = function (comment, profileId, date_) {
        return $http.post("/Manage/UpdateComment", { comment: comment, restaurantId: profileId, date: date_ });
    };


    CommonContentService.deleteCommentInManagePage = function (commentId_) {
        return $http.post("/Manage/DeleteComment", { commentId: commentId_ });
    };

    CommonContentService.getModerator = function () {
        return $http.post("/Manage/GetModerator");
    };


    CommonContentService.deleteComment = function (commentId_) {
        return $http.post("/ListOfRestaurant/DeleteCommentFromRestaurant", { commentId: commentId_ });

    };

    CommonContentService.addComment = function (comment_, profileId, date_) {
        return $http.post("/ListOfRestaurant/AddComment", { comment: comment_, restaurantId: profileId, date: date_ });
    };

    CommonContentService.getPhotosById = function (id) {
        return $http.get("/ListOfRestaurant/GetPhotosById", { params: { profileId: id } });
    };

    CommonContentService.deletePhotoById = function (photoId_) {
        return $http.post("/ListOfRestaurant/DeletePhotoById", { photoId: photoId_ });
    };

    CommonContentService.setMainPhotoOfProfile = function (photoId_, profileId_) {
        return $http.post("/ListOfRestaurant/SetMainPhoto", { photoId: photoId_, profileId: profileId_ });
    };

    CommonContentService.GetNewsFromRestaurantId = function (id) {
        return $http.get("/ListOfRestaurant/GetNewsByRestaurantId", { params: { restaurantId: id } });
    };

    CommonContentService.DeleteNewsFromRestaurant = function (newsId, profileId) {
        return $http.post("/ListOfRestaurant/DeleteNewsFromRestaurant", { newsId: newsId, restaurantId: profileId });
    };

    CommonContentService.AddNewsToRestaurant = function (_news, profileId, date_) {
        return $http.post("/ListOfRestaurant/AddNews", { news: _news, restaurantId: profileId, date: date_ });
    };

    CommonContentService.UpdateRestaurantNews = function (_news, profileId, date_) {
        return $http.post("/ListOfRestaurant/UpdateRestaurantNews", { news: _news, restaurantId: profileId, date: date_ })
    };

    CommonContentService.deleteFavoriteById = function (profileId_) {
        return $http.post("/Manage/DaleteFavorite", { profileId: profileId_ });
    };

    CommonContentService.GetCommentsByUser = function () {
        return $http.get("/Manage/GetAllComments");
    };

    CommonContentService.EditIsShowed = function (isShowed, id) {
        return $http.post("/Moder/EditProfile", { isShowed: isShowed, profileId: id });
    };

    CommonContentService.GetCommentsByModerProfile = function () {
        return $http.get("/Manage/GetAllCommentsForModerator");
    };


    CommonContentService.GetUserFavoritesByUser = function () {
        return $http.get("/Manage/GetAllFavorites");
    };


    CommonContentService.GetUserReservationsByUser = function () {
        return $http.get("/Manage/GetAllReservations");
    };

    CommonContentService.GetProfileReservationsForModerator = function () {
        return $http.get("/Manage/GetAllReservationsForModerator");
    };

    CommonContentService.GetAllCommentsForModerator = function () {
        return $http.get("/Manage/GetAllCommentsForModerator");
    };


    CommonContentService.GetProfileForAdmin = function () {
        return $http.get("/Manage/GetProfilesForAdmin");
    };


    CommonContentService.getCurrentDate = function () {

        var today = new Date();
        var day = today.getDate();
        var month = today.getMonth() + 1;
        var yearD = new Date();
        yearD.getFullYear(yearD.getFullYear());
        var year = yearD.getFullYear();
        var hour = today.getHours();
        var minutes = today.getMinutes();
        formatSize = 10;
        if (day < formatSize)
            day = "0" + day;
        if (month < formatSize)
            month = "0" + month;
        if (hour < formatSize)
            hour = "0" + hour;
        if (minutes < formatSize)
            minutes = "0" + minutes;
        var result = day + "/" + month + "/" + year + " " + hour + ':' + minutes;
        return result;
    };


    CommonContentService.showConfirm = function (dialog, title, message, callBack, callBackParamenter) {

        dialog.show({
            locals: { Title: title, Message: message },
            controller: ['$scope', '$mdDialog', 'Title', 'Message', function ($scope, $mdDialog, title, message) {

                $scope.Title = title;
                $scope.Message = message;

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function (answer) {
                    $mdDialog.hide(answer);
                };

            }],
            templateUrl: 'app/views/restaurant/common-items/confirmBox.html',
            parent: angular.element(document.body),
            targetEvent: null,
            clickOutsideToClose: true
        }).then(function (answer) {

            callBack(answer, callBackParamenter);
        })


    };


    CommonContentService.showConfirmInManagePage = function (dialog, title, message, callBack, callBackParamenter) {
        dialog.show({
            locals: { Title: title, Message: message },
            controller: ['$scope', '$mdDialog', 'Title', 'Message', function ($scope, $mdDialog, title, message) {

                $scope.Title = title;
                $scope.Message = message;

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function (answer) {
                    $mdDialog.hide(answer);
                };

            }],
            templateUrl: '/app/views/restaurant/common-items/confirmBox.html',
            parent: angular.element(document.body),
            targetEvent: null,
            clickOutsideToClose: true
        }).then(function (answer) {
            callBack(answer, callBackParamenter);
        })


    };


    return CommonContentService;
}]);


