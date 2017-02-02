'use strict';

app.controller('PromotionController', ['$scope', 'dataService', '$state', '$uibModal', function ($scope, dataService, $state, $uibModal) {
    $scope.data = "";

    dataService.GetAuthorizeData().then(function (data) {
        console.log("Authorized");
    }, function (error) {
        console.log("No longer logged in");
        alert("You have been logged out due to session timeout");
    })

    var promotionsquery = dataService.getPromotions().query();
    promotionsquery.$promise.then(function (data) {
        //debugger;
        $scope.promotions = data;
    }, function (error) {
        console.log("Error: Could not load promotions");
    })

    $scope.isPromotionExpired = function (promotionEndDate) {
        var currentDate = new Date();
        var promotionEndDate = new Date(promotionEndDate);
        return currentDate.getTime() > promotionEndDate.getTime();
    }

    $scope.showCreatePromotion = function () {
        $uibModal.open({
            templateUrl: '/src/views/createPromotion.html',
            controller: 'modalController',
            size: 'lg',
            scope: $scope,
            resolve: {
                promotionDetails: {},
                edit: false
            }
        }).result.then(function (result) {
            //console.log(result);
            // $scope.promotions.push(result);
        }, function () {
            console.log("Modal dismissed");
        });
    }

    $scope.editPromotion = function (promotion) {
        $uibModal.open({
            templateUrl: '/src/views/createPromotion.html',
            controller: 'modalController',
            size: 'lg',
            scope: $scope,
            resolve: {
                promotionDetails: promotion,
                edit: true
            }
        }).result.then(function (updatedPromotionData) {
            $scope.promotions.forEach(function (element, index) {
                if (element.PromotionId === promotion.PromotionId) {
                    $scope.promotions[index] = updatedPromotionData;
                }
            });
        }, function () {
            console.log("Modal dismissed");
        });
    }

    $scope.deletePromotion = function (promotion, index) {
        dataService.deletePromotion().delete({
            promotion: promotion.PromotionId
        }).$promise.then(function () {
            $scope.promotions.splice(index, 1);
        });
    }

    $scope.editPromotionDate = function (promotion) {
        $uibModal.open({
            templateUrl: '/src/views/changePromotionDate.html',
            controller: 'changeDateModalController',
            size: 'lg',
            scope: $scope,
            resolve: {
                promotionDetails: promotion
            }
        }).result.then(function (result) {
            console.log(result);
            console.log($scope.promotions);
            promotion["PromotionStartDate"] = result.startDate;
            promotion["PromotionEndDate"] = result.endDate;

            dataService.updatePromotion().update({
                promotion: promotion.PromotionId
            }, promotion).$promise.then(function (user) {
                $scope.promotions.forEach(function (element, index) {
                    if (element.PromotionId === promotion.PromotionId) {
                        $scope.promotions[index].PromotionStartDate = result.startDate;
                        $scope.promotions[index].PromotionEndDate = result.endDate;
                    }
                });
            });
        }, function () {
            console.log("Modal dismissed");
        });
    }

}]);