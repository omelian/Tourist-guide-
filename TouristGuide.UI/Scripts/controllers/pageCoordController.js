app.controller('PageCoordController', ['$scope', '$routeParams', 'CoordsService', function ($scope, $routeParams, CoordsService) {
    $scope.restaurantId = $routeParams.profileId;
    $scope.Coord = {};
    if (typeof ($routeParams.profileId) != "undefined") {
        CoordsService.GetCoordById($scope.restaurantId).then(function (response) {
            $scope.Coord = response.data;
            var location = { lat: $scope.Coord.Lantitude, lng: $scope.Coord.Longtitude };
            initMap(location);
            addMarkerById(location);
        });
    }
    $scope.callEmptyMap = function () {
        initEmptyMap();
    }
}])