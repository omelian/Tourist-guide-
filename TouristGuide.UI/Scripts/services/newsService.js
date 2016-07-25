app.factory("NewsService", ['$http', function ($http) {
    var NewsService = {};
    NewsService.GetNewsFromRestaurantId = function (id) {
        return $http.get("/ListOfRestaurant/GetNewsByRestaurantId", { params: { restaurantId: id } });
    };

    NewsService.DeleteNewsFromRestaurant = function (newsId,profileId) {
        return $http.post("/ListOfRestaurant/DeleteNewsFromRestaurant", { newsId: newsId, restaurantId: profileId });
    };

    NewsService.AddNewsToRestaurant = function (_news, profileId) {
        return $http.post("/ListOfRestaurant/AddNews", { news: _news, restaurantId: profileId });
    };

    NewsService.UpdateRestaurantNews = function (_news, profileId) {
        return $http.post("/ListOfRestaurant/UpdateRestaurantNews", { news: _news, restaurantId: profileId })
    };


    return NewsService;
}]);