(function () {
    "use strict";

    var jsFileLocation = $('script[src*="AJ_angularModule2/Script/app"]').attr('src');  // the js file path
    jsFileLocation = jsFileLocation.replace('app.js', '');   // the js folder path
    if (jsFileLocation.indexOf('?') > -1) {
        jsFileLocation = jsFileLocation.substr(0, jsFileLocation.indexOf('?'));
    }
    angular
        .module("itemApp", ["ngRoute", "ngDialog", "ngProgress", "ui.sortable"])
        .config(function ($routeProvider) {
            $routeProvider.
                otherwise({
                    templateUrl: jsFileLocation + "Template/index.html",
                    controller: "itemController",
                    controllerAs: "vm"
                });
            ////testa lägga till andra routes med dennna kod----------------------
            //.when("/list", { templateUrl: jsFileLocation + "ClientList.html", controller: "clientController", controllerAs: "vm" })
            //    .when("/edit/:ClientId", { templateUrl: jsFileLocation + "ClientEdit.html", controller: "clientController", controllerAs: "vm" })
            //    .otherwise({ redirectTo: '/list' });
        });

})();