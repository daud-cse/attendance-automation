'use strict';
/* Controllers */
// Student controller
app.controller('teacherDetailsCtrl', ['$scope', '$http', 'TeacherService', '$filter', 'toaster', '$stateParams', 'modalService', '$state', 'FileUploader',

    function ($scope, $http, TeacherService, $filter, toaster, $stateParams, modalService, $state,FileUploader) {

        // init
        $scope.entity = { Name: "Employee" };
        $scope.isLoading = true;
        $scope.VmTeacher = null;
        $scope.ProfileImage = '';

        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;

        //--------------- Student ---------------
        $scope.GetTeacher = function () {
            TeacherService.get({ id: $stateParams.teacherId, 'foobar': new Date().getTime() }, function (VmTeacher) {
                $scope.VmTeacher = VmTeacher; // Student.query();
                $scope.ProfileImage = $scope.getRandom();
                $scope.isLoading = false;
            }, function (err) {
                showErrors(toaster, err);
            });
        };
        $scope.GetTeacher();
        
 
        //------------ Addresses -------------------
        $scope.addAddress = function () {
            if ($scope.VmTeacher.Addresses == null)
                $scope.VmTeacher.Addresses = [];
            $scope.VmTeacher.Addresses.push(angular.copy($scope.VmTeacher.SingleAddresses));
        };
        $scope.removeAddress = function (index) {
            //
            if ($scope.VmTeacher.Addresses[index].Id == 0) {
                $scope.VmTeacher.Addresses.splice(index, 1);
            } else {
                $scope.VmTeacher.Addresses[index].IsActive = false;
            }

        };
         
 

        $scope.Print = function() {
            $('#myDiv').jqprint();
        };
        $scope.getRandom = function () {
            var random = new Date().getTime();
            return apiUrlPrefix+'api/image/refcode?id=' + $scope.VmTeacher.UserInfo.Id + '&refcode=Userinfo_Photo&foo=' + random;
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