
var myCtrl = angular.module('WorkflowModule.Controller', []);

myCtrl.controller('workflowController', function ($scope, workflowSvc) {

    $scope.itemList = [];

    $scope.workflowList = [{ id: 1, name: "Assign" }, { id: 2, name: "Delete" }, { id: 3, name: "Update" }];
    $scope.usersList = [{ id: 1, name: "M" }, { id: 2, name: "S" }, { id: 3, name: "R" }];


    $scope.selectedWorkflow = function (item) {
        $scope.itemList = [];
        $scope.itemList.push(item.name);
    };





    $scope.assignWorkflow = function () {

        wfSelected = function (text) {
            $scope.hubValue = text;
        };

        workflowSvc.init(wfSelected);


        // workflowSvc.callSignalR();
    };
});