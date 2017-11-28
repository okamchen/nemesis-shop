angular.module('imageApp').controller('imageController', function ($scope, $rootScope, $http, $mdDialog) {

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