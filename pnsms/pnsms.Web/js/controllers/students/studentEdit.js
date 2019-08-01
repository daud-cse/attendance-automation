'use strict';
/* Controllers */
// Student controller
app.controller('StudentEditCtrl', ['$scope', '$http', 'StudentService', '$filter', 'toaster', '$stateParams', 'modalService', '$state', 'FileUploader', '$location',

    function ($scope, $http, StudentService, $filter, toaster, $stateParams, modalService, $state, FileUploader, $location) {

        // init
        $scope.entity = { Name: "Student" };
        $scope.isLoading = true;
        $scope.isUploading = false;
        $scope.isUpload = true;
        $scope.VmStudent = null;
        $scope.AcademicSectionList = null;
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

        $scope.getScetionClasswise = function () {

            if ($scope.VmStudent.Student.CurrentAcademicClassId != null) {
                StudentService.getScetionClasswise({ id: $scope.VmStudent.Student.CurrentAcademicClassId }, function (result) {

                    $scope.AcademicSectionList = result;
                    $scope.VmStudent.Student.AcademicSectionList = $scope.AcademicSectionList;

                })
            }
        };


        //--------------- Student ---------------
        $scope.GetStudent = function () {
            StudentService.get({ id: $stateParams.studentId, 'foobar': new Date().getTime() }, function (vmStudent) {
                $scope.VmStudent = vmStudent; // Student.query();
                $scope.GetDates($scope.VmStudent.UserInfo.DOB);
                $scope.isLoading = false;
                $scope.image.myCroppedImage = 'data:image/JPEG;base64,' + $scope.VmStudent.ProfileImage;
                angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
            }, function (err) {
                showErrors(toaster, err);
            });
        };
        $scope.GetStudent();
        $scope.AddStudent = function () {
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Save updates',
                bodyText: 'Are you sure you want to save changes?',
                type: 'warning'
            };
            modalService.showModal({}, modalOptions).then(function (result) {
               
            
                if (result === 'ok') {
                    $scope.isLoading = true;
                    if ($scope.image.myCroppedImage.length > 500) {
                        $scope.Upload();
                    } else {
                        UpdateStudent();
                    }


                }
            });



        };

        function UpdateStudent() {
            //console.log($scope.VmStudent.Student.CoCurricularActivities);
            $scope.VmStudent.Student.CoCurricularActivities = $('input[name=coCurricular]:checked').map(function () { return $(this).val(); }).get();
            $scope.VmStudent.Student.Scholarships = $('input[name=cboScholarship]:checked').map(function () { return $(this).val(); }).get();
            console.log($scope.VmStudent.Student.Scholarships);
            $scope.isLoading = true;
            var vmstudent = angular.copy($scope.VmStudent);
            //--- set null to list properties ----
            vmstudent.CountryList = [];
            vmstudent.DistrictOrStateList = [];
            vmstudent.UserInfo.GenderList = [];
            vmstudent.UserInfo.NationalityList = [];
            vmstudent.UserInfo.ReligionList = [];
            vmstudent.UserInfo.BloodGroupList = [];
            vmstudent.Student.AcademicBranchList = [];
            vmstudent.Student.AcademicClassList = [];
            vmstudent.Student.AcademicGroupList = [];
            vmstudent.Student.AcademicSectionList = [];
            vmstudent.Student.AcademicSessionList = [];
            vmstudent.Student.AcademicVerssionList = [];
            vmstudent.Student.AcademicShiftList = [];
            vmstudent.Student.CoCurricularActivityList = [];
            vmstudent.SingleGuardian = [];
            vmstudent.SingleAddresses = [];

            // -----------------------
            StudentService.update({ id: vmstudent.Student.StudentId }, vmstudent, function (result) {
                
                toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                $location.path("/app/students/" + $scope.VmStudent.Student.StudentId);

            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
        }
        //------------ Guardian -------------------
        $scope.addGuardian = function () {
            if ($scope.VmStudent.Guardians == null) {
                $scope.VmStudent.Guardians = [];

            }
            $scope.VmStudent.Guardians.push(angular.copy($scope.VmStudent.SingleGuardian));
        };
        $scope.removeGuardian = function (index) {
            //
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Remove Guardian',
                bodyText: 'Are you sure you want to remove this guardian?',
                type: 'error'
            };

            modalService.showModal({}, modalOptions).then(function (result) {

                if (result === 'ok') {

                    //console.log($scope.VmStudent.Guardians[index].UserInfo.Id);
                    //console.log($scope.VmStudent.Guardians[$scope.VmStudent.Guardians.indexOf(index)]);
                    if ($scope.VmStudent.Guardians[index].UserInfo.Id == 0 || $scope.VmStudent.Guardians[index].UserInfo.Id == undefined) {
                        $scope.VmStudent.Guardians.splice(index, 1);
                    } else {
                        $scope.VmStudent.Guardians[index].UserInfo.IsActive = false;
                    }

                }
            });


        };
        //------------ Addresses -------------------
        $scope.addAddress = function () {
            if ($scope.VmStudent.Addresses == null)
                $scope.VmStudent.Addresses = [];
            $scope.VmStudent.Addresses.push(angular.copy($scope.VmStudent.SingleAddresses));
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
                    if ($scope.VmStudent.Addresses[index].Id == 0 || $scope.VmStudent.Addresses[index].Id == undefined) {
                        $scope.VmStudent.Addresses.splice(index, 1);
                    } else {
                        $scope.VmStudent.Addresses[index].IsActive = false;
                    }
                }
            });



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

            UpdateStudent();
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


 