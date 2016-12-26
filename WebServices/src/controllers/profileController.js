'use strict';

app.controller('ProfileController', ['$scope', 'dataService', '$state', 'authenticationService', function ($scope, dataService, $state, authenticationService) {

    $scope.data = "";

    dataService.GetAuthorizeData().then(function (data) {
        console.log(data);
        $scope.data = data;
    },function (error) {
        console.log("No longer logged in");
        //$state.go("login");
    })

}]);