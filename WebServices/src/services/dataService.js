app.factory('dataService', ['$http', '$resource', function ($http, $resource) {
    var fac = {};
    fac.GetAuthorizeData = function () {
        return $http.get('/api/data/home').then(function (response) {
            return response.data;
        })
    }
    fac.getVendors = function () {
        return $resource('/api/vendors/:vendor', { user: "@vendor" });
    }
    fac.getPromotions = function () {
        return $resource('/api/promotions/:promotion', { user: "@promotion" });
    }

    fac.updatePromotion = function () {
        return $resource('/api/promotions/:promotion', null, {
            'update': {
                method: 'PUT'
            }
        });
    }
    fac.deletePromotion = function () {
        return $resource('/api/promotions/:promotion', null, {
            'delete': {
                method: 'DELETE'
            }
        });
    }
    return fac;
}])
