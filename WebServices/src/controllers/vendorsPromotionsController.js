'use strict';

app.controller('vendorsPromotionsController', ['$scope', 'dataService', '$state', '$uibModal', function ($scope, dataService, $state, $uibModal) {
    $scope.data = "";

    dataService.GetAuthorizeData().then(function (data) {
        console.log("Authorized");
    }, function (error) {
        console.log("No longer logged in");
        alert("You have been logged out due to session timeout");
    })

    var promotionsquery = dataService.getAllPromotionFromSpecificVendor(localStorage.id).query();
    promotionsquery.$promise.then(function (data) {
        $scope.mypromotions = data;
    }, function (error) {
        console.log("Error: Could not load promotions");
    })
  
    
}]);
