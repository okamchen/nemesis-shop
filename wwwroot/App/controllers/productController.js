angular.module('nemesisApp').controller('productController', function ($scope, $http, $mdDialog) {

  $scope.product = {}

  $scope.refresh = () => {
    $http.get('/api/produto')
      .then(res => {
        $scope.products = res.data
      }, err => {
        console.log(err)
      })
  }

  $scope.refresh()

  $scope.remove = (product) => {
    var confirm = $mdDialog.confirm()
      .title('Remover este produto ?')
      .textContent(product.nome)
      .ok('Sim')
      .cancel('Não');

    $mdDialog.show(confirm).then(() => {

      $http.delete('api/produto/' + product.id)
        .then((response) => {
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
        })

    })
  }

  $scope.openModal = (product) => {
    let callback = $scope.refresh
    $mdDialog.show({
      clickOutsideToClose: false,
      templateUrl: 'App/templates/editProduct.html',
      controller: function ($scope, $mdDialog, $http) {
        $scope.close = () => { $mdDialog.hide() }
        $scope.product = product ? angular.copy(product) : {}

        $http.get('/api/categoria')
          .then(res => {
            $scope.categories = res.data
          }, err => {
            console.log(err)
          })

        $scope.save = () => {
          if ($scope.product.id > 0) {
            $http.put('api/produto/', $scope.product)
              .then((response) => {
                $scope.errors = response.data.errors
                if (!$scope.errors) {
                  callback()
                  $mdDialog.hide()
                }
              });
          } else {
            $http.post('api/produto/', $scope.product)
              .then((response) => {
                $scope.errors = response.data.errors
                if (!$scope.errors) {
                  callback()
                  $mdDialog.hide()
                }
              });
          }
        }
      }
    })
  }
})