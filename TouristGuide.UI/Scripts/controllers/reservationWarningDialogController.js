
 app.controller('ReservationWarningDialogController', ['$scope', '$mdDialog', '$mdMedia', '$routeParams', 'reservations', 'ReservationService',
    function ($scope, $mdDialog, $mdMedia, $routeParams, reservations, ReservationService) {
        $scope.reservations = reservations;

        $scope.hide = function () {
            $mdDialog.hide();
        };


        $scope.cancel = function () {
            $mdDialog.cancel();
        };


        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };

        $scope.showReservationInfo = function (reservation,ev) {
            var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
            $mdDialog.show({
                scope: $scope,
                preserveScope: true,
                locals: { reservation: reservation },
                controller: 'ReservationInfoDialogController',
                templateUrl: 'app/views/restaurant/restaurant-items/reservationInfoDialog.html',
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


        $scope.showReservation = function (reservation) {
            $scope.showReservationInfo(reservation);
        }

        $scope.addReservation = function () {
            $scope.showReservationAdd();
        }

    }])