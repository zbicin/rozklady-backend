'use strict';
timetableApp.controller('variantStopsCreateOrEditController', ['$scope','$routeParams', 'Page', 'Store', function ($scope, $routeParams, Page, Store) {
    Page.title = isNewAction() ? 'Dodaj przystanki na trasie' : 'Edytuj przystanki na trasie';
    Page.back.url = isNewAction() ? '#departures/createOrEdit' : '#lines';

    $scope.errorMessage = null;
    $scope.rawVariantStop = '';

    $scope.submitForm = submitForm;

    fetchInitialVariantStops();

    // --------------------------------
    function submitForm() {
        Store.stops.addOrEdit($scope.stop)
            .then(
                function () {
                    location.hash = '#stops';
                },
                    function (errorMessage) {
                        $scope.errorMessage = errorMessage;
                    }
            );
    }

    // --------------------------------
    function fetchInitialVariantStops() {
        if (isNewAction()) {
            if (Store.temp.line.variant[0].variantStops.length > 0) {
                $scope.variantStops = Store.temp.line.variant[0].variantStops;
            }
            else {
                $scope.variantStops = [];
            }
        }
    }

    function isNewAction() {
        return typeof $routeParams.variantId === 'undefined';
    }
}]);