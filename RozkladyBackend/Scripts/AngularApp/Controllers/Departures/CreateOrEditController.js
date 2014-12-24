'use strict';
timetableApp.controller('departuresCreateOrEditController', ['$scope','$routeParams', 'Page', 'Store', function ($scope, $routeParams, Page, Store) {
    Page.title = 'Odjazdy linii';
    Page.back.url = '#lines';

    $scope.errorMessage = null;
    $scope.hoursArray = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23];
    $scope.isValidOnWorkdays = true;
    $scope.isValidOnSaturday = true;
    $scope.isValidOnSunday = true;
    $scope.rawDeparture = '';

    $scope.proceed = proceed;
    $scope.removeDeparture = removeDeparture;
    $scope.submitForm = submitForm;

    fetchInitialData();

    // --------------------------------
    var removalQueue = [];
    var addQueue = [];

    function proceed() {
        Store.departures.update($scope.variant.id, $scope.departures, removalQueue)
        .then(
            function () {
                location.hash = '#lines';
            },
                function (errorMessage) {
                    $scope.errorMessage = errorMessage;
                }
        );
    }

    function removeDeparture(departureToRemove) {
        var index = $scope.departures.indexOf(departureToRemove);
        var removedElementArray = $scope.departures.splice(index, 1);
        removalQueue.push(removedElementArray[0]);
    }

    function submitForm() {
        var splittedRawInput = $scope.rawDeparture.split(':');
        var hour = splittedRawInput[0];
        var minute = splittedRawInput[1].substr(0, 2);
        var symbol = splittedRawInput[1].substr(2);
        var newDeparture = {
            id: 0,
            hour: parseInt(hour,10),
            isValidOnMonday: $scope.isValidOnWorkdays,
            isValidOnTuesday: $scope.isValidOnWorkdays,
            isValidOnWednesday: $scope.isValidOnWorkdays,
            isValidOnThursday: $scope.isValidOnWorkdays,
            isValidOnFriday: $scope.isValidOnWorkdays,
            isValidOnSaturday: $scope.isValidOnSaturday,
            isValidOnSunday: $scope.isValidOnSunday,
            minute: parseInt(minute, 10),
            symbol: symbol
        };

        $scope.departures.push(newDeparture);

        $scope.rawDeparture = '';
        $scope.rawDepartureForm.$setPristine();
    }

    // --------------------------------
    function fetchInitialData() {
        Store.variants.getWithDepartures($routeParams.variantId).then(function (variant) {
            $scope.variant = variant;
            $scope.departures = variant.departures.$values;
        });
    }

}]);