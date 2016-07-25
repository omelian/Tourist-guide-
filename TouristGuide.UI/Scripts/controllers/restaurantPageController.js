app.controller('RestaurantPageController', ['$scope', '$http', '$location', '$routeParams',
    '$cookies', 'RestaurantService', 'ClaimService',
    '$mdDialog', '$mdMedia', 'ReservationService', 'CommonContentService',
function ($scope, $http, $location, $routeParams, $cookies, RestaurantService,
    ClaimService, $mdDialog, $mdMedia, ReservationService, CommonContentService) {
    $scope.restaurantId = $routeParams.profileId;

    $scope.restaurant = {};
    RestaurantService.GetRestaurantById($scope.restaurantId).then(function (response) {
        $scope.restaurant = response.data;
        if ($scope.restaurant.IsShowed == true) {
            $scope.isShowedText = "Profile is available for users";
        }
        else {
            $scope.isShowedText = "Profile isnt available for users";
        }
    });

    $scope.isAvailable = function () {
        if ($scope.restaurant.IsShowed) {
            $scope.isShowedText = "Profile isnt available for users";
        }
        else {
            $scope.isShowedText = "Profile is available for users";
        }
        CommonContentService.EditIsShowed($scope.restaurant.IsShowed, $scope.restaurantId);
    }

    $scope.currentUser = { Role: '', Name: '' };
    $scope.IsInFavorites = false;

    ClaimService.GetClaim().then(function (response) {
        $scope.currentUser = response.data;
        if ($scope.currentUser.Role == 'User') {
            $scope.getReserv();
            $scope.getAllReserv();
        }
    })

    RestaurantService.IsInFavorites($scope.restaurantId).then(function (response) {
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
    $scope.goToRegister = function (param) {
        if (param) {
            $http({
                url: "/Account/RedirectRegister",
                method: "GET",
                params: { returnUrl: window.location.href }
            });
        }
        else {
             $http.get('/Account/RedirectLogin', {params : { returnUrl: window.location.href} }).get();
        }
    }

    $scope.showReservationAdd = function (ev) {
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            controller: ReservationDialogController,
            templateUrl: 'app/views/restaurant/restaurant-items/reservationDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: useFullScreen
        })
        .then(function () {
            $scope.getReserv();
            $scope.getAllReserv();

        }).catch(function () {
            $scope.getReserv();
            $scope.getAllReserv();

        });
    };

    $scope.showReservationInfo = function (ev) {
        $scope.getReserv();
        $scope.getAllReserv();
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            locals: { reservation: $scope.reservation },
            controller: 'ReservationInfoDialogController',
            templateUrl: 'app/views/restaurant/restaurant-items/reservationInfoDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: useFullScreen
        })
        .then(function () {
        });

    };


    $scope.showReservationWarning = function (ev) {
        var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            locals: { reservations: $scope.reservations },
            controller: 'ReservationWarningDialogController',
            templateUrl: 'app/views/restaurant/restaurant-items/reservationWarningDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true,
            fullscreen: useFullScreen
        })
        .then(function () {
            $scope.getReserv();
            $scope.getAllReserv();
        }).catch(function () {
            $scope.getReserv();
            $scope.getAllReserv();

        });
    };

    $scope.addToFavorits = function () {
        if ($scope.currentUser.Role != 'User') {
            $scope.showConfirm($mdDialog, "Sign in or register a new account", "This action can be performed only  as registered user", $scope.goToRegister, null);
        }
        if ($scope.currentUser.Role == 'User') {
            var elem = document.getElementById("page_buttons");
            var val = elem.innerHTML;
            if (elem.innerHTML == '<span class="glyphicon 	glyphicon glyphicon-heart-empty" aria-hidden="true"></span> Add to favorite') {
                RestaurantService.AddFavorite($scope.restaurantId).then(function () {
                    elem.innerHTML = '<span class="glyphicon glyphicon-heart" aria-hidden="true"></span> Already in favorites';
                })
            }
            else {
                RestaurantService.RemoveFavorite($scope.restaurantId).then(function () {
                    elem.innerHTML = '<span class="glyphicon 	glyphicon glyphicon-heart-empty" aria-hidden="true"></span> Add to favorite';
                })
            }
        }
    }


    $scope.reservations = [];
    $scope.reservation = {};
    $scope.getReserv = function () {
        ReservationService.GetReservationsByUserName($scope.currentUser.Name, $scope.restaurantId
        ).then(function (response) {
            $scope.reservation = response.data;

        })
        return true;
    }

    $scope.getAllReserv = function () {
        ReservationService.GetAllReservationsByUserName($scope.currentUser.Name).then(function (response) {
            $scope.reservations = response.data;

        })
        return true;
    }

    $scope.reservate = function () {

        if ($scope.currentUser.Role != 'User') {
            $scope.showConfirm($mdDialog, "For this functionality you must create user account", "This action can be performed only as registered user", $scope.goToRegister, null);
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

    $scope.checkFavorite = function () {
        return $scope.IsInFavorites;
    }


    $scope.checkedComments = function () {
        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Guest')
                return true;
            else
                return false;
        }
    }

    $scope.reservationEditTimeOut = function (reservation) {
        if (reservation.ReservationId != undefined) {
            var today = new Date();
            reservation.ReservationDate = reservation.ReservationDate.replace(".", "/");
            reservation.ReservationDate = reservation.ReservationDate.replace(".", "/");
            var splitRes = reservation.ReservationDate.split(" ");
            var myDate = splitRes[0].split("/");
            var reservationMonth = myDate[1];
            var reservationYear = myDate[2];
            var reservationDays = myDate[0];
            var Time = splitRes[1].split(":");
            var reservationHours = Time[0];
            var reservationMinutes = Time[1];

            var reservationDate = new Date(reservationMonth + "-" + reservationDays + "-" + reservationYear + " " + reservationHours + ":" + reservationMinutes);
            var diffDate = (reservationDate - today);
            var diffMins = Math.round((diffDate / 1000) / 60);

            if (diffMins > 60) {
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }
    };

}])