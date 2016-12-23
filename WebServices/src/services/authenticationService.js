app.factory('authenticationService', ['$http', '$q', 'userService1', function ($http, $q, userService1) {
    var fac = {};
    fac.login = function (user) {
        var obj = { 'username': user.username, 'password': user.password, 'grant_type': 'password' };
        Object.toparams = function ObjectsToParams(obj) {
            var p = [];
            for (var key in obj) {
                p.push(key + '=' + encodeURIComponent(obj[key]));
            }
            return p.join('&');
        }

        var defer = $q.defer();
        $http({
            method: 'post',
            url: "/token",
            data: Object.toparams(obj),
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).then(function (response) {
            userService1.SetCurrentUser(response.data);
            defer.resolve(response.data);
        }, function (error) {
            defer.reject(error.data);
        })
        return defer.promise;
    }
    fac.logout = function () {
        userService1.CurrentUser = null;
        userService1.SetCurrentUser(userService1.CurrentUser);
    }
    return fac;
}])