'use strict';
/* Controllers */
// Teacher controller
app.controller('TeacherAttendanceListCtrl', ['$scope', '$http', 'TeacherAttendanceService', '$filter', 'toaster',
function ($scope, $http, TeacherAttendanceService, $filter, toaster) {

    $scope.entity = { Name: "Employee Attendance" };
    $scope.VmSearchAttendance = null;
    $scope.isLoading = true;
    var myTimeUnformatted = new Date();
    var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
    $scope.isLoading = false;
    TeacherAttendanceService.list({ startDate: date, endDate: date }, function (result) {
        $scope.VmSearchAttendance = result;
        date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
        $scope.VmSearchAttendance.startDate = date;
        $scope.VmSearchAttendance.endDate = date;
        $scope.isLoading = false;
    });

    $scope.GetFilterResult = function () {
        $scope.isLoading = true;
        TeacherAttendanceService.list($scope.VmSearchAttendance, function (attendanceList) {
            $scope.VmSearchAttendance = attendanceList;
            $scope.isLoading = false;
        });

    };
}]);