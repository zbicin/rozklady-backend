'use strict';
var timetableApp = angular.module('TimetableApp', ['ngRoute', 'ui.bootstrap', 'ui.utils', 'ngAnimate']);

timetableApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
        .when('/', {
            templateUrl: '/ViewsAngular/Home/Index.html',
            controller: 'homeIndexController'
        })
        .when('/home', {
            templateUrl: '/ViewsAngular/Home/Index.html',
            controller: 'homeIndexController'
        })

        // --------------------------------
        .when('/stops', {
            templateUrl: '/ViewsAngular/Stops/Index.html',
            controller: 'stopsIndexController'
        })
        .when('/stops/createOrEdit', {
            templateUrl: '/ViewsAngular/Stops/CreateOrEdit.html',
            controller: 'stopsCreateOrEditController'
        })
        .when('/stops/createOrEdit/:stopId', {
            templateUrl: '/ViewsAngular/Stops/CreateOrEdit.html',
            controller: 'stopsCreateOrEditController'
        })

        // --------------------------------
        .when('/lines', {
            templateUrl: '/ViewsAngular/Lines/Index.html',
            controller: 'linesIndexController'
        })
        .when('/lines/createOrEdit', {
            templateUrl: '/ViewsAngular/Lines/CreateOrEdit.html',
            controller: 'linesCreateOrEditController'
        })
        .when('/lines/createOrEdit/:lineId', {
            templateUrl: '/ViewsAngular/Lines/CreateOrEdit.html',
            controller: 'linesCreateOrEditController'
        })

        // --------------------------------
        .when('/variants/createOrEdit', {
            templateUrl: '/ViewsAngular/Variants/CreateOrEdit.html',
            controller: 'variantsCreateOrEditController'
        })
        .when('/variants/createOrEdit/:variantId', {
            templateUrl: '/ViewsAngular/Variants/CreateOrEdit.html',
            controller: 'variantsCreateOrEditController'
        })
        .when('/variants/addToLine/:lineId', {
            templateUrl: '/ViewsAngular/Variants/CreateOrEdit.html',
            controller: 'variantsCreateOrEditController'
        })

        // --------------------------------
        .when('/departures/createOrEdit', {
            templateUrl: '/ViewsAngular/Departures/CreateOrEdit.html',
            controller: 'departuresCreateOrEditController'
        })
        .when('/departures/createOrEdit/:variantId', {
            templateUrl: '/ViewsAngular/Departures/CreateOrEdit.html',
            controller: 'departuresCreateOrEditController'
        })

        // --------------------------------
        .when('/variantStops/createOrEdit', {
            templateUrl: '/ViewsAngular/VariantStops/CreateOrEdit.html',
            controller: 'variantStopsCreateOrEditController'
        })
        .when('/variantStops/createOrEdit/:variantId', {
            templateUrl: '/ViewsAngular/VariantStops/CreateOrEdit.html',
            controller: 'variantStopsCreateOrEditController'
        })
        // --------------------------------
        .when('/generate', {
            templateUrl: '/ViewsAngular/Generate.html',
            controller: 'generateController'
        })
        .when('/announcements', {
            templateUrl: '/ViewsAngular/Announcements.html',
            controller: 'announcementsController'
        })
        .when('/help', {
            templateUrl: '/ViewsAngular/Help.html',
            controller: 'helpController'
        })
        .otherwise({ redirectTo: '/404' });
}]);