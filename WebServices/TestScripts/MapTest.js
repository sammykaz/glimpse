
describe('modalController', function () {
    var $controller, $rootScope;
    beforeEach(angular.mock.module('myApp'));

    beforeEach(angular.mock.inject(function (_$controller_, _$rootScope_) {
        $controller = _$controller_;
        $rootScope = _$rootScope_;
    }));

    it('should have the google maps url initialized', function () {
        var scope = $rootScope.$new();
        var controller = $controller('mapController', { $scope: scope });
        expect(scope.googleMapsUrl).toBe("https://maps.googleapis.com/maps/api/js?key=AIzaSyDCezs9lCKQtmKlP8mm_fBWZt25DlXoUjg&callback=initMap");
    });
});