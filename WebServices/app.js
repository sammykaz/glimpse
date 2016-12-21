'use strict';

var app = angular.module('myApp', ['ui.router', 'ngRoute', 'ui.bootstrap', 'ngResource', 'blockUI']);

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
.controller('appController', function ($scope){
    $scope.test = "test";
});
