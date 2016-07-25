app.factory("RestaurantService", ['$http', function ($http) {
    var RestaurantService = {};
    RestaurantService.GetRestaurant = function () {
        return $http.get('');
    }
    RestaurantService.GetRestaurantFromJson = function () {
        return $http.get("/ListOfRestaurant/GetAllRestaurants");
    };
    RestaurantService.GetRestaurantById = function (id) {
        return $http.get("/ListOfRestaurant/GetRestaurantById", { params: { restaurantId: id } });
    };

    RestaurantService.AddFavorite = function (restaurantId) {
        return $http.post("/ListOfRestaurant/AddFavorite", { id: restaurantId })
    };

    RestaurantService.RemoveFavorite = function (restaurantId) {
        return $http.post("/ListOfRestaurant/RemoveFavorite", { id: restaurantId })
    };

    RestaurantService.AddRestaurant = function (data) {
        return $http.post("/Admin/AddProfile", { newProfile: data });
    };

    RestaurantService.EditRestaurant = function (data) {
        return $http.post("/Admin/EditProfile", { newProfile: data });
    };

    RestaurantService.DeleteRestaurant = function (data) {
        return $http.post("/Admin/DeleteProfile", { id: data });
    }


    RestaurantService.IsInFavorites = function (restaurantId) {
        return $http.post("/ListOfRestaurant/IsInFavorites", { id: restaurantId });
    };


    return RestaurantService;
}]);
