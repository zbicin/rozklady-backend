'use strict';
timetableApp.controller('stopsCreateOrEditController', ['$scope','$routeParams', 'Page', 'Storage', function ($scope, $routeParams, Page, Storage) {
    Page.title = isNewAction() ? 'Dodaj przystanek' : 'Edytuj przystanek';
    Page.back.url = '#stops';

    $scope.errorMessage = null;
    $scope.stop = null;

    $scope.submitForm = submitForm;

    fetchInitialStop();

    // --------------------------------
    function submitForm() {
        Storage.stops.addOrEdit($scope.stop)
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
    function fetchInitialStop() {
        if(isNewAction()) {
            $scope.stop = {
                id: 0,
                name: '',
                description: '',
                latitude: 0,
                longitude: 0
            }; 
        }
        else {
            Storage.stops.get($routeParams.stopId).then(function(stop) {
                $scope.stop = stop;
            });
        }
    }

    function isNewAction() {
        return typeof $routeParams.stopId === 'undefined';
    }
}]);