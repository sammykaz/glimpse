'use strict';

app.controller('LoginController', ['$scope', '$http', 'loginService', function ($scope, $http, loginService) {

    $scope.test = "test from login";
    $scope.users = loginService.query().$promise.then(function (data) {
        console.log(data);
    });
 
    console.log($scope.users);

    //console.log(loginService.get({ userId: 2 }));
    
    //loginService.delete({ userId: 7 });
    //$scope.add = function () {
    //loginService.save({ Email: "test@hotmail.com", FirstName: "Test", IsVendor: false, LastName: "1", Password: "sad", UserName: "Jonny" });
    //}

    //console.log(loginService.get({ FirstName: "test@hotmail.com" }));
    console.log(loginService.get({ UserId: 8 }));
}]);
