'use strict';
/* Controllers */
// Teacher controller
app.controller('EmployeeAttendanceListCtrl', ['$scope', '$http', 'EmployeeAttendanceService', '$filter', 'toaster',
function ($scope, $http, EmployeeAttendanceService, $filter, toaster) {

    $scope.entity = { Name: "Employee Attendance" };
    $scope.VmSearchAttendance = null;
    $scope.isLoading = true;
    var myTimeUnformatted = new Date();
    var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
    $scope.isLoading = false;
    EmployeeAttendanceService.list({ startDate: date, endDate: date }, function (result) {
        $scope.VmSearchAttendance = result;
        date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
        $scope.VmSearchAttendance.startDate = date;
        $scope.VmSearchAttendance.endDate = date;
        $scope.isLoading = false;
    });

    $scope.GetFilterResult = function () {
        $scope.isLoading = true;
        EmployeeAttendanceService.list($scope.VmSearchAttendance, function (attendanceList) {
            $scope.VmSearchAttendance = attendanceList;
            $scope.isLoading = false;
        });

    };
}]);