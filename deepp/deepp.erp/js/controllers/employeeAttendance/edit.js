'use strict';
/* Controllers */
app.controller('EmployeeAttendanceEditCtrl', ['$scope', '$state', '$stateParams', '$http', 'EmployeeAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $stateParams, $http, EmployeeAttendanceService, modalService, $filter, toaster) {

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


        //------------------------------
        $scope.EditEmployeeAttendance = function () {
			 $scope.VmUserAttendance.InTime =new Date( $scope.VmUserAttendance.InTime.toString());
            $scope.VmUserAttendance.OutTime = new Date($scope.VmUserAttendance.OutTime.toString());
            EmployeeAttendanceService.updateemployeeattendance($scope.VmUserAttendance, function (result) {
                $state.go('app.employeeAttendance.list');
                toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
            }, function (err) {
                showErrors(toaster, err);
            }
           );
        };

    }]);

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