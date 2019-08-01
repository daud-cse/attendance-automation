'use strict';
/* Controllers */
// Student controller
app.controller('EmployeeAttendanceDetailsCtrl', ['$scope', '$state', '$http', '$stateParams', 'EmployeeAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, EmployeeAttendanceService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Employee Attendance" };
        $scope.isLoading = true;
        $scope.VmUserAttendance = null;

        $scope.GetSingleAttendance = function () {
            EmployeeAttendanceService.getsingle({ id: $stateParams.employeeattendanceId }, function (result) {
                $scope.VmUserAttendance = result;
                $scope.isLoading = false;
            });
        }
        $scope.GetSingleAttendance();

    }]);