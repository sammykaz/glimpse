
describe('Analysis Controller', function () {
    var $controller, $rootScope, $q, dataService, $scope, getDeferred, getAuthorizedataDeferred, updateDeferred, httpBackend;

    dataService = {
        GetAuthorizeData: function () {
            getAuthorizedataDeferred = $q.defer();
            return getAuthorizedataDeferred.promise;
        },
        getPromotionClicks: function () {
            return {
                get: function () {
                    getDeferred = $q.defer();
                    return {
                        $promise: getDeferred.promise
                    };
                }
            }
        }
    };
    beforeEach(angular.mock.module('myApp'));


    beforeEach(angular.mock.inject(function (_$controller_, _$rootScope_, _$q_, $httpBackend) {
        httpBackend = $httpBackend;
        $controller = _$controller_;
        $rootScope = _$rootScope_;
        $q = _$q_;
    }));

    afterEach(function () {
        httpBackend.verifyNoOutstandingExpectation();
        httpBackend.verifyNoOutstandingRequest();
    });

   

    beforeEach(angular.mock.inject(function ($controller) {
        $scope = $rootScope.$new();

        spyOn(dataService, 'GetAuthorizeData').and.callThrough();
        spyOn(dataService, 'getPromotionClicks').and.callThrough();

        $controller('ProfileController', {
            '$scope': $scope,
            'dataService': dataService,
            '$state': {},
            'authenticationService': {}
        });
    }));

    it('should have the noPromotionClicked to set as false initially', function () {
        var scope = $rootScope.$new();
        var controller = $controller('analysisController', { $scope: scope });
        expect(scope.noPromotionClicked).toBe(false);
    });

    it('should run the Test to get the link data from the go backend', function () {
        var scope = $rootScope.$new();
        var controller = $controller('analysisController', { $scope: scope });
        scope.urlToScrape = 'success.com';

        httpBackend.expect('GET', '/api/promotionclicks')
            .respond({
                "success": true
            });

        // have to use $apply to trigger the $digest which will
        // take care of the HTTP request
        scope.$apply(function () {
        });

        httpBackend.flush();

    });


    describe('GetAuthorizeData.query', function () {

        it('should call the GetAuthorizeData method', function () {
            expect(dataService.GetAuthorizeData).toHaveBeenCalled();
        });
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