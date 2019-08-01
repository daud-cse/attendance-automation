'use strict';
/* Controllers */
// Student controller
app.controller('StudentDetailsCtrl', ['$scope', '$http', 'StudentService', '$filter', 'toaster', '$stateParams', 'modalService', '$state','FileUploader',

    function ($scope, $http, StudentService, $filter, toaster, $stateParams, modalService, $state,FileUploader) {

        // init
        $scope.entity = { Name: "Student" };
        $scope.isLoading = true;
        $scope.VmStudent = null;
        $scope.ProfileImage = "";
        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;
        //--------------- Student ---------------
        $scope.GetStudent = function () {
            StudentService.get({ id: $stateParams.studentId, 'foobar': new Date().getTime() }, function (vmStudent) {
                $scope.VmStudent = vmStudent; // Student.query();
                $scope.ProfileImage = $scope.getRandom();
                $scope.isLoading = false;
            }, function (err) {
                    showErrors(toaster, err);
                    $scope.isLoading = false;
                });
        };
        $scope.GetStudent();
       
        $scope.Print = function() {
            $('#myDiv').jqprint();
        };
        $scope.getRandom = function () {
            var random = new Date().getTime();
            return apiUrlPrefix+'api/image/refcode?id=' + $scope.VmStudent.UserInfo.Id + '&refcode=Userinfo_Photo&foo=' + random;
        };

    }]);


 