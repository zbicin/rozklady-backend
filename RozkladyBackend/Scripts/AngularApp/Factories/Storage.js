'use strict';
timetableApp.factory('Storage', ['$http', '$q', function ($http, $q) {
    return {
        addOrEditStop: addOrEditStop,
        getStop: getStop,
        listStops: listStops,
        removeStop: removeStop
    };

    // --------------------------------
    function addOrEditStop(stop) {
        var request = $http({
            method: 'post',
            url: '/Ajax/AddOrEditStop',
            data: stop
        });

        return (request.then(handleSuccess, handleError));
    }

    function getStop(stopId) {
        var request = $http({
            method: 'get',
            url: '/Ajax/GetStop?stopId='+stopId,
        });

        return (request.then(handleSuccess, handleError));
    }

    function listStops() {
        var request = $http({
            method: 'get',
            url: '/Ajax/ListStops'
        });

        return (request.then(handleSuccess, handleError));
    }

    function removeStop(stopId) {
        var request = $http({
            method: 'post',
            url: '/Ajax/RemoveStop',
            data: {
                stopId: stopId
            }
        });

        return (request.then(handleSuccess, handleError));
    }

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

}]);