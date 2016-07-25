app.factory("AddMenuService", ['$http', function ($http) {
    var AddMenuService = {};
    AddMenuService.AddMenuItem = function (restId,Name,Description, Price, Calories,PreparationTime,DishType,PhotoUrl) {
        return $http.post('/AddMenuItem/AddMenu', {restaurantId: restId,name:Name,description:Description,price:Price,calories:Calories,preparationTime:PreparationTime,dishType:DishType,photoUrl:PhotoUrl });
    }
    return AddMenuService;
}]);