app.factory("EventService", ['$http', function ($http) {
    var EventService = {};
    EventService.GetEventsBySightseeingId = function (id) {
        return $http.get('/Event/GetEventBySightseeingId', { params: { sightseeingId: id } });
    };
    EventService.AddEvent = function (profileId, name, description,dateString, price, duration, numberOfParticipant, photoUrl)
    {
        return $http.post('/Event/AddEvent', {
            profileId: profileId, name: name, description: description,dateString:dateString, price: price, duration: duration,
            numberOfParticipant: numberOfParticipant,  photoUrl: photoUrl
        });
    }
    EventService.UpdateEvent = function (eventIdEdit, profileIdEdit, nameEdit, descriptionEdit, dateStringEdit, priceEdit, durationEdit, numberOfParticipantEdit, photoUrlEdit) {
        return $http.post('/Event/UpdateEvent', {
            eventId: eventIdEdit, profileId: profileIdEdit, name: nameEdit, description: descriptionEdit, dateString: dateStringEdit, price: priceEdit, duration: durationEdit,
           numberOfParticipant: numberOfParticipantEdit, photoUrl: photoUrlEdit
        });
    }
    EventService.DeleteEvent = function (eventId) {
        return $http.post('/Event/DeleteEvent', { eventId: eventId });
    }
    EventService.GetEventsSubscriptionByProfileId = function (id, UserId) {
        return $http.get('/Event/GetEventSubscriptionByProfileId', { params: { profileId: id, userId: UserId } });
    };

    EventService.AddEventSubscription = function (numberOfPersons,id) {
        return $http.post('/Event/AddEventSubscription', { numberOfPersons: numberOfPersons, eventId: id });
    };
    EventService.GetEventSubscriptionByEventId = function (id, UserId) {
        return $http.get('/Event/GetEventSubscriptionByEventId', { params: { eventId: id, userId: UserId } });
    };
    EventService.DeleteEventSubscription = function (subscriptionId)
    {
        return $http.post('/Event/DeleteEventSubscription', { subscriptionId: subscriptionId });
    }

   
    return EventService;
}]);