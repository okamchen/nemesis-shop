angular.module('imageApp').controller('loginController', function ($location, $scope, $http, $rootScope) {
  $scope.login = function () {
    $http.post('/token', { name: $scope.name, password: $scope.password })
      .then(res => {
        localStorage.setItem('imageToken', res.data);
        location.reload();
      });
  }
});