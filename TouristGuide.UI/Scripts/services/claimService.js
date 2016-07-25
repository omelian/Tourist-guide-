app.factory("ClaimService", ['$http', function ($http) {
    var ClaimService = {};
    ClaimService.GetClaim = function () {
        return $http.get('/Account/GetClaim');
    }
    
    return ClaimService;
}]);