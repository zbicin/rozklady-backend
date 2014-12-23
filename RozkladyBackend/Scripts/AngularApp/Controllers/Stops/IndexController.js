﻿'use strict';
timetableApp.controller('stopsIndexController', ['$scope', 'Page', 'Storage', function ($scope, Page, Storage) {
    Page.title = 'Przystanki';
    Page.back.url = '#home';

    $scope.removeStop = removeStop;
    $scope.stops = [];

    fetchStops();

    // --------------------------------
    function removeStop(stop) {
        if (confirm('Czy jesteś pewien, że chcesz usunąć przystanek ' + stop.name + '?')) {
            Storage.stops.remove(stop.id).then(function () {
                fetchStops();
            });
        }
    }

    // --------------------------------
    function fetchStops() {
        Storage.stops.list().then(function (stops) {
            $scope.stops = stops;
        });
    }

}]);