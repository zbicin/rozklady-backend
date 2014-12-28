'use strict';
timetableApp.factory('Page', function () {
    var back,
        proceed,
        cancel;

    initFields();

    function initFields() {
        back = {
            title: 'Powrót',
            url: '',
            action: null
        };

        proceed = {
            title: 'Zapisz zmiany',
            url: '',
            action: null
        };

        cancel = {
            title: 'Anuluj',
            url: '',
            action: null
        };
    }

    function isNavigationVisible(which) {
        var navigation;
        if (which === 'back') {
            navigation = back;
        }
        else if (which === 'proceed') {
            navigation = proceed;
        }
        else if (which === 'cancel') {
            navigation = cancel;
        }
        else {
            return false;
        }

        return navigation.url.length > 0 || navigation.action !== null;
    }

    return {
        title: '',
        back: back,
        proceed: proceed,
        cancel: cancel,
        isBackVisible: function () {
            return isNavigationVisible('back');
        },
        isProceedVisible: function () {
            return isNavigationVisible('proceed');
        },
        isCancelVisible: function () {
            return isNavigationVisible('cancel');
        },
        pendingRequestsCount: 0,
        reset: function () {
            this.back.url = '';
            this.proceed.url = '';
            this.cancel.url = '';
            this.back.action = null;
            this.proceed.action = null;
            this.cancel.action = null;
        }
    };
});