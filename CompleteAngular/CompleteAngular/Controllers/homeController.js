
var app = angular.module("CompleteAngularApp.controllers", []);

app.controller("angularController", function ($scope) {
    $scope.messasge = "Yes it is from controller";
});

angularController.$inject = ['$scope'];