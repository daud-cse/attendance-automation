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

app.controller('FeesEnrollAutoGenerateEditCtrl', ['$scope', '$state', '$http', '$stateParams', 'FeesAutoGeneratService', 'modalService', '$filter', 'toaster', '$modal',
 function ($scope, $state, $http, $stateParams, FeesAutoGeneratService, modalService, $filter, toaster, $modal) {

     $scope.entity = { Name: "Fees Auto Generate for Enrollment" };
     $scope.isLoading = true;
     $scope.feesAutoGenModel = { selectedBranches: [], selectedVersions: [], selectedClasses: [], selectedGroups: [], selectedShifts: [], BranchList: [], FeesAutoGenerateConfig:null };
     $scope.selected = {};

     $scope.Math = window.Math;
     $scope.tSumTotal = 0;
     $scope.items = [];

     $scope.Monthes = [
          { "Key": '1', "Value": "January" },
          { "Key": '2', "Value": "February" },
          { "Key": '3', "Value": "March" },
          { "Key": '4', "Value": "April" },
          { "Key": '5', "Value": "May" },
          { "Key": '6', "Value": "June" },
          { "Key": '7', "Value": "July" },
          { "Key": '8', "Value": "August" },
          { "Key": '9', "Value": "September" },
          { "Key": '10', "Value": "October" },
          { "Key": '11', "Value": "November" },
          { "Key": '12', "Value": "December" }
     ];

     $scope.GetFeesGenerate = function () {
         FeesAutoGeneratService.getsingleEnroll({ id: $stateParams.feesAutoGenerateId }, function (result) {

             $scope.feesAutoGenModel.selectedBranches = [];
             $scope.feesAutoGenModel.selectedVersions = [];
             $scope.feesAutoGenModel.selectedClasses = [];
             $scope.feesAutoGenModel.selectedGroups = [];
             $scope.feesAutoGenModel.selectedShifts = [];

             $scope.feesAutoGenModel = result;
             $scope.feesAutoGenModel.FacilityList = result.FacilityList;
             $scope.feesAutoGenModel.BranchList = result.BranchList;
             $scope.feesAutoGenModel.VersionList = result.VersionList;
             $scope.feesAutoGenModel.ClassList = result.ClassList;
             $scope.feesAutoGenModel.GroupList = result.GroupList;
             $scope.feesAutoGenModel.ShiftList = result.ShiftList;
             $scope.feesAutoGenModel.FeesGenerateHeadList = result.FeesGenerateHeadList;
             $scope.feesAutoGenModel.DayList = result.dayList;

             $scope.feesAutoGenModel.SessionList = [
                  { "Key": '116', "Value": "This Session" },
                  { "Key": '117', "Value": "Next Session" }
             ];


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

             $scope.items = result.enrollDetails;

             angular.forEach($scope.items, function (item1) {
                 item1["Session"] = $filter('filter')($scope.feesAutoGenModel.SessionList, { Key: item1.SessionId })[0].Value;
                 item1["Month"] = $filter('filter')($scope.Monthes, { Key: item1.MonthId })[0].Value;
                 item1["Day"] = $filter('filter')($scope.feesAutoGenModel.DayList, { Key: item1.DayId })[0].Value;
                 item1["DueMonth"] = $filter('filter')($scope.Monthes, { Key: item1.DueMonthId })[0].Value;
                 item1["DueDay"] = $filter('filter')($scope.feesAutoGenModel.DayList, { Key: item1.DueDayId })[0].Value;
                 angular.forEach(item1.AmountDetails, function (item) {
                     $scope.tSumTotal += item.Total;
                 });
             });

             $scope.isLoading = false;
         });
     };
     $scope.GetFeesGenerate();

     //-----------------------------
     $scope.UpdateAmount = function (x) {
         angular.forEach($scope.feesAutoGenModel.FeesGenerateHeadList, function (item1) {
             if (item1.FeesHeadId == x.FeesHeadId) {
                 item1["Amount"] = x.Amount;
             }
         });
     };

     $scope.AddSingle = function (selected) {

         if (selected != null) {

             var selectedSession = "";
             var selectedMonth = "";
             var selectedDay = "";
             var dueSelectedMonth = "";
             var dueSelectedDay = "";

             if (selected.Session != null)
             { selectedSession = $filter('filter')($scope.feesAutoGenModel.SessionList, { Key: selected.Session })[0].Value; }

             if (selected.Month != null)
             { selectedMonth = $filter('filter')($scope.Monthes, { Key: selected.Month })[0].Value; }

             if (selected.Day != null)
             { selectedDay = $filter('filter')($scope.feesAutoGenModel.DayList, { Key: selected.Day })[0].Value; }

             if (selected.DueMonth != null)
             { dueSelectedMonth = $filter('filter')($scope.Monthes, { Key: selected.DueMonth })[0].Value; }

             if (selected.DueDay != null)
             { dueSelectedDay = $filter('filter')($scope.feesAutoGenModel.DayList, { Key: selected.DueDay })[0].Value; }

             var details = [];

             angular.forEach($scope.feesAutoGenModel.FeesGenerateHeadList, function (item1) {
                 details.push({ HeadId: item1.FeesHeadId, Amount: item1.Amount, Vat: (Math.round((item1.Amount * item1.VAT / 100))), Total: (Math.round((item1.Amount * item1.VAT / 100) + item1.Amount)) });
             });

             $scope.items.push({
                 SessionId: selected.Session,
                 Session: selectedSession,
                 MonthId: selected.Month,
                 Month: selectedMonth,
                 DayId: selected.Day,
                 Day: selectedDay,
                 DueMonthId: selected.DueMonth,
                 DueMonth: dueSelectedMonth,
                 DueDayId: selected.DueDay,
                 DueDay: dueSelectedDay,
                 AmountDetails: details
             });

             $scope.tSumTotal = 0;

             angular.forEach($scope.items, function (item) {
                 angular.forEach(item.AmountDetails, function (item1) {
                     $scope.tSumTotal += item1.Total;
                 });
             });

         }

         // Clear input fields after push
         selected = null;

     };


     $scope.DeleteSingle = function (index) {
         $scope.items.splice(index, 1);

         $scope.tSumTotal = 0;

         angular.forEach($scope.items, function (item) {
             angular.forEach(item.AmountDetails, function (item1) {
                 $scope.tSumTotal += item1.Total;
             });
         });
     };

     $scope.UpdateFeesAutoEnrollGenerate = function () {

         $scope.feesAutoGenModel.BranchList = [];
         $scope.feesAutoGenModel.VersionList = [];
         $scope.feesAutoGenModel.ClassList = [];
         $scope.feesAutoGenModel.GroupList = [];
         $scope.feesAutoGenModel.ShiftList = [];

         if ($scope.feesAutoGenModel.selectedBranches != null) {
             angular.forEach($scope.feesAutoGenModel.selectedBranches, function (obj) {
                 $scope.feesAutoGenModel.BranchList.push({ Key: obj.Key });
             });
         }
         if ($scope.feesAutoGenModel.selectedVersions != null) {
             angular.forEach($scope.feesAutoGenModel.selectedVersions, function (obj) {
                 $scope.feesAutoGenModel.VersionList.push({ Key: obj.Key });
             });
         }
         if ($scope.feesAutoGenModel.selectedClasses != null) {
             angular.forEach($scope.feesAutoGenModel.selectedClasses, function (obj) {
                 $scope.feesAutoGenModel.ClassList.push({ Key: obj.Key });
             });
         }
         if ($scope.feesAutoGenModel.selectedGroups != null) {
             angular.forEach($scope.feesAutoGenModel.selectedGroups, function (obj) {
                 $scope.feesAutoGenModel.GroupList.push({ Key: obj.Key });
             });
         }
         if ($scope.feesAutoGenModel.selectedShifts != null) {
             angular.forEach($scope.feesAutoGenModel.selectedShifts, function (obj) {
                 $scope.feesAutoGenModel.ShiftList.push({ Key: obj.Key });
             });
         }
         $scope.feesAutoGenModel.enrollDetails = $scope.items;
         $scope.feesAutoGenModel.FeesAutoGenerateConfig.IsActive = ($scope.feesAutoGenModel.FeesAutoGenerateConfig.IsActive == 1) ? true : false;

         FeesAutoGeneratService.saveEnroll($scope.feesAutoGenModel, function (result) {
             $state.go('app.feesAutoGenerate.listEnroll');
             toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
         }, function (err) {
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
