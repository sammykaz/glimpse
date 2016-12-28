'use strict';

app.controller('HomeController', ['$scope', 'dataService', '$state', 'authenticationService', 'blockUI',function ($scope, dataService, $state, authenticationService, blockUI) {

    $scope.data = "";

    dataService.GetAuthorizeData().then(function (data) {
        console.log(data);
        $scope.data = data;
    },function (error) {
        console.log("No longer logged in");
        $state.go("login");
    })

    $scope.logout = function () {
        authenticationService.logout();
        $state.go("login");
    }
}]);