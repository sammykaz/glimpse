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
            //$scope.promotions.push(result);
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
                edit : true
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



app.controller('modalController', function ($scope, $uibModalInstance, Upload, $timeout, dataService, $http, promotionDetails, $q, edit) {
    $scope.edit = edit;
    $scope.promotionTitle = promotionDetails.Title || '';
    $scope.category = promotionDetails.Category || undefined;
    $scope.description = promotionDetails.Description || '';
    $scope.promotionDescription = promotionDetails.Description || '';
    $scope.startDay = promotionDetails.PromotionStartDate || undefined;
    $scope.endDay = promotionDetails.PromotionEndDate || undefined;
    $scope.sdt = new Date($scope.startDay);
    $scope.edt = new Date($scope.endDay);
    $scope.showDateWarning = false;
    $scope.showTitleWarning = false;
    $scope.showDescriptionWarning = false;
    $scope.showCategorynWarning = false;
    $scope.showImageWarning = false;
    $scope.isResetEnable = false;
    $scope.previewImage = promotionDetails.PromotionImage ? "data:image/JPEG;base64," + promotionDetails.PromotionImage : '';

    function getImageData() {

        var defer = $q.defer();
        var imageData = null;
        if (typeof $scope.previewImage === 'object') {
            Upload.base64DataUrl($scope.previewImage).then(function (base64) {
                imageData = base64;
                defer.resolve(imageData);
            }).catch(function () {
                defer.resolve(imageData);
            });
        } else {
            imageData = $scope.previewImage;
            defer.resolve(imageData);
        }

        return defer.promise;
    }

    $scope.ok = function () {
        if ($scope.sdt > $scope.edt)
            $scope.showDateWarning = true;
        else if ($scope.promotionTitle.length == 0) {
            $scope.showDateWarning = false;
            $scope.showTitleWarning = true;
        }
        else if ($scope.promotionDescription.length == 0) {
            $scope.showTitleWarning = false;
            $scope.showDescriptionWarning = true;
        }
        else if ($scope.category == undefined) {
            $scope.showDescriptionWarning = false;
            $scope.showCategorynWarning = true;
        }
        else if ($scope.previewImage == null || $scope.previewImage.length == 0) {
            $scope.showCategorynWarning = false;
            $scope.showImageWarning = true;
        }
        
        else {
            var isEditMode = !(angular.equals({}, promotionDetails));
            var image = {
                file: $scope.previewImage
            };
            var formdata = new FormData();
            formdata.append("img", image);
            var sdt = $scope.sdt;
            var edt = $scope.edt;
            var promotionData = {
                title: $scope.promotionTitle,
                description: $scope.promotionDescription,
                category: $scope.category,
                promotionStartDate: sdt,
                promotionEndDate: edt
            }

            if (isEditMode) {
                promotionData["vendorId"] = promotionDetails.VendorId;
            } else {
                promotionData["vendorId"] = localStorage.id;
            }

            if (!!$scope.previewImage) {
                getImageData().then(function (imageBased64) {
                    promotionData["promotionImage"] = imageBased64.split(",")[1];

                    if (isEditMode) {
                        onEditClick(promotionDetails.PromotionId, promotionData);
                    } else {
                        onSaveClick();
                    }

                });
            } else {
                promotionData["promotionImage"] = null;
                if (isEditMode) {
                    onEditClick(promotionDetails.PromotionId, promotionData);
                } else {
                    onSaveClick();
                }
            }

            function onSaveClick() {

                dataService.getPromotions().save(promotionData, function (resp, headers) {
                    $uibModalInstance.close(promotionData);
                },
                function (err) {
                });

            }

            function onEditClick(promotionId, promotion) {
                var promotionData = {};
                promotionData["Category"] = promotion.category || '';
                promotionData["Description"] = promotion.description || '';
                promotionData["PromotionEndDate"] = promotion["promotionEndDate"];
                promotionData["PromotionId"] = promotionId;
                promotionData["PromotionStartDate"] = promotion["promotionStartDate"];
                promotionData["PromotionImage"] = promotion["promotionImage"];
                promotionData["Title"] = promotion["title"];
                promotionData["Vendor"] = promotion["vendor"] || null;
                promotionData["VendorId"] = promotion["vendorId"];

                dataService.updatePromotion().update({
                    promotion: promotionId
                }, promotionData).$promise.then(function () {
                    $uibModalInstance.close(promotionData);
                }).catch(function (err) {
                    console.log(err);
                    $uibModalInstance.close({});
                });
            }
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

    $scope.closeWarning = function (warning) {
        switch (warning) {
            case 0:
                $scope.showDateWarning = false;
                break;
            case 1:
                $scope.showTitleWarning = false;
                break;
            case 2:
                $scope.showDescriptionWarning = false;
                break;
            case 3:
                $scope.showCategorynWarning = false;
                break;
            case 4:
                $scope.showImageWarning = false;
                break;
            default:
                break;
        }
    }

    $scope.croppedDataUrl = '';
    $scope.isCropImageEnable = false;
    $scope.saveCrop = false;
    var imageFile = '';

    $scope.$watch('picFile', function () {
        if (!!$scope.picFile) {
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
        $scope.isCropImageEnable = true;
        $scope.saveCrop = true;
    }

    $scope.doneCrop = function () {
        $scope.isCropImageEnable = false;
        $scope.saveCrop = false;
        $scope.previewImage = $scope.croppedDataUrl;
    }

    $scope.cancelCrop = function () {
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
    }
    $scope.resetFilter = function () {
        $scope.isResetEnable = false;
        $scope.selectedFilter = 'Apply Filters';
        Caman("#previewImage", function () {
            this.reset();
        });
    }
    $scope.saveFilter = function () {
        $scope.isResetEnable = false;
        Caman("#previewImage", function() {
            var imageBase64data = this.toBase64('jpeg');
            $scope.$apply(function() {
                $scope.previewImage = imageBase64data;
            });
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

    $scope.closeWarning = function (warning) {
        switch (warning) {
            case 0:
                $scope.showDateWarning = false;
                break;
            case 1:
                $scope.showTitleWarning = false;
                break;
            case 2:
                $scope.showDescriptionWarning = false;
                break;
            case 3:
                $scope.showCategorynWarning = false;
                break;
            case 4:
                $scope.showImageWarning = false;
                break;
            default:
                break;
        }
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
