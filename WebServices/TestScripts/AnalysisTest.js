
describe('Analysis Controller', function () {
    var $controller, $rootScope;
    beforeEach(angular.mock.module('myApp'));

    beforeEach(angular.mock.inject(function (_$controller_, _$rootScope_) {
        $controller = _$controller_;
        $rootScope = _$rootScope_;
    }));

    it('should have the noPromotionClicked to set as false initially', function () {
        var scope = $rootScope.$new();
        var controller = $controller('analysisController', { $scope: scope });
        expect(scope.noPromotionClicked).toBe(false);
    });

    it('should have the labels to be defined', function () {
        var scope = $rootScope.$new();
        var controller = $controller('analysisController', { $scope: scope });
        expect(scope.labels).toBeDefined();
    });

    it('should have the series to be defined', function () {
        var scope = $rootScope.$new();
        var controller = $controller('analysisController', { $scope: scope });
        expect(scope.series).toBeDefined();
    });

    it('should have the seriesTitle to be defined', function () {
        var scope = $rootScope.$new();
        var controller = $controller('analysisController', { $scope: scope });
        expect(scope.seriesTitle).toBeDefined();
    });

    it('should have the promotionClicks toBeUndefined initially', function () {
        var scope = $rootScope.$new();
        var controller = $controller('analysisController', { $scope: scope });
        expect(scope.promotionClicks).toBeUndefined();
    });

});