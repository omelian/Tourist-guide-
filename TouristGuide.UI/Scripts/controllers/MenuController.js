app.controller("MenuController", function ($scope, $mdDialog, $mdMedia, $routeParams, MenuService, filterService, ReservationService, CommonContentService) {
    $scope.restaurantId = $routeParams.profileId;

    MenuService.GetMenuById($scope.restaurantId).then(function (response) {
        $scope.menus = response.data;
        $scope.loadDishType();
    });
    $scope.modalShown = false;
    $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');

    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };
    $scope.selectModel = "All";
    $scope.showModel = '';
    $scope.changeModel = function () {
        $scope.showModel = $scope.selectModel;
        if ($scope.selectModel == "All") {
            $scope.showModel = '';
        }
    }
    $scope.showMenuDialog = function (ev, name, price, pictureUrl, description, calories, preparationTime, menuId_) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            locals: { nameById: name, priceById: price, pictureUrlById: pictureUrl, descriptionById: description, caloriesById: calories, preparationTimeById: preparationTime, menuId: menuId_ },
            controller: ['$scope', '$mdDialog', 'priceById', 'nameById', 'pictureUrlById', 'descriptionById', 'caloriesById', 'preparationTimeById', 'menuId', function ($scope, $mdDialog, priceById, nameById, pictureUrlById, descriptionById, caloriesById, preparationTimeById, menuId) {
                var reservation = [];
                reservation.MenuItems = [];
                reservation.MenuItems.length = 0;
                if ($scope.reservation.ReservationId != undefined)
                    reservation = $scope.reservation;

                $scope.menuId = menuId;
                $scope.priceById = priceById;
                $scope.nameById = nameById;
                $scope.pictureUrlById = pictureUrlById;
                $scope.descriptionById = descriptionById;
                $scope.caloriesById = caloriesById;
                $scope.preparationTimeById = preparationTimeById;

                $scope.Count = 0;
                for (var i = 0; i < reservation.MenuItems.length; i++) {
                    if (reservation.MenuItems[i].MenuId === $scope.menuId) {
                        $scope.Count = reservation.MenuItems[i].Count;
                        break;
                    }
                }

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function (answer) {
                    $mdDialog.hide(answer);
                };

                $scope.addItemToReservation = function () {

                    for (var i = 0; i < reservation.MenuItems.length; i++) {
                        if (reservation.MenuItems[i].MenuId === $scope.menuId) {
                            if (reservation.MenuItems[i].Count < reservation.NumberOfPersons * 2) {
                                reservation.MenuItems[i].Count += 1;
                                reservation.MenuItems[i].PictureUrl = $scope.pictureUrlById;
                                $scope.Count = reservation.MenuItems[i].Count;
                                ReservationService.UpdateRestaurantReservationMenu(reservation.ReservationId, reservation.MenuItems);
                                return;
                            }
                            else {
                                alert("You have to many items for reservation!");
                                return;
                            }

                        }

                    }

                    item = {
                        RestaurantReservationMenuModelId: '',
                        MenuId: $scope.menuId,
                        Name: $scope.nameById,
                        Price: $scope.priceById,
                        Callories: $scope.caloriesById,
                        PictureUrl: $scope.pictureUrlById,
                        Count: 1
                    }
                    $scope.Count = 1;
                    reservation.MenuItems.push(item);
                    ReservationService.UpdateRestaurantReservationMenu(reservation.ReservationId, reservation.MenuItems).then(function () {
                        ReservationService.GetReservationsByUserName($scope.currentUser.Name, $scope.restaurantId
                          ).then(function (response) {
                              var items = response.data.MenuItems;
                              for (var i = 0; i < items.length; i++) {
                                  if (items[i].MenuId == reservation.MenuItems[i].MenuId) {
                                      reservation.MenuItems[i].RestaurantReservationMenuModelId = items[i].RestaurantReservationMenuModelId;

                                  }
                              }
                          })
                    })
                };


            }],
            templateUrl: 'app/views/restaurant/restaurant-items/menuDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        })
    };
    $scope.loadDishType = function () {
        $scope.dishTypes = ["All"];
        var flag = false;
        for (var i = 0; i < $scope.menus.length; i++) {
            flag = false;
            for (var j = 0; j < $scope.dishTypes.length; j++) {
                if ($scope.menus[i].DishType == $scope.dishTypes[j]) {
                    flag = true;
                }
            }
            if (flag == false) {
                $scope.dishTypes.push($scope.menus[i].DishType);
            }
        };
    };
    $scope.showEditMenuDialog = function (ev, menu, restaurantId) {
        $mdDialog.show({
            locals: { editMenu: menu, restaurantById: restaurantId },
            controller: ['$scope', '$mdDialog', 'MenuService', 'editMenu', 'restaurantById', function ($scope, $mdDialog, MenuService, editMenu, restaurantById) {
                $scope.editMenu = angular.copy(editMenu);
                $scope.restaurantById = restaurantById;
                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function (answer) {
                    $mdDialog.hide(answer);
                };
                $scope.onDrop = function (ev) {
                    if (ev.files.length != 0) {
                        var Extension = ev.files[0].name.substring(ev.files[0].name.lastIndexOf('.') + 1).toLowerCase();

                        if (Extension == "gif" || Extension == "png" || Extension == "bmp"
                                    || Extension == "jpeg" || Extension == "jpg") {
                            if (ev.files[0].size < 10585760) {
                                var formData = new FormData();
                                formData.append("uploadedFile", ev.files[0]);
                                httpRequest = new XMLHttpRequest();
                                httpRequest.open("POST", "/ListOfRestaurant/UploadPhoto", false);
                                httpRequest.send(formData);
                                url = httpRequest.response;
                                $scope.editMenu.PictureUrl = url;

                            }
                            else {
                                alert("Photo size is to large!")
                            }
                        }
                        else {
                            alert("Photo has incorrect format!")
                        }
                    }
                }
                $scope.updateMenu = function () {
                    MenuService.UpdateMenu($scope.editMenu, $scope.restaurantById);
                    editMenu.Name = $scope.editMenu.Name;
                    editMenu.Price = $scope.editMenu.Price;
                    editMenu.Description = $scope.editMenu.Description;
                    editMenu.PictureUrl = $scope.editMenu.PictureUrl;
                    editMenu.DishType = $scope.editMenu.DishType;
                    editMenu.Callories = $scope.editMenu.Callories;
                    editMenu.PreparationTime = $scope.editMenu.PreparationTime;
                    $scope.hide();
                };




                
                    $scope.dishTypes = [
                        "Main dish",
                        "Hot snack",
                        "Cold snack",
                        "Salad",
                        "Drink",
                        "Alcohol drink",
                        "Sauce",
                        "Desert"
                    ];
                
            }],
            templateUrl: 'app/views/restaurant/restaurant-items/EditMenuItemDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        })
    };

    $scope.showAddMenuItemDialog = function (ev) {
        filterService.addModel($scope.menus);
        $mdDialog.show({
            controller: addMenuItemController,
            templateUrl: 'app/views/restaurant/restaurant-items/addMenuItemDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        });
    };
    $scope.$on('changeModel', function (event, model) {
        $scope.menus.push(model);
    })
    $scope.deleteMenuItem = function (ev, menu) {
        //$scope.showConfirm(ev, menu);
        CommonContentService.showConfirm($mdDialog, " Please, confirm deleting", "Do you really want to delete this item?", $scope.delete, menu);

    };

    $scope.delete = function (isConfirmed, menu) {
        if (isConfirmed) {
            var index = $scope.menus.indexOf(menu);
            MenuService.DeleteMenuItem(menu.MenuId, $scope.restaurantId).then(function () {
                $scope.menus.splice(index, 1);
            });
        }
    }
})
