'use strict';
var timetableApp = angular.module('TimetableApp', ['ngRoute']);

timetableApp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider

        .when('/', {
            templateUrl: '/ViewsAngular/Home.html',
            controller: 'homeController'
        })

        .when('/home', {
            templateUrl: '/ViewsAngular/Home.html',
            controller: 'homeController'
        })

        .when('/generate', {
            templateUrl: '/ViewsAngular/Generate.html',
            controller: 'generateController'
        })
        .when('/announcements', {
            templateUrl: '/ViewsAngular/Announcements.html',
            controller: 'announcementsController'
        })
        .when('/stops', {
            templateUrl: '/ViewsAngular/Stops.html',
            controller: 'stopsController'
        })
        .when('/lines', {
            templateUrl: '/ViewsAngular/Lines.html',
            controller: 'linesController'
        })
        .when('/help', {
            templateUrl: '/ViewsAngular/Help.html',
            controller: 'helpController'
        })
        .otherwise({ redirectTo: '/404' });
}]);