
describe('modalController', function () {
    var $controller, $rootScope;
    beforeEach(angular.mock.module('myApp'));

    beforeEach(angular.mock.inject(function (_$controller_, _$rootScope_) {
        $controller = _$controller_;
        $rootScope = _$rootScope_;
    }));

    it('should have the google maps zoomed at montreal area', function () {
        var scope = $rootScope.$new();
        var controller = $controller('mapController', { $scope: scope });
        expect(scope.map.center.latitude).toBe(45.4581475);
        expect(scope.map.center.latitude).toBe(-73.64009765625);
    });
});