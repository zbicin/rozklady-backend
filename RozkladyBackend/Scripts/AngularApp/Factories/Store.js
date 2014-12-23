'use strict';
timetableApp.factory('Store', ['$http', '$q', function ($http, $q) {

    var departures = {
        addOrEditMultiple: function(departures) {
            var request = $http({
                method: 'post',
                url: '/Ajax/AddOrEditMultipleDepartures',
                data: {
                    departures: departures
                }
            });

            return (request.then(handleSuccess, handleError));
        },
        getByVariantId: function (variantId) {
            var request = $http({
                method: 'get',
                url: '/Ajax/GetDeparturesByVariantId?variantId=' + variantId,
            });

            return (request.then(handleSuccess, handleError));
        },
        removeMultiple: function(departures) {
            var request = $http({
                method: 'post',
                url: '/Ajax/RemoveMultipleDepartures',
                data: {
                    departures: departures
                }
            });

            return (request.then(handleSuccess, handleError));
        }
    };

    var lines = {
        addOrEdit: function (line) {
            var request = $http({
                method: 'post',
                url: '/Ajax/AddOrEditLine',
                data: line
            });

            return (request.then(handleSuccess, handleError));
        },
        get: function (lineId) {
            var request = $http({
                method: 'get',
                url: '/Ajax/GetLine?lineId=' + lineId,
            });

            return (request.then(handleSuccess, handleError));
        },
        list: function () {
            var request = $http({
                method: 'get',
                url: '/Ajax/ListLines'
            });

            return (request.then(handleSuccess, handleError));
        },
        remove: function (lineId) {
            var request = $http({
                method: 'post',
                url: '/Ajax/RemoveLine',
                data: {
                    lineId: lineId
                }
            });

            return (request.then(handleSuccess, handleError));
        }
    };

    var stops = {
        addOrEdit: function (stop) {
            var request = $http({
                method: 'post',
                url: '/Ajax/AddOrEditStop',
                data: stop
            });

            return (request.then(handleSuccess, handleError));
        },
        get: function (stopId) {
            var request = $http({
                method: 'get',
                url: '/Ajax/GetStop?stopId=' + stopId,
            });

            return (request.then(handleSuccess, handleError));
        },
        list: function () {
            var request = $http({
                method: 'get',
                url: '/Ajax/ListStops'
            });

            return (request.then(handleSuccess, handleError));
        },
        remove: function (stopId) {
            var request = $http({
                method: 'post',
                url: '/Ajax/RemoveStop',
                data: {
                    stopId: stopId
                }
            });

            return (request.then(handleSuccess, handleError));
        }
    };

    var temp = {
        clear: function () {
            this.departures = null;
            this.line = null;
            this.stops = null;
            this.variant = null;
            this.variantStops = null;
        },
        departures: null,
        line: null,
        stops: null,
        variant: null,
        variantStops: null
    };

    var variants = {
        get: function (variantId) {
            var request = $http({
                method: 'get',
                url: '/Ajax/GetVariant?variantId=' + variantId,
            });

            return (request.then(handleSuccess, handleError));
        },
        getWithDepartures: function (variantId) {
            var request = $http({
                method: 'get',
                url: '/Ajax/GetVariantWithDepartures?variantId=' + variantId,
            });

            return (request.then(handleSuccess, handleError));
        },
        remove: function (variantId) {
        var request = $http({
            method: 'post',
            url: '/Ajax/RemoveVariant',
            data: {
                variantId: variantId
            }
        });

        return (request.then(handleSuccess, handleError));
    }
    };

    // --------------------------------
    function handleError(response) {

        // The API response from the server should be returned in a
        // nomralized format. However, if the request was not handled by the
        // server (or what not handles properly - ex. server error), then we
        // may have to normalize it on our end, as best we can.
        if (!angular.isObject(response.data) || !response.data.message) {
            return ($q.reject('An unknown error occurred.'));
        }
        // Otherwise, use expected error message.
        return ($q.reject(response.data.message));
    }

    function handleSuccess(response) {
        return (response.data);
    }

    // --------------------------------
    return {
        departures: departures,
        stops: stops,
        lines: lines,
        temp: temp,
        variants: variants
    };

}]);