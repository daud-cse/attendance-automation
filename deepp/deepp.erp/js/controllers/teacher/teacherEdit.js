'use strict';
/* Controllers */
// Student controller
app.controller('TeacherEditCtrl', ['$scope', '$http', 'TeacherService', '$filter', 'toaster', '$stateParams', 'modalService', '$state', 'FileUploader', '$location',

    function ($scope, $http, TeacherService, $filter, toaster, $stateParams, modalService, $state, FileUploader,$location) {

        // init
        $scope.entity = { Name: "Employee" };
        $scope.isLoading = true;
        $scope.VmTeacher = null;

        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;

        //------------ Months---------------
        $scope.Months = [
            { "Key": '01', "Value": "January" },
            { "Key": '02', "Value": "February" },
            { "Key": '03', "Value": "March" },
            { "Key": '04', "Value": "April" },
            { "Key": '05', "Value": "May" },
            { "Key": '06', "Value": "June" },
            { "Key": '07', "Value": "July" },
            { "Key": '08', "Value": "August" },
            { "Key": '09', "Value": "September" },
            { "Key": '10', "Value": "October" },
            { "Key": '11', "Value": "November" },
            { "Key": '12', "Value": "December" }
        ];
        $scope.GetDates = function (dates) {
            console.log($filter('date')(dates, "yyyy"));
            $scope.bYear = $filter('date')(dates, "yyyy");
            $scope.bMonth = $filter('date')(dates, "MM");
            $scope.bDay = $filter('date')(dates, "dd");
        };
        //--------------- Student ---------------
        $scope.GetTeacher = function () {
            TeacherService.get({ id: $stateParams.teacherId, 'foobar': new Date().getTime() }, function (VmTeacher) {
                $scope.VmTeacher = VmTeacher; // Student.query();
                $scope.GetDates($scope.VmTeacher.UserInfo.DOB);
                $scope.isLoading = false;
                $scope.image.myCroppedImage = 'data:image/JPEG;base64,' + $scope.VmTeacher.ProfileImage;
                angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
            }, function (err) {
                showErrors(toaster, err);
            });
        };
        $scope.GetTeacher();
        $scope.AddTeacher = function () {
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Save updates',
                bodyText: 'Are you sure you want to save changes?',
                type: 'warning'
            };
            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    if ($scope.image.myCroppedImage.length > 500) {
                        $scope.Upload();
                    } else {

                        UpdateTeacher();
                    }
                }
            });



        };
        function UpdateTeacher() {
             
            //--- set null to list properties ----
            var teacher = angular.copy($scope.VmTeacher);
            teacher.CountryList = [];
            teacher.DistrictOrStateList = [];
            teacher.UserInfo.GenderList = [];
            teacher.UserInfo.NationalityList = [];
            teacher.UserInfo.ReligionList = [];
            teacher.UserInfo.BloodGroupList = [];
            teacher.UserInfo.Teacher.AcademicBranchList = [];
            teacher.UserInfo.Teacher.AcademicClassList = [];
            teacher.UserInfo.Teacher.AcademicSectionList = [];
            teacher.UserInfo.Teacher.MaritalStatusList = [];
            teacher.UserInfo.Teacher.DesignationList = [];
            teacher.SingleAddresses = [];
           
            teacher.UserInfo.Teacher.AcademicBranches = [];
            teacher.UserInfo.Teacher.AcademicBranches = $('input[name=cbBranch]:checked').map(function () { return $(this).val(); }).get();
            teacher.UserInfo.UserRoles = [];
            teacher.UserInfo.UserRoles = $('input[name=cbRole]:checked').map(function () { return $(this).val(); }).get();
            //------
            TeacherService.update({ id: teacher.UserInfo.Id }, teacher, function (result) {
                toaster.pop("success", "Success", $scope.entity.Name + " updated.");

                $location.path("/app/teacher/" + $scope.VmTeacher.UserInfo.Id);
            }, function (err) {
                showErrors(toaster, err);
            }
            );
        }
        //------------ Addresses -------------------
        $scope.addAddress = function () {
            if ($scope.VmTeacher.Addresses == null)
                $scope.VmTeacher.Addresses = [];
            $scope.VmTeacher.Addresses.push(angular.copy($scope.VmTeacher.SingleAddresses));
        };
        $scope.removeAddress = function (index) {
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Remove Address',
                bodyText: 'Are you sure you want to remove this address?',
                type: 'error'
            };
            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {
                    if ($scope.VmTeacher.Addresses[index].Id == 0 || $scope.VmTeacher.Addresses[index].Id == undefined) {
                        $scope.VmTeacher.Addresses.splice(index, 1);
                    } else {
                        $scope.VmTeacher.Addresses[index].IsActive = false;
                    }
                }
            });



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

        //--  check box required
        $scope.checkBoxChange = function () {

            return $('input[name=cbBranch]:checked').length;
        };
        //--  role check box required
        $scope.checkBoxRoleChange = function () {

            return $('input[name=cbRole]:checked').length;
        };
        //--------------- file uploader ---------------
        $scope.pUploaderPhotoUrl = false;
        // TODO: refactor on success event
        var imageApi = apiUrlPrefix + "api/image";

        var uploaderPhotoUrl = $scope.uploaderPhotoUrl = new FileUploader({
            url: imageApi,
            autoUpload: false,
        });

        uploaderPhotoUrl.onSuccessItem = function (fileItem, response, status, headers) {
            $scope.ProfilePic = response;

            $scope.isUpload = false;
            $scope.isUploading = false;
            $scope.pUploaderPhotoUrl = true;
        };
        uploaderPhotoUrl.onAfterAddingAll = function (addedFileItems) {
            $scope.pUploaderPhotoUrl = false;
            uploaderPhotoUrl.progress = 0;
        };

        uploaderPhotoUrl.onCompleteAll = function () {
            UpdateTeacher();
        };
        //----------------
        $scope.myImage = null;
        $scope.myCroppedImage = null;
        $scope.cropType = "square";
        $scope.image = {
            myImage: '',
            myCroppedImage: ''
        };
        var handleFileSelect = function (evt) {
            var file = evt.currentTarget.files[0];
            var reader = new FileReader();
            reader.onload = function (evt) {
                $scope.$apply(function ($scope) {
                    $scope.image.myImage = evt.target.result;
                });
            };
            reader.readAsDataURL(file);
        };

        $scope.Upload = function () {
            $scope.isUploading = true;
            if ($scope.image.myCroppedImage == '') {
                return false;
            }
            
            var file = base64ToBlob($scope.image.myCroppedImage.replace('data:image/png;base64,', ''), 'image/jpeg');

            uploaderPhotoUrl.addToQueue(file);
            uploaderPhotoUrl.uploadAll();

        };
        $scope.RemoveImage = function () {

        };
        // base64 To Blob
        function base64ToBlob(base64Data, contentType) {
            contentType = contentType || '';
            var sliceSize = 1024;
            var byteCharacters = atob(base64Data);
            var bytesLength = byteCharacters.length;
            var slicesCount = Math.ceil(bytesLength / sliceSize);
            var byteArrays = new Array(slicesCount);

            for (var sliceIndex = 0; sliceIndex < slicesCount; ++sliceIndex) {
                var begin = sliceIndex * sliceSize;
                var end = Math.min(begin + sliceSize, bytesLength);

                var bytes = new Array(end - begin);
                for (var offset = begin, i = 0 ; offset < end; ++i, ++offset) {
                    bytes[i] = byteCharacters[offset].charCodeAt(0);
                }
                byteArrays[sliceIndex] = new Uint8Array(bytes);
            }
            return new Blob(byteArrays, { type: contentType });
        }
       


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