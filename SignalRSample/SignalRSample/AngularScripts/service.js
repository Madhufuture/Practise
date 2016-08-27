
var myService = angular.module('WorkflowModule.Service', []);
myService.value('$', $);

//myService.service('workflowSvc', function () {

//    this.signalRInit = function ($, $rootScope) {

//        return {
//            proxy: null,
//            init: function (workflowCallback) {

//                connection = $.hubConnection();
//                this.proxy = connection.createHubProxy('workflowService');

//                connection.start();

//                this.proxy.on('assignWF', function (name) {
//                    $rootScope.$apply(function () {
//                        workflowCallback(name);
//                    });
//                });
//            },
//            callSignalR: function (callback) {
//                this.proxy.invoke('assignWorkflow');
//            }

//        };

//    };
//});

myService.factory('workflowSvc', function ($, $rootScope) {
    return {
        proxy: null,
        init: function (assignWFCallback) {
            //Getting the connection object
            connection = $.hubConnection();

            //Creating proxy
            this.proxy = connection.createHubProxy('chatHub');

            //Starting connection
            //connection.start();



            //Attaching a callback to handle acceptGreet client call
            this.proxy.on('assignWF', function (name) {
                $rootScope.$apply(function () {
                    assignWFCallback(name);
                });
            });


            connection.start().done(function () {
                this.proxy.invoke('assignWorkflow');
            });

        }
        //callSignalR: function (callback) {
        //    //Invoking greetAll method defined in hub
        //    this.proxy.invoke('assignWorkflow');
        //}
    }
});