app.factory("SightseeingService", ['$http', function ($http) {
    var SightseeingService = {};

    SightseeingService.GetSightseeingsFromJson = function () {
        return $http.get("/ListOfSightseeing/GetAllSightseeings");
    };

    SightseeingService.AddFavorite = function (sightseeingId) {
        return $http.post("/ListOfSightseeing/AddFavorite", { id: sightseeingId })
    };

    SightseeingService.RemoveFavorite = function (sightseeingId) {
        return $http.post("/ListOfSightseeing/RemoveFavorite", { id: sightseeingId })
    };

    SightseeingService.IsInFavorites = function (sightseeingId) {
        return $http.post("/ListOfSightseeing/IsInFavorites", { id: sightseeingId });
    };

    SightseeingService.GetSightseeingById = function (id) {
        return $http.get("/ListOfRestaurant/GetRestaurantById", { params: { restaurantId: id } });
    };

    SightseeingService.AddSightseeing = function (data) {
        return $http.post("/Admin/AddProfile", { newProfile: data });
    };

    SightseeingService.EditSightseeing = function (data) {
        return $http.post("/Admin/EditProfile", { newProfile: data });
    };

    SightseeingService.DeleteSightseeing = function (data) {
        return $http.post("/Admin/DeleteProfile", { id: data });
    }

    return SightseeingService;
}]);