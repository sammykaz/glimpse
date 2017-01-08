'use strict';

app.controller('PromotionController', ['$scope', 'dataService', '$state', '$uibModal', function ($scope, dataService, $state, $uibModal) {
    $scope.data = "";

    var promotionsquery = dataService.getPromotions().query();
    promotionsquery.$promise.then(function (data) {
        $scope.promotions = data;
        console.log(data);
    }, function (error) {
        console.log("Error: Could not load promotions");
    })
    console.log($scope.promotions);
    dataService.GetAuthorizeData().then(function (data) {
        console.log("Authorized");
    }, function (error) {
        console.log("No longer logged in");
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
                promotionDetails: {}
        }
        }).result.then(function (result) {
            console.log(result);
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
                promotionDetails: promotion
            }
        }).result.then(function (result) {
            console.log(result);
        }, function () {
            console.log("Modal dismissed");
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

    var convertCategory = function () {
        switch ($scope.promotions.Category) {
            case 0:
                $scope.promotions.Category = "Footwear"
                break;
            case 1:
                $scope.promotions.Category = "Electronics"
                break;
            case 2:
                $scope.promotions.Category = "Jewellery"
                break;
            case 3:
                $scope.promotions.Category = "Restaurants"
                break;
            case 4:
                $scope.promotions.Category = "Services"
                break;
            case 5:
                $scope.promotions.Category = "Apparel"
                break;
            default:
                break;
        }
    }
}]);



app.controller('modalController', function ($scope, $uibModalInstance, Upload, $timeout, dataService, $http, promotionDetails) {
    console.log(promotionDetails);
    $scope.promotionTitle = promotionDetails.Title || '';
    $scope.category = promotionDetails.Category || undefined;
    $scope.description = promotionDetails.Description || '';
    $scope.promotionDescription = promotionDetails.Description || '';
    $scope.startDay = promotionDetails.PromotionStartDate || undefined;
    $scope.endDay = promotionDetails.PromotionEndDate || undefined;
    $scope.sdt = new Date($scope.startDay);
    $scope.edt = new Date($scope.endDay);
    $scope.showDateWarning = false;
    $scope.isResetEnable = false;
    $scope.previewImage = promotionDetails.PromotionImage ? "data:image/JPEG;base64," + promotionDetails.PromotionImage : undefined; $scope.ok = function () {
        if ($scope.sdt > $scope.edt)
            $scope.showDateWarning = true;
        else {
            var image = { file: $scope.previewImage };
            var formdata = new FormData();
            formdata.append("img", image);
            var sdt = $scope.sdt;
            var edt = $scope.edt;
            var promotionData = {
                vendorId: "37",//localStorage.id,
                title: $scope.promotionTitle,
                description: $scope.promotionDescription,
                category: $scope.category,
                promotionStartDate: sdt,
                promotionEndDate: edt,
                //promotionImage: formdata
            }

            dataService.getPromotions().save(promotionData, function (resp, headers) {
                //success callback
                console.log(resp);
            },
            function (err) {
                console.log(err);
            });
            console.log(promotionData);
            $uibModalInstance.close("");
        }
    };

    $scope.arrayBufferToBase64 = function (buffer) {
        var binary = '';
        var bytes = new Uint8Array(buffer);
        var len = bytes.byteLength;
        for (var i = 0; i < len; i++) {
            binary += String.fromCharCode(bytes[i]);
        }
        return window.btoa(binary);
    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.closeWarning = function () {
        $scope.showDateWarning = false;
    }

    $scope.croppedDataUrl = '';
    $scope.isCropImageEnable = false;
    $scope.saveCrop = false;
    var imageFile = '';

    $scope.$watch('picFile', function () {
        if (!!$scope.picFile) {
            // debugger;
            imageFile = $scope.picFile;
            $scope.previewImage = imageFile;
        }
    });

    var getCategory = function () {
        switch ($scope.promotions.Category) {
            case 0:
                $scope.promotions.Category = "Footwear"
                break;
            case 1:
                $scope.promotions.Category = "Electronics"
                break;
            case 2:
                $scope.promotions.Category = "Jewellery"
                break;
            case 3:
                $scope.promotions.Category = "Restaurants"
                break;
            case 4:
                $scope.promotions.Category = "Services"
                break;
            case 5:
                $scope.promotions.Category = "Apparel"
                break;
            default:
                break;
        }
    }
    $scope.cropImage = function () {
        debugger;
        $scope.isCropImageEnable = true;
        $scope.saveCrop = true;
    }

    $scope.doneCrop = function () {
        debugger;
        $scope.isCropImageEnable = false;
        $scope.saveCrop = false;
        $scope.previewImage = $scope.croppedDataUrl;
    }

    $scope.cancelCrop = function () {
        debugger;
        $scope.isCropImageEnable = false;
        $scope.saveCrop = false;
    }




    $scope.selectedFilter = 'Apply Filters';
    $scope.applyFilter = function (filterType) {
        $scope.selectedFilter = filterType || 'Apply Filters';
        Caman("#previewImage", function () {
            if ($scope.isResetEnable) {
                this.reset(function () { });
            }

            switch (filterType) {
                case 'Gamma':
                    this.gamma(50);
                    break;
                case 'Greyscale':
                    this.greyscale();
                    break;
                case 'Hue':
                    this.hue(90);
                    break;
                case 'Noise':
                    this.noise(15);
                    break;
                case 'Saturation':
                    this.saturation(-30);
                    break;
                case 'Invert':
                    this.invert();
                    break;
                case 'Sepia':
                    this.sepia(50);
                    break;
                case 'Vibrance':
                    this.vibrance(15);
                    break;
                default:
                    break;
            }
            this.render(function () {
                $scope.$apply(function () {
                    $scope.isResetEnable = true;
                })
            });
            $('#previewImage').css({
                height: '100%',
                width: 'auto'
            })
        });
    }

    $scope.upload = function (dataUrl, name) {
        console.log(dataUrl);
        //Upload.upload({
        //    url: '',
        //    data: {
        //        file: Upload.dataUrltoBlob(dataUrl, name)
        //    },
        //}).then(function (response) {
        //    $timeout(function () {
        //        $scope.result = response.data;
        //    });
        //}, function (response) {
        //    if (response.status > 0) $scope.errorMsg = response.status
        //        + ': ' + response.data;
        //}, function (evt) {
        //    $scope.progress = parseInt(100.0 * evt.loaded / evt.total);
        //});
    }

    $scope.removeImage = function () {
        $scope.picFile = null;
        $scope.croppedDataUrl = null;
        $scope.isCropImageEnable = false;
        $scope.previewImage = null;
        console.log($scope.croppedDataUrl);
    }
    $scope.resetFilter = function () {
        $scope.isResetEnable = false;
        $scope.selectedFilter = 'Apply Filters';
        Caman("#previewImage", function () {
            this.reset();
        });
    }
    $scope.today = function () {
        $scope.sdt = new Date();
        $scope.edt = new Date();
    };
    if (!($scope.startDay || $scope.endDay)) {
        $scope.today();
    }
    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: new Date(),
        showWeeks: true
    };

    $scope.startDateOptions = {
        format: 'dd-MMMM-yyyy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    $scope.endDateOptions = {
        format: 'dd-MMMM-yyyy',
        maxDate: new Date(2020, 5, 22),
        minDate: $scope.sdt,
        startingDay: 1
    };

    $scope.openStartDate = function () {
        $scope.startDate.opened = true;
    };

    $scope.openEndDate = function () {
        $scope.endDate.opened = true;
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];

    $scope.startDate = {
        opened: false
    };

    $scope.endDate = {
        opened: false
    };

    function getDayClass(data) {
        var date = data.date,
          mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    }

});

