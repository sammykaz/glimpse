'use strict';

app.controller('PromotionController', ['$scope', 'dataService', '$state', function ($scope, dataService, $state) {

    $scope.data = "";

    dataService.GetAuthorizeData().then(function (data) {
        console.log(data);
        $scope.data = data;
    }, function (error) {
        console.log("No longer logged in");
        //$state.go("login");
    })

}]);