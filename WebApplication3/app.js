'use strict';

var app = angular.module('myApp', ['ui.router', 'ngRoute', 'ui.bootstrap', 'ngResource']);

app.config(function ($stateProvider, $urlRouterProvider) {
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
})
.controller('appController', function ($scope){
    $scope.test = "test";
});
