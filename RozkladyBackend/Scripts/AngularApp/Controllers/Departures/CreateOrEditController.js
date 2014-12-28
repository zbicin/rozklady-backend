'use strict';
timetableApp.controller('departuresCreateOrEditController',    ['$scope','$routeParams', 'Page', 'Store', '$filter',         function ($scope, $routeParams, Page, Store, $filter) {
    Page.title = 'Odjazdy linii';
    Page.cancel.url = '#lines';
    Page.proceed.action = proceed;
    Page.back.url = '';

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
    $scope.removeExplanation = removeExplanation;
    $scope.submitForm = submitForm;
    $scope.toggleAbbreviation = toggleAbbreviation;

    fetchInitialData();

    // --------------------------------
    var departuresRemovalQueue = [];
    var explanationsRemovalQueue = [];

    function isValidOnWorkdaysFilter(item) {
        return item.isValidOnMonday ||
            item.isValidOnTuesday ||
            item.isValidOnThursday ||
            item.isValidOnWednesday ||
            item.isValidOnFriday;
    }

    function proceed() {
        Store.departures.update($scope.variant, departuresRemovalQueue, explanationsRemovalQueue)
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
        var index = $scope.variant.departures.indexOf(departureToRemove);
        var removedElementArray = $scope.variant.departures.splice(index, 1);
        departuresRemovalQueue.push(removedElementArray[0]);
    }

    function removeExplanation(explanation) {
        if (confirm('Czy na pewno chcesz usunąć objaśnienie "' + explanation.abbreviation + ' - ' + explanation.definition + '"? Usunie to również litery objaśnień przy powiązanych godzinach odjazdu.')) {
            for (var i = 0; i < $scope.variant.departures.length; i++) {
                var currentDeparture = $scope.variant.departures[i];

                for (var j = 0; j < currentDeparture.explanations.length; j++) {
                    if (currentDeparture.explanations[j].abbreviation === explanation.abbreviation) {
                        currentDeparture.explanations.splice(j, 1);
                        break;
                    }
                }
            }

            $scope.explanations.splice($scope.explanations.indexOf(explanation), 1);
            explanationsRemovalQueue.push(explanation);
        }
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
            symbols: symbols,
            explanations: []
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
                var newExplanation = {
                    id: 0,
                    abbreviation: currentSymbol,
                    definition: newDefinition
                };

                $scope.explanations.push(newExplanation);
                newDeparture.explanations.push(newExplanation);
            }
            else {
                newDeparture.explanations.push($filter('filter')($scope.explanations, { abbreviation: currentSymbol })[0]);
            }
        }

        $scope.variant.departures.push(newDeparture);

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
        });
        Store.explanations.list().then(function (explanations) {
            $scope.explanations = explanations;
        });
    }

}]);