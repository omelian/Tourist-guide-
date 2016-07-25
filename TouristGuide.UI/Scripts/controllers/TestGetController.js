app.controller("CoordsController", function ($scope, CoordsService) {
    $scope.Coords = {};
    CoordsService.GetLastCoords().then(function (response) {

        $scope.Coords = response.data;
    })
});