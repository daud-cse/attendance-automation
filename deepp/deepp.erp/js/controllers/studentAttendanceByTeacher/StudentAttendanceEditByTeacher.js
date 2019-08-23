﻿'use strict';
/* Controllers */
// Student controller
app.controller('StudentAttendanceEditByTeacherCtrl', ['$scope', '$state', '$http', '$stateParams', 'StudentAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, StudentAttendanceService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Student Attendance" };
        $scope.isLoading = true;
        $scope.VmStudentAttendance = null;

        $scope.GetSingleAttendance = function () {
            StudentAttendanceService.get({ id: $stateParams.attenanceId }, function (result) {
                $scope.VmStudentAttendance = result;
                $scope.isLoading = true;
            });
        }
        $scope.GetSingleAttendance();


        //------------------------------
        $scope.AttendanceEditSubmit = function () {
            StudentAttendanceService.updateattendance($scope.VmStudentAttendance, function (result) {
                $state.go('app.studentAttendanceByTeacher.StudentAttendanceListByTeacher');
                toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
            }, function (err) {
                showErrors(toaster, err);
            }
           );
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