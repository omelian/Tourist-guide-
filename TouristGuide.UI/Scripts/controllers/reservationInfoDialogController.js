app.controller('ReservationInfoDialogController', ['$scope', '$mdDialog', '$mdMedia', '$routeParams',
    'reservation', 'ReservationService', 'ClaimService','CommonContentService',
    function ($scope, $mdDialog, $mdMedia, $routeParams, reservation, ReservationService, ClaimService, CommonContentService) {

        $scope.restaurantId = $routeParams.profileId;
        $scope.myRerervation = reservation;
        
        $scope.hide = function () {
            $mdDialog.hide();
        };


        $scope.cancel = function () {
            $mdDialog.cancel();
        };


        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };


        $scope.showAlert = function (ev) {
            // Appending dialog to document.body to cover sidenav in docs app
            // Modal dialogs should fully cover application
            // to prevent interaction outside of dialog
            $mdDialog.show(
              $mdDialog.alert()
                .parent(angular.element(document.querySelector('#popupContainer')))
                .clickOutsideToClose(true)
                .textContent('You can not edit a reservation less than an hour to order')
                .ariaLabel('Alert Dialog Demo')
                .ok('Cancel')
                .targetEvent(ev)
            );
        };
       
        $scope.showEditReservation = function (ev) {

            var useFullScreen = ($mdMedia('sm') || $mdMedia('xs')) && $scope.customFullscreen;
            $mdDialog.show({
                locals: { reservation: reservation },
                controller: 'EditReservationController',
                templateUrl: 'app/views/restaurant/restaurant-items/editReservationDialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true,
                fullscreen: useFullScreen
            })
            .then(function (answer) {
                $scope.getReserv();
                $scope.getAllReserv();

            }).catch(function () {
                $scope.getReserv();
                $scope.getAllReserv();

            });

        };
        
        $scope.showConfirm = function (ev) {
            // Appending dialog to document.body to cover sidenav in docs app
            var confirm = $mdDialog.confirm()

                  .textContent('Do you really want to delete the reservation?')
                  .ariaLabel('Lucky day')
                  .targetEvent(ev)
                  .ok('Yes')
                  .cancel('No');
            $mdDialog.show(confirm).then(function () {
                ReservationService.DeleteReservation(reservation.ReservationId).then(function (response) {
                    $scope.status = "Ok";
                    $scope.getReserv();
                    $scope.getAllReserv();
                  
                })

                }).catch(function () {
                    $scope.getReserv();
                    $scope.getAllReserv();
               
            });
        }

        $scope.delete = function ()
        {
            ReservationService.DeleteReservation(reservation.ReservationId).then(function (response) {
                $scope.status = "Ok";
                $scope.getReserv();
                $scope.getAllReserv();

            })
        }
        $scope.editReservation = function () {
            format = moment(reservation.ReservationDate, 'DD/MM/YYYY HH:mm').format("MM/DD/YYYY HH:mm");
            reservData = new Date(Date.parse(format));
            reservDate = new Date(reservation.ReservationDate);
            date= new Date();
           
            if (reservDate.getDate() == date.getDate() && reservDate.getHours() - 1 <= date.getHours()
                && reservDate.getMinutes() > date.getMinutes()) {
                $scope.showAlert();
            }
            else
            {
                $scope.showEditReservation();
            }

        };

        $scope.deleteReservation = function () {
            CommonContentService.showConfirm($mdDialog, " Please, confirm deleting", "Do you really want to delete this item?", $scope.delete,null);
            $scope.cancel();
        };

    }]);