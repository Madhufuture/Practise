//"use strict";
//(function () {
//    var app = angular.module('app');


//    app.controller('bookController', bookController);

//    bookController.$inject = ['bookService'];

//    function getBooks(bookService) {
//        var books = bookService.getBooks();

//        promise.then(function (response) {
//            this.books = response.data;
//        });

//    }

var angularModule = angular.module('bookModule.controller', []);


//Injecting the Service 'bookService'
angularModule.controller('bookController', function ($scope, bookService) {

    $scope.bookFilter = null;
    $scope.books = [];

    $scope.bookFilter = function (book) {
        var keyword = new RegExp($scope.bookTextFilter, 'i');
        return !$scope.bookTextFilter || keyword.test(book.Title);
    };

    bookService.getBooks().success(function (data,status,headers,config) {
        $scope.books = data;
    }).
    error(function (response) {
        alert(status);
    });
});

//angularModule.controller('bookController', function ($scope, $http) {
//    $http.get('http://localhost/BookAPI/api/books').
//        success(function (data, status, headers, config) {
//            $scope.books = data;
//        }).
//    error(function (data, status, headers, config) {
//        alert("error");
//    });

//});

//})();