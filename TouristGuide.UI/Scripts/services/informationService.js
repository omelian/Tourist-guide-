app.factory("InformationService", ['$http', function ($http) {
    var InformationService = {};
    
    InformationService.GetInformationByAttractionId = function (sightseeingId) {
        return $http.get("/SightseeingInformation/GetInformationBySightseeingId", { params: { sightseeingId: sightseeingId } });
    }

    InformationService.AddInformation = function (information, sightseeingId) {
        return $http.post('/SightseeingInformation/AddInformation', {article: information, sightseeingId: sightseeingId});
    }

    InformationService.EditInformation = function (information) {
        return $http.post('/SightseeingInformation/EditInformation', { article : information });
    }

    return InformationService;
}]);