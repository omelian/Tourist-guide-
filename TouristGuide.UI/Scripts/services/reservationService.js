app.factory("ReservationService", ['$http', function ($http) {
    var ReservationService = {};
    ReservationService.AddReservation = function (NumberOfPersons,DateString,restId) {
        return $http.post('/Reservation/AddReservation',  { numberOfPersons: NumberOfPersons,dateString: DateString, restaurantId: restId } );
    }

    ReservationService.GetReservationsByUserName = function (userName,restId) {
        return $http.get('/Reservation/GetReservationByUserName', { params: { userName: userName, restaurantId: restId } });
    }
   
    ReservationService.UpdateReservation = function (NumberOfPersons, DateString, reservId) {
        return $http.post('/Reservation/EditReservation', { numberOfPersons: NumberOfPersons, dateString: DateString, reservationId: reservId });
    }

    ReservationService.DeleteReservation = function (reservId) {
        return $http.post('/Reservation/DeleteReservation', {  reservationId: reservId });
    }
    ReservationService.GetAllReservationsByUserName = function (userName) {
        return $http.get('/Reservation/GetAllReservationsByUserName', { params: { userName: userName } });
    }

    ReservationService.UpdateRestaurantReservationMenu = function (reservationId_, menu_) {
        return $http.post('/Reservation/UpdateRestaurantReservationMenu', { reservationId: reservationId_, menu: menu_ });
    }
    return ReservationService;
}]);