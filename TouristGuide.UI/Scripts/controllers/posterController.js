app.controller("PosterController", function ($scope, $mdDialog, $mdMedia, $routeParams, filterService, EventService, CommonContentService, ClaimService) {
    $scope.sightseeingId = $routeParams.profileId;
    $scope.events = [];
    EventService.GetEventsBySightseeingId($scope.sightseeingId).then(function (response) {
        $scope.events = response.data;
    })
    $scope.currentUser = {};
    ClaimService.GetClaim().then(function (response) {
        $scope.currentUser = response.data;
    })
    $scope.modalShown = false;
    $scope.customFullscreen = $mdMedia('xs') || $mdMedia('sm');

    $scope.hide = function () {
        $mdDialog.hide();
    };
    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };
   
    $scope.showModel = '';
    
    $scope.showEventDialog = function (ev,event) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            locals: { event: event },
            controller: ['$scope', 'event', '$rootScope', function (ev, event, $rootScope,eventController) {
                $scope.event = event;
                $scope.eventSubscription = null;
                if ($scope.currentUser.Role == 'User') {
                    EventService.GetEventSubscriptionByEventId($scope.event.EventId,$scope.currentUser.Id).then(function (response) {
                        $scope.eventSubscription = response.data;
                    })
                }
                $scope.checkedUser = function () {
                    if ($scope.currentUser.Role != null) {
                        if ($scope.currentUser.Role == 'User')
                            return true;
                        else
                            return false;
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


                $scope.showAddUserEventItemDialog = function (ev) {
                    $mdDialog.show({
                        scope: $scope,
                        preserveScope: true,
                        controller: ['$scope', '$rootScope','EventService' , 'CommonContentService', function (ev, $rootScope,EventService,CommonContentService) {
                           
                            $scope.numOfPersons = null;
                            $scope.addUserEvent = function ()
                            {
                                EventService.AddEventSubscription($scope.numOfPersons, $scope.event.EventId).then(function (response) {
                                    EventService.GetEventSubscriptionByEventId($scope.event.EventId, $scope.currentUser.Id).then(function (response)
                                    { $rootScope.$broadcast('userEventList', response.data); })
                                   
                                });

                                
                                $scope.cancel();
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
                        templateUrl: 'app/views/restaurant/sightseeing-items/addUserEventItemDialog.html',
                        parent: angular.element(document.body),
                        targetEvent: ev,
                        clickOutsideToClose: true
                    }).then(function () {
                        
                    }).catch(function () {
                       
                    });;
                };
                $scope.deleteEventSubscriptionItem = function (ev) {
                    CommonContentService.showConfirm($mdDialog, " Please, confirm deleting", "Do you really want to delete this subscription?", $scope.deleteSubscription);

                };

                $scope.deleteSubscription = function (isConfirmed) {
        
                    if (isConfirmed) {
                        EventService.DeleteEventSubscription($scope.eventSubscription.SubscriptionId).then(function () {
                            EventService.GetEventSubscriptionByEventId($scope.event.EventId,$scope.currentUser.Id).then(function (response) {
                                $scope.eventSubscription = response.data;
                               
                                $rootScope.$broadcast('userDeleteEventList', $scope.event.EventId); })

                            })

                        };
                    }
                $scope.glyphicon = "";
                $scope.tooltip = "";
                $scope.checkEventSubscription = function () {
                    if (($scope.eventSubscription == "" || $scope.eventSubscription == null) || $scope.eventSubscription.UserId != $scope.currentUser.Id) {
                        $scope.glyphicon = "glyphicon glyphicon-log-in pull-right reserve-icon";
                        $scope.tooltip = "Click to subscribe on event";
                    }
                    else
                    {
                        $scope.glyphicon = "glyphicon glyphicon-log-out pull-right reserve-icon";
                        $scope.tooltip = "Click to delete your subscription";
                    }
                }
                $scope.eventSubscribe = function()
                {
                    if (($scope.eventSubscription == "" || $scope.eventSubscription == null ) || $scope.eventSubscription.UserId!= $scope.currentUser.Id ) {
                        
                        $scope.showAddUserEventItemDialog();
                       
                    }
                    else
                        $scope.deleteEventSubscriptionItem();
                }
            }],
                templateUrl: 'app/views/restaurant/sightseeing-items/eventDialog.html',
                parent: angular.element(document.body),
                targetEvent: ev,
                clickOutsideToClose: true
            })
    };

            
    
    $scope.showAddEventItemDialog = function (ev) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            controller: 'AddEventDialogController',
            templateUrl: 'app/views/restaurant/sightseeing-items/addEventItemDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function () {
            EventService.GetEventsBySightseeingId($scope.sightseeingId).then(function (response) {
                $scope.events = response.data;
            })

        }).catch(function () {
            EventService.GetEventsBySightseeingId($scope.sightseeingId).then(function (response) {
                $scope.events = response.data;
            })

        });;
    };

    $scope.showEditEventItemDialog = function (ev,event) {
        $mdDialog.show({
            scope: $scope,
            preserveScope: true,
            locals: { event: event },
            controller: 'EditEventDialogController',
            templateUrl: 'app/views/restaurant/sightseeing-items/editEventItemDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function () {
            EventService.GetEventsBySightseeingId($scope.sightseeingId).then(function (response) {
                $scope.events = response.data;
            })

        }).catch(function () {
            EventService.GetEventsBySightseeingId($scope.sightseeingId).then(function (response) {
                $scope.events = response.data;
            })

        });;
    };

    $scope.editEventDialog = function (ev,event)
    {
        $scope.showEditEventItemDialog(ev, event);
    }
             
    $scope.deleteEventItem = function (ev, event) {
        //$scope.showConfirm(ev, menu);
        CommonContentService.showConfirm($mdDialog, " Please, confirm deleting", "Do you really want to delete this item?", $scope.delete, event);

    };

    $scope.delete = function (isConfirmed,event) {
        
        if (isConfirmed) {
            EventService.DeleteEvent(event.EventId).then(function () {
                EventService.GetEventsBySightseeingId($scope.sightseeingId).then(function (response) {
                    $scope.events = response.data;
                })
            });
        }
        
    }

   
   





})
