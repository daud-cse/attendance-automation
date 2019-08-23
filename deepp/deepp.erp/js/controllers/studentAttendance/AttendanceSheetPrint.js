'use strict';
/* Controllers */
// Student controller
app.controller('AttendanceSheetPrintCtrl', ['$scope', '$state', '$http',  '$stateParams', 'StudentAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, StudentAttendanceService,  modalService, $filter, toaster) {

        $scope.entity = { Name: "Student Attendance Sheet" };
        $scope.VmStudentAttendance = null;
        $scope.isLoading = true;
            StudentAttendanceService.attendancesheetprint(function (result) {
                $scope.VmSearchAttendance = result;
                $scope.isLoading = false;
            });

            $scope.AttendanceSheetPrint = function () {
                StudentAttendanceService.attendancesheetprint($scope.VmSearchAttendance, function (result) {
                    var selectedClass="";
                    if ($scope.VmSearchAttendance.ClassId != 0 && $scope.VmSearchAttendance.ClassId != null)
                    { selectedClass = $filter('filter')($scope.VmSearchAttendance.ClassList, { Key: $scope.VmSearchAttendance.ClassId })[0].Value; }
                    $scope.selectedClass = selectedClass;
                    var selectedSection = "";
                    if ($scope.VmSearchAttendance.SectionId != 0 && $scope.VmSearchAttendance.SectionId != null)
                    { selectedSection = $filter('filter')($scope.VmSearchAttendance.SectionList, { Key: $scope.VmSearchAttendance.SectionId })[0].Value; }
                    $scope.selectedSection = selectedSection;
                    var selectedBranch = "";
                    if ($scope.VmSearchAttendance.BranchId != 0 && $scope.VmSearchAttendance.BranchId != null)
                    { selectedBranch = $filter('filter')($scope.VmSearchAttendance.BranchList, { Key: $scope.VmSearchAttendance.BranchId })[0].Value; }
                    $scope.selectedBranch = selectedBranch;
                    //var range = [];
                    //for (var i = 0; i < 27; i++) {
                    //    range.push(i);
                    //}
                    //$scope.range = range;
                    $scope.VmSearchAttendance = result;
                    $scope.isLoading = false;
                }, function (err) {
                    showErrors(toaster, err);
                }
               );
            };

            $scope.SheetPrint = function () {                
                $('#divToPrint').jqprint();  
            }

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