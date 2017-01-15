'use strict';
app.controller('analysisController', ['$scope', 'dataService', function ($scope, dataService) {

    $scope.data = [];
    $scope.totalClicked = [];
    $scope.vendorPromotionsClicked = [];
    $scope.series = [];
    $scope.seriesTitle = [];
    $scope.noPromotionClicked = false;
    var promotionsquery = dataService.getAllPromotionFromSpecificVendor(localStorage.id).query();
    promotionsquery.$promise.then(function (data) {
        $scope.promotions = data;
        getPromotionClicks();
    }, function (error) {
        console.log("Error: Could not load promotions");
    })

    var getPromotionClicks = function () {
        var promotionClicksquery = dataService.getPromotionClicks().query();
        promotionClicksquery.$promise.then(function (data) {
            $scope.promotionClicks = data;
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
                            $scope.data[indexSerie][0]++;
                            break;
                        case $scope.labels[1]:
                            $scope.data[indexSerie][1]++;
                            break;
                        case $scope.labels[2]:
                            $scope.data[indexSerie][2]++;
                            break;
                        case $scope.labels[3]:
                            $scope.data[indexSerie][3]++;
                            break;
                        case $scope.labels[4]:
                            $scope.data[indexSerie][4]++;
                            break;
                        case $scope.labels[5]:
                            $scope.data[indexSerie][5]++;
                            break;
                        case $scope.labels[6]:
                            $scope.data[indexSerie][6]++;
                            break;
                        default:
                            break;
                    }
                }
            })
        })

        angular.forEach($scope.data, function (element, index) {
            angular.forEach(element, function (element1, index1) {
                console.log(element1);
                $scope.totalClicked[index] += element1;
            })
        })
        console.log($scope.totalClicked);
        
    }

    var today = new Date();
    $scope.labels = [today.getDate() - 6, today.getDate() - 5, today.getDate() - 4, today.getDate() - 3, today.getDate() - 2, today.getDate() - 1, today.getDate()];

    var initializeData = function () {
        angular.forEach($scope.series, function (serie, indexSerie) {
            $scope.data[indexSerie] = [0, 0, 0, 0, 0, 0, 0]
            $scope.totalClicked[indexSerie] = 0;
        })
    }

    $scope.onClick = function (points, evt) {
        console.log(points, evt);
    };
    $scope.datasetOverride = [{ yAxisID: 'y-axis-1' }];
    $scope.chartLineOptions = {
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

    $scope.chartPieOptions = {
        legend: {
            display: true,
            position: 'right',
            labels: {
                boxWidth: 40,
            }
        }
       
    };
}]);
