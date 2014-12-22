'use strict';
timetableApp.controller('stopsController', ['$scope', 'Page', 'Storage', function ($scope, Page, Storage) {
    Page.title = 'Przystanki';
    $scope.stops = [];

    Storage.getStops().then(function(stops) {
        $scope.stops = stops;
    });

}]);