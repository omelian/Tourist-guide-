app.controller('CommentsController', function ($scope, $routeParams, CommonContentService, ClaimService, $http, $mdDialog) {
    $scope.restaurantId = $routeParams.profileId;

    $scope.currentUser = {};
    $scope.restaurantComments = [];
    $scope.userComments = [];
    $scope.profileComments = [];

    CommonContentService.GetCommentsByRestaurantId($scope.restaurantId).then(function (response) {
        $scope.restaurantComments = response.data;
    }).then(function () {
        ClaimService.GetClaim().then(function (response) {
            $scope.currentUser = response.data;
        })

    })

    CommonContentService.GetCommentsByUser().then(function (response) {
        $scope.userComments = response.data;
    });


    CommonContentService.GetCommentsByModerProfile().then(function (response) {
        $scope.profileComments = response.data;
    });


    $scope.CheckHeight = function (id) {
        var element = document.getElementById(id);
        var hint = document.getElementById(id + "hint");

        if (element.clientHeight === 80) {
            hint.innerHTML = "Show less...";
            element.style.height = 'auto';

        }
        else {
            hint.innerHTML = "Show more...";
            element.style.height = '80px';

        }


    };

    $scope.CheckOverflowLengthOfContent = function (id) {
        var element = document.getElementById(id);
        var contentLength = element.scrollHeight;
        if (contentLength > 90)
            return false;
        else
            return true;

    };







    $scope.AddComment = function (comment_) {
        var currentDate = CommonContentService.getCurrentDate();
        CommonContentService.addComment(comment_, $scope.restaurantId, currentDate).then(function () {
       
            CommonContentService.GetCommentsByRestaurantId($scope.restaurantId).then(function (response) {
                $scope.restaurantComments = response.data;
                var element = document.getElementById("commentTextArea");
                element.value = "";
            })
        })
    };

   

    $scope.timeOut = function (comment) {

        var today = new Date();
     
        var splitRes = comment.DateTime.split(" ");
        var myDate = splitRes[0].split("/");
        var commentMonth = myDate[1];
        var commentYear = myDate[2];
        var commentDays = myDate[0];
        var Time = splitRes[1].split(":");
        var commentHours = Time[0];
        var commentMinutes = Time[1];

        var commentDate = new Date(commentMonth + "-" + commentDays + "-" + commentYear + " " + commentHours + ":" + commentMinutes);
        var diffDate = (today - commentDate);
        var diffMins = Math.round((diffDate / 1000) / 60);

        if (diffMins < 30) {
            if ($scope.currentUser.Id == comment.UserId) {
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }
    };


    $scope.timeOutInManagePage = function (comment) {

        var today = new Date();

        var splitRes = comment.CommentDateTime.split(" ");
        var myDate = splitRes[0].split("/");
        var commentMonth = myDate[1];
        var commentYear = myDate[2];
        var commentDays = myDate[0];
        var Time = splitRes[1].split(":");
        var commentHours = Time[0];
        var commentMinutes = Time[1];

        var commentDate = new Date(commentMonth + "-" + commentDays + "-" + commentYear + " " + commentHours + ":" + commentMinutes);
        var diffDate = (today - commentDate);
        var diffMins = Math.round((diffDate / 1000) / 60);

        if (diffMins < 30)
        {
          return true;        
        }
        else {
          return false;
        }
    };



    $scope.editComment = function (ev, comment) {


        $mdDialog.show({
            locals: { EditComment: comment },
            controller: ['$scope', '$mdDialog', 'EditComment', function ($scope, $mdDialog, EditComment) {
                $scope.commentToEdit = angular.copy(EditComment);
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
            templateUrl: 'app/views/restaurant/common-items/editCommentDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function (answer) {
            var currentDate = CommonContentService.getCurrentDate();
            CommonContentService.editComment(answer, $scope.restaurantId, currentDate);
            answer.DateTime = currentDate;
            for (i = 0; i < $scope.restaurantComments.length; i++) {
                if ($scope.restaurantComments[i].CommentId === answer.CommentId)
                {
                    $scope.restaurantComments[i] = answer;
                    break;
                }
            }
        })

    };

    $scope.deleteComment = function (comment) {
        CommonContentService.deleteComment(comment.CommentId).then(function () {
            var index = $scope.restaurantComments.indexOf(comment);
            $scope.restaurantComments.splice(index, 1);
        })


    };


    $scope.editCommentInManagePage = function (ev, comment, profileId) {


        $mdDialog.show({
            locals: { EditComment: comment },
            controller: ['$scope', '$mdDialog', 'EditComment', function ($scope, $mdDialog, EditComment) {
                $scope.commentToEdit = angular.copy(EditComment);
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
            templateUrl: '/app/views/restaurant/common-items/editCommentDialog.html',
            parent: angular.element(document.body),
            targetEvent: ev,
            clickOutsideToClose: true
        }).then(function (answer) {
            var currentDate = CommonContentService.getCurrentDate();
            CommonContentService.editcommentInManagePage(answer, profileId, currentDate);
            answer.DateTime = currentDate;
            for (i = 0; i < $scope.userComments.length; i++) {
                if ($scope.userComments[i].CommentId === answer.CommentId) {
                    $scope.userComments[i] = answer;
                    break;
                }
            }
        })

    };

    $scope.deleteCommentInManagePage = function (comment) {
        CommonContentService.deleteCommentInManagePage(comment.CommentId).then(function () {
            var index = $scope.userComments.indexOf(comment);
            $scope.userComments.splice(index, 1);
        })


    };
});



