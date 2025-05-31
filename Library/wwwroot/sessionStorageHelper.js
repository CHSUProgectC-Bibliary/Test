// wwwroot/js/sessionStorageHelper.js

window.sessionStorageHelper = {
    saveUser: function (email) {
        localStorage.setItem('userEmail', email);
    },
    clearUser: function () {
        localStorage.removeItem('userEmail');
    },
    getUser: function () {
        return localStorage.getItem('userEmail');
    }
};
