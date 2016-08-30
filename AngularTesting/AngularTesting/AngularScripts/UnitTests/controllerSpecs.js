/// <reference path="~/Scripts/jasmine.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/angular-mocks.js" />


/// <reference path="../app.js" />
/// <reference path="../controller.js" />

"use strict";
describe("cityCtrl", function () {
    
    beforeEach(module("angularApp.Controller"));

    describe("City Controller", function () {

        var scope, city, controller;

        beforeEach(inject(function ($rootScope, $controller) {
            scope = $rootScope.$new();

            city = {
                query: function() {
                    return [{ country: "Test", city: "Test" }];
                }
            };

            controller = $controller("cityCtrl", { $scope: scope, City: city });
        }));

        it("page title", function () {
            expect(scope.pageTitle).toBe("Cities");
        });


        it("city count should be 1", function () {
            expect(scope.cities.length).toBe(1);
        });

    });

});