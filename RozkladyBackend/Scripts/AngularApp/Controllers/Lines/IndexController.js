'use strict';
timetableApp.controller('linesIndexController', ['Page', 'Storage', '$scope', function (Page, Storage, $scope) {
    Page.title = 'Linie';
    Page.back.url = '#home';

    $scope.lines = [];

    fetchLines();

    // --------------------------------
    function fetchLines() {
        Storage.listLines().then(function (lines) {
            $scope.lines = lines;
        });
    }
}]);