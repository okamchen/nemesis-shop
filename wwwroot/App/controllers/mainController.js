angular.module('nemesisApp').controller('mainController',
  function ($scope, $mdSidenav, $rootScope, $location, $http, $mdDialog) {

    $scope.toggleLeft = buildToggler('left');

    $scope.email = $rootScope.user ? $rootScope.user.email : '';
    $scope.image = $rootScope.user && $rootScope.user.image ? 'image/' + $rootScope.user.image : 'img/menu.svg'

    $scope.init = function () {
      if ($location.path() != '/login') {
        var menu = $scope.menus[0];
        $location.path(menu.location);
        $scope.currentMenu = menu;
      }
    }

    $scope.menus = [
      { name: 'Categorias', location: '/category' },
      { name: 'Usuários', location: '/users' }
    ];

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
      localStorage.removeItem('rentUser');
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