'use strict';
app.controller('analysisController', ['$scope', 'dataService', function ($scope, dataService) {

    $scope.data = [];
    $scope.vendorPromotionsClicked = [];
    $scope.series = [];
    $scope.seriesTitle = [];
    $scope.noPromotionClicked = false;
    var promotionsquery = dataService.getAllPromotionFromSpecificVendor(localStorage.id).query();
    promotionsquery.$promise.then(function (data) {
        $scope.promotions = data;
        console.log(data);
        getPromotionClicks();
    }, function (error) {
        console.log("Error: Could not load promotions");
    })

    var getPromotionClicks = function () {
        var promotionClicksquery = dataService.getPromotionClicks().query();
        promotionClicksquery.$promise.then(function (data) {
            $scope.promotionClicks = data;
            console.log(data);
            getVendorPromotionsClicked();
        }, function (error) {
            console.log("Error: Could not load promotions");
        })
    }
    
    var getVendorPromotionsClicked = function () {
        angular.forEach($scope.promotions, function (element, index) {
            angular.forEach($scope.promotionClicks, function(element1, index1){
                if (element.PromotionId == element1.PromotionId) {
                    element1.title = element.Title;
                    $scope.vendorPromotionsClicked.push(element1);
                    var newDate = new Date(element1.Time);
                }
            })
        });
        console.log($scope.vendorPromotionsClicked);
        if ($scope.vendorPromotionsClicked.length == 0) {
            $scope.noPromotionClicked = true;
        }
        insertData();
    }
    
    var insertData = function () {
        angular.forEach($scope.vendorPromotionsClicked, function (element, index) {
            if ($scope.series.indexOf(element.PromotionId) == -1) {
                $scope.series.push(element.PromotionId);
                $scope.seriesTitle.push(element.title);
            }
        })
        initializeData();
        angular.forEach($scope.series, function (serie, indexSerie) {
            angular.forEach($scope.vendorPromotionsClicked, function (elementClicked, indexClicked) {
                if (serie == elementClicked.PromotionId) {
                    var newDate = new Date(elementClicked.Time);
                    var date = newDate.getDate();
                    switch(date) {
                        case $scope.labels[0]:
                            console.log("Clicked 6 days ago");
                            $scope.data[indexSerie][0]++;
                            break;
                        case $scope.labels[1]:
                            console.log("Clicked 5 days ago");
                            $scope.data[indexSerie][1]++;
                            break;
                        case $scope.labels[2]:
                            console.log("Clicked 4 days ago");
                            $scope.data[indexSerie][2]++;
                            break;
                        case $scope.labels[3]:
                            console.log("Clicked 3 days ago");
                            $scope.data[indexSerie][3]++;
                            break;
                        case $scope.labels[4]:
                            console.log("Clicked 2 days ago");
                            $scope.data[indexSerie][4]++;
                            break;
                        case $scope.labels[5]:
                            console.log("Clicked 1 days ago");
                            $scope.data[indexSerie][5]++;
                            break;
                        case $scope.labels[6]:
                            console.log("Clicked today ago");
                            $scope.data[indexSerie][6]++;
                            break;
                        default:
                            break;
                    }
                }
            })
        })
    }

    var today = new Date();
    $scope.labels = [today.getDate() - 6, today.getDate() - 5, today.getDate() - 4, today.getDate() - 3, today.getDate() - 2, today.getDate() - 1, today.getDate()];

    var initializeData = function () {
        angular.forEach($scope.series, function (serie, indexSerie) {
            $scope.data[indexSerie] = [0,0,0,0,0,0,0]
        })
    }

    $scope.onClick = function (points, evt) {
        console.log(points, evt);
    };
    $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }];
    $scope.options = {
        scales: {
            yAxes: [
              {
                  id: 'y-axis-1',
                  type: 'linear',
                  display: true,
                  position: 'left'
              }
            ]
        },
        legend: {display: true}
         
    };
}]);
