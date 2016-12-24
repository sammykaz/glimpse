'use strict';

var app = angular.module('myApp', ['ui.router', 'ngRoute', 'ui.bootstrap', 'ngResource', 'blockUI', 'LocalStorageModule', 'ngMap']);

app.config(function ($stateProvider, $urlRouterProvider, $qProvider, $locationProvider) {

    $qProvider.errorOnUnhandledRejections(false);
    $urlRouterProvider.otherwise('/login');
    $stateProvider
        .state('login', {
            url: '/login',
            controller: 'LoginController',
            templateUrl: 'src/views/loginView.html'
        })
        .state('signup', {
            url: '/signup',
            controller: 'SignupController',
            templateUrl: 'src/views/signupView.html'
        })
        .state('home', {
            url: '/home',
            controller: 'PromotionController',
            templateUrl: 'src/views/homeView.html'
        })
        .state('home.viewPromotion', {
            url: '/promotions',
            controller: 'PromotionController',
            templateUrl: 'src/views/viewPromotion.html'
        })
        .state('home.createPromotion', {
            url: '/create-promotions',
            controller: 'PromotionController',
            templateUrl: 'src/views/createPromotion.html'
        })
        .state('map', {
            url: '/map',
            controller: 'mapController',
            templateUrl: 'src/views/mapView.html'
        })
})

.controller('appController', function ($scope) {
    $scope.test = "test";
})

.config(function ($httpProvider) {
    var interceptor = function (userService, $q, $location)
    {
        return {
            request: function (config) {
                var currentUser = userService.GetCurrentUser();
                if (currentUser != null) {
                    config.headers['Authorization'] = 'Bearer ' + currentUser.access_token;
                }
                return config;
            },
            responseError : function(rejection)
            {
                if (rejection.status === 401) {
                    console.log("$location.path('/home')");
                    return $q.reject(rejection);
                }
                if (rejection.status === 403) {
                    $location.path('/unauthorized');
                    return $q.reject(rejection);
                }
                return $q.reject(rejection);
            }

        }
    }
    var params = ['userService', '$q', '$location'];
    interceptor.$inject = params;
    $httpProvider.interceptors.push(interceptor);
});


