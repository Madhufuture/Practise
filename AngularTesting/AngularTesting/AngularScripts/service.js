

var service = angular.module('angularApp.Service', []);

service.factory('City', function () {

    var cities = [
        { country: "USA", city: "Troy" },
        { country: "Australia", city: "Melbourne" },
        {country:"India",city:"Tirupathi"}
    ];

    return {
        query: function () {
            return cities;
        },
        add: function (city) {
            cities.push(city);
        }
    };

});