'use strict';

app.controller('LoginController', ['$scope', '$http', 'blockUI', 'authenticationService', '$state', function ($scope, $http, blockUI, authenticationService, $state) {
    $scope.user;

    $scope.login = function () {
        authenticationService.login($scope.user).then(function (data) {
            console.log(data);
            $state.go("home.viewPromotion");
        }, function (error) {
            $scope.message = error.error_description;
            console.log($scope.message);
            $scope.incorrect = true;
        })
    }

}]);
