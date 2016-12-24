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
}]);

app.controller('modalController', function ($scope, $uibModalInstance) {
    $scope.title = '';
    $scope.categories = undefined;
    $scope.description = '';
    $scope.startDay = undefined;
    $scope.endDay = undefined;

    $scope.ok = function () {
        $uibModalInstance.close($scope.categories);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.today = function () {
        $scope.dt = new Date();
    };
    $scope.today();

    $scope.clear = function () {
        $scope.dt = null;
    };

    $scope.inlineOptions = {
        customClass: getDayClass,
        minDate: new Date(),
        showWeeks: true
    };

    $scope.dateOptions = {
        formatYear: 'yy',
        maxDate: new Date(2020, 5, 22),
        minDate: new Date(),
        startingDay: 1
    };

    $scope.open1 = function () {
        $scope.popup1.opened = true;
    };

    $scope.open2 = function () {
        $scope.popup2.opened = true;
    };

    $scope.setDate = function (year, month, day) {
        $scope.dt = new Date(year, month, day);
    };

    $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
    $scope.format = $scope.formats[0];

    $scope.popup1 = {
        opened: false
    };

    $scope.popup2 = {
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