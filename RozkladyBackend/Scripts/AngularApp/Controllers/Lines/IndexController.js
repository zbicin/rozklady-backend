'use strict';
timetableApp.controller('linesIndexController', ['Page', 'Store', '$scope', function (Page, Store, $scope) {   
    Page.title = 'Linie';
    Page.back.url = '#home';
    Page.proceed.action = null;
    Page.cancel.url = '';

    $scope.goToAddLine = goToAddLine;
    $scope.lines = [];
    $scope.removeLine = removeLine;
    $scope.removeVariant = removeVariant;

    fetchLines();

    // --------------------------------
    function goToAddLine() {
        Store.temp.clear();
        location.hash = '#lines/createOrEdit';
    }

    function removeLine(line) {
        if (confirm('Czy na pewno chcesz usunąć linię ' + line.name + ' wraz z wszystkimi jej wariantami?')) {
            Store.lines.remove(line.id).then(function () {
                fetchLines();
            });
        }
    }

    function removeVariant(line, variant) {
        if (confirm('Czy na pewno chcesz usunąć wariant linii ' + line.name + ' ' + variant.symbol + ', ' + variant.description + '?')) {
            Store.variants.remove(variant.id).then(function () {
                fetchLines();
            });
        }
    }

    // --------------------------------
    function fetchLines() {
        Store.lines.list().then(function (lines) {
            $scope.lines = lines;
        });
    }

}]);