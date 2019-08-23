'use strict';
/* Controllers */
// Student controller
app.controller('teacherDetailsCtrl', ['$scope', '$http', 'TeacherService', '$filter', 'toaster', '$stateParams', 'modalService', '$state', 'FileUploader',

    function ($scope, $http, TeacherService, $filter, toaster, $stateParams, modalService, $state,FileUploader) {

        // init
        $scope.entity = { Name: "Teacher" };
        $scope.isLoading = true;
        $scope.VmTeacher = null;

        //--------------- Student ---------------
        $scope.GetTeacher = function () {
            TeacherService.get({ id: $stateParams.teacherId, 'foobar': new Date().getTime() }, function (VmTeacher) {
                $scope.VmTeacher = VmTeacher; // Student.query();
                $scope.isLoading = false;
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
        //------------------------------
        $scope.clear = function () {
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Cancel updates',
                bodyText: 'Are you sure you want to discard changes?',
                type: 'warning'
            };
            //  if (!$scope.dataHasChanged) {
            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    // $scope.UserModel = createBlankModel();
                }
            });
            //  }

        };

        $scope.pUploaderPhotoUrl = false;
        //--------------- file uploader ---------------
        // TODO: refactor on success event
        var imageApi = "api/student/imageup";
        var uploaderPhotoUrl = $scope.uploaderPhotoUrl = new FileUploader({
            url: imageApi,
            autoUpload: true,
        });

        var uploaderSmallPhotoUrl = $scope.uploaderSmallPhotoUrl = new FileUploader({
            url: imageApi,
            autoUpload: true,
        });

        uploaderPhotoUrl.onSuccessItem = function (fileItem, response, status, headers) {

            $scope.VmTeacher.UserInfo.PhotoUrl = response.split("#")[1];
            $scope.pUploaderPhotoUrl = true;
        };
        uploaderPhotoUrl.onAfterAddingAll = function (addedFileItems) {
            $scope.pUploaderPhotoUrl = false;
            uploaderPhotoUrl.progress = 0;
        };
        uploaderSmallPhotoUrl.onSuccessItem = function (fileItem, response, status, headers) {

            $scope.VmTeacher.UserInfo.SmallPhotoUrl = response.split("#")[1];
            console.log('onSuccessItem', fileItem, response, status, headers);
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