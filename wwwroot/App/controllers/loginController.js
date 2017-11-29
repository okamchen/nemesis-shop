angular.module('nemesisApp').controller('loginController', function ($location, $scope, $http, $rootScope) {

    $scope.login = function () {
        $http.post('/token', { login: $scope.name, password: $scope.password })
            .then(res => {
                localStorage.setItem('nemesisToken', res.data);
                location.reload();
            }).catch(err => {
                $scope.error = 'Credencias inválidas.'
            });
    }
});