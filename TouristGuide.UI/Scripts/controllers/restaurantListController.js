app.controller("RestaurantListController", function ($scope, $rootScope, RestaurantService, filterService, RatesService, ClaimService, $mdDialog, CommonContentService) {
    $scope.choose = '';
    RestaurantService.GetRestaurantFromJson().then(function (response) {
        $scope.restaurants = response.data;
    });
    $scope.setModel = function () {
        filterService.addModel($scope.choose);
    }
    $scope.setModelNull = function () {
        filterService.addModel('');
        filterService.addPath(0);
    }
    $scope.setPath = function setPath(id, travelMode) {
        $rootScope.$broadcast('makePath', id, travelMode);
    }
    $scope.currentUser = {};
    ClaimService.GetClaim().then(function (response) {
        $scope.currentUser = response.data;
    })

    $scope.checkedAdmin = function () {
        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Admin') {
                document.getElementById("addProfileButton").style.display = "block";
                return false;
            }
            else
                return true;
        }
    }


    $scope.showProfileDialog = function (profile) {
        $mdDialog.show({
            locals: { Profile: profile },
            controller: ['$scope', '$mdDialog', 'RestaurantService', 'Profile', function ($scope, $mdDialog, RestaurantService, Profile) {
                $scope.restData = {
                    Name: '',
                    Country: '',
                    City: '',
                    Street: '',
                    Number: '',
                    XCoord: '',
                    YCoord: '',
                    ProfileType: 0,
                    Id: 0
                }
                $scope.Title = "";
                if (Profile == undefined) {
                    $scope.Title = "Add";
                }
                else {
                    $scope.Title = "Update";
                    $scope.restData.Name = Profile.Name;
                    $scope.restData.XCoord = Profile.XCoord;
                    $scope.restData.YCoord = Profile.YCoord;
                    $scope.restData.Country = 'Україна';
                    $scope.restData.City = Profile.City;
                    $scope.restData.Street = Profile.Street;
                    $scope.restData.Number = Profile.Number;
                    $scope.restData.Id = Profile.ProfileId;
                }

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };

                $scope.answer = function () {
                    $scope.restData.Name = document.getElementById("name").value;
                    $scope.restData.Country = document.getElementById("country").value;
                    $scope.restData.City = document.getElementById("locality").value;
                    $scope.restData.Street = document.getElementById("route").value;
                    $scope.restData.Number = document.getElementById("street_number").value;
                    var el1 = document.getElementById("restLong");
                    var el2 = document.getElementById("restLan");
                    var xcoord = parseFloat(el1.value).toFixed(8);
                    var ycoord = parseFloat(el2.value).toFixed(8);
                    $scope.restData.XCoord = xcoord;
                    $scope.restData.YCoord = ycoord;
                    if (Profile == undefined) {
                        var progerss = document.getElementById("formWithoutData");
                        var progerssData = document.getElementById("formWithData");
                        var btn = document.getElementById("dialog-btn");
                        btn.disabled = true;
                        progerss.style.display = "block";
                        progerssData.style.display = "none";
                        RestaurantService.AddRestaurant($scope.restData).success(function () {
                            progerss.style.display = "none";
                            progerssData.style.display = "block";
                            btn.disabled = false;
                            $mdDialog.hide();
                        }).error(function () {
                            progerss.style.display = "none";
                            progerssData.style.display = "block";
                            btn.disabled = false;
                            document.getElementById('profNameError').style.display = "inline-block";
                        })
                    }
                    else {
                        var progerss = document.getElementById("formWithoutData");
                        var progerssData = document.getElementById("formWithData");
                        var btn = document.getElementById("dialog-btn");
                        btn.disabled = true;
                        progerss.style.display = "block";
                        progerssData.style.display = "none";
                        RestaurantService.EditRestaurant($scope.restData).success(function () {
                            progerss.style.display = "none";
                            progerssData.style.display = "block";
                            btn.disabled = false;
                            $mdDialog.hide();
                        }).error(function () {
                            progerss.style.display = "none";
                            progerssData.style.display = "block";
                            btn.disabled = false;
                            document.getElementById('profNameError').style.display = "inline-block";                            
                        })
                    }
                };
            }],
            templateUrl: 'app/views/restaurant/addProfileDialog.html',
            parent: angular.element(document.body),
            targetEvent: null,
            clickOutsideToClose: true
        }).then(function (data) {
            RestaurantService.GetRestaurantFromJson().then(function (response) {
                $scope.restaurants = response.data;
                $rootScope.$broadcast('mapReload');
            });

        })
    }


    $scope.onDeletingConfirmed = function (isConfirmed, profile) {
        if (isConfirmed) {
            var index = $scope.restaurants.indexOf(profile);
            RestaurantService.DeleteRestaurant(profile.ProfileId).then(function () {
                $scope.restaurants.splice(index, 1);
                $rootScope.$broadcast('mapReload');
            })
        }
    }


    $scope.DeleteProfile = function (profile) {
        CommonContentService.showConfirm($mdDialog, "Please, confirm deleting", "Do you really want to delete this profile?", $scope.onDeletingConfirmed, profile);
    };

    $scope.travelMode = "DRIVING"
    $scope.clickTravelMode = function (item, id) {
        hideAll();
        if (item == 'Driving') {
            $scope.travelMode = "DRIVING";
            $scope.setPath(id, $scope.travelMode);

        }
        if (item == 'Walking') {
            $scope.travelMode = "WALKING";
            $scope.setPath(id, $scope.travelMode);
        }
        if (item == 'Transit') {
            $scope.travelMode = "TRANSIT";
            $scope.setPath(id, $scope.travelMode);
        }

    }

    var hideAll = function () {
        for (var i = 0; i < $scope.restaurants.length; i++) {
            var element = document.getElementById("infobox" + $scope.restaurants[i].ProfileId);
            if (element != undefined)
                element.innerText = "";
        }
    };

    var profileAverageRating = function (rates) {
        var profileMarksSum = 0;

        for (var i = 0; i < rates.length; ++i) {
            profileMarksSum += rates[i].Mark;
        };
        return profileMarksSum / rates.length;

    };


    $scope.SetRestaurantRateByProfileId = function (id, mark, userId) {

        RatesService.SetRestaurantRate(id, mark.Rate, userId).then(function () {
            RatesService.GetRatesByRestaurantId(id).then(function (response) {
                var profileRates = response.data;
                mark.Rate = profileAverageRating(profileRates);
            })
        })
    };



    $scope.returnHref = function () {
        if ($scope.currentUser != undefined) {
            if ($scope.currentUser.Role == 'Admin')
                return 'moderators';
            else
                return 'news';
        }
    }
});