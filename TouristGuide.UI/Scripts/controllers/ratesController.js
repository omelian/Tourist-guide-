app.controller('RatesController', function ($scope, $routeParams, ClaimService, RatesService,$mdDialog) {
    $scope.restaurantId = $routeParams.profileId;

    $scope.currentUser = {};
    $scope.restaurantRates = [];

    $scope.max = 10;
    $scope.isReadonly = false;
    

    if ($scope.restaurantId != undefined) {
        RatesService.GetRatesByRestaurantId($scope.restaurantId).then(function (response) {
            $scope.restaurantRates = response.data;
            $scope.profileAverageRating();
        })
    }
   
    
    ClaimService.GetClaim().then(function (response) {
        $scope.currentUser = response.data;
    })
    $scope.rate = null;

    $scope.profileAverageRating = function () {
        var profileMarksSum = 0;
        if ($scope.restaurantRates.length == 0) {
            $scope.rate = 0;
        }
        else {
            for (var i = 0; i < $scope.restaurantRates.length; ++i) {

                profileMarksSum += $scope.restaurantRates[i].Mark;
            };
            $scope.rate = profileMarksSum / $scope.restaurantRates.length;
        }
    };

    $scope.hoveringOver = function(value) {
        $scope.overStar = value;
        $scope.percent = 100 * (value / $scope.max);
    };

    $scope.showConfirm = function (dialog, title, message, callBack, callBackParamenter) {
        dialog.show({
            locals: { Title: title, Message: message },
            controller: ['$scope', '$mdDialog', 'Title', 'Message', function ($scope, $mdDialog, title, message) {

                $scope.Title = title;
                $scope.Message = message;

                $scope.hide = function () {
                    $mdDialog.hide();
                };

                $scope.url = encodeURIComponent(window.location.href);

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


    $scope.SetRestaurantRate = function (mark) {
        if ($scope.currentUser.Role != 'User')
        {
            $scope.showConfirm($mdDialog, "Sign in or register a new account", "This action can be performed only as registered user", $scope.goToRegister, null);
        }
        else {
            $scope.rate = "  ";
            RatesService.SetRestaurantRate($scope.restaurantId, mark, $scope.currentUser.Id).then(function () {
                RatesService.GetRatesByRestaurantId($scope.restaurantId).then(function (response) {
                    $scope.restaurantRates = response.data;
                    $scope.profileAverageRating();
                })
            })
        }
    };

   
});