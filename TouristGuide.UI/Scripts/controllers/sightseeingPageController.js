app.controller('SightseeingPageController', ['$scope', '$location', '$routeParams',
    '$cookies', 'SightseeingService', 'ClaimService',
    '$mdDialog', '$mdMedia', 'EventService', 'CommonContentService',
function ($scope, $location, $routeParams, $cookies, SightseeingService,
    ClaimService, $mdDialog, $mdMedia, EventService, CommonContentService) {
    $scope.sightseeingId = $routeParams.profileId;

    $scope.sightseeing = {};
    SightseeingService.GetSightseeingById($scope.sightseeingId).then(function (response) {
        $scope.sightseeing = response.data;
        if ($scope.sightseeing.IsShowed == true) {
            $scope.isShowedText = "Profile is available for users";
        }
        else {
            $scope.isShowedText = "Profile isnt available for users";
        }
    });
    $scope.isAvailable = function () {
        if ($scope.sightseeing.IsShowed) {
            $scope.isShowedText = "Profile isnt available for users";
        }
        else {
            $scope.isShowedText = "Profile is available for users";
        }
        CommonContentService.EditIsShowed($scope.sightseeing.IsShowed, $scope.sightseeingId);
    }
    $scope.currentUser = { Role: '', Name: '',Id:'' };
    $scope.IsInFavorites = false;
    ClaimService.GetClaim().then(function (response) {
        $scope.currentUser = response.data;
        EventService.GetEventsSubscriptionByProfileId($scope.sightseeingId, $scope.currentUser.Id).then(function (response) {
            $scope.userEvents = response.data;
        })
    })
    SightseeingService.IsInFavorites($scope.sightseeingId).then(function (response) {
        $scope.IsInFavorites = response.data;
        if ($scope.IsInFavorites == true) {
            var elem = document.getElementById("page_buttons");
            elem.innerHTML = '<span class="glyphicon glyphicon-heart" aria-hidden="true"></span> Already in favorites';
        }
        else {
            var elem = document.getElementById("page_buttons");
            elem.innerHTML = '<span class="glyphicon 	glyphicon glyphicon-heart-empty" aria-hidden="true"></span> Add to favorite';
        }
    });
    $scope.modalShown = false;
    $scope.toggleModal = function () {
        $scope.modalShown = !$scope.modalShown;
    };

    $scope.status = '  ';
    $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');


    $scope.userEvents = [];

    $scope.$on('userEventList', function (event, model) {
        $scope.userEvents.push(model);
    })
    $scope.$on('userDeleteEventList', function (event, eventId) {
        for (i = 0; i < $scope.userEvents.length; i++)
            if ($scope.userEvents[i].EventId == eventId) {
                delete $scope.userEvents[i];
                $scope.userEvents.length--;
            }

    })


    $scope.showConfirm = function (dialog, title, message, callBack, callBackParamenter) {
        dialog.show({
            locals: { Title: title, Message: message },
            controller: ['$scope', '$mdDialog', 'Title', 'Message', function ($scope, $mdDialog, title, message) {

                $scope.Title = title;
                $scope.Message = message;
                $scope.url = encodeURIComponent(window.location.href);

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function (answer) {
                    $mdDialog.hide(answer);
                };

            }],
            templateUrl: 'app/views/restaurant/common-items/confirmRegister.html',
            parent: angular.element(document.body),
            targetEvent: null,
            clickOutsideToClose: true
        }).then(function (answer) {
            callBack(answer, callBackParamenter);
        })



    };

    $scope.showUserEvents = function () {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            controller: ['$scope', '$mdDialog', 'EventService', 'CommonContentService', function ($scope, $mdDialog, EventService, CommonContentService) {

                $scope.userEventsList = [];
                EventService.GetEventsBySightseeingId($scope.sightseeingId).then(function (response) {
                    $scope.events = response.data;
                    for (i = 0 ; i < $scope.events.length; i++) {
                        for (j = 0; j < $scope.userEvents.length; j++)
                            if ($scope.events[i].EventId == $scope.userEvents[j].EventId)
                                $scope.userEventsList.push($scope.userEvents[j]);
                    }
                })

                $scope.deleteSubscription = function (ev, subscriptionId) {
                    //$scope.showConfirm(ev, menu);
                    CommonContentService.showConfirm($mdDialog, " Please, confirm deleting", "Do you really want to delete subscription?", $scope.delete, subscriptionId);

                };

                $scope.delete = function (isConfirmed, subscriptionId) {

                    if (isConfirmed) {
                        EventService.DeleteEventSubscription(subscriptionId).then(function () {
                            EventService.GetEventsSubscriptionByProfileId($scope.sightseeingId, $scope.currentUser.Id).then(function (response) {
                                $scope.userEvents = response.data;
                            })
                        });
                    }

                }

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function (answer) {
                    $mdDialog.hide(answer);
                };

            }],
            templateUrl: 'app/views/restaurant/sightseeing-items/userEventSubscriptionsDialog.html',
            parent: angular.element(document.body),
            targetEvent: null,
            clickOutsideToClose: true
        }).then(function (answer) {

        })



    };
    $scope.goToRegister = function () { window.location.assign('/Account/Register'); }


 

    $scope.userEventsCheck = function () {
        if ($scope.userEvents.length > 0)
            return true;
        else
            return false;
    }

    $scope.addToFavorits = function () {
        if ($scope.currentUser.Role != 'User') {
            $scope.showConfirm($mdDialog, "Sign in or register a new account", "This action can be performed only as registered user", $scope.goToRegister, null);
        }
        if ($scope.currentUser.Role == 'User') {
            var elem = document.getElementById("page_buttons");
            var val = elem.innerHTML;
            if (elem.innerHTML == '<span class="glyphicon 	glyphicon glyphicon-heart-empty" aria-hidden="true"></span> Add to favorite') {
                SightseeingService.AddFavorite($scope.sightseeingId).then(function () {
                    elem.innerHTML = '<span class="glyphicon glyphicon-heart" aria-hidden="true"></span> Already in favorites';
                })
            }
            else {
                SightseeingService.RemoveFavorite($scope.sightseeingId).then(function () {
                    elem.innerHTML = '<span class="glyphicon 	glyphicon glyphicon-heart-empty" aria-hidden="true"></span> Add to favorite';
                })
            }
        }
    }

    $scope.checkedModerator = function () {

        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Moderator') {
                if ($scope.sightseeing.Moderators != null) {
                    for (index = 0; index < $scope.sightseeing.Moderators.length; ++index) { 
                        if ($scope.sightseeing.Moderators[index] == $scope.currentUser.Id) {
                            return false;
                        }
                    }
                }
                return true;
            }
            else {
                return true;
            }

        }
    }

    $scope.checkedAdmin = function () {
        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Admin')
                return false;
            else
                return true;
        }
    }

    $scope.checkedComments = function () {
        if ($scope.currentUser.Role != null) {
            if ($scope.currentUser.Role == 'Guest')
                return true;
            else
                return false;
        }
    }
    $scope.isCollapsed = true;



}])