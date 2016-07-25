app.factory("ModeratorService", ['$http', function ($http) {
    var ModeratorService = {};
    ModeratorService.GetModeatorsByRestaurantId = function (id) {
        return $http.get("/Admin/GetModeratorsByRestaurantId", { params: { profileId: id } });
    };

    ModeratorService.AddModerator = function (moderator_, profileId_) {
        return $http.post("/Admin/AddModerator", { moderator: moderator_, profileId: profileId_ });
    };

    ModeratorService.SetModerator = function (email, profileId_) {
        return $http.post("/Admin/SetModerator", { email: email, profileId: profileId_});
    }

    ModeratorService.DeleteModerator = function (moderatorId_) {
        return $http.post("/Admin/DeleteModerator", { moderId: moderatorId_ });
    };    

    return ModeratorService;
}]);