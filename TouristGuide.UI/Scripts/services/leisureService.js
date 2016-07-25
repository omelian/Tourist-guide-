app.factory("LeisureService", ['$http', function ($http) {
    var LeisureService = {};

    LeisureService.GetLeisureFromJson = function () {
        return $http.get("/ListOfLeisure/GetAllLeisure");
    };
    LeisureService.GetLeisureById = function (id) {
        return $http.get("/ListOfRestaurant/GetRestaurantById", { params: { restaurantId: id } });
    };

    LeisureService.AddLeisure = function (data) {
        return $http.post("/Admin/AddProfile", { newProfile: data });
    };

    LeisureService.EditLeisure = function (data) {
        return $http.post("/Admin/EditProfile", { newProfile: data });
    };

    LeisureService.DeleteLeisure = function (data) {
        return $http.post("/Admin/DeleteProfile", { id: data });
    }

    LeisureService.AddFavorite = function (leisureId) {
        return $http.post("/ListOfLeisure/AddFavorite", { id: leisureId })
    };

    LeisureService.RemoveFavorite = function (leisureId) {
        return $http.post("/ListOfLeisure/RemoveFavorite", { id: leisureId })
    };

    LeisureService.IsInFavorites = function (leisureId) {
        return $http.post("/ListOfLeisure/IsInFavorites", { id: leisureId });
    };


    return LeisureService;
}]);