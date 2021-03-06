﻿'use strict';
timetableApp.factory('Store', ['$http', '$q', 'Page', function ($http, $q, Page) {

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
        },
        update: function (variant, departuredToRemove, explanationsToRemove) {
            return basicRequest('post', 'UpdateDepartures', {variant: variant, departuresToRemove: departuredToRemove, explanationsToRemove: explanationsToRemove});
        }
    };

    var explanations = {
        list: function () {
            return basicRequest('get', 'ListExplanations');
        }
    };

    var lines = {
        addOrEdit: function (line) {
            return basicRequest('post',  'AddOrEditLine', line);
        },
        get: function (lineId) {
            return basicRequest('get', 'GetLine?lineId=' + lineId);
        },
        getWithVariants: function(lineId) {
            return basicRequest('get', 'GetLineWithVariants?lineId=' + lineId);
        },
        list: function () {
            return basicRequest('get', 'ListLines');
        },
        remove: function (lineId) {
            return basicRequest('post', 'RemoveLine', { lineId: lineId });
        },
        updateWithVariants: function (line, variants, variantsToRemove) {
            return basicRequest('post', 'UpdateLineWithVariants', { line: line, variants: variants, variantsToRemove: variantsToRemove });
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
            return basicRequest('get', 'ListStops');
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
            return basicRequest('get', 'GetVariant?variantId=' + variantId);
        },
        getWithDeparturesAndExplanations: function (variantId) {
            var request = $http({
                method: 'get',
                url: '/Ajax/GetVariantWithDeparturesAndExplanations?variantId=' + variantId,
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

    var variantStops = {
        getForVariant: function (variantId) {
            return basicRequest('get', 'GetVariantStopsForVariant?variantId=' + variantId);
        },
        update: function (variantId, stops, variantStops, variantStopsToRemove) {
            return basicRequest('post', 'UpdateVariantStops', {variantId: variantId, stops: stops, variantStops: variantStops, variantStopsToRemove: variantStopsToRemove});
        }
    };

    // --------------------------------
    function basicRequest(method, url, data) {
        Page.pendingRequestsCount = Page.pendingRequestsCount+1;
        var request;
        if (method === 'get') {
            request = $http({
                method: method,
                url: '/Ajax/' + url
            });
        }
        else {
            request = $http({
                method: method,
                url: '/Ajax/' + url,
                data: data
            });
        }

        return (request.then(handleSuccess, handleError));
    }

    function handleError(response) {
        Page.pendingRequestsCount = Page.pendingRequestsCount - 1;
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
        Page.pendingRequestsCount = Page.pendingRequestsCount - 1;
        return (response.data);
    }

    // --------------------------------
    return {
        departures: departures,
        explanations: explanations,
        stops: stops,
        lines: lines,
        temp: temp,
        variants: variants,
        variantStops: variantStops
    };

}]);