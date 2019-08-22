'use strict';
/* Controllers */
// Student controller
app.controller('StudentAttendanceDetailsByTeacherCtrl', ['$scope', '$state', '$http', '$stateParams', 'StudentAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, StudentAttendanceService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Student Attendance" };
        $scope.VmStudentAttendance = null;

        $scope.GetSingleAttendance = function () {
            $scope.isLoading = true;
            StudentAttendanceService.getsingle({ id: $stateParams.attenanceId }, function (result) {
                $scope.VmStudentAttendance = result;
                $scope.isLoading = false;
            });
        }
        $scope.GetSingleAttendance();

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