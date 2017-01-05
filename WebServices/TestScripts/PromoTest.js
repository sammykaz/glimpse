
describe('modalController', function () {
    var $controller, $rootScope;
    beforeEach(angular.mock.module('myApp'));

    beforeEach(angular.mock.inject(function (_$controller_, _$rootScope_) {
        $controller = _$controller_;
        $rootScope = _$rootScope_;
    }));

    it('should have the reset filter set as false initially', function () {
        var scope = $rootScope.$new();
        var controller = $controller('modalController', { $scope: scope, $uibModalInstance: {} });
        expect(scope.isResetEnable).toBe(false);
    });

    it('should set crop enable as false initially', function () {
        var scope = $rootScope.$new();
        var controller = $controller('modalController', { $scope: scope, $uibModalInstance: {} });
        expect(scope.isCropImageEnable).toBe(false);
    });

    it('should have saveCrop set as false initially', function () {
        var scope = $rootScope.$new();
        var controller = $controller('modalController', { $scope: scope, $uibModalInstance: {} });
        expect(scope.saveCrop).toBe(false);
    });

    it('should have isCropImageEnable and saveCrop enabled when the cropImage method is called', function () {
        var scope = $rootScope.$new();
        var controller = $controller('modalController', { $scope: scope, $uibModalInstance: {} });
        scope.cropImage();
        expect(scope.saveCrop).toBe(true);
        expect(scope.isCropImageEnable).toBe(true);
    });

    it('should have isCropImageEnable and saveCrop disabled when the cancelCrop method is called', function () {
        var scope = $rootScope.$new();
        var controller = $controller('modalController', { $scope: scope, $uibModalInstance: {} });
        scope.cancelCrop();
        expect(scope.saveCrop).toBe(false);
        expect(scope.isCropImageEnable).toBe(false);
    });

    it('should have clear image and hide crop preview when the removeImage method is called', function () {
        var scope = $rootScope.$new();
        var controller = $controller('modalController', { $scope: scope, $uibModalInstance: {} });
        scope.removeImage();
        expect(scope.picFile).toBe(null);
        expect(scope.croppedDataUrl).toBe(null);
        expect(scope.picFile).toBe(null);
        expect(scope.previewImage).toBe(null);
        expect(scope.isCropImageEnable).toBe(false);
    });
    it('should have apply filter initially set to selectedFilter and when calling applyfilter its value should change to the selected parameter', function () {
        var scope = $rootScope.$new();
        var controller = $controller('modalController', { $scope: scope, $uibModalInstance: {} });
        expect(scope.selectedFilter).toBe("Apply Filters");
        scope.applyFilter('Greyscale');
        expect(scope.selectedFilter).toBe("Greyscale");
    });
});