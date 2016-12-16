'use strict';
app.factory('loginService', function ($resource, $http) {
    
    var res = $resource('/api/users/:UserId', { UserId: "@User" });

    return res;

});