app.controller('ReservationMenuController', function ($scope, $routeParams, MenuService, ReservationService,$mdDialog) {

    $scope.currentUser = {};
    $scope.menus = [];
    $scope.userMenus = [];
 

  
    $scope.restaurantId = $routeParams.profileId;
    $scope.reservationId = -1;
    MenuService.GetMenuById($scope.restaurantId).then(function (response) {
        $scope.menus = response.data;

        for (j = 0; j < $scope.menus.length; ++j) {
            $scope.menus[j].Count = 0;
        }
    });

    $scope.loadReservationMenuList = function (reservation) {

        for (i = 0; i < reservation.MenuItems.length; ++i) {
            for (j = 0; j < $scope.menus.length; ++j) {
                if (reservation.MenuItems[i].MenuId === $scope.menus[j].MenuId) {
                    reservation.MenuItems[i].PictureUrl = $scope.menus[j].PictureUrl;
                }
            }
        }

    }

    $scope.editMenu = function (reservation) {


        $scope.reservationId = reservation.ReservationId;
        $scope.userMenus = reservation.MenuItems;




        $mdDialog.show({
            locals: { reservationMenu: $scope.menus, userMenu: reservation.MenuItems, numberOfPersons: reservation.NumberOfPersons },
                controller: ['$scope', '$mdDialog', 'reservationMenu', 'userMenu', 'numberOfPersons', function ($scope, $mdDialog, reservationMenu, userMenu, numberOfPersons) {
                    $scope.maxValue = numberOfPersons*2;
                    $scope.validated = true;

                    $scope.TotalPrice = 0;
                    $scope.TotalCalories = 0;

                    $scope.selectModel = "All";
                    $scope.showModel = "";
                    $scope.dishTypes = [];
                    $scope.dishTypes = [
                          "All",
                          "Main dish",
                          "Hot snack",
                          "Cold snack",
                          "Salad",
                          "Drink",
                          "Alcohol drink",
                          "Sauce",
                          "Desert"
                    ];

                    $scope.Message = "";
                    $scope.changeModel = function (val) {
                        $scope.showModel = val.selectModel;
                        if ($scope.showModel == "All") {
                            $scope.showModel = "";
                        }
                    }

                    if (userMenu != undefined)
                        $scope.userMenus = angular.copy(userMenu);
                    else
                        $scope.userMenus = [];

                    $scope.menuList = angular.copy(reservationMenu);


                    var userMenusCount = 0;
                    if ($scope.userMenus != undefined) {
                        userMenusCount = $scope.userMenus.length;
                    }

                    for (j = 0; j < $scope.menuList.length; ++j) {
                        for (i = 0; i < userMenusCount; ++i) {
                            if ($scope.userMenus[i].MenuId === $scope.menuList[j].MenuId) {
                                $scope.menuList[j].Count = $scope.userMenus[i].Count;
                                $scope.menuList[j].RestaurantReservationMenuModelId = $scope.userMenus[i].RestaurantReservationMenuModelId;
                                $scope.TotalPrice += $scope.userMenus[i].Price * $scope.userMenus[i].Count;
                                $scope.TotalCalories += $scope.userMenus[i].Callories * $scope.userMenus[i].Count;
                            }
                        }
                    }


                    $scope.hide = function () {
                        $mdDialog.hide();
                    };
                    $scope.cancel = function () {
                        $mdDialog.cancel();
                    };
                    $scope.answer = function () {
                        if ($scope.validated)
                            $mdDialog.hide($scope.userMenus);
                    };


                    $scope.chooseOne = function (value) {
                        if (value.Count < $scope.maxValue){
                            value.Count += 1;
                        }

                        $scope.onItemCountChange(value)
                    }



                    $scope.onItemCountChange = function (value) {
                        if (value.Count >= 0 && value.Count <= $scope.maxValue) {
                            $scope.validated = true;
                            item = {
                                RestaurantReservationMenuModelId: value.RestaurantReservationMenuModelId,
                                MenuId: value.MenuId,
                                Name: value.Name,
                                Price: value.Price,
                                Callories: value.Callories,
                                Count: value.Count,
                                PictureUrl: value.PictureUrl
                            }

                            if (value.Count > 0) {
                                value.Opacity = 1;
                            }
                            else {
                                value.Opacity = 0.5;
                            }


                            $scope.TotalPrice = 0;
                            $scope.TotalCalories = 0;
                            var update = false;
                            for (j = 0; j < $scope.userMenus.length; ++j) {
                                if (item.MenuId === $scope.userMenus[j].MenuId) {
                                    $scope.userMenus[j] = item;
                                    update = true;
                                }
                                $scope.TotalPrice += $scope.userMenus[j].Price * $scope.userMenus[j].Count;
                                $scope.TotalCalories += $scope.userMenus[j].Callories * $scope.userMenus[j].Count;
                            }
                            if (!update) {
                                $scope.userMenus.push(item);
                                $scope.TotalPrice += item.Price * item.Count;
                                $scope.TotalCalories += item.Callories * item.Count;
                            }
                        }
                        else{
                            $scope.validated = false;
                        }
                    }


                }],
                templateUrl: 'app/views/restaurant/restaurant-items/editReservationMenuDialog.html',
                parent: angular.element(document.body),
                targetEvent: null,
                clickOutsideToClose: true
        }).then(function (answer) {
            reservation.MenuItems = answer;
            ReservationService.UpdateRestaurantReservationMenu($scope.reservationId, answer).then(function () {
               
                for (var j = 0; j < reservation.MenuItems.length; j++) {
                    if (reservation.MenuItems[j].Count == 0) {
                        reservation.MenuItems.splice(j, 1);
                        j--;
                    }
                }

                    ReservationService.GetReservationsByUserName(reservation.UserName, $scope.restaurantId).then(function (response) {
                        var items = response.data.MenuItems;
                        for (var i = 0; i < items.length; i++) {
                            if (items[i].MenuId == reservation.MenuItems[i].MenuId) {
                                reservation.MenuItems[i].RestaurantReservationMenuModelId = items[i].RestaurantReservationMenuModelId;
                            }
                        }
                      })
                })
            })

    }
    


    $scope.isCollapsed = true;




});