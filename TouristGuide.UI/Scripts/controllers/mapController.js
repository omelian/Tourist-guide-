//directive for map resizing for use just type for example <div set-height></div>
app.directive('setHeight', function ($window) {
    return {
        link: function (scope, element, attrs) {
            element.css('height', $window.innerHeight - 200 + 'px');
        }
    }
});
app.controller("MapController", function ($scope, CoordsService, filterService, $location) {
    $scope.Coords = {};
    $scope.choose = '';
    $scope.direction = '';
    $scope.currentPosition = null;
    $scope.pathLength = 0;
    $scope.durationTime = "";
    $scope.travelMode = "DRIVING";
    if ($location.path() == "/main")
        CoordsService.GetLastCoords().then(function (response) {
            $scope.Coords = response.data;
        });
    if ($location.path() == "/restaurantList")
        CoordsService.GetRestaurantCoords().then(function (response) {
            $scope.Coords = response.data;
        });
    if ($location.path() == "/sightseeingList")
        CoordsService.GetSightseeingsCoords().then(function (response) {
            $scope.Coords = response.data;
        });
    if ($location.path() == "/leisureList") {
        CoordsService.GetLeisureCoords().then(function (response) {
            $scope.Coords = response.data;
        });
    }

    $scope.getMarkerIcon = function (coord) {
        if (coord.TypeOfProfile == "Restaurant")
            return '/Content/restaurantMarker.png';
        if (coord.TypeOfProfile == "Sightseeing")
            return '/Content/attractionMarker.png';
        if (coord.TypeOfProfile == "Leisure")
            return '/Content/leisure.png';
    }
    $scope.callModel = function () {
        $scope.choose = filterService.getModel();
    };
    $scope.check = function () {
        if ($scope.direction != '') {
            return true;
        }
        else {
            return false;
        }
    }
    $scope.$on('mapReload', function () {
        if ($location.path() == "/restaurantList")
            CoordsService.GetRestaurantCoords().then(function (response) {
                $scope.Coords = response.data;
            });
        if ($location.path() == "/sightseeingList")
            CoordsService.GetSightseeingsCoords().then(function (response) {
                $scope.Coords = response.data;
            });
        if ($location.path() == "/leisureList") {
            CoordsService.GetLeisureCoords().then(function (response) {
                $scope.Coords = response.data;
            });
        }
    });

    $scope.$on('makePath', function (event, id, travelMode) {
        $scope.pathLength = 0;
        $scope.durationTime = "";
        $scope.travelMode = travelMode;

        for (i = 0; i < $scope.Coords.length; i++) {
            if ($scope.Coords[i].Id == id) {
                $scope.direction = String($scope.Coords[i].Lantitude) + "," + String($scope.Coords[i].Longtitude);
            };
        }
        //promises for corrtctly working
        navigator.geolocation.getCurrentPosition(function (pos) {
            $scope.currentPosition = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
            var directionsService = new google.maps.DirectionsService();
            var request = {
                origin: $scope.currentPosition,
                destination: $scope.direction,
                travelMode: travelMode
            };
            directionsService.route(request, function (response, status) {
                if (status === 'OK') {
                    var point = response.routes[0].legs[0];
                    $scope.durationTime = point.duration.text;
                    $scope.pathLength = point.distance.text;
                }
                document.getElementById("infobox" + id).innerHTML = "<b>Path length: " + $scope.pathLength + " Duration time:" + $scope.durationTime + "</b>";
            });

        });
    });
    $scope.getCurrent = function () {
        var options = {
            enableHighAccuracy: true,
        };
        navigator.geolocation.getCurrentPosition(function (pos) {
            $scope.currentPosition = new google.maps.LatLng(pos.coords.latitude, pos.coords.longitude);
        }, function (error) {
            alert('Unable to get location: ' + error.message);
        }, options);
    }
});