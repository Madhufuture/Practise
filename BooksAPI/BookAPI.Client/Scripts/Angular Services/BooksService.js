//"use strict";

var angularModule = angular.module('bookModule.service', []);

angularModule.factory('bookService', function ($http) {

    var bookDetails = {};
    bookDetails.getBooks = function () {

        return $http({
            method: 'GET',
            url: 'http://localhost/BookAPI/api/books'
        });
    }
    return bookDetails;
});


//(function () {
//    var app = angular.module('app');

//    app.factory('bookService', function ($http, $q) {
//        return {
//            getBooks: function () {
//                var deffered = $q.defer();

//                $http({
//                    method: 'GET',
//                    URL: 'http://localhost/BookAPI/api/books'
//                }).then(function success(response) {
//                    deffered.resolve(response);
//                }, function error(response) {
//                    deffered.reject(response);
//                });

//                return deffered.promise;
//            }
//        }
//    });

//})();