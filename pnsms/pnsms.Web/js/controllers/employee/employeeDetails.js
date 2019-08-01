'use strict';
/* Controllers */
// Employee controller
app.controller('EmployeeDetailsCtrl', ['$scope', '$http', 'EmployeeService', '$filter', 'toaster', '$stateParams', 'modalService', '$state','FileUploader',

    function ($scope, $http, EmployeeService, $filter, toaster, $stateParams, modalService, $state,FileUploader) {

        // init
        $scope.entity = { Name: "Employee" };
        $scope.isLoading = true;
        $scope.VmEmployee = null;
        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;
        //--------------- Employee ---------------
        $scope.GetEmployee = function () {
            EmployeeService.get({ id: $stateParams.employeeId, 'foobar': new Date().getTime() }, function (vmEmployee) {
                $scope.VmEmployee = vmEmployee; // Employee.query();
                $scope.isLoading = false;
                $scope.ProfileImage = $scope.getRandom();
            }, function (err) {
                showErrors(toaster, err);
            });
        };
        $scope.GetEmployee();
    
        $scope.Print = function() {
            $('#myDiv').jqprint();
        };
        $scope.getRandom = function () {
            var random = new Date().getTime();
            return apiUrlPrefix+'api/image/refcode?id=' + $scope.VmEmployee.UserInfo.Id + '&refcode=Userinfo_Photo&foo=' + random;
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