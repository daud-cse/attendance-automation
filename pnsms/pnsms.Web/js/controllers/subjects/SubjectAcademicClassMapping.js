'use strict';

app.controller('SubjectAcademicClassMappingCtrl', ['$scope', '$http', 'SubjectAcademicClassMappingService', '$filter', 'toaster', '$modal',
    function ($scope, $http, SubjectAcademicClassMappingService, $filter, toaster, $modal) {

        // init
        $scope.entity = { Name: "Class wise Subjects" };
        $scope.isLoading = true;
        $scope.authorLoading = true;
        $scope.isDataLoading = false;

        $scope.RefSubjectAcademicClassMapping = {};
        $scope.TempSubjectAcademicClassMapping = {};
        $scope.SubjectAcademicClassMapping =
            {
                AcademicBranchId: "",
                AcademicShiftId: "",
                AcademicClassId: "",
                AcademicSectionId: "",
                SubjectId: "",
                SubjectGroupId: "",
                SubjectSplitId: "",
                TeacherId: ""
            };


        $scope.items = [];

        //inline add/edit
        $scope.newField = {};
        $scope.editing = {};
        $scope.isNew = false;
        $scope.selected = {};
        $scope.infoText = "";

        $scope.GetNew = function () {
            SubjectAcademicClassMappingService.new(function (result) {
                $scope.RefSubjectAcademicClassMapping = result;
                $scope.isLoading = false;
                //$scope.GetAll();
            });
        };
        ///---
        // Get all data
        $scope.SearchMappiing = function () {
            $scope.isDataLoading = true;
            SubjectAcademicClassMappingService.query({
                // academicBranchId: $scope.RefSubjectAcademicClassMapping.AcademicBranchId,
                academicClassId: $scope.RefSubjectAcademicClassMapping.AcademicClassId,
                AcademicBranchId: $scope.RefSubjectAcademicClassMapping.AcademicBranchId
                //  academicSessionId: $scope.RefSubjectAcademicClassMapping.AcademicSessionId,
                //  academicGroupId: $scope.RefSubjectAcademicClassMapping.AcademicGroupId,

            }, function (SubjectAcademicClassMappings) {
                $scope.SubjectAcademicClassMappings = SubjectAcademicClassMappings;
                $scope.items = SubjectAcademicClassMappings;
                // $scope.IsEnableSubjectGroup = ($scope.RefSubjectAcademicClassMapping.AcademicClass.IsEnableAcademicGroup == true ? true : false);
                $scope.isLoading = false;
                $scope.isDataLoading = false;

            }, function (err) {
                showErrors(toaster, err);
            });
        };

        $scope.copyInfo = function () {
            $scope.isDataLoading = true;
            SubjectAcademicClassMappingService.copy({
                academicSessionId: $scope.RefSubjectAcademicClassMapping.AcademicSessionId,
                academicClassId: $scope.RefSubjectAcademicClassMapping.AcademicClass.Id,
                academicGroupId: $scope.RefSubjectAcademicClassMapping.AcademicGroupId ? $scope.RefSubjectAcademicClassMapping.AcademicGroupId : 'null',
                toAcademicSessionId: $scope.RefSubjectAcademicClassMapping.ToAcademicSessionId,
                toAcademicClassId: $scope.RefSubjectAcademicClassMapping.ToAcademicClassId
            }, function (result) {
                $scope.isDataLoading = false;
                toaster.pop("success", "Success", "Copyied Subject Class mapinng successfuly");
                //$scope.GetAll();
            }
            , function (err) {
                $scope.isDataLoading = false;
                showErrors(toaster, err);
            });
        }


        $scope.GetNew();
        //$scope.DynamicPageAuthores = [];

        $scope.selectSubjectType = function (items, tempItems) {
            var selectedItem = {};
            var isDefault = "";
            var itemsCopy = angular.copy(items);
            for (var i = 0; i < items.length; i++) {
                if (tempItems.indexOf(items[i]) == -1) {
                    selectedItem = items[i];
                }
            }
            angular.forEach($scope.RefSubjectAcademicClassMapping.subjectTypeDetailsList, function (value) {
                if (selectedItem.Key == value.Id) {
                    if (value.IsDefault) {
                        isDefault = 1;
                    } else {
                        isDefault = 2
                    }
                }
            });
            if (isDefault == 1) {
                angular.forEach($scope.RefSubjectAcademicClassMapping.subjectTypeDetailsList, function (value) {
                    for (var i = 0; i < itemsCopy.length; i++) {
                        if (value.Id == itemsCopy[i].Key) {
                            if (value.IsDefault == undefined) {
                                var index = itemsCopy.indexOf(itemsCopy[i]);
                                itemsCopy.splice(index, 1);
                                i--;
                            }
                        }
                    }
                });
            }
            if (isDefault == 2) {
                angular.forEach($scope.RefSubjectAcademicClassMapping.subjectTypeDetailsList, function (value) {
                    for (var i = 0; i < itemsCopy.length; i++) {
                        if (value.Id == itemsCopy[i].Key) {
                            if (value.IsDefault == true) {
                                var index = itemsCopy.indexOf(itemsCopy[i]);
                                itemsCopy.splice(index, 1);
                                i--;
                            }
                        }
                    }
                });
            }
            for (var i = 0; i < items.length; i++) {
                var isFound = false;
                for (var j = 0; j < itemsCopy.length; j++) {
                    if (items[i].Key == itemsCopy[j].Key) {
                        isFound = true;
                    }
                }
                if (!isFound) {
                    var index = items.indexOf(items[i]);
                    items.splice(index, 1);
                    i--;
                }
            }
            tempItems = items;
            return tempItems;
        }
        $scope.AddNew = function () {
            $scope.SubjectAcademicClassMapping = {};
            $scope.isNew = true;
        };

        $scope.Add = function () {
            $scope.isLoading = true;

            var items = $filter('filter')($scope.items, { IsActive: true }, true);

            var msg = "";
          
            if (items.length==0) {                
                msg += "Select Subject is required for .";           
            }

           
            angular.forEach(items, function (item) {


                if (!item.OrderBy) {
                    msg += "Subject Order by is required for " + item.SubjectName + "." + "\n"

                }
                if (!item.SubjectMarks) {
                    msg += "Subject Exam Marks Entry  is required for " + item.SubjectName + "." + "\n"

                }
                if (item.SubjectTypes.length > 0) {
                    if (item.SubjectTypes.length == 0) {
                        msg += "Subject Types is required for " + item.SubjectName + "." + "\n"

                    }

                }

                if (item.SubjectTypeId == null || item.SubjectTypeId === "") {
                    msg += "Subject Types is required for " + item.SubjectName + "." + "\n"

                }               
            });
            if (msg.length > 0) {
                toaster.pop("warning", msg);
                $scope.isLoading = false;
                return false;
            }
            //angular.copy($scope.items);

            SubjectAcademicClassMappingService.save(items, function (result) {

                $scope.isNew = false;
                $scope.SearchMappiing();
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
            }, function (err) {
                $scope.isLoading = false;
                showErrors(toaster, err);
            }
           );


        };
        $scope.Edit = function (item) {
            $scope.editing = $scope.pagedItems[$scope.currentPage].indexOf(item);
            $scope.selected = angular.copy(item);
        };


        ///
        // gets the template to ng-include for a table row / item
        $scope.getTemplate = function (item) {

            //if (item.Id === $scope.selected.Id)
            //    return "edit";
            //else

            return 'display';
        };

        $scope.reset = function () {
            $scope.pagedItems[$scope.currentPage][$scope.editing] = angular.copy($scope.selected);
            $scope.selected = {};
        };



    }]);

//function GetNewModel() {
//    return { Id: 0, IsActive: true, Name: "", Quantity: 1, ISBN: "" };

//}
function GetNewAuthorModel() {
    return { Id: 0, IsActive: true, Name: "" };

}

function myCkeditor(a, b, c) {
    window.parent.CKEDITOR.tools.callFunction(a, b, c);
}