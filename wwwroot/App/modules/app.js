angular.module('imageApp', ['ngRoute', 'ngMaterial', 'ngFileUpload'])
    .config(function ($routeProvider, $mdThemingProvider) {
        $mdThemingProvider.theme('default').dark()
        //$mdThemingProvider.theme('default').primaryPalette('red')

        $routeProvider
            .when("/login", {
                templateUrl: "App/templates/login.html",
                controller: 'loginController'
            })
            .when("/images", {
                templateUrl: "App/templates/images.html",
                controller: 'imageController'
            })
            .otherwise("/login", {
                templateUrl: "App/templates/login.html",
                controller: 'loginController'
            });
    })
    .run(function ($rootScope, $location, $http) {
        $rootScope.token = localStorage.getItem('imageToken');
        if ($rootScope.token) {
            $http.defaults.headers.common.token = $rootScope.token;
            $location.path('/images');
        }
        else
            $location.path('/login');
    });