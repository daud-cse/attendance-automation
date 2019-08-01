'use strict';
/* Controllers */
app.controller('FeesGenerateConfigPanelCtrl', ['$scope', '$state', '$http', 'FeesGenerateConfigService', 'modalService', '$filter', 'toaster', '$modal',
    function ($scope, $state, $http, FeesGenerateConfigService, modalService, $filter, toaster, $modal) {

        $scope.entity = { Name: "Fees Generate Config" };
        $scope.feesGenConfigModel = null;
        $scope.isLoading = true;
        $scope.workspacePanel = {};
        
        FeesGenerateConfigService.config(function (result) {
            $scope.workspaces = result.feesAutoGenTypeList;
            $scope.workspaces[0].active = true;
            $scope.isLoading = false;
        });

        var setAllInactive = function () {
            angular.forEach($scope.workspaces, function (workspace) {
                workspace.active= false;
            });
        };

        $scope.addWorkspace = function () {
            setAllInactive();
            addNewWorkspace();
        };

        $scope.editTab = function (id) {
            $scope.workspacePanel[id] = true;
        }

        $scope.saveTab = function (workType) {
            $scope.workspacePanel[workType.id] = false;
            FeesGenerateConfigService.edittype(workType, function (result) {
                toaster.pop("success", "Success", "Config type name edited.");
            });
        }

        $scope.cancelTab = function (id) {
            $scope.workspacePanel[id] = false;
        }

        //$scope.reset = function () {
        //    $scope.pagedItems[$scope.currentPage][$scope.editing] = angular.copy($scope.selected);
        //    $scope.selected = {};
        //};

        $scope.deleteTab = function (id) {
            var typeName = $filter('filter')($scope.workspaces, { id: id })[0].name;
            FeesGenerateConfigService.checktype(id, function (result1) {
                if (result1.IsActive == true) {
                    var modalOptions = {
                        closeButtonText: 'No',
                        actionButtonText: 'Yes',
                        headerText: 'Are you sure to Inactive ' + typeName + '?',
                        bodyText: 'Please Move Fees Generate Config List of ' + typeName + ' to another Config Type or Disable those',
                        type: 'danger'
                    };

                    modalService.showModal({}, modalOptions).then(function (result) {
                        if (result === 'ok') {

                        }
                    });

                } else {
                    var modalOptions = {
                        closeButtonText: 'No',
                        actionButtonText: 'Yes',
                        headerText: 'Inactive ' + typeName + '?',
                        bodyText: 'Are you sure?',
                        type: 'danger'
                    };

                    modalService.showModal({}, modalOptions).then(function (result) {
                        if (result === 'ok') {
                            $scope.isLoading = true;
                            FeesGenerateConfigService.inactivetype(id, function (result) {
                                $scope.workspaces = result.feesAutoGenTypeList;
                                $scope.workspaces[0].active = true;
                                $scope.isLoading = false;
                                toaster.pop("success", "Success", "Config Type Inactive.");
                            });
                        }
                    });

                }
            });

        }

        // Modal
        $scope.AuthorModel = GetNewAuthorModel();
        $scope.items = ['item1', 'item2', 'item3'];
        $scope.selectedImage = null;
        var addNewWorkspace = function () {
            $scope.AuthorModel = GetNewAuthorModel();
            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                size: 'sm',
                resolve: {
                    items: function () {
                        return $scope.items;
                    },
                    Author: function () {
                        return $scope.AuthorModel;
                    }
                }
            });

            modalInstance.result.then(function (data) {

                $scope.AuthorModel = data.Author;
                $scope.isLoading = true;
                FeesGenerateConfigService.addtype($scope.AuthorModel, function (result) {
                    $scope.workspaces = result.feesAutoGenTypeList;
                    $scope.workspaces[0].active = true;
                    $scope.isLoading = false;
                    toaster.pop("success", "Success", "type created.");
                });

                

                if (data.IsRemove == true) {
                    //var index = $scope.VmGallery.Images.indexOf($scope.selectedImage);
                    //$scope.VmGallery.Images.splice(index, 1);
                }
                console.log($scope.AuthorModel);
            }, function () {
                console.log(" modal");
            });
        };


    }]);

app.controller('ModalInstanceCtrl', [
    '$scope', '$modalInstance', 'items', 'Author', 'modalService', function ($scope, $modalInstance, items, Author, modalService) {
        $scope.items = items;
        $scope.selectedAuthor = Author;


        $scope.ok = function () {
            //console.log($scope.selectedImage);
            var data = { Author: $scope.selectedAuthor, IsRemove: false };
            $modalInstance.close(data);
        };
        
        $scope.remove = function () {
            // console.log($scope.selectedImage);
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Remove Image',
                bodyText: 'Are you sure you want to remove this image?',
                type: 'danger'
            };
            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {

                    var data = { Image: $scope.selectedImage, IsRemove: true };
                    $modalInstance.close(data);


                }
            });

        };
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
]);
function GetNewModel() {
    return { Id: 0, InstituteId: 0, Name: "", IsActive: true };

}
function GetNewAuthorModel() {
    return { Id: 0, InstituteId: 0, Name: "", IsActive: true };

}

//Todo: remove all validation error to a common js.
function showErrors(toaster, err) {

    if (err.data.ExceptionMessage) {

        toaster.pop("error", "Error", err.data.ExceptionMessage);
    }
    if (err.data.ModelState) {
        var msg = "";
        for (var key in err.data.ModelState) {
            msg += err.data.ModelState[key] + "\n";
        }

        toaster.pop("error", "Error", msg);
    }
}
