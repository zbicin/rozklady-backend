'use strict';
timetableApp.controller('linesCreateOrEditController', ['$scope','$routeParams', 'Page', 'Store', function ($scope, $routeParams, Page, Store) {
    Page.title = isNewAction() ? 'Dodaj linię' : 'Edytuj linię';
    Page.reset();
    Page.proceed.action = submitForm;
    Page.cancel.url = '#lines';

    $scope.isNewAction = isNewAction();
    $scope.errorMessage = null;

    $scope.addVariant = addVariant;
    $scope.removeVariant = removeVariant;
    $scope.submitForm = submitForm;

    fetchInitialData();

    // --------------------------------
    var removalQueue = [];

    function addVariant() {
        $scope.line.variants.push(angular.copy(variantTemplate));
    }

    function removeVariant($index) {
        var removedVariantArray = $scope.line.variants.splice($index, 1);
        removalQueue.push(removedVariantArray[0]);
    }

    function submitForm() {
        Store.lines.updateWithVariants($scope.line, $scope.line.variants, removalQueue)
            .then(
                function () {
                    location.hash = '#lines';
                },
                    function (errorMessage) {
                        $scope.errorMessage = errorMessage;
                    }
            );
    }

    // --------------------------------
    var variantTemplate = {
        id: 0,
        symbol: '',
        departures: [],
        variantStops: []
    };

    function fetchInitialData() {
        if (isNewAction()) {
            $scope.line = {
                id: 0,
                name: '',
                variants: [angular.copy(variantTemplate)]
            };
        }
        else {
            Store.lines.getWithVariants($routeParams.lineId).then(function(line) {
                $scope.line = line;
            });
        }
    }

    function isNewAction() {
        return typeof $routeParams.lineId === 'undefined';
    }

}]);