app.controller('changeDateModalController', function ($scope, $uibModalInstance, promotionDetails) {

    $scope.sdt = new Date(promotionDetails.PromotionStartDate);
    $scope.edt = new Date(promotionDetails.PromotionEndDate);
    $scope.showDateWarning = false;

    $scope.done = function () {

        if ($scope.sdt > $scope.edt) {
            $scope.showDateWarning = true;

        } else {
            $uibModalInstance.close({ startDate: $scope.sdt, endDate: $scope.edt });
        }

    }

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    }

    $scope.closeWarning = function () {
        $scope.showDateWarning = false;
    }

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: new Date(),
        showWeeks: true
    };

    $scope.startDateOptions = {
        format: 'dd-MMMM-yyyy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    $scope.endDateOptions = {
        format: 'dd-MMMM-yyyy',
        maxDate: new Date(2020, 5, 22),
        minDate: $scope.sdt,
        startingDay: 1
    };

    $scope.openStartDate = function () {
        $scope.startDate.opened = true;
    };

    $scope.openEndDate = function () {
        $scope.endDate.opened = true;
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];

    $scope.startDate = {
        opened: false
    };

    $scope.endDate = {
        opened: false
    };

    function getDayClass(data) {
        var date = data.date,
			mode = data.mode;
        if (mode === 'day') {
            var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

            for (var i = 0; i < $scope.events.length; i++) {
                var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                if (dayToCheck === currentDay) {
                    return $scope.events[i].status;
                }
            }
        }

        return '';
    }

});
