app.controller('AttractionPageController', ['$scope', '$location', '$routeParams',
    '$cookies', 'AttractionService', 'ClaimService',
    '$mdDialog', '$mdMedia', 
function ($scope, $location, $routeParams, $cookies, AttractionService,
    ClaimService, $mdDialog, $mdMedia) {
    $scope.attractionId = $routeParams.profileId;

    $scope.attraction = {};
    AttractionService.GetAttractionById($scope.attractionId).then(function (response) {
        $scope.attraction = response.data;
    })
    $scope.currentUser = { Role: '', Name: '' };
    ClaimService.GetClaim().then(function (response) {
        $scope.currentUser = response.data;
    })

    $scope.modalShown = false;
    $scope.toggleModal = function () {
        $scope.modalShown = !$scope.modalShown;
    };

    $scope.status = '  ';
    $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');


    $scope.showConfirm = function (dialog, title, message, callBack, callBackParamenter) {
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
            templateUrl: 'app/views/restaurant/common-items/confirmRegister.html',
            parent: angular.element(document.body),
            targetEvent: null,
            clickOutsideToClose: true
        }).then(function (answer) {
            callBack(answer, callBackParamenter);
        })



    };
    $scope.goToRegister = function () { window.location.assign('/Account/Register'); }




    $scope.addToFavorits = function () {
        if ($scope.currentUser.Role != 'User') {
            $scope.showConfirm($mdDialog, "For this functionality you must create user account", "You can just login", $scope.goToRegister, null);
        }
        if ($scope.currentUser.Role == 'User') {
            RestaurantService.AddFavorite($scope.restaurantId).then()
        }
    }

    $scope.showBoat = function (ev) {
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            locals: { reservations: $scope.reservations },
            controller:  ['$scope', '$mdDialog', function ($scope, $mdDialog) {

               

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
            templateUrl: 'app/views/restaurant/attraction-items/boatDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: useFullScreen
        })
        .then(function () {
            
        }).catch(function () {
            
        });
    };


    $scope.addBoat = function () {
        if (scope.currentUser.Role != 'User') {
            $scope.showConfirm($mdDialog, "For this functionality you must create user account", "You can just login", $scope.goToRegister, null);
        }
        else {
            $scope.showBoat();
        }
        
    }
 

   
    $scope.checkedModerator = function () {

        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Moderator') {
                if ($scope.restaurant.Moderators != null) {
                    for (index = 0; index < $scope.restaurant.Moderators.length; ++index) { //!!!!!!! uncomment when admin could confirm moders
                        if ($scope.restaurant.Moderators[index] == $scope.currentUser.Id) {
                            return false;
                        }
                    }
                }
                return true;
            }
            else {
                return true;
            }

        }
    }

    $scope.checkedAdmin = function () {
        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Admin')
                return false;
            else
                return true;
        }
    }

    $scope.checkedComments = function () {
        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Guest')
                return true;
            else
                return false;
        }
    }



}])