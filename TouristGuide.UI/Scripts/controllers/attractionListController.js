app.controller("AttractionListController", function ($scope, AttractionService, filterService, ClaimService, $mdDialog) {
    $scope.choose = '';
    AttractionService.GetAttractionFromJson().then(function (response) {
        $scope.attractions = response.data;
    });
    $scope.setModel = function () {
        filterService.addModel($scope.choose);
    }
    $scope.setModelNull = function () {
        filterService.addModel('');
        filterService.addPath(0);
    }
    $scope.setPath = function setPath(id) {
        filterService.addPath(id);
    }
    $scope.currentUser = {};
    ClaimService.GetClaim().then(function (response) {
        $scope.currentUser = response.data;
    })

    $scope.checkedAdmin = function () {
        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Admin')
                return false;
            else
                return true;
        }
    }
 
});