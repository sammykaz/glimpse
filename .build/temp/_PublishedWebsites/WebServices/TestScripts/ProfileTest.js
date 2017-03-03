
describe('ProfileController', function () {
    var $controller, $rootScope;
    beforeEach(angular.mock.module('myApp'));

    beforeEach(angular.mock.inject(function (_$controller_, _$rootScope_) {
        $controller = _$controller_;
        $rootScope = _$rootScope_;
    }));

    it('should have editOn set as false initially', function () {
        var scope = $rootScope.$new();
        var controller = $controller('ProfileController', { $scope: scope });
        expect(scope.editOn).toBe(false);
    });

    it('should have editOn set as true when edit method called ', function () {
        var scope = $rootScope.$new();
        var controller = $controller('ProfileController', { $scope: scope });
        scope.edit();
        expect(scope.editOn).toBe(true);
    });
});