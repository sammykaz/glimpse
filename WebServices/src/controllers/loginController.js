'use strict';

app.controller('LoginController', ['$scope', '$http', 'blockUI', 'authenticationService', '$state', function ($scope, $http, blockUI, authenticationService, $state) {
    $scope.user;
    $scope.test = "test from login";
    //$scope.users = userService.getUsers().query();

    
    //$scope.login = function () {
    //    authService.login($scope.user).then(function (response) {
    //        $state.go("home");
    //    },
    //     function (err) {
    //         $scope.message = err.error_description;
    //     });
    //};

    $scope.login = function () {
        authenticationService.login($scope.user).then(function (data) {
            console.log(data);
            $state.go("home.viewPromotions");
        }, function (error) {
            $scope.message = error.error_description;
            console.log($scope.message);
        })
    }

}]);
