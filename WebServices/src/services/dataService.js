app.factory('dataService', ['$http', '$resource', function ($http, $resource) {
    var fac = {};
    fac.GetAuthorizeData = function () {
        return $http.get('/api/data/home').then(function (response) {
            return response.data;
        })
    }
    fac.GetVendors = function () {
        return $resource('/api/vendors', { user: "@vendor" });
    }
    return fac;
}])
