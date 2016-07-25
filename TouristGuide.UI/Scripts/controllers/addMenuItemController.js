function addMenuItemController($scope, $mdDialog, $routeParams, $rootScope, AddMenuService, filterService) {
    $scope.restaurantId = $routeParams.profileId;
    $scope.name = null;
    $scope.description = null;
    $scope.price = null;
    $scope.calories = null;
    $scope.preparation = null;
    $scope.photoUrl = null;
    $scope.dishType = null;

    $scope.slides = [];
    $scope.ProgressName = "";

    $scope.loadDishType = function () {
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
                    $scope.photoUrl = url;
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

    $scope.hide = function () {
        $mdDialog.hide();
    };

    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };

    $scope.sendMenu = function () {
        if ($scope.dishType == null) {
            $scope.dishType = "Main dish";
        }

        var progerss = document.getElementById("formWithoutData");
        var progerssData = document.getElementById("formWithData");
        var btn = document.getElementById("dialog-btn");
        btn.disabled = true;
        progerss.style.display = "block";
        progerssData.style.display = "none";

        AddMenuService.AddMenuItem($scope.restaurantId, $scope.name, $scope.description, $scope.price, $scope.calories, $scope.preparation, $scope.dishType, $scope.photoUrl).success(function (response) {
            var model = filterService.getModel();
            var menuId;
            if (model.length == 0) {
                menuId = 0;
            }
            else {
                menuId = model[model.length - 1].MenuId + 1;
            }

            var newMenuItem = { Name: $scope.name, ProfileId: $scope.restaurantId, Description: $scope.description, Price: $scope.price, Callories: $scope.calories, PreparationTime: $scope.preparation, DishType: $scope.dishType, PictureUrl: $scope.photoUrl, MenuId: menuId };
            $rootScope.$broadcast('changeModel', newMenuItem);
            progerss.style.display = "none";
            progerssData.style.display = "block";
            btn.disabled = false;
            $scope.cancel();
        }).error(function (response) {
            progerss.style.display = "none";
            progerssData.style.display = "block";
            btn.disabled = false;
            alert("Element wasnt Added");
})
    }
};