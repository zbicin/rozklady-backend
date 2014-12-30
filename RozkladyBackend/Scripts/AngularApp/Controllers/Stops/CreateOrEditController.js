'use strict';
timetableApp.controller('stopsCreateOrEditController', ['$scope','$routeParams', 'Page', 'Store', function ($scope, $routeParams, Page, Store) {
    Page.title = isNewAction() ? 'Dodaj przystanek' : 'Edytuj przystanek';
    Page.back.url = '#stops';

    $scope.errorMessage = null;
    $scope.stop = null;

    $scope.getJsonUrl = getJsonUrl;
    $scope.isNewAction = isNewAction;
    $scope.submitForm = submitForm;

    fetchInitialStop();

    // --------------------------------
    function getJsonUrl() {
        return location.protocol + '//' + location.host + '/API/Timetable?stopId=' + $routeParams.stopId;
    }

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
            Store.stops.get($routeParams.stopId).then(function(stop) {
                $scope.stop = stop;
            });
        }
    }

    function isNewAction() {
        return typeof $routeParams.stopId === 'undefined';
    }
}]);