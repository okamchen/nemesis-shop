angular.module('imageApp').controller('mainController',
  function ($scope, $mdSidenav, $rootScope, $location, $http, $mdDialog) {

    $scope.logout = function () {
      localStorage.removeItem('imageToken');
      location.reload();
    }
  });