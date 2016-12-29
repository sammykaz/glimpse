'use strict';

app.controller('PromotionController', ['$scope', 'dataService', '$state', '$uibModal', function ($scope, dataService, $state, $uibModal) {
    $scope.data = "";
    
    $scope.promotions = dataService.getPromotions().query();
    console.log($scope.promotions);
    dataService.GetAuthorizeData().then(function (data) {
        console.log("Authorized");
    }, function (error) {
        console.log("No longer logged in");
        //$state.go("login");
    })

    $scope.showCreatePromotionModal = function () {
        $uibModal.open({
            templateUrl: '/src/views/createPromotion.html',
            controller: 'modalController',
            size: 'lg',
            scope: $scope
        }).result.then(function (result) {
            console.log(result);
        }, function () {
            console.log("Modal dismissed");
        });
    }
   
}]);

app.controller('modalController', function ($scope, $uibModalInstance, Upload, $timeout, dataService, categories) {
    $scope.promotionTitle = '';
    $scope.categories = undefined;
    $scope.description = '';
    $scope.startDay = undefined;
    $scope.endDay = undefined;
    $scope.showDateWarning = false;
    $scope.isResetEnable = false;
    $scope.ok = function () {
        if ($scope.sdt > $scope.edt)
            $scope.showDateWarning = true;
        else {
            var categories1 = [];
            categories1.push(3);
            //var categories1 = {0:1, 1:3};
            var image = $scope.picFile;
            var sdt = $scope.sdt;
            var edt = $scope.edt;
            var promotionData = {
                vendorId: "37",//localStorage.id,
                title: $scope.promotionTitle,
                description: $scope.promotionDescription,
                category: categories1,
                promotionStartDate: sdt,
                promotionEndDate: edt,
                promotionImage: image
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

    //Object.prototype.getKeyByValue = function( value ) {
    //    for( var prop in this ) {
    //        if( this.hasOwnProperty( prop ) ) {
    //            if( this[ prop ] === value )
    //                return prop;
    //        }
    //    }
    //}

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

    $scope.$watch('picFile', function() {
        if(!!$scope.picFile){
            debugger;
            imageFile = $scope.picFile;
            $scope.previewImage = imageFile;
        }
    });
    
    var getPromotions = function () {
        var promotions = [];
        var categories = $scope.categories;
        if (categories != null) {
            for (var key in categories) {
                if (categories.hasOwnProperty(key)) {
                    if (categories[key] == true) {
                        promotions.push(key);
                    }
                }
            }
        }
        return promotions;
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
                    this.gamma(100);
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
    $scope.today();

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