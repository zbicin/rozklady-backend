'use strict';
timetableApp.controller('variantStopsCreateOrEditController', ['$scope','$routeParams', 'Page', 'Store', '$filter', function ($scope, $routeParams, Page, Store, $filter) {
    Page.title = 'Przystanki na trasie linii';
    Page.back.url = '#lines';

    $scope.errorMessage = null;
    $scope.rawVariantStop = '';

    $scope.handleKeyDown = handleKeyDown;
    $scope.isAutocompleteVisible = isAutocompleteVisible;
    $scope.removeVariantStop = removeVariantStop;
    $scope.submitForm = submitForm;

    fetchInitialData();

    // --------------------------------
    var variantStopRemovalQueue = [];

    function handleKeyDown(event) {
        if (isAutocompleteVisible() && event.ctrlKey === true && event.keyCode === 39) {
            var filteredStopArray = $filter('filter')($scope.stops, { name: $scope.rawVariantStopForm.rawVariantStop.$viewValue });

            if (filteredStopArray.length > 0) {
                $scope.rawVariantStop = filteredStopArray[0].name + ' ';
            }
        }
    }

    function isAutocompleteVisible() {
        return typeof $scope.rawVariantStopForm.rawVariantStop.$viewValue !== 'undefined' &&
            $scope.rawVariantStopForm.rawVariantStop.$viewValue !== null &&
            $scope.rawVariantStopForm.rawVariantStop.$viewValue.length > 2;
    }

    function removeVariantStop(variantStop) {
        $scope.variantStops.splice($scope.variantStops.indexOf(variantStop), 1);
        variantStopRemovalQueue.push(variantStop);
    }

    function submitForm() {
        var spaceIndex = $scope.rawVariantStop.lastIndexOf(' ');
        var stopPart = $scope.rawVariantStop.substr(0, spaceIndex);
        var offset = $scope.rawVariantStop.substr(spaceIndex+1, $scope.rawVariantStop.length-spaceIndex-2);

        var stopAlreadyUsed = $filter('filter')($scope.variantStops, {$: stopPart}).length > 0;
        if (stopAlreadyUsed) {
            alert('Przystanek "' + stopPart + '" jest już umieszczony na trasie tego wariantu linii. Jeśli chcesz zmienić jego czas dojazdu, usuń go z listy po prawej stronie a następnie dodaj ponownie.');
        }
        else {
            var chosenStopArray = $filter('filter')($scope.stops, { name: stopPart });
            var stopToAdd;
            if (chosenStopArray.length > 0) {
                stopToAdd = chosenStopArray[0];
            }
            else {
                stopToAdd = {
                    id: 0,
                    name: stopPart,
                    description: ''
                };
                $scope.stops.push(stopToAdd);
            }

            $scope.variantStops.push({
                id: 0,
                variantId: $routeParams.variantId,
                stop: stopToAdd,
                timeOffset: offset
            });

            $scope.rawVariantStop = '';
            $scope.rawVariantStopForm.$setPristine();
        }
    }

    // --------------------------------
    function fetchInitialData() {
        Store.stops.list().then(function (stops) {
            $scope.stops = stops;
        });

        Store.variantStops.getForVariant($routeParams.variantId).then(function (variantStops) {
            $scope.variantStops = variantStops;
        });
        
        /*Store.variants.getWithDeparturesAndExplanations($routeParams.variantId).then(function (variant) {
            $scope.variant = variant;
        });
        Store.explanations.list().then(function (explanations) {
            $scope.explanations = explanations;
        });*/
    }

}]);