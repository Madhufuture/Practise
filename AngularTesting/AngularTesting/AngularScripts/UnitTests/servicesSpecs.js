/// <reference path="~/Scripts/jasmine.js" />
/// <reference path="~/Scripts/angular.js" />
/// <reference path="~/Scripts/angular-mocks.js" />


/// <reference path="../app.js" />
/// <reference path="../service.js" />

"use strict";
describe("City", function () {

    beforeEach(module("angularApp.Service"));

    describe("City Service", function () {

        var city;

        beforeEach(inject(function ($injector) {
            city = $injector.get('City');
        }));

        it('should return 3 cities when querying', function () {
            expect(city.query().length).toBe(3);
        });

        it('should add one more city and return 4 cities', function () {
            city.add({ country: "South Africa", city: "Cape Town" });
            expect(city.query().length).toBe(4);
        });

    });
});
