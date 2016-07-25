app.controller('EditEventDialogController', ['$scope', '$mdDialog', '$mdMedia', '$routeParams', '$rootScope', 'EventService','event',

function ($scope, $mdDialog, $mdMedia, $routeParams, $rootScope, EventService,event) {
    $scope.profileId = $routeParams.profileId;
    $scope.name = event.Name;
    $scope.description = event.Description;
    $scope.price = event.Price;
    $scope.duration = event.Duration;
    $scope.numberOfParticipant = event.NumberOfParticipant;
    $scope.photoUrl = event.EventPhoto;

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
                    $scope.photoUrl = url;
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
    format = moment(event.EventDate, 'DD/MM/YYYY HH:mm:ss').format("MM/DD/YYYY HH:mm:ss");
    eventData = new Date(Date.parse(format));
    $scope.dayOfEvent = new Date(
        eventData.getFullYear(),
        eventData.getMonth(),
        eventData.getDate());
    today = new Date();
    $scope.minDate = new Date(
        today.getFullYear(),
        today.getMonth(),
        today.getDate());
    $scope.maxDate = new Date(
        today.getFullYear(),
        today.getMonth() + 5,
        today.getDate());

    $scope.hide = function () {
        $mdDialog.hide();
    };

    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };
    function days_in_month(month, year) {
        var MonthDays = [
                  [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31],
                 [31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31]
        ];
        if (month < 1 || month > 12) return 0;
        return MonthDays[((month == 2) || ((year % 4 == 0) || ((year % 100 != 0) || (year % 400 == 0)))) ? 1 : 0][month - 1];
    }
    $scope.hourOfEvent = eventData.getHours();
    $scope.minuteOfEvent = eventData.getMinutes();
    
    $scope.checked = function () {
        if ($scope.numOfPersons != null) { $scope.numberStatus = ''; }
        if ($scope.dayOfEvent != null) { $scope.dayStatus = ''; }
        if ($scope.hourOfEvent != null && $scope.wrongHour == false) { $scope.hourStatus = ''; }
        if ($scope.minuteOfEvent != null && $scope.wrongMinute == false) { $scope.minuteStatus = ''; }
    }
    $scope.loadHourOfEvent = function () {
        formatNumber = 10;
        $scope.hours = [];
        var date = new Date();
        if ($scope.dayOfEvent != null) {
            hourLimit = 24;
            for (i = 0; i < hourLimit; i++) {
                var hourView = i;
                if (hourView < formatNumber) {
                    hourView = "0" + hourView;
                }
                $scope.hours[i] = { view: hourView + "", id: i, hour: i };

            }
        }
    }

    $scope.loadMinuteOfEvent = function () {
        $scope.minutes = [];

        var date = new Date();
        if ($scope.hourOfEvent != null) {
            minuteLimit = 45;

            minute = 0;
            for (i = 0; minute <= minuteLimit ; i++, minute += 15) {

                var minuteView = minute;
                if (minuteView < formatNumber) {
                    minuteView = "0" + minuteView;
                }
                $scope.minutes[i] = { view: minuteView + "", id: i, minute: minute };
            }

        }

    }

    $scope.checked = function () {

        if ($scope.dayOfReservation != null) { $scope.dayStatus = ''; }
        if ($scope.hourOfReservation != null) { $scope.hourStatus = ''; }
        if ($scope.minuteOfReservation != null) { $scope.minuteStatus = ''; }
    }
    $scope.sendEvent = function () {

        auditDate = new Date();
        if ($scope.dayOfEvent != null) {

            day = $scope.dayOfEvent.getDate();
            month = $scope.dayOfEvent.getMonth();
            year = $scope.dayOfEvent.getFullYear();
            $scope.dayStatus = '';
        }
        else {
            $scope.dayStatus = 'You need choose day, hour and minute of Event';
            return;
        }

        if ($scope.hourOfEvent != null) {
            hour = $scope.hourOfEvent.hour;
            if (day == auditDate.getDate() && hour <= auditDate.getHours()) {
                $scope.hourStatus = 'This is too early ';
                $scope.wrongHour = true;
                return;
            }
            $scope.wrongHour = false;
            $scope.hourStatus = '';
        }
        else {
            $scope.hourStatus = 'You need choose  hour and minute of Event';
            return;
        }
        if ($scope.minuteOfEvent != null) {
            minute = $scope.minuteOfEvent.minute;
            if (day == auditDate.getDate() && hour <= auditDate.getHours() + 1 && minute <= auditDate.getMinutes()) {
                $scope.minuteStatus = 'This is too early ';
                $scope.wrongMinute = true;
                return;
            }
            $scope.wrongMinute = false;
            $scope.minuteStatus = '';
        }
        else {
            $scope.minuteStatus = 'You need choose minute of Event';
            return;
        }
        formatSize = 10;
        if (day < formatSize)
            day = "0" + day;
        if (month < formatSize)
            month = "0" + month;
        if (hour < formatSize)
            hour = "0" + hour;
        if (minute < formatSize)
            minute = "0" + minute;
        var time = day + "/" + month + "/" + year + " " + hour + ':' + minute;

        EventService.UpdateEvent(event.EventId, $scope.profileId, $scope.name, $scope.description, event.EventDate, $scope.price, $scope.duration, $scope.numberOfParticipant, $scope.photoUrl).then(function (response) {
            $scope.cancel();
        })
    }


}]);