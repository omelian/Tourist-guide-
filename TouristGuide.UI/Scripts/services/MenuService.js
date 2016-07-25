app.factory("MenuService", ['$http', function ($http) {
    var MenuService = {};
    MenuService.GetMenu = function () {
        return $http.get('/Menu/TestGetMenu');
    };
    MenuService.GetMenuById = function (id) {
        return $http.get("/Menu/GetMenu", { params: { restaurantId: id } });
    };
     MenuService.DeleteMenuItem = function (menuId, profileId) {
        return $http.post("/AddMenuItem/DeleteMenu", { menuId: menuId, restaurantId: profileId });
     };
     MenuService.UpdateMenu = function (editMenu, restaurantId) {
         return $http.post("/AddMenuItem/UpdateMenu", { name: editMenu.Name, price: editMenu.Price, pictureUrl: editMenu.PictureUrl, description: editMenu.Description, calories: editMenu.Callories, preparationTime: editMenu.PreparationTime, id: restaurantId, dishType: editMenu.DishType,menuId: editMenu.MenuId })
     };
    return MenuService;
}]);