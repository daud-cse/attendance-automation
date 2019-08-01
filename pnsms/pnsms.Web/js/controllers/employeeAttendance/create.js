'use strict';
/* Controllers */
app.controller('EmployeeAttendanceCreateCtrl', ['$scope', '$state', '$http', 'EmployeeAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, EmployeeAttendanceService, modalService, $filter, toaster) {
         
        $scope.entity = { Name: "Employee Attendance" };
        $scope.VmUserAttendance = null;
        $scope.isLoading = true;
        EmployeeAttendanceService.new($scope.VmUserAttendance, function (result) {
            $scope.VmUserAttendance = result;
            $scope.isLoading = false;
        });

        $scope.getEmployeesBranchWise = function () {
            $scope.isLoading = true;
            EmployeeAttendanceService.new($scope.VmUserAttendance, function (result) {
                if (result.AttendanceDetails != null) {
                    $scope.VmUserAttendance = result;
                    $scope.isLoading = false;
                }
            })
        }


        //------------------------------
        $scope.AddEmployeeAttendance = function () {

            if ($scope.VmUserAttendance.AttendanceDetails.length != 0) {
                $.each($scope.VmUserAttendance.AttendanceDetails, function (i, v) {
                    $scope.VmUserAttendance.AttendanceDetails[i].InTime = DateConvert($scope.VmUserAttendance.AttendanceDetails[i].InTime);
                    $scope.VmUserAttendance.AttendanceDetails[i].OutTime = DateConvert($scope.VmUserAttendance.AttendanceDetails[i].OutTime);

                });
            }
            console.log($scope.VmUserAttendance);
            EmployeeAttendanceService.saveemployeeattendance($scope.VmUserAttendance, function (result) {
                $state.go('app.employeeAttendance.list');
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
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