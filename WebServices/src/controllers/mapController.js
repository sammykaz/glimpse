'use strict';
app.controller("mapController", ['$scope', 'dataService', function ($scope, dataService) {
    var pins = [];
    var i = 0;
    var promotionsquery = dataService.getPromotions().query().$promise.then(function (data) {
        angular.forEach(data, function (promo) {
            var vendorId = promo.VendorId;
            var vendor = dataService.getVendors().get({ vendor: vendorId });
            vendor.$promise.then(function (data) {
                var pin = {
                    "id": i++,
                    "latitude": data.Location.Lat,
                    "longitude": data.Location.Lng,
                    "title": i
                }
                pins.push(pin);
            }, function (error) {
                console.log("vendor not found");
            })
        })
        $scope.promotions = data;
        console.log(data);
        $scope.randomMarkers = pins;
    }, function (error) {
        console.log("Error: Could not load promotions");
    })

    console.log(dataService.getVendors().get({ vendor: 36 }))
    $scope.map = {
        center: {
            latitude: 45.4581475,
            longitude: -73.64009765625
        },
        zoom: 8

    };
    $scope.options = {
        scrollwheel: false
    };
    
    $scope.getPromotionPins = function () {
        for (var i = 0; i < $scope.promotions.length; i++) {
            var vendorId = $scope.promotions[i].VendorId;
            var vendor = dataService.getVendors().get({ vendor: vendorId });
            vendor.$promise.then(function (data) {
                var pin = {
                    "id": i,
                    "latitude": data.Location.Lat,
                    "longitude": data.Location.Lng,
                    "title": $scope.promotions[i].title
                }
                pins.push(pin);
            }, function (error) {
                console.log("vendor not found");
            })

            console.log(vendor);
        }
        
    }
}]);
