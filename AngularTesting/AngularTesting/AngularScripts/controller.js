
var ctrl = angular.module('angularApp.Controller', []);

ctrl.controller('cityCtrl', ['$scope', 'City', function ($scope, City) {
    $scope.pageTitle = 'Cities';
    $scope.cities = City.query();
}]);
