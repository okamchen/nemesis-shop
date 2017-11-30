angular.module('nemesisApp').controller('categoryController', function ($scope, $http, $mdDialog) {

  $scope.profissions = [];

  $scope.refresh = () => {
    $http.get('/api/categoria')
      .then(res => {
        $scope.categories = res.data;
      }, err => {
        console.log(err)
      });
  }

  $scope.refresh();

  $scope.remove = (profission, ev) => {
    $mdDialog.show(
      $mdDialog.confirm()
        .textContent('Excluir categoria ?')
        .ariaLabel('Excluir')
        .targetEvent(ev)
        .ok('Sim')
        .cancel('Não')
    ).then(function () {
      $http.delete('api/categoria/' + profission.id)
        .then(res => {
          $scope.refresh();
        }, err => {
          if (err.data.error) {
            $mdDialog.show(
              $mdDialog.alert()
                .clickOutsideToClose(false)
                .title(err.data.error)
                .ariaLabel('Error')
                .ok('ok')
                .targetEvent(ev));
          }
        });
    })
  }

  $scope.new = function (ev) {
    var confirm = $mdDialog.prompt()
      .placeholder('Nome da categoria')
      .ariaLabel('Nome')
      .targetEvent(ev)
      .ok('ok')
      .cancel('Cancelar');

    $mdDialog.show(confirm).then(function (name) {
      $http.post('api/categoria/', { nome: name })
        .then(response => {
          const errors = response.data.errors
          if (errors) {
            $mdDialog.show(
              $mdDialog.alert()
                .clickOutsideToClose(true)
                .title(errors[0])
                .ariaLabel('Alert Dialog Demo')
                .ok('OK')
            )
          } else
            $scope.refresh()
        });
    });
  }
});