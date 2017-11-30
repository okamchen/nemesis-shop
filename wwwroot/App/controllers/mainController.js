angular.module('nemesisApp').controller('mainController',
  function ($scope, $mdSidenav, $rootScope, $location, $http, $mdDialog) {

    $scope.init = function () {
      if ($location.path() != '/login') {
        var menu = $scope.menus[0];
        $location.path(menu.location);
        $scope.currentMenu = menu;
      }
    }

    if (!$rootScope.user)
      return;

    $scope.toggleLeft = buildToggler('left');

    $scope.email = $rootScope.user.email;
    $scope.image = $rootScope.user.image ? 'image/' + $rootScope.user.image : 'img/menu.svg'

    $scope.menus = [];

    if ($rootScope.user.tipo.toUpperCase() == 'ADMIN') {
      $scope.menus.push({ name: 'Usuários', location: '/users' });
      $scope.menus.push({ name: 'Categorias', location: '/category' });
      $scope.menus.push({ name: 'Produtos', location: '/product' });
      $scope.menus.push({ name: 'Pedidos', location: '/order' });
    }else{
      $scope.menus.push({ name: 'Meus Pedidos', location: '/order' });
    }    

    $scope.changeLocation = function (menu) {
      $location.path(menu.location);
      $mdSidenav('left').toggle();
      $scope.currentMenu = menu;
    }

    function buildToggler(componentId) {
      return function () {
        $mdSidenav(componentId).toggle();
      };
    }

    $scope.openMenu = function ($mdOpenMenu, ev) {
      $mdOpenMenu(ev);
    };

    $scope.logout = function () {
      localStorage.removeItem('nemesisUser');
      location.reload();
    }

    $scope.openUploadImage = function (ev) {

      $mdDialog.show({
        templateUrl: '/App/templates/upload.html',
        parent: angular.element(document.body),
        targetEvent: ev,
        clickOutsideToClose: true,
        fullscreen: $scope.customFullscreen
      })
    }
  });