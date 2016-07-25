function ReservationDialogController($scope, $mdDialog, $routeParams, ReservationService) {
    $scope.restaurantId = $routeParams.profileId;
    $scope.hide = function () {
        $mdDialog.hide();
    };

    $scope.cancel = function () {
        $mdDialog.cancel();
    };
    $scope.answer = function (answer) {
        $mdDialog.hide(answer);
    };


    $scope.dayOfReservation = null;
    $scope.hourOfReservation = null;
    $scope.minuteOfReservation = null;
    $scope.numOfPersons = null;
    $scope.loadDateOfReservation = function () {
        var date = new Date();

        $scope.days = [{ view: "", id: 0 }];
        numberOfDays = 2;
        formatNumber = 10;
        for (i = 0; i < numberOfDays; i++) {
            var dayView = date.getDate() + i;
            if (dayView < formatNumber) {
                dayView = "0" + dayView;
            }
            var monthView = date.getMonth()+1;
            if (monthView < formatNumber) {
                monthView = "0" + monthView;
            }
            $scope.days[i] = {
                view: dayView + ":" + monthView + ":" + date.getFullYear(),
                id: i,
                year: date.getFullYear(),
                month: date.getMonth()+1,
                day: date.getDate() + i
            };

        }
    }
    $scope.checked= function()
    {
         if ($scope.numOfPersons != null ) { $scope.numberStatus = ''; }
        if ($scope.dayOfReservation != null) { $scope.dayStatus = ''; }
        if ($scope.hourOfReservation != null && $scope.wrongHour == false) { $scope.hourStatus = ''; }
        if ($scope.minuteOfReservation != null &&  $scope.wrongMinute == false) { $scope.minuteStatus = ''; }
    }
    $scope.loadHourOfReservation = function () {
        $scope.hours = [];
        var date = new Date();
        if ($scope.dayOfReservation != null) {

            if ($scope.dayOfReservation.id == 0) {
                hourLimit = 24;
                countMinutes = 0;
                if (date.getMinutes() > 45) { countMinutes = 1; }
                for (i = 0; i < hourLimit - date.getHours() - countMinutes; i++) {

                    var hourView = date.getHours() + i + 1;
                    if (hourView < formatNumber) {
                        hourView = "0" + hourView;
                    }
                    $scope.hours[i] = { view: hourView + "", id: i, hour: date.getHours() + i + 1 };
                }
            }
            else {
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
    }
    $scope.loadMinuteOfReservation = function () {
        $scope.minutes = [];
       
        var date = new Date();
        if ($scope.hourOfReservation != null) {
            minuteLimit = 45;
            if ($scope.hourOfReservation.hour == date.getHours() + 1 && $scope.dayOfReservation.day == date.getDate()) {
                minute = date.getMinutes();
                if (minute <= 15) {
                    minute = 15;
                }
                else {
                    if (minute <= 30) {
                        minute = 30;
                    }
                    else {
                        if (minute <= 45) {
                            minute = 45;
                        }
                    }
                }
                for (i = 0; minute <= minuteLimit ; i++, minute += 15) {

                    var minuteView = minute;
                    if (minuteView < formatNumber) {
                        minuteView = "0" + minuteView;
                    }
                    $scope.minutes[i] = { view: minuteView + "", id: i, minute: minute };
                }
            }
            else {
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

    }
    $scope.numberStatus = '';
    $scope.timeStatus = '';
    $scope.wrongHour = false;
    $scope.wrongMinute = false;
    $scope.sendReservation = function () {
        var reservate = {};
        auditDate = new Date();
        if ($scope.numOfPersons != null) {
            $scope.numberStatus = '';
            NumberOfPersons = $scope.numOfPersons;
        }
        else {
            $scope.numberStatus = 'You need choose number of persons';

        }
        if ($scope.dayOfReservation != null) {
            day = $scope.dayOfReservation.day;
            month = $scope.dayOfReservation.month;
            year = $scope.dayOfReservation.year;
            $scope.dayStatus = '';
        }
        else {
            $scope.dayStatus = 'You need choose day, hour and minute of reservation';
            return;
        }

        if ($scope.hourOfReservation != null) {
            hour = $scope.hourOfReservation.hour;
            if (day == auditDate.getDate() && hour <= auditDate.getHours()) {
                $scope.hourStatus = 'This is too early hour';
                $scope.wrongHour = true;
                return;
            }
            $scope.wrongHour = false;
            $scope.hourStatus = '';
        }
        else {
            $scope.hourStatus = 'You need choose  hour and minute of reservation';
            return;
        }
        if ($scope.minuteOfReservation != null) {
            minute = $scope.minuteOfReservation.minute;
            if (day == auditDate.getDate() && hour <= auditDate.getHours()+1 && minute <= auditDate.getMinutes()) {
                $scope.minuteStatus = 'This is too early minute';
                $scope.wrongMinute = true;
                return;
            }
            $scope.wrongMinute = false;
            $scope.minuteStatus = '';
        }
        else {
            $scope.minuteStatus = 'You need choose minute of reservation';
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
       
    
        ReservationService.AddReservation( NumberOfPersons, time , $scope.restaurantId).then(function (response) {
            $scope.status = "ok";
            $scope.cancel();
        })

    }



};