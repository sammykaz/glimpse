app.factory('dataService', ['$http', function ($http, serviceBasePath) {
    var fac = {};
    fac.GetAuthorizeData = function () {
        return $http.get('/api/data/home').then(function (response) {
            return response.data;
        })
    }
    return fac;
}])
