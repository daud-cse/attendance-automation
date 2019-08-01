'use strict';
/* Controllers */
// Employee controller
app.controller('EmployeeCtrl', ['$scope', '$http', 'EmployeeService', '$filter', 'toaster', 'FileUploader', '$location',

    function ($scope, $http, EmployeeService, $filter, toaster, FileUploader, $location) {

        // init
        $scope.entity = { Name: "Employee" };
        $scope.isLoading = true;
        $scope.VmEmployee = null;
        $scope.SiblingId = null;
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

        //--------------- Employee ---------------
        $scope.GetEmployee = function () {
            EmployeeService.New(function (VmEmployee) {
                $scope.VmEmployee = VmEmployee; // Employee.query();
                $scope.VmEmployee.UserInfo.FirstName = '';
                $scope.VmEmployee.UserInfo.MiddleName = '';
                $scope.VmEmployee.UserInfo.LastName = '';
                $scope.isLoading = false;
                angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
            });

        };
        $scope.GetEmployee();
        $scope.AddEmployee = function () {
            if ($scope.image.myCroppedImage.length > 500) {
                $scope.Upload();
            } else {
                CreateEmployee();
            }

        };

        function CreateEmployee() {
            $scope.isLoading = true;
            //--- set null to list properties ----
            var vmEmployee = angular.copy($scope.VmEmployee);
            vmEmployee.CountryList = [];
            vmEmployee.DistrictOrStateList = [];
            vmEmployee.UserInfo.GenderList = [];
            vmEmployee.UserInfo.NationalityList = [];
            vmEmployee.UserInfo.ReligionList = [];
            vmEmployee.UserInfo.BloodGroupList = [];
            vmEmployee.UserInfo.Employee.AcademicBranchList = [];
            vmEmployee.UserInfo.Employee.MaritalStatusList = [];
            vmEmployee.UserInfo.Employee.DesignationList = [];
            vmEmployee.UserInfo.Employee.DepartmentList = [];
            vmEmployee.SingleAddresses = [];

            vmEmployee.UserInfo.Employee.AcademicBranches = [];

            if ($scope.VmEmployee.UserInfo.Employee.AcademicBranchList.length == 1) {
                vmEmployee.UserInfo.Employee.AcademicBranches.push($scope.VmEmployee.UserInfo.Employee.AcademicBranchList[0].Key);
            } else {
                vmEmployee.UserInfo.Employee.AcademicBranches = $('input[name=cbBranch]:checked').map(function () { return $(this).val(); }).get();
            }
            vmEmployee.UserInfo.UserRoles = [];
            vmEmployee.UserInfo.UserRoles = $('input[name=cbRole]:checked').map(function () { return $(this).val(); }).get();
            //------

            EmployeeService.save(vmEmployee, function (result) {
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
                //console.debug(result);
                //$scope.isLoading = false;
                $location.path("/app/employee/" + result.Id);//result.data.Id
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
        }
        //------------ Addresses -------------------
        $scope.addAddress = function () {
            if ($scope.VmEmployee.Addresses == null)
                $scope.VmEmployee.Addresses = [];
            $scope.VmEmployee.Addresses.push(angular.copy($scope.VmEmployee.SingleAddresses));
        };
        $scope.removeAddress = function (index) {
            $scope.VmEmployee.Addresses.splice(index, 1);
        };

        $scope.checkBoxChange = function () {

            return $('input[name=cbBranch]:checked').length;
        };
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
            CreateEmployee();
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
            console.log($scope.image.myCroppedImage, "---------");
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