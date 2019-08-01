'use strict';
/* Controllers */
app.filter('propsFilter', function () {
    return function (items, props) {
        var out = [];

        if (angular.isArray(items)) {
            items.forEach(function (item) {
                var itemMatches = false;

                var keys = Object.keys(props);
                for (var i = 0; i < keys.length; i++) {
                    var prop = keys[i];
                    var text = props[prop].toLowerCase();
                    if (item[prop].toString().toLowerCase().indexOf(text) !== -1) {
                        itemMatches = true;
                        break;
                    }
                }

                if (itemMatches) {
                    out.push(item);
                }
            });
        } else {
            // Let the output be the input untouched
            out = items;
        }

        return out;
    };
});

app.controller('FeesAutoGenerateEditCtrl', ['$scope', '$state', '$http', '$stateParams', 'FeesAutoGeneratService', 'modalService', '$filter', 'toaster', '$modal',
 function ($scope, $state, $http, $stateParams, FeesAutoGeneratService, modalService, $filter, toaster, $modal) {

     $scope.entity = { Name: "Fees Auto Generate" };
        $scope.isLoading = true;
        $scope.feesAutoGenModel = { selectedBranches: [], selectedVersions: [], selectedClasses: [], selectedGroups: [], selectedShifts: [],BranchList:[] };
        $scope.selected = {};
     
        $scope.Math = window.Math;
        
     $scope.GetFeesGenerate = function() {
         FeesAutoGeneratService.getsingle({ id: $stateParams.feesAutoGenerateId }, function (result) {
            
             $scope.feesAutoGenModel.selectedBranches = [];
             $scope.feesAutoGenModel.selectedVersions = [];
             $scope.feesAutoGenModel.selectedClasses = [];
             $scope.feesAutoGenModel.selectedGroups = [];
             $scope.feesAutoGenModel.selectedShifts = [];

             $scope.feesAutoGenModel = result;
             $scope.feesAutoGenModel.feesAutoGenTypeList = result.feesAutoGenTypeList;
             $scope.feesAutoGenModel.FacilityList = result.FacilityList;
             $scope.feesAutoGenModel.BranchList = result.BranchList;
             $scope.feesAutoGenModel.VersionList = result.VersionList;
             $scope.feesAutoGenModel.ClassList = result.ClassList;
             $scope.feesAutoGenModel.GroupList = result.GroupList;
             $scope.feesAutoGenModel.ShiftList = result.ShiftList;
             $scope.feesAutoGenModel.FeesGenerateHeadList = result.FeesGenerateHeadList;

             if (!$scope.feesAutoGenModel.FeesAutoGenerateConfig.IsAllAcademicBranch) {
                 angular.forEach(result.selectedBranches, function (obj) {
                     var selectedValue = $filter('filter')($scope.feesAutoGenModel.BranchList, { Key: obj.Key })[0];
                     if (selectedValue != null) {
                         $scope.feesAutoGenModel.selectedBranches.push(selectedValue);
                     }
                 });
             }
             if (!$scope.feesAutoGenModel.FeesAutoGenerateConfig.IsAllAcademicVerssion) {
                 angular.forEach(result.selectedVersions, function (obj) {
                     var selectedValue = $filter('filter')($scope.feesAutoGenModel.VersionList, { Key: obj.Key })[0];
                     if (selectedValue != null) {
                         $scope.feesAutoGenModel.selectedVersions.push(selectedValue);
                     }
                 });
             }
             if (!$scope.feesAutoGenModel.FeesAutoGenerateConfig.IsAllAcademicClass) {
                 angular.forEach(result.selectedClasses, function (obj) {
                     var selectedValue = $filter('filter')($scope.feesAutoGenModel.ClassList, { Key: obj.Key })[0];
                     if (selectedValue != null) {
                         $scope.feesAutoGenModel.selectedClasses.push(selectedValue);
                     }
                 });
             }
             if (!$scope.feesAutoGenModel.FeesAutoGenerateConfig.IsAllAcademicGroup) {
                 angular.forEach(result.selectedGroups, function (obj) {
                     var selectedValue = $filter('filter')($scope.feesAutoGenModel.GroupList, { Key: obj.Key })[0];
                     if (selectedValue != null) {
                         $scope.feesAutoGenModel.selectedGroups.push(selectedValue);
                     }
                 });
             }
             if (!$scope.feesAutoGenModel.FeesAutoGenerateConfig.IsAllAcademicShift) {
                 angular.forEach(result.selectedShifts, function (obj) {
                     var selectedValue = $filter('filter')($scope.feesAutoGenModel.ShiftList, { Key: obj.Key })[0];
                     if (selectedValue != null) {
                         $scope.feesAutoGenModel.selectedShifts.push(selectedValue);
                     }
                 });
             }
           
             $scope.isLoading = false;
         });
     };
       $scope.GetFeesGenerate();

        //-----------------------------

     $scope.UpdateFeesAutoGenerate = function() {

         $scope.feesAutoGenModel.BranchList = [];
         $scope.feesAutoGenModel.VersionList = [];
         $scope.feesAutoGenModel.ClassList = [];
         $scope.feesAutoGenModel.GroupList = [];
         $scope.feesAutoGenModel.ShiftList = [];

         if ($scope.feesAutoGenModel.selectedBranches != null) {
             angular.forEach($scope.feesAutoGenModel.selectedBranches, function(obj) {
                 $scope.feesAutoGenModel.BranchList.push({ Key: obj.Key });
             });
         }
         if ($scope.feesAutoGenModel.selectedVersions != null) {
             angular.forEach($scope.feesAutoGenModel.selectedVersions, function(obj) {
                 $scope.feesAutoGenModel.VersionList.push({ Key: obj.Key });
             });
         }
         if ($scope.feesAutoGenModel.selectedClasses != null) {
             angular.forEach($scope.feesAutoGenModel.selectedClasses, function(obj) {
                 $scope.feesAutoGenModel.ClassList.push({ Key: obj.Key });
             });
         }
         if ($scope.feesAutoGenModel.selectedGroups != null) {
             angular.forEach($scope.feesAutoGenModel.selectedGroups, function(obj) {
                 $scope.feesAutoGenModel.GroupList.push({ Key: obj.Key });
             });
         }
         if ($scope.feesAutoGenModel.selectedShifts != null) {
             angular.forEach($scope.feesAutoGenModel.selectedShifts, function(obj) {
                 $scope.feesAutoGenModel.ShiftList.push({ Key: obj.Key });
             });
         }

         $scope.feesAutoGenModel.FeesAutoGenerateConfig.IsActive = ($scope.feesAutoGenModel.FeesAutoGenerateConfig.IsActive == 1) ? true : false;

         FeesAutoGeneratService.save($scope.feesAutoGenModel, function(result) {
             $state.go('app.feesGenerateConfig.panel');
             toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
         }, function(err) {
             showErrors(toaster, err);
         });
     };

 }]);
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
