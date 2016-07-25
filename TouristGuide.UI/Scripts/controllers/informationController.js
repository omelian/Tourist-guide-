app.controller('InformationController', function ($scope, InformationService, $routeParams,  $http, $mdDialog) {
    $scope.attractionId = $routeParams.profileId;
    $scope.article = {};
    InformationService.GetInformationByAttractionId($scope.attractionId).then(function (response) {
        $scope.article = response.data[0];
    })

    $scope.sendInformation = function (information) {
        InformationService.GetInformationByAttractionId($scope.attractionId).then(function (response) {
            $scope.article = response.data[0];
            if ($scope.article == undefined) {
                InformationService.AddInformation(information, $scope.attractionId);
            }
            else {
                InformationService.EditInformation(information);
            }
            $scope.article = information;
        })
    };

    $scope.sendEditInformation = function (information) {
        InformationService.EditInformation(information);
        $scope.article = information;
    };

    $scope.ShowInformationEditor = function (ev, information) {
        $mdDialog.show({
            locals: { EditInformation: information },
            controller: ['$scope', '$mdDialog', 'EditInformation', function ($scope, $mdDialog, EditInformation) {
                $scope.editInformation = angular.copy(EditInformation);
                $scope.TitleCorrect = true;
                $scope.ContentCorrect = true;
                $scope.TitleStyle = "";
                $scope.TitleError = "Please, enter title";
                $scope.ContentStyle = "";
                $scope.ContentError = "Please, enter content";

                $scope.CheckContent = function (val) {
                    if (val != undefined) {
                        if (val.length > 20 && val.length < 10000) {
                            $scope.ContentError = "Please, enter content";
                            $scope.ContentStyle = "";
                            $scope.ContentCorrect = true;
                        }
                        else {
                            $scope.ContentError = "Content length error(content length must be between 20 and 10000 characters)";
                            $scope.ContentStyle = "has-error";
                            $scope.ContentCorrect = false;
                        }
                    }
                    else
                    {
                        $scope.ContentError = "Content length error(content length must be between 20 and 10000 characters)";
                        $scope.ContentStyle = "has-error";
                        $scope.ContentCorrect = false;
                    }
                };

                $scope.CheckTitle = function (val) {
                    if (val != undefined) {
                        if (val.length > 5 && val.length < 100) {
                            $scope.TitleError = "Please, enter title";
                            $scope.TitleStyle = "";
                            $scope.TitleCorrect = true;
                        }
                        else {
                            $scope.TitleError = "Title length error(title length must be between 5 and 100 characters)";
                            $scope.TitleStyle = "has-error";
                            $scope.TitleCorrect = false;
                        }
                    }
                    else {
                        $scope.TitleError = "Title length error(title length must be between 5 and 100 characters)";
                        $scope.TitleStyle = "has-error";
                        $scope.TitleCorrect = false;
                    }

                };

                $scope.ValidateForm = function () {

                    if ($scope.TitleCorrect && $scope.ContentCorrect)
                        return true;
                    else
                        return false;
                }
                ;
                $scope.onDrop = function (ev) {

                    if (ev.files.length != 0) {
                        var Extension = ev.files[0].name.substring(ev.files[0].name.lastIndexOf('.') + 1).toLowerCase();

                        if (Extension == "gif" || Extension == "png" || Extension == "bmp"
                                    || Extension == "jpeg" || Extension == "jpg") {
                            if (ev.files[0].size < 10585760) {
                                var formData = new FormData();
                                formData.append("uploadedFile", ev.files[0]);
                                httpRequest = new XMLHttpRequest();
                                httpRequest.open("POST", "/ListOfRestaurant/UploadPhoto", false);
                                httpRequest.send(formData);
                                url = httpRequest.response;
                                $scope.editInformation.PictureUrl = url;
                            }
                            else {
                                alert("Photo size is to large!")
                            }
                        }
                        else {
                            alert("Photo has incorrect format!")
                        }
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
            templateUrl: 'app/views/restaurant/sightseeing-items/informationDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function (answer) {
            $scope.sendInformation(answer);
        })
    };
});