'use strict';

app.controller('LoginController', ['$scope', '$http', 'blockUI', 'authenticationService', 'dataService', function ($scope, $http, blockUI, authenticationService, dataService) {
    $scope.user;
    $scope.test = "test from login";
    //$scope.users = userService.getUsers().query();

    $scope.data = "";
    
    $scope.getData = function () {
        authenticationService.login($scope.user).then(function (data) {
            console.log(data);
        }, function (error) {
            $scope.message = error.error_description;
            console.log($scope.message);
        })

        dataService.GetAuthorizeData().then(function (data) {
            $scope.data = data;
        })
    }

}]);
