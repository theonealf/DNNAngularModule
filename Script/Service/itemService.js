console.log("itemService.js");
(function () {
    "use strict";

    angular
        .module("itemApp")
        .factory("itemService", itemService);

    itemService.$inject = ["$http", "serviceRoot"];

    function itemService($http, serviceRoot) {

        var urlBase = serviceRoot + "item/";
        var service = {};
        service.getAllItems = getAllItems;
        service.updateItem = updateItem;
        service.newItem = newItem;
        service.deleteItem = deleteItem;
        service.reorderItems = reorderItems;

        function getAllItems() {
            return $http.get(urlBase + "list");
        };

        function updateItem(item) {
            return $http.post(urlBase + "edit", item);
        }

        function newItem(item) {
            return $http.post(urlBase + "new", item);
        }

        function deleteItem(item) {
            return $http.post(urlBase + "delete", item);
        }
        function reorderItems(sortItems) {
            return $http.post(urlBase + "reorder", sortItems);
        }

        return service;
    }
})();