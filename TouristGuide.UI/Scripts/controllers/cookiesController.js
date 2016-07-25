var favoriteCookie = "Guest";
app.controller('ExampleController', ['$cookies', '$scope', function ($cookies, $scope) {
    // Retrieving a cookie
    
    favoriteCookie = $cookies.get('User');
    $scope.lox = favoriteCookie;
    // Setting a cookie
    //$cookies.put('myFavorite', 'oatmeal');
}]);