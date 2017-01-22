'use strict';

app.controller('ProfileController', ['$scope', 'dataService', '$state', 'authenticationService', function ($scope, dataService, $state, authenticationService) {
    var VendorId;
    $scope.data = "";
    $scope.editOn = false;
    dataService.GetAuthorizeData().then(function (data) {
        debugger;
        VendorId = data;
        console.log(data);
        $scope.email = localStorage.email;
        $scope.company = localStorage.company;
        $scope.address = localStorage.address;
        $scope.tel = localStorage.tel;
    },function (error) {
        console.log("No longer logged in");
        alert("You have been logged out due to session timeout");
    })

    $scope.edit = function () {
        if ($scope.editOn == true) {
            $scope.editOn = false;
        } else {
            $scope.editOn = true;
        }
      
    }
    $scope.save = function () {
        var profileInfo = {
            "email": $scope.email,
            "address": $scope.address,
            "tel": $scope.tel,
            "company": $scope.company,
            "id": localStorage.id
        }


        dataService.updateVendorDetails().update({
            VendorId: VendorId
        }, profileInfo).$promise.then(function (data) {
            debugger;
            console.log(data);
        }).catch(function (err) {
            console.log(err);
        });
    }

}]);