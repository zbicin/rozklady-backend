'use strict';
timetableApp.controller('linesCreateOrEditController', ['$scope','$routeParams', 'Page', 'Store', function ($scope, $routeParams, Page, Store) {
    Page.title = isNewAction() ? 'Dodaj linię' : 'Edytuj linię';
    Page.back.url = '#lines';

    $scope.errorMessage = null;
    $scope.line = null;

    $scope.submitForm = submitForm;

    fetchInitialLine();

    // --------------------------------
    function submitForm() {
        if (isNewAction()) {
            Store.temp.line = $scope.line;
            Store.temp.line.variants.push({
                id: 0,
                symbol: '',
                departures: [],
                variantStops: []
            });

            location.hash = '#departures/createOrEdit';
        }
        else {
            Store.lines.addOrEdit($scope.line)
                .then(
                    function () {
                        location.hash = '#lines';
                    },
                        function (errorMessage) {
                            $scope.errorMessage = errorMessage;
                        }
                );
        }
    }

    // --------------------------------
    function fetchInitialLine() {
        if (isNewAction()) {
            if (Store.temp.line === null) {
                $scope.line = {
                    id: 0,
                    name: '',
                    variants: []
                };
            }
            else {
                $scope.line = Store.temp.line;
            }
        }
        else {
            Store.lines.get($routeParams.lineId).then(function(line) {
                $scope.line = line;
            });
        }
    }

    function isNewAction() {
        return typeof $routeParams.lineId === 'undefined';
    }
}]);