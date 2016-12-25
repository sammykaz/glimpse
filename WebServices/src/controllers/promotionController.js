'use strict';

app.controller('PromotionController', ['$scope', 'dataService', '$state', '$uibModal', function ($scope, dataService, $state, $uibModal) {

    $scope.data = "";

    dataService.GetAuthorizeData().then(function (data) {
        console.log(data);
        $scope.data = data;
    }, function (error) {
        console.log("No longer logged in");
        //$state.go("login");
    })

    $scope.showCreatePromotionModal = function () {
        $uibModal.open({
            templateUrl: '/src/views/createPromotion.html',
            controller: 'modalController',
            scope: $scope
        }).result.then(function (result) {
            console.log(result);
        }, function () {
            console.log("Modal dismissed");
        });
    }
    $scope.myImage = 'https://x.kinja-static.com/assets/images/logos/placeholders/default.png';
    $scope.myCroppedImage = '';

    var handleFileSelect = function (evt) {
        var file = evt.currentTarget.files[0];
        var reader = new FileReader();
        reader.onload = function (evt) {
            $scope.$apply(function ($scope) {
                $scope.myImage = evt.target.result;
            });
        };
        reader.readAsDataURL(file);
    };
    angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
}]);

app.controller('modalController', function ($scope, $uibModalInstance, Upload, $timeout) {
    $scope.title = '';
    $scope.categories = undefined;
    $scope.description = '';
    $scope.startDay = undefined;
    $scope.endDay = undefined;
    $scope.showDateWarning = false;
    $scope.ok = function () {
        if ($scope.sdt > $scope.edt)
            $scope.showDateWarning = true;
        else
            $uibModalInstance.close($scope.edt);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.closeWarning = function () {
        $scope.showDateWarning = false;
    }

    $scope.uploadPic = function(file) {
        file.upload = Upload.upload({
            url: 'https://angular-file-upload-cors-srv.appspot.com/upload',
            data: {username: $scope.username, file: file},
        });

        file.upload.then(function (response) {
            $timeout(function () {
                file.result = response.data;
            });
        }, function (response) {
            if (response.status > 0)
            $scope.errorMsg = response.status + ': ' + response.data;
        }, function (evt) {
            // Math.min is to fix IE which reports 200% sometimes
            file.progress = Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
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