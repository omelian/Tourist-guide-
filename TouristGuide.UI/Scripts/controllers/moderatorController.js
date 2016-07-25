app.controller('ModeratorsController', function ($scope, $routeParams, ModeratorService, $http, $mdDialog, CommonContentService) {
    $scope.restaurantId = $routeParams.profileId;
    $scope.restaurantModerators = {};
    ModeratorService.GetModeatorsByRestaurantId($scope.restaurantId).then(function (response) {
       $scope.restaurantModerators = response.data;
    })
   
    $scope.AddModerator = function (moderator_) {
        ModeratorService.AddModerator(moderator_, $scope.restaurantId).then(function () {
            ModeratorService.GetModeatorsByRestaurantId($scope.restaurantId).then(function (response) {
                $scope.restaurantModerators = response.data;
            })
        })
    };


    $scope.onDeletingConfirmed = function (isConfirmed, moderator) {
        if (isConfirmed) {
            var index = $scope.restaurantModerators.indexOf(moderator);            
            ModeratorService.DeleteModerator(moderator.Id).then(function () {
                $scope.restaurantModerators.splice(index, 1);
            })
        }
    }


    $scope.DeleteModerator = function (moderator) {
        CommonContentService.showConfirm($mdDialog, "Please, confirm deleting", "Do you really want to delete this moderator?", $scope.onDeletingConfirmed, moderator);
    };


    $scope.showSetModerator = function (ev) {
        $mdDialog.show({
            locals: { restId: $routeParams.profileId },
            controller: ['$scope', '$mdDialog', 'ModeratorService', 'restId', function ($scope, $mdDialog, ModeratorService, restId) {
                
                $scope.moder = {
                    Email: ''
                };

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function (answer) {
                    var progerss = document.getElementById("formWithoutData");
                    var progerssData = document.getElementById("formWithData");
                    var btn = document.getElementById("dialog-btn");
                    btn.disabled = true;
                    progerss.style.display = "block";
                    progerssData.style.display = "none";
                    ModeratorService.SetModerator(answer.Email, restId).success(function (response) {
                        btn.disabled = false;
                        document.getElementById('emainError').innerHTML = response;
                        document.getElementById('emainError').style.display = "inline-block";
                        progerss.style.display = "none";
                        progerssData.style.display = "block";
                        if(response == "" || response == null)
                        {
                            $mdDialog.hide();
                        }                        
                    }).error(function (response) {
                        btn.disabled = false;
                        progerss.style.display = "none";
                        progerssData.style.display = "block";
                        document.getElementById('emainError').innerHTML = "Server error, reload this page";
                        document.getElementById('emainError').style.display = "inline-block";
                    })
                };
            }],
            templateUrl: 'app/views/restaurant/restaurant-items/moderSetDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function () {
            ModeratorService.GetModeatorsByRestaurantId($scope.restaurantId).then(function (response) {
                $scope.restaurantModerators = response.data;
            })
        })
    }

   
    $scope.showAddModerator = function (ev)
    {
        $mdDialog.show({
            locals: { restId: $routeParams.profileId},
            controller: ['$scope', '$mdDialog', 'ModeratorService', 'restId', function ($scope, $mdDialog, ModeratorService, restId) {
                
                $scope.isValid = false;
                $scope.checkPassword = function () {
                    if (document.getElementById('Password').value == document.getElementById('ConfirmPassword').value) {
                        isValid = true;
                        document.getElementById('passwordError').style.display = "none";
                    }
                    else {
                        isValid = false;
                        document.getElementById('passwordError').style.display = "inline-block";
                    }
                }

                $scope.chackValid = function () {
                    if (isValid) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }

                $scope.ShowHidePassword = function (id) {
                    element = document.getElementById(id);
                    if (element.type == 'password') {
                        element.type = 'text'
                    }
                    else {
                        element.type = 'password'
                    }
                }

                $scope.moder = {
                    Email: '',
                    Password: ''
                };                

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function (answer) {
                    var progerss = document.getElementById("formWithoutData");
                    var progerssData = document.getElementById("formWithData");
                    var btn = document.getElementById("dialog-btn");
                    btn.disabled = true;
                    progerss.style.display = "block";
                    progerssData.style.display = "none";
                    ModeratorService.AddModerator(answer, restId).success(function () {
                        btn.disabled = false;
                        progerss.style.display = "none";
                        progerssData.style.display = "block";
                        $mdDialog.hide();
                    }).error(function () {
                        btn.disabled = false;
                        progerss.style.display = "none";
                        progerssData.style.display = "block";
                        document.getElementById('emainError').style.display = "inline-block";
                   })                 
                };
            }],
            templateUrl: 'app/views/restaurant/restaurant-items/moderAddDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function () {
            ModeratorService.GetModeatorsByRestaurantId($scope.restaurantId).then(function (response) {
                $scope.restaurantModerators = response.data;
            })
        })
    }

});