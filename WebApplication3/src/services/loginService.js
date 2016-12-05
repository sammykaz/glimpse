'use strict';
app.factory('loginService', function ($resource) {
    return $resource('http://glimpsews.azurewebsites.net/api/users', { user: "@user" });
});
