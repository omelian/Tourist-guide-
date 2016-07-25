app.controller('LeisurePageController', ['$scope', '$location', '$routeParams',
    '$cookies', 'LeisureService', 'ClaimService',
    '$mdDialog', '$mdMedia', 'ReservationService', 'CommonContentService',
function ($scope, $location, $routeParams, $cookies, LeisureService,
    ClaimService, $mdDialog, $mdMedia, ReservationService, CommonContentService) {
    $scope.leisureId = $routeParams.profileId;
    $scope.leisure = {};
    LeisureService.GetLeisureById($scope.leisureId).then(function (response) {
        $scope.leisure = response.data;
        if ($scope.leisure.IsShowed == true) {
            $scope.isShowedText = "Profile is available for users";
        }
        else {
            $scope.isShowedText = "Profile isnt available for users";
        }
    });
    $scope.currentUser = { Role: '', Name: '' };
    $scope.IsInFavorites = false;
    ClaimService.GetClaim().then(function (response) {
        $scope.currentUser = response.data;
    });



    $scope.isAvailable = function () {
        if ($scope.leisure.IsShowed) {
            $scope.isShowedText = "Profile isnt available for users";
        }
        else {
            $scope.isShowedText = "Profile is available for users";
        }
        CommonContentService.EditIsShowed($scope.leisure.IsShowed, $scope.leisureId);
    }
    LeisureService.IsInFavorites($scope.leisureId).then(function (response) {
        $scope.IsInFavorites = response.data;
        if ($scope.IsInFavorites == true) {
            var elem = document.getElementById("page_buttons");
            elem.innerHTML = '<span class="glyphicon glyphicon-heart" aria-hidden="true"></span> Already in favorites';
        }
        else {
            var elem = document.getElementById("page_buttons");
            elem.innerHTML = '<span class="glyphicon 	glyphicon glyphicon-heart-empty" aria-hidden="true"></span> Add to favorite';
        }
    });
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
                $scope.url = encodeURIComponent(window.location.href);

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



    $scope.reservation = {};
    $scope.reservations = [];
    $scope.getReserv = function () {
        ReservationService.GetReservationsByUserName($scope.currentUser.Name, $scope.restaurantId
        ).then(function (response) {
            $scope.reservation = response.data;

        })
        return true;
    }

    $scope.addToFavorits = function () {
        if ($scope.currentUser.Role != 'User') {
            $scope.showConfirm($mdDialog, "Sign in or register a new account", "This action can be performed only as registered user", $scope.goToRegister, null);
        }
        if ($scope.currentUser.Role == 'User') {
            var elem = document.getElementById("page_buttons");
            var val = elem.innerHTML;
            if (elem.innerHTML == '<span class="glyphicon 	glyphicon glyphicon-heart-empty" aria-hidden="true"></span> Add to favorite') {
                LeisureService.AddFavorite($scope.leisureId).then(function () {
                    elem.innerHTML = '<span class="glyphicon glyphicon-heart" aria-hidden="true"></span> Already in favorites';
                })
            }
            else {
                LeisureService.RemoveFavorite($scope.leisureId).then(function () {
                    elem.innerHTML = '<span class="glyphicon 	glyphicon glyphicon-heart-empty" aria-hidden="true"></span> Add to favorite';
                })
            }
        }
    }

    $scope.getAllReserv = function () {
        ReservationService.GetAllReservationsByUserName($scope.currentUser.Name).then(function (response) {
            $scope.reservations = response.data;

        })
        return true;
    }

    $scope.reservate = function () {

        if ($scope.currentUser.Role != 'User') {
            $scope.showConfirm($mdDialog, "For this functionality you must create user account", "You can just login", $scope.goToRegister, null);
        }
        else {

            res = $scope.reservation.ProfileId + "";
            if (res == $scope.restaurantId) {
                $scope.showReservationInfo();
            }
            else {
                if ($scope.reservations != null && $scope.reservations.length > 0) {
                    $scope.showReservationWarning();
                }
                else {
                    $scope.showReservationAdd();

                }
            }
        }


    }
    $scope.checkedModerator = function () {

        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Moderator') {
                if ($scope.leisure.Moderators != null) {
                    for (index = 0; index < $scope.leisure.Moderators.length; ++index) { //!!!!!!! uncomment when admin could confirm moders
                        if ($scope.leisure.Moderators[index] == $scope.currentUser.Id) {
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