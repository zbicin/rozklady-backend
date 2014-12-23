'use strict';
timetableApp.controller('departuresCreateOrEditController', ['$scope', '$routeParams', 'Page', 'Store', function ($scope, $routeParams, Page, Store) {
    Page.title = isNewAction() ? 'Dodaj odjazdy' : 'Edytuj odjazdy';
    Page.back.url = isNewAction() ? '#lines/createOrEdit' : '#lines';

    $scope.errorMessage = null;
    $scope.variant = null;
    $scope.form = {
        rawDeparture: '',
        isValidOnMonday: true,
        isValidOnTuesday: true,
        isValidOnWednesday: true,
        isValidOnThursday: true,
        isValidOnFriday: true,
        isValidOnSaturday: true,
        isValidOnSunday: true
    };

    $scope.proceed = proceed;
    $scope.removeDeparture = removeDeparture;
    $scope.submitForm = submitForm;

    checkForTempData();
    fetchInitialData();

    // --------------------------------
    function proceed() {
        if (isNewAction()) {
            Store.temp.variant = $scope.variant;
            Store.temp.departures = $scope.variant.departures;

            Store.lines.addOrEdit(Store.temp.line)
                .then(function () {
                    location.hash = '#lines';
                },
                function (errorMessage) {
                    $scope.errorMessage = errorMessage;
                });
            //location.hash = '#variantStops/createOrEdit';
        }
        else {
            Store.departures.removeMultiple(removalQueue)
                .then(
                    function () {

                        Store.departures.addOrEditMultiple($scope.variant.departures)
                            .then(
                                function () {
                                    location.hash = '#lines';
                                },
                                    function (errorMessage) {
                                        $scope.errorMessage = errorMessage;
                                    }
                            );
                    },
                    function (errorMessage) {
                        $scope.errorMessage = errorMessage;
                    });
        }
    }

    function removeDeparture($index) {
        removalQueue.push(angular.copy($scope.variant.departures[$index]));
        $scope.variant.departures.splice($index, 1);
    }

    function submitForm() {
        var splittedInput = $scope.form.rawDeparture.split(':');
        var hour = splittedInput[0];
        var minute = splittedInput[1].substr(0, 2);
        var symbol = splittedInput[1].substr(2);

        var newDeparture = angular.copy($scope.form);
        newDeparture.id = 0;
        newDeparture.hour = hour;
        newDeparture.minute = minute;
        newDeparture.symbol = symbol;
        if (!isNewAction()) {
            newDeparture.variantId = $scope.variant.id;
        }

        $scope.variant.departures.push(newDeparture);

        $scope.form.rawDeparture = '';
        $scope.createOrEditDeparturesForm.$setPristine();
    }

    // --------------------------------
    var removalQueue = [];

    function checkForTempData() {
        if (isNewAction() && Store.temp.line === null) {
            location.hash = '#lines/createOrEdit';
        }
    }

    function fetchInitialData() {
        if (isNewAction()) {
            $scope.variant = Store.temp.line.variants[0];
        }
        else {
            Store.variants.getWithDepartures($routeParams.variantId).then(function (variant) {
                $scope.variant = variant;
            });
        }
    }

    function isNewAction() {
        return typeof $routeParams.lineId === 'undefined';
    }
}]);