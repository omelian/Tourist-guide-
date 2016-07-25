app.controller('NewsController', function ($scope, $routeParams, CommonContentService, $http, $mdDialog) {
    $scope.restaurantId = $routeParams.profileId;
    CommonContentService.GetNewsFromRestaurantId($scope.restaurantId).then(function (response) {
        $scope.restaurantNews = response.data;
    })


    $scope.CheckHeight = function (id) {
        var element = document.getElementById(id);
        var hint = document.getElementById(id + "hint");
        
        if (element.clientHeight === 180)
        {
            hint.innerHTML = "Show less...";
            element.style.height = 'auto';
        }
        else
        {
            hint.innerHTML = "Show more...";
            element.style.height = '180px';
       
        }
           

    };

    $scope.CheckOverflowLengthOfContent = function (id) {
        var element = document.getElementById(id);
        var contentLength = element.scrollHeight;
       // alert(contentLength);
        if (contentLength > 190)
            return false;
        else
            return true;

    };


    $scope.sendNews = function (_news) {
        var currentDate = CommonContentService.getCurrentDate();
        CommonContentService.AddNewsToRestaurant(_news, $scope.restaurantId, currentDate).then(function () {
            CommonContentService.GetNewsFromRestaurantId($scope.restaurantId).then(function (response) {
                $scope.restaurantNews = response.data;
            })
        })
    };

    $scope.onDeletingConfirmed = function (isConfirmed,_news) {
        if(isConfirmed)
        { 
            var index = $scope.restaurantNews.indexOf(_news);
            CommonContentService.DeleteNewsFromRestaurant(_news.NewsId, $scope.restaurantId).then(function () {
                $scope.restaurantNews.splice(index, 1);
            })
        }
    }

    
    $scope.deleteNews = function (_news) {
        CommonContentService.showConfirm($mdDialog, "Please, confirm deleting", "Do you really want to delete this item?", $scope.onDeletingConfirmed, _news);
    };


    $scope.SendEditNews = function (_news) {
        var currentDate = CommonContentService.getCurrentDate();
        CommonContentService.UpdateRestaurantNews(_news, $scope.restaurantId, currentDate);
        _news.DateTime = currentDate;
        for (i = 0; i < $scope.restaurantNews.length; i++) {
            if ($scope.restaurantNews[i].NewsId === _news.NewsId)
            {
                $scope.restaurantNews[i] = _news;
                break;
            }
        }
      
    };

    $scope.showNewsEditor = function (ev, news) {
        $mdDialog.show({
            locals: { EditNews: news },
            controller: ['$scope', '$mdDialog', 'EditNews', function ($scope, $mdDialog, EditNews) {
                $scope.editNews = angular.copy(EditNews);
                $scope.TitleCorrect = true;
                $scope.ContentCorrect = true;
                $scope.TitleStyle = "";
                $scope.TitleError = "Please, enter title";
                $scope.ContentStyle = "";
                $scope.ContentError = "Please, enter content";


                $scope.CheckContent = function (val) {
                    if (val != undefined) {
                        if (val.length > 20 && val.length < 2000) {
                            $scope.ContentError = "Please, enter content";
                            $scope.ContentStyle = "";
                            $scope.ContentCorrect = true;
                        }
                        else {
                            $scope.ContentError = "Content length error(content length must be between 20 and 2000 characters)";
                            $scope.ContentStyle = "has-error";
                            $scope.ContentCorrect = false;
                        }
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
                                $scope.editNews.NewsImageUrl = url;
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



                $scope.CheckTitle = function (val) {
                    if (val != undefined) {
                        if (val.length > 5 && val.length < 50) {
                            $scope.TitleError = "Please, enter title";
                            $scope.TitleStyle = "";
                            $scope.TitleCorrect = true;
                        }
                        else {
                            $scope.TitleError = "Title length error(title length must be between 5 and 50 characters)";
                            $scope.TitleStyle = "has-error";
                            $scope.TitleCorrect = false;
                        }
                    }
                };



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
            templateUrl: 'app/views/restaurant/common-items/newsDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function (answer) {
            $scope.News = answer;
            $scope.SendEditNews(answer);
        })
    };



 


    $scope.showNewsAdd = function (ev) {
        $mdDialog.show({
            controller: ['$scope', '$mdDialog', function ($scope, $mdDialog) {

        
                $scope.editNews = {
                    NewsId: '',
                    TextContent: '',
                    NewsImageUrl: ' ',
                    Title: ''
                };

                $scope.TitleCorrect = false;
                $scope.ContentCorrect = false;
                $scope.TitleStyle = "";
                $scope.TitleError = "Please, enter title";
                $scope.ContentStyle = "";
                $scope.ContentError = "Please, enter content";



                $scope.CheckContent = function (val) {
                     if(val != undefined){
                    if (val.length > 20 && val.length < 2000) {
                        $scope.ContentError = "Please, enter content";
                        $scope.ContentStyle = "";
                        $scope.ContentCorrect = true;
                    }
                    else {
                        $scope.ContentError = "Content length error(content length must be between 20 and 2000 characters)";
                        $scope.ContentStyle = "has-error";
                        $scope.ContentCorrect = false;
                    }
                  }
                };





                $scope.ValidateForm = function () {
                   
                    if ($scope.TitleCorrect && $scope.ContentCorrect)
                        return true;
                    else
                        return false;

                };


                $scope.CheckTitle = function (val) {
                    if (val != undefined) {
                        if (val.length > 5 && val.length < 50) {
                            $scope.TitleError = "Please, enter title";
                            $scope.TitleStyle = "";
                            $scope.TitleCorrect = true;
                        }
                        else {
                            $scope.TitleError = "Title length error(title length must be between 5 and 50 characters)";
                            $scope.TitleStyle = "has-error";
                            $scope.TitleCorrect = false;
                        }
                    }
                       
                };



                $scope.onDrop = function (ev) {

                    if (ev.files.length != 0)
                    {
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
                                $scope.editNews.NewsImageUrl = url;
                            }
                            else
                            {
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
            templateUrl: 'app/views/restaurant/common-items/newsAddDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function (answer) {
            $scope.sendNews(answer);
        })
    };
});



