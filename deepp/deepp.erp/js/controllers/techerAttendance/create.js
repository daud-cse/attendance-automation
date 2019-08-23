'use strict';
/* Controllers */
app.controller('TeacherAttendanceCreateCtrl', ['$scope', '$state', '$http', 'TeacherAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, TeacherAttendanceService, modalService, $filter, toaster) {
         
        $scope.entity = { Name: "Employee  Attendance" };
        $scope.VmUserAttendance = null;
        $scope.isLoading = true;
        TeacherAttendanceService.new($scope.VmUserAttendance ,function (result) {
            $scope.VmUserAttendance = result;
            $scope.isLoading = false;
        });

        $scope.getTeachersBranchWise = function () {
            $scope.isLoading = true;
            TeacherAttendanceService.new($scope.VmUserAttendance, function (result) {
                if (result.AttendanceDetails != null) {
                    $scope.VmUserAttendance = result;
                    $scope.isLoading = false;
                }
            })
        }


        //------------------------------
        $scope.AddTeacherAttendance = function () {

            //$scope.VmUserAttendance.AttendanceDetails[0].InTime = DateConvert($scope.VmUserAttendance.AttendanceDetails[0].InTime);
            //$scope.VmUserAttendance.AttendanceDetails[0].OutTime = DateConvert($scope.VmUserAttendance.AttendanceDetails[0].OutTime);

            if ($scope.VmUserAttendance.AttendanceDetails.length != 0) {
                $.each($scope.VmUserAttendance.AttendanceDetails, function (i, v) {
                    $scope.VmUserAttendance.AttendanceDetails[i].InTime = DateConvert($scope.VmUserAttendance.AttendanceDetails[i].InTime);
                    $scope.VmUserAttendance.AttendanceDetails[i].OutTime = DateConvert($scope.VmUserAttendance.AttendanceDetails[i].OutTime);

                });
            }
            console.log($scope.VmUserAttendance);
            TeacherAttendanceService.saveteacherattendance($scope.VmUserAttendance, function (result) {
                $state.go('app.techerAttendance.list');
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