'use strict';
app.controller("mapController", ['$scope', 'dataService', function ($scope, dataService) {

    var promotionsquery = dataService.getPromotions().query();
    promotionsquery.$promise.then(function (data) {
        $scope.promotions = data;
        console.log(data);
    }, function (error) {
        console.log("Error: Could not load promotions");
    })

    $scope.map = {
        center: {
            latitude: 40.1451,
            longitude: -99.6680
        },
        zoom: 4,
        bounds: {
            northeast: {
                latitude: 45.1451,
                longitude: -80.6680
            },
            southwest: {
                latitude: 30.000,
                longitude: -120.6680
            }
        }
    };
    $scope.options = {
        scrollwheel: false
    };

    //var addPromotionPins = function 
    var createRandomMarker = function (i, bounds, idKey) {
        var lat_min = bounds.southwest.latitude,
          lat_range = bounds.northeast.latitude - lat_min,
          lng_min = bounds.southwest.longitude,
          lng_range = bounds.northeast.longitude - lng_min;

        if (idKey == null) {
            idKey = "id";
        }

        var latitude = lat_min + (Math.random() * lat_range);
        var longitude = lng_min + (Math.random() * lng_range);
        var ret = {
            latitude: latitude,
            longitude: longitude,
            title: 'm' + i
        };
        ret[idKey] = i;
        return ret;
    };
    var markers = [];
    var x = {
        id: 2,
        latitude: 36.47237295026877,
        longitude: -93.59872415879792,
        title: 'm'
    }
    markers.push(x);
    markers.push(x);
    $scope.randomMarkers = markers;

    $scope.test = alert("clicked");
}]);
