app.factory("AttractionService", ['$http', function ($http) {
    var AttractionService = {};
    AttractionService.GetAttraction = function () {
        return $http.get('');
    }
    AttractionService.GetAttractionFromJson = function () {
        return $http.get("/ListOfAttractions/GetAllAttractions");
    };
    AttractionService.GetAttractionById = function (id) {
        return $http.get("/ListOfRestaurant/GetRestaurantById", { params: { restaurantId: id } });
    };

    AttractionService.AddFavorite = function (restaurantId) {
        return $http.post("/ListOfRestaurant/AddFavorite", { id: restaurantId })
    };
    AttractionService.AddAttraction = function (data) {
        return $http.post("/Admin/AddRestaurant", { restaurant: data });
    };
    return AttractionService;
}]);