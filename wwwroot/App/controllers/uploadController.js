angular.module('imageApp').controller('uploadController', ['Upload', '$route', function (Upload, $route) {
  console.log($route)
  var vm = this;
  vm.submit = function () {
    if (vm.upload_form.file.$valid && vm.file) {
      vm.upload(vm.file);
    }
  }
  vm.upload = function (file) {
    Upload.upload({
      url: '/image',
      data: { file: file }
    }).then(function (res) {
      var user = JSON.parse(localStorage.getItem('rentUser'));
      user.image = res.data.fileName;
      localStorage.setItem('rentUser', JSON.stringify(user))
      location.reload();
    }, function (err) {
      console.error(err);
    });
  };
}])