app.controller("manageController", function ($scope, CommonContentService , $mdDialog) {
   

    $scope.onDeletingConfirmed = function (isConfirmed, id) {
        if (isConfirmed) {
            CommonContentService.deleteFavoriteById(id).then(function (response)
            {
                $scope.deleted = response.data;
               if ($scope.deleted == true)
                 {
                   var fav = document.getElementById(id + "+containerr");
                   fav.style.display = 'none';
                 }
        
            })
        }
    }

    $scope.deleteFavorite = function (id) {
        CommonContentService.showConfirmInManagePage($mdDialog, " Please, confirm deleting", "Do you really want to delete this item?", $scope.onDeletingConfirmed, id);
    };


    $scope.userFavorites = [];
    CommonContentService.GetUserFavoritesByUser().then(function (response) {
        $scope.userFavorites = response.data;
    });

    $scope.Moder = null;
    CommonContentService.getModerator().then(function (response) {

        $scope.Moder = response.data;
    });
    $scope.userReservations = [];
    CommonContentService.GetUserReservationsByUser().then(function (response) {
    $scope.userReservations = response.data;
    });


    $scope.profileReservations = [];
    CommonContentService.GetProfileReservationsForModerator().then(function (response) {
    $scope.profileReservations = response.data;
    });

    $scope.profileComments = [];
    CommonContentService.GetAllCommentsForModerator().then(function (response) {
    $scope.profileComments = response.data;
    });

    $scope.adminProfiles = [];
    CommonContentService.GetProfileForAdmin().then(function (response) {
    $scope.adminProfiles = response.data;
    });

    
})