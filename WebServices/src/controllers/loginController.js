'use strict';

app.controller('LoginController', ['$scope', '$http', 'userService','blockUI', function ($scope, $http, userService, blockUI) {
    $scope.user;
    $scope.test = "test from login";
    $scope.users = userService.getUsers().query();
 
    console.log(userService.getUsers().get({ UserId: 13 }));
    console.log(userService.getVendors().query());
    //$scope.login = function () {
    //    blockUI.start();
    //    var data = userService.getUsers().get({ UserId: 13 },
    //        function (response, headers) {
    //            console.log(response);
    //            if ($scope.user.username == data.FirstName && $scope.user.password == data.Password) {
    //                alert("Login");
    //            }
    //        },
    //        function(error){
    //            console.log(error);
    //        });
    //    blockUI.stop();
    //}

    $scope.login = function () {
        //var userData = { grant_type: "password", userName: $scope.user.username };

        //userService.login().loginUser(userData, function (data) {
        //    $scope.token = data.access_token;
        //}),
        //function (error) {
        //    console.log(error);
        //}
        var userLogin = {
            grant_type: 'password',
            username: $scope.user.username,
            password: $scope.user.password
        };

        var promiselogin = userService.login(userLogin);

        promiselogin.then(function (resp) {
            //Store the token information in the SessionStorage
            //So that it can be accessed for other views
            $scope.token = resp.access_token;
        }, function (err) {
            $scope.responseData = "Error " + err.status;
        });

    }

    //console.log(loginService.get({ userId: 2 }));
    //loginService.delete({ UserId: 9 });
    //$scope.add = function () {
    //loginService.save({ Email: "test@hotmail.com", FirstName: "Test", IsVendor: false, LastName: "1", Password: "sad", UserName: "Jonny" });
    //}
}]);
