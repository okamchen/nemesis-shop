angular.module('nemesisApp').controller('userController',
  function ($scope, $mdSidenav, $rootScope, $location, $http, $mdDialog) {

    $scope.users = [{ login: "admin" }, { login: 'teste' }]

  });