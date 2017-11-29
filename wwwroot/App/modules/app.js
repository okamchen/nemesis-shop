angular.module('nemesisApp', ['ngRoute', 'ngMaterial'])
    .config(function ($routeProvider, $mdThemingProvider) {

        $mdThemingProvider.theme('default').dark()

        $routeProvider
            .when("/login", {
                templateUrl: "App/templates/login.html",
                controller: 'loginController'
            })
            .when("/category", {
                templateUrl: "App/templates/category.html",
                controller: 'categoryController'
            })
            .when("/users", {
                templateUrl: "App/templates/user.html",
                controller: 'userController'
            })
            .otherwise("/category", {
                templateUrl: "App/templates/category.html",
                controller: 'categoryController'
            });
    })
    .run(function ($rootScope, $location, $http) {
        $rootScope.token = localStorage.getItem('nemesisToken');
        if ($rootScope.token) {
            $http.defaults.headers.common['Authorization'] = 'Bearer ' + $rootScope.token;
            $location.path('/category');
        }
        else
            $location.path('/login');
    });