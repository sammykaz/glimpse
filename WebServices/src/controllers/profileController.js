'use strict';

app.controller('ProfileController', ['$scope', 'dataService', '$state', 'authenticationService', function ($scope, dataService, $state, authenticationService) {

    $scope.data = "";
    $scope.editOn = false;
    dataService.GetAuthorizeData().then(function (data) {
        console.log(data);
        $scope.data = data;
    },function (error) {
        console.log("No longer logged in");
        //$state.go("login");
    })

    $scope.edit = function () {
        if ($scope.editOn == true) {
            $scope.editOn = false;
        } else {
            $scope.editOn = true;
        }
    }

}]);