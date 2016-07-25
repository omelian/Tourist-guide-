app.controller('PhotoPageController', function ($scope, $routeParams, CommonContentService, $timeout, $mdDialog) {
    $scope.restaurantId = $routeParams.profileId;
    $scope.noWrapSlides = false;
    $scope.active = 0;
    $scope.myInterval = 5000;
    $scope.slide = [];
    $scope.loadSlides = [];
    $scope.ProgressContainer = [];
    $scope.loaded = false;
    $scope.slides = [];
    $scope.MarkedItems = [];
    $scope.loaderActivated = false;
    $scope.canConfirmDeleting = false;
    $scope.photosUploading = false;
    $scope.ProgressId = 0;
    $scope.ProgressValue = 0;
    $scope.ProgressName = "";
    $scope.Message = "";
    var responseBlock = document.getElementById("serverResponse");
    var currIndex = 0;

    CommonContentService.getPhotosById($scope.restaurantId).then(function (response) {
        $scope.loadSlides = response.data;
    }).then(function () {
        $scope.slides = $scope.loadSlides;
            if ($scope.loadSlides.length>0) {
                $scope.loaderActivated = true;
                currIndex = 0;
                for (i = 0; i < $scope.slides.length; ++i) {
                    $scope.slides[i].InnerId = currIndex;

                    var start = $scope.loadSlides[i].Url.indexOf(".com/");
                    var subEnd = $scope.loadSlides[i].Url.substring(start);
                    var end = subEnd.indexOf("?");
                    start =5;
                    var name = subEnd.substring(start, end);

                    $scope.slides[i].Name = name;
                    if ($scope.loadSlides[i].isDeleted != true) {
                        $scope.slides[i].isDeleted = false;
                    }

                    currIndex++;
                }
                if (!$scope.$$phase) {
                    $scope.$apply();
                }

                $timeout(function () {
                    $scope.loaded = true;
                    $scope.loaderActivated = false;

                }, 2000);

            }
            else {
                $scope.loaded = true;
            }

    });



    $scope.onDeletingConfirmed = function (isConfirmed, param) {
        if (isConfirmed) {
            $scope.saveDeleted();
            $scope.MarkedItems = [];
            $scope.canConfirmDeleting = false;
        }
    }


    $scope.ConfirmSaveDeleted = function ()
    {
        CommonContentService.showConfirm($mdDialog, " Please, confirm deleting", "Do you really want to delete this photos?", $scope.onDeletingConfirmed, null);
    }
  


    $scope.saveDeleted = function () {


        if ($scope.loaderActivated === false) {

            var refresh = false;


            var lenght = $scope.slides.length;
            for (i = 0; i < $scope.slides.length; ++i) {
                if ($scope.slides[i].isDeleted === true) {

                    var element = document.getElementById($scope.slides[i].Id);
                    element.style.color = "white";

                    CommonContentService.deletePhotoById($scope.slides[i].Id);
                    refresh = true;
                }

            }


            if (refresh === true) {

                $scope.loadSlides = $scope.slides;

                $scope.slides = [];
                currIndex = 0;
                $scope.active = currIndex;

                for (i = 0; i < $scope.loadSlides.length; ++i) {

                    if ($scope.loadSlides[i].isDeleted === false) {

                        $scope.slides.push({
                            Id: $scope.loadSlides[i].Id,
                            Url: $scope.loadSlides[i].Url,
                            Name:  $scope.loadSlides[i].Name,
                            isDeleted: false,
                            Descripton: $scope.loadSlides[i].Descripton
                        });
                        $scope.slides[$scope.slides.length - 1].InnerId = currIndex;



                        currIndex++;
                    }

                }
                if (!$scope.$$phase) {
                    $scope.$apply();
                }

            }

        }


    }

    $scope.setAsMainPhoto = function () {
        var id = -1;
        for (i = 0; i < $scope.slides.length; ++i) {
            if ($scope.slides[i].InnerId == $scope.active) {
                id = $scope.slides[i].Id;
                break;
            } 
        }

       CommonContentService.setMainPhotoOfProfile(id, $scope.restaurantId).then(function (response) {
           showMessage(response);
        });
        
    }

    var showMessage = function(response){
        $scope.Message = response.data;
        if (response.status == 200)
            responseBlock.className = "bg-success photo-server-message";
        else
            responseBlock.className = "bg-danger photo-server-message";

        $timeout(function () {
            $scope.Message = "";
            responseBlock.className = "photo-server-message";
        }, 4000);
    }

    $scope.markAsDeleted = function (photo) {
        if ($scope.photosUploading === false) {
            var element = null;
            var index = $scope.slides.indexOf(photo);
            var id = -1;
            if (index < 0) {
                id = photo;
                element = document.getElementById(photo);
                for (i = 0; i < $scope.slides.length; ++i)
                    if ($scope.slides[i].Id === photo)
                        index = i;
            }
            else {
                id = photo.Id;
                element = document.getElementById(photo.Id);
            }

            if ($scope.slides[index].isDeleted === false) {
                element.style.color = "red";
                $scope.slides[index].isDeleted = true;
                $scope.MarkedItems.push(
                    {
                        Id: photo.Id,
                        Name: photo.Name
                    });
                $scope.canConfirmDeleting = true;
            }
            else {

                element.style.color = "white";
                $scope.slides[index].isDeleted = false;



                //  var ind = $scope.MarkedItems.indexOf(item);
                for (i = 0; i < $scope.MarkedItems.length; ++i) {
                    if ($scope.MarkedItems[i].Id === id) {
                        $scope.MarkedItems.splice(i, 1);
                    }
                }
                if ($scope.MarkedItems.length === 0)
                    $scope.canConfirmDeleting = false;


            }

        }
        else
        {
            alert("Please, wait while photos are uploading to the server...");
        }
    }

    var dropZone = document.getElementById("dropFileInput");
    dropZone.ondrop = function (e) {
        e.preventDefault();
        $scope.addSlide(e.dataTransfer.files);
    }

    $scope.addSlide = function (dropFiles) {
        if ($scope.MarkedItems.length === 0) {


            var files = [];
            var fileInput = document.getElementById("fileInput");
            if (dropFiles === undefined) {
                files = fileInput.files;
            }
            else {
                files = dropFiles;
            }

            var httpRequest = [];

            var element = document.getElementById("description");
            var description = element.value;

            for (i = 0; i < files.length; ++i) {
                (
                function (i) {

                    var Extension = files[i].name.substring(files[i].name.lastIndexOf('.') + 1).toLowerCase();

                    if (Extension == "gif" || Extension == "png" || Extension == "bmp"
                                || Extension == "jpeg" || Extension == "jpg") {

                        if (files[i].size < 10585760) {

                            var formData = new FormData();
                            httpRequest[i] = new XMLHttpRequest();
                            $scope.slides.push({
                                Id: currIndex,
                                Url: URL.createObjectURL(files[i]),
                                isDeleted: false,
                                Descripton: description,
                                Name: files[i].name
                            });

                            $scope.ProgressName = files[i].name;
                            $scope.ProgressContainer.push(
                                {
                                    Id: $scope.ProgressId,
                                    ProgressValue: 0,
                                    ProgressName: files[i].name
                                });
                            $scope.photosUploading = true;
                            var index = angular.copy($scope.ProgressId);


                            formData.append("uploadedFile", files[i]);
                            formData.append("descripton", description);
                            formData.append("profileId", $scope.restaurantId);


                            httpRequest[i].upload.addEventListener("progress", function (evt) {

                                $scope.uploadProgress(evt, index);
                            }, false);

                            httpRequest[i].addEventListener("load", function (evt) {
                                $scope.uploadComplete(evt, index);
                            }, false);


                            httpRequest[i].addEventListener("error", $scope.uploadFailed, false);
                            httpRequest[i].addEventListener("abort", $scope.uploadCanceled, false);
                            httpRequest[i].open("POST", "/ListOfRestaurant/AddPhotoToProfile", true);


                            httpRequest[i].send(formData);

                            $scope.ProgressId++;


                        }
                    }
                })(i);
            };
            fileInput.value = null;
            currIndex = 0;
            $scope.active = currIndex;
            for (i = 0; i < $scope.slides.length; ++i) {
                $scope.slides[i].InnerId = currIndex;
                currIndex++;
            }
        }
        else
        {
            alert("Please, сonfirm or cancel photos deleting before uploading");
        }
    };


    $scope.uploadProgress = function ( event,id) {
        $scope.$apply(function () {
            if (event.lengthComputable ) {
                if ($scope.ProgressContainer[id]!= undefined)
                $scope.ProgressContainer[id].ProgressValue = Math.round(event.loaded * 100 / event.total);
            }

        });
    }



    $scope.uploadFailed = function (event, id) {
        alert("Upload failed!");
    }

    $scope.uploadCanceled = function (event, id) {
        $scope.$apply(function () {

        });
    }




    $scope.uploadComplete = function (event, id) {
        for (i = 0; i < $scope.loadSlides.length; ++i) {
            if ($scope.ProgressContainer[i] != undefined)
            {
                $scope.ProgressContainer.splice(i, 1);
            } 
        }

        $scope.ProgressId--;


        if ($scope.ProgressId === 0)
        {
             CommonContentService.getPhotosById($scope.restaurantId).then(function (response) {
            $scope.loadSlides = response.data;
        }).then(function () {



            currIndex = 0;
            $scope.slides = $scope.loadSlides;
            for (i = 0; i < $scope.slides.length; ++i) {
                $scope.slides[i].Id = $scope.loadSlides[i].Id;
                $scope.slides[i].InnerId = currIndex;

                var start = $scope.loadSlides[i].Url.indexOf(".com/");
                var subEnd = $scope.loadSlides[i].Url.substring(start);
                var end = subEnd.indexOf("?");
                start = 5;
                var name = subEnd.substring(start, end);
                $scope.slides[i].Name = name;

                if ($scope.slides[i].isDeleted != true) {
                    $scope.slides[i].isDeleted = false;
        }
                currIndex++;
        }

  
        });
             $scope.photosUploading = false;
        }

       
        
    }



    $scope.ShowEditPhotosDialog = function (ev) {
        if ($scope.photosUploading === false) {

        $mdDialog.show({
            locals: { slides: $scope.slides },
            controller: ['$scope', '$mdDialog', 'slides', function ($scope, $mdDialog, slides) {

                var newArray = angular.copy(slides);
                $scope.myslides = newArray;
                var allDeleted = true;



                $scope.mark = function (slide) {

                    if (slide.isDeleted === true) {
                        slide.isDeleted = false;
                        slide.Opacity = 1;
                    }
                    else {
                        slide.isDeleted = true;
                        slide.Opacity = 0.3;
                    }

                };

                $scope.selectAll = function () {              
                    for (i = 0; i < $scope.myslides.length; i++) {
                        $scope.myslides[i].isDeleted = allDeleted;

                        if ($scope.myslides[i].isDeleted === true) {
                            $scope.myslides[i].Opacity = 0.3;
                        }
                        else {
                            $scope.myslides[i].Opacity = 1;
                        }
                    }
                    allDeleted = !allDeleted;
                };

                $scope.hide = function () {
                    $mdDialog.hide();
                };
                $scope.cancel = function () {
                    $mdDialog.cancel();
                };
                $scope.answer = function () {
                    $mdDialog.hide($scope.myslides);
                };
            }],
            templateUrl: 'app/views/restaurant/common-items/editPhotosDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function (answer) {

            $scope.slides = answer;
            $scope.saveDeleted();
           })

        }
        else
        {
                    alert("Please, wait while photos are uploading to the server...");
        }

    };


});