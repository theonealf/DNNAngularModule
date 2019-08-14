console.log("itemcontroller.js");
(function () {
    "use strict";

    angular
        .module("itemApp")
        .controller("itemController", itemController);

    itemController.$inject = ["$scope", "$window", "$log", "ngDialog", "ngProgress", "itemService", "userlist", "resources", "settings", "editable", "moduleId"];

    function itemController($scope, $window, $log, ngDialog, ngProgress, itemService, userlist, resources, settings, editable, moduleId) {

        var vm = this;
        vm.Items = [];
        vm.AddEditTitle = "";
        vm.EditIndex = -1;
        vm.UserList = JSON.parse(userlist);
        vm.localize = JSON.parse(resources);
        vm.settings = JSON.parse(settings);
        vm.EditMode = (editable.toLowerCase() === "true");
        vm.ModuleId = parseInt(moduleId);
        vm.Item = {};

        vm.getAllItems = getAllItems;
        vm.createUpdateItem = createUpdateItem;
        vm.deleteItem = deleteItem;
        vm.showAdd = showAdd;
        vm.showEdit = showEdit;
        vm.reset = resetItem;
        vm.userSelected = userSelected;
        vm.sortableOptions = { stop: sortStop, disabled: !vm.EditMode };

        var jsFileLocation = $('script[src*="AJ_angularModule2/Script/app"]').attr('src');  // the js file path
        jsFileLocation = jsFileLocation.replace('app.js', '');   // the js folder path
        if (jsFileLocation.indexOf('?') > -1) {
            jsFileLocation = jsFileLocation.substr(0, jsFileLocation.indexOf('?'));
        }

        resetItem();
        getAllItems();

        function getAllItems() {
            ngProgress.color('red');
            ngProgress.start();
            itemService.getAllItems()
                .success(function (response) {
                    vm.Items = response;
                    ngProgress.complete();
                })
                .error(function (errData) {
                    $log.error('failure loading items', errData);
                    ngProgress.complete();
                });
        };

        function createUpdateItem(form) {
            vm.invalidSubmitAttempt = false;
            if (form.$invalid) {
                vm.invalidSubmitAttempt = true;
                return;
            }

            if (vm.Item.ItemId > 0) {
                itemService.updateItem(vm.Item)
                    .success(function (response) {
                        if (vm.EditIndex >= 0) {
                            vm.Items[vm.EditIndex] = vm.Item;
                        }
                    })
                    .catch(function (errData) {
                        $log.error('failure saving item', errData);
                    });
            } else {
                itemService.newItem(vm.Item)
                    .success(function (response) {
                        if (response.ItemId > 0) {
                            vm.Items.push(response);
                        }
                    })
                    .error(function (errData) {
                        $log.error('failure saving new item', errData);
                    });
            }
            ngDialog.close();
        };

        function deleteItem(item, idx) {
            if (confirm('Are you sure to delete "' + item.Title + '" ?')) {
                itemService.deleteItem(item)
                    .success(function (response) {
                        vm.Items.splice(idx, 1);
                    })
                    .error(function (errData) {
                        $log.error('failure deleting item', errData);
                    });
            }
        };

        function showAdd() {
            vm.reset();
            vm.AddEditTitle = "Add Item";
            ngDialog.open({
                template: jsFileLocation + 'Templates/itemForm.html',
                className: 'ngdialog-theme-default',
                scope: $scope
            });
        };

        function showEdit(item, idx) {
            vm.Item = angular.copy(item);
            vm.EditIndex = idx;
            vm.AddEditTitle = "Edit Item: #" + item.ItemId;
            ngDialog.open({
                template: jsFileLocation + 'Templates/itemForm.html',
                className: 'ngdialog-theme-default',
                scope: $scope
            });
        };

        function resetItem() {
            vm.Item = {
                ItemId: 0,
                ModuleId: vm.ModuleId,
                Title: '',
                Description: '',
                AssignedUserId: ''
            };
        };

        function userSelected() {
            for (var i = 0; i < vm.UserList.length; i++) {
                if (vm.UserList[i].id == vm.Item.AssignedUserId) {
                    vm.Item.AssignedUserName = vm.UserList[i].text;
                }
            }
        }
        function sortStop(e, ui) {

            var sortItems = [];
            for (var index in vm.Items) {
                if (vm.Items[index].ItemId) {
                    var sortItem = { ItemId: vm.Items[index].ItemId, Sort: index };
                    vm.Items[index].Sort = index;
                    sortItems.push(sortItem);
                }
            }
            itemService.reorderItems(angular.toJson(sortItems))
                .catch(function (errData) {
                    $log.error('failure reordering items', errData.data);
                });
        };
    };
})();