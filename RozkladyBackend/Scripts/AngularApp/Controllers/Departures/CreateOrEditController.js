'use strict';
timetableApp.controller('departuresCreateOrEditController',    ['$scope','$routeParams', 'Page', 'Store', '$filter',         function ($scope, $routeParams, Page, Store, $filter) {
    Page.title = 'Odjazdy linii';
    Page.back.url = '#lines';

    $scope.errorMessage = null;
    $scope.explanationsAddQueue = [];
    $scope.hoursArray = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23];
    $scope.isValidOnWorkdays = true;
    $scope.isValidOnSaturday = true;
    $scope.isValidOnSunday = true;
    $scope.isValidOnWorkdaysFilter = isValidOnWorkdaysFilter;
    $scope.rawDeparture = '';

    $scope.proceed = proceed;
    $scope.removeDeparture = removeDeparture;
    $scope.submitForm = submitForm;
    $scope.toggleAbbreviation = toggleAbbreviation;

    fetchInitialData();

    // --------------------------------
    var departuresRemovalQueue = [];

    function isValidOnWorkdaysFilter(item) {
        return item.isValidOnMonday ||
            item.isValidOnTuesday ||
            item.isValidOnThursday ||
            item.isValidOnWednesday ||
            item.isValidOnFriday;
    }

    function proceed() {
        Store.departures.update($scope.variant.id, $scope.departures, departuresRemovalQueue)
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
        departuresRemovalQueue.push(removedElementArray[0]);
    }

    function submitForm() {
        var splittedRawInput = $scope.rawDeparture.split(':');
        var hour = splittedRawInput[0];
        var minute = splittedRawInput[1].substr(0, 2);
        var symbols = splittedRawInput[1].substr(2);
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
            symbols: symbols
        };

        var splittedSymbols = symbols.split('');
        for (var i = 0; i < splittedSymbols.length; i++) {
            var currentSymbol = splittedSymbols[i];
            if ($filter('filter')($scope.explanations, { abbreviation: currentSymbol }).length < 1) {
                // there is no such symbol already
                var newDefinition = prompt('Użyłeś nowej litery objaśnienia, której nie ma jeszcze w bazie. Podaj opis oznaczenia "' + currentSymbol + '"');
                if (newDefinition === null) {
                    return;
                }
                
                $scope.explanations.push({
                    id: 0,
                    abbreviation: currentSymbol,
                    definition: newDefinition
                });
            }
        }

        $scope.departures.push(newDeparture);

        $scope.rawDeparture = '';
        $scope.rawDepartureForm.$setPristine();
    }

    function toggleAbbreviation(abbr) {
        if ($scope.rawDeparture.indexOf(abbr) > -1) {
            $scope.rawDeparture = $scope.rawDeparture.split(abbr).join('');
        }
        else {
            $scope.rawDeparture += abbr;
        }
    }

    // --------------------------------
    function fetchInitialData() {
        Store.variants.getWithDeparturesAndExplanations($routeParams.variantId).then(function (variant) {
            $scope.variant = variant;
            $scope.departures = variant.departures.$values;
        });
        Store.explanations.list().then(function (explanations) {
            $scope.explanations = explanations;
        });
    }

}]);