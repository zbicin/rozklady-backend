'use strict';
timetableApp.factory('Page', function () {
    var back = {
        title: 'Powrót',
        url: '',
        action: null
    };

    var next = {
        title: 'Dalej',
        url: '',
        action: null
    };

    function isNavigationVisible(which) {
        var navigation;
        if (which === 'back') {
            navigation = back;
        }
        else if (which === 'next') {
            navigation = next;
        }
        else {
            return false;
        }

        return navigation.url.length > 0 || navigation.action !== null;
    }

    return {
        title: '',
        back: back,
        next: next,
        isBackVisible: function () {
            return isNavigationVisible('back');
        },
        isNextVisible: function () {
            return isNavigationVisible('next');
        }
    };
});