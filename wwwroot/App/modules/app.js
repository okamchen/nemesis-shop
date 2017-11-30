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
            .when("/product", {
                templateUrl: "App/templates/product.html",
                controller: 'productController'
            })
            .otherwise("/order", {
                templateUrl: "App/templates/order.html",
                controller: 'orderController'
            });
    })
    .run(function ($rootScope, $location, $http) {
        $rootScope.user = JSON.parse(localStorage.getItem('nemesisUser'));
        if ($rootScope.user) {
            $http.defaults.headers.common['Authorization'] = 'Bearer ' + $rootScope.user.token;
            $location.path('/category');
        }
        else
            $location.path('/login');
    });