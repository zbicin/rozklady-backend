'use strict';
timetableApp.controller('mainController', ['$scope', 'Page', function ($scope, Page) {
    $scope.Page = Page;

    $scope.handleKeyDown = handleKeyDown;
    // --------------------------------
    function handleKeyDown(event) {
        if (Page.isProceedVisible()) {
            if (event.keyCode === 13 && event.ctrlKey === true) {
                if (Page.proceed.action !== null) {
                    Page.proceed.action();
                }
                else {
                    location.hash = Page.proceed.url;
                }
            }
        }
    }
}]);