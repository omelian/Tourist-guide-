app.factory("CommentsService", ['$http', function ($http) {
    var CommentsService = {};
    CommentsService.GetCommentsByRestaurantId = function (id) {
        return $http.get("/ListOfRestaurant/GetCommentsByRestaurantId", { params: { restaurantId: id } });
    };

    CommentsService.GetCommentsByUser = function () {
        return $http.get("/Manage/GetAllComments");
    };

    return CommentsService;
}]);