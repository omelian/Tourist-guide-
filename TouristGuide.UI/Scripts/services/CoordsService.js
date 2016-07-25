app.factory("CoordsService", ['$http',function ($http) {
    var CoordsService = {};
    CoordsService.GetLastCoords = function () {
        return $http.get('/Coords/GetAllCoords');
    };
    CoordsService.GetRestaurantCoords = function () {
        return $http.get('/Coords/GetRestaurantCoords');
    };
    CoordsService.GetSightseeingsCoords = function () {
        return $http.get('/Coords/GetSightseeingsCoords');
    };
    CoordsService.GetLeisureCoords = function () {
        return $http.get('/Coords/GetLeisureCoords');
    };
CoordsService.GetCoordById = function (id) {
        return $http.get('/Coords/GetCoordById', { params: { RestaurantId: id } });
    };
    return CoordsService;
}]);