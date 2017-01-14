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
    fac.getAllPromotionFromSpecificVendor = function (vendorId) {
        return $resource('/api/vendors/'+vendorId+'/promotions', { user: "@promotion" })
    }
    fac.getPromotionClicks = function () {
        return $resource('/api/promotionclicks', { user: "@promotionclicks" })
    }
    return fac;
}])
