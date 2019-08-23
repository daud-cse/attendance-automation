'use strict';
/* Controllers */
// Student controller
app.controller('EmployeeEditCtrl', ['$scope', '$http', 'EmployeeService', '$filter', 'toaster', '$stateParams', 'modalService', '$state', 'FileUploader', '$location',

    function ($scope, $http, EmployeeService, $filter, toaster, $stateParams, modalService, $state, FileUploader, $location) {

        // init
        $scope.entity = { Name: "Employee" };
        $scope.isLoading = true;
        $scope.VmEmployee = null;
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
        $scope.GetEmployee = function () {
            EmployeeService.get({ id: $stateParams.employeeId, 'foobar': new Date().getTime() }, function (VmEmployee) {
                $scope.VmEmployee = VmEmployee; // Student.query();
                $scope.GetDates($scope.VmEmployee.UserInfo.DOB);
                $scope.isLoading = false;
                $scope.image.myCroppedImage = 'data:image/JPEG;base64,' + $scope.VmEmployee.ProfileImage;
                angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
            }, function (err) {
                showErrors(toaster, err);
            });
        };
        $scope.GetEmployee();
        $scope.AddEmployee = function () {
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
                        UpdateEmployee();
                    }

                }
            });



        };
        function UpdateEmployee() {

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
            vmEmployee.UserInfo.Employee.AcademicBranches = $('input[name=cbBranch]:checked').map(function () { return $(this).val(); }).get();
            vmEmployee.UserInfo.UserRoles = [];
            vmEmployee.UserInfo.UserRoles = $('input[name=cbRole]:checked').map(function () { return $(this).val(); }).get();
            //------
 
            EmployeeService.update({ id: $scope.VmEmployee.UserInfo.Id }, vmEmployee, function (result) {
                toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                // TODO: refresh enire page, avoid route.reload/state.reload
                //$state.transitionTo($state.current, $stateParams, {
                //    reload: true,
                //    inherit: false,
                //    notify: true
                //});
                $scope.isLoading = false;
                $location.path("/app/employee/" + $scope.VmEmployee.UserInfo.Id);
            }, function (err) {
                $scope.isLoading = false;
                showErrors(toaster, err);
            }
            );
        }
        //------------ Addresses -------------------
        $scope.addAddress = function () {
            if ($scope.VmEmployee.Addresses == null)
                $scope.VmEmployee.Addresses = [];
            $scope.VmEmployee.Addresses.push(angular.copy($scope.VmEmployee.SingleAddresses));
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
                    if ($scope.VmEmployee.Addresses[index].Id == 0 || $scope.VmEmployee.Addresses[index].Id == undefined) {
                        $scope.VmEmployee.Addresses.splice(index, 1);
                    } else {
                        $scope.VmEmployee.Addresses[index].IsActive = false;
                    }
                }
            });



        };

        //--  branch check box required
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

        uploaderPhotoUrl.onCompleteAll = function () {
            UpdateEmployee();
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