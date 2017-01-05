'use strict';

app.controller('SignupController', ['$scope', '$http', 'dataService','$state', function ($scope, $http, dataService, $state) {

    $scope.user = undefined;
    console.log(dataService.getVendors().query());
    $scope.createUser = function () {
        var addressData = {
            PostalCode: $scope.user.postal,
            city: $scope.user.city,
            country: $scope.user.country,
            province: $scope.user.province,
            street: $scope.user.streetname,
            streetNumber: $scope.user.streetnumber
        }
        console.log()
        var location = {
            Lat: 0.0,
            Lng: 0.0
        }
        var userData = {
            email: $scope.user.email,
            companyName: $scope.user.company,
            password: $scope.user.password,
            address: $scope.user.streetnumber + ", " + $scope.user.streetname + ", " + $scope.user.postal + ", " + $scope.user.city + ", " + $scope.user.province + ", " + $scope.user.country,
            telephone: $scope.user.personalphone,
            Location: location
        }
        dataService.getVendors().save(userData, function (resp, headers) {
            //success callback
            $state.go("login");
            console.log(resp);
        },
            function(err){
            // error callback
            console.log(err);
        });
    }
}]);
