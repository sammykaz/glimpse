'use strict';

var app = angular.module('myApp', ['ui.router', 'ngRoute', 'ui.bootstrap', 'ngResource', 'blockUI', 'LocalStorageModule']);

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
            controller: 'homeController',
            templateUrl: 'src/views/homeView.html'
        })
})


.factory('userService1', function () {
    var fac = {};
    fac.CurrentUser = null;
    fac.SetCurrentUser = function (user) {
        fac.CurrentUser = user;
        sessionStorage.user = angular.toJson(user);
    }
    fac.GetCurrentUser = function () {
        fac.CurrentUser = angular.fromJson(sessionStorage.user);
        return fac.CurrentUser;
    }
    return fac;
})


.controller('appController', function ($scope) {
    $scope.test = "test";
});

app.config(function ($httpProvider) {
    var interceptor = function(userService1, $q, $location)
    {
        return {
            request: function (config) {
                var currentUser = userService1.GetCurrentUser();
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
    var params = ['userService1', '$q', '$location'];
    interceptor.$inject = params;
    $httpProvider.interceptors.push(interceptor);
});


