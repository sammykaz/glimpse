'use strict';

app.controller('SignupController', ['$scope', '$http','userService', function ($scope, $http, userService) {

    $scope.user = undefined;

    $scope.createUser = function () {
        var telData = {
            personalPhoneNumber: $scope.user.personalphone,
            businessPhoneNumber: $scope.user.businessphone
        }
        var addressData = {
            PostalCode: $scope.user.postal,
            city: $scope.user.city,
            country: $scope.user.country,
            province: $scope.user.province,
            street: $scope.user.streetname,
            streetNumber: $scope.user.streetnumber
        }
        var location = {
            Lat: 0.0,
            Lng: 0.0
        }
        var userData = {
            firstname: $scope.user.firstname,
            lastname: $scope.user.lastname,
            email: $scope.user.email,
            company: $scope.user.company,
            password: $scope.user.password,
            address: addressData,
            telephone: telData,
            Location: location
        }
        userService.getVendors().save(userData, function (resp, headers) {
            //success callback
            console.log(resp);
        },
            function(err){
            // error callback
            console.log(err);
        });
    }
}]);
