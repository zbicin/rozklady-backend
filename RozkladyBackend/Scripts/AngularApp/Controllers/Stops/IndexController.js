'use strict';
timetableApp.controller('stopsIndexController', ['$scope', 'Page', 'Store', function ($scope, Page, Store) {
    Page.title = 'Przystanki';
    Page.back.url = '#home';

    $scope.removeStop = removeStop;
    $scope.stops = [];

    fetchStops();

    // --------------------------------
    function removeStop(stop) {
        if (confirm('Czy jesteś pewien, że chcesz usunąć przystanek ' + stop.name + '?')) {
            Store.stops.remove(stop.id).then(function () {
                fetchStops();
            });
        }
    }

    // --------------------------------
    function fetchStops() {
        Store.stops.list().then(function (stops) {
            $scope.stops = stops;
        });
    }

}]);