'use strict';
var timetableApp = angular.module('TimetableApp', ['ngRoute']);

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
        .when('/generate', {
            templateUrl: '/ViewsAngular/Generate.html',
            controller: 'generateController'
        })
        .when('/announcements', {
            templateUrl: '/ViewsAngular/Announcements.html',
            controller: 'announcementsController'
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
            templateUrl: '/ViewsAngular/Lines.html',
            controller: 'linesController'
        })
        .when('/help', {
            templateUrl: '/ViewsAngular/Help.html',
            controller: 'helpController'
        })
        .otherwise({ redirectTo: '/404' });
}]);