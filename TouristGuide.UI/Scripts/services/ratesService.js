app.factory("RatesService", ['$http', function ($http) {
    var RatesService = {};
    RatesService.GetRatesByRestaurantId = function (profileId) {
        return $http.get("/Rates/GetRatesByRestaurantId", { params: { restaurantId: profileId } });
    };

    RatesService.SetRestaurantRate = function (profileId, mark, userId) {
        return $http.post("/Rates/SetRestaurantRate", { restaurantId: profileId, mark: mark, userId: userId });
    };

    return RatesService;
}]);