describe('PromotionController', function () {
    var $controller, $rootScope;
    beforeEach(angular.mock.module('myApp'));

    beforeEach(angular.mock.inject(function (_$controller_, _$rootScope_) {
        $controller = _$controller_;
        $rootScope = _$rootScope_;
    }));

    it('should test expired date', function () {
        var scope = $rootScope.$new();
        var controller = $controller('PromotionController', {
            $scope: scope,
            $state: {},
            $uibModal: {}
        });


        expect(scope.isPromotionExpired(new Date("01-02-2015"))).toBe(true);
        expect(scope.isPromotionExpired(new Date("21-02-2018"))).toBe(false);
    });


    it('should have defined editPromotion method', function () {
        var scope = $rootScope.$new();
        var controller = $controller('PromotionController', {
            $scope: scope,
            $state: {},
            $uibModal: {}
        });
        expect(scope.editPromotion).toBeDefined();
    });

    it('should have defined deletePromotion method', function () {
        var scope = $rootScope.$new();
        var controller = $controller('PromotionController', {
            $scope: scope,
            $state: {},
            $uibModal: {}
        });
        expect(scope.deletePromotion).toBeDefined();
    });

});