'use strict';
/* Controllers */
// Student controller
app.controller('StudentCtrl', ['$scope', '$http', 'StudentService', '$filter', 'toaster', 'FileUploader', '$location',

    function ($scope, $http, StudentService, $filter, toaster, FileUploader, $location) {

        // init
        $scope.entity = { Name: "Student" };
        $scope.isLoading = true;
        $scope.isUpload = true;
        $scope.isUploading = false;
        $scope.VmStudent = null;
        $scope.SiblingId = null;
        $scope.AcademicSectionList=null;
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

        //--------------- Student ---------------
        $scope.GetStudent = function () {
            StudentService.New(function (vmStudent) {
                var id = vmStudent.Student.AcademicBranchList[0].Key;
                $scope.VmStudent = vmStudent;
                // set userInfo.Name
                $scope.VmStudent.UserInfo.FirstName = '';
                $scope.VmStudent.UserInfo.MiddleName = '';
                $scope.VmStudent.UserInfo.LastName = '';
                $scope.isLoading = false;
                //set profile image crop envent
                angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
                
                $scope.VmStudent.Student.CurrentAcademicBranchId = id;
            });

        };
        $scope.getScetionClasswise = function () {
          
            if ($scope.VmStudent.Student.CurrentAcademicClassId != null) {
                StudentService.getScetionClasswise({ id: $scope.VmStudent.Student.CurrentAcademicClassId }, function (result) {                   
                 
                    $scope.AcademicSectionList = result;                
                    $scope.VmStudent.Student.AcademicSectionList = $scope.AcademicSectionList;
                  
                })
            }
        };

        $scope.GetStudent();
        $scope.AddStudent = function () {
            $scope.VmStudent.Student.CoCurricularActivities = $('input[name=coCurricular]:checked').map(function () { return $(this).val(); }).get();
            $scope.VmStudent.Student.Scholarships = $('input[name=cboScholarship]:checked').map(function () { return $(this).val(); }).get();
            console.log($scope.VmStudent.Student.Scholarships);
            if ($scope.image.myCroppedImage.length > 500) {
                $scope.Upload();
            } else {
                CreateStudent();
            }

        };
        // create student----------------
        function CreateStudent() {
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
            StudentService.save(vmstudent, function (result) {
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
                $location.path("/app/students/" + result.Id);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
        }
        //--------------- Sibling ---------------
        $scope.GetSibling = function () {
            if ($scope.SiblingId != null) {
                $scope.isLoading = true;
                StudentService.get({ id: $scope.SiblingId, 'foobar': new Date().getTime() }, function (vmStudent) {
                    $scope.VmStudent = vmStudent;
                    $scope.VmStudent.UserInfo.Id = 0;
                    $scope.VmStudent.Student.Id = 0;
                    $scope.VmStudent.Student.StudentId = 0;
                    $scope.VmStudent.UserInfo.FirstName = '';
                    $scope.VmStudent.UserInfo.MiddleName = '';
                    $scope.VmStudent.UserInfo.LastName = '';
                    $scope.VmStudent.UserInfo.Name = '';
                    $scope.VmStudent.UserInfo.GenderId = '';
                    $scope.VmStudent.UserInfo.DOB = '';
                    $scope.VmStudent.UserInfo.SSN = '';
                    $scope.VmStudent.UserInfo.PassportNo = '';
                    $scope.VmStudent.UserInfo.ReligionId = '';
                    $scope.VmStudent.UserInfo.NationalityId = '';
                    $scope.VmStudent.UserInfo.BloodGroupId = '';
                    $scope.VmStudent.UserInfo.EmailAddress = '';
                    $scope.VmStudent.UserInfo.ContactNumber1 = '';
                    $scope.VmStudent.UserInfo.ContactNumber2 = '';
                    $scope.VmStudent.UserInfo.GoogleId = '';
                    $scope.VmStudent.UserInfo.FacebookId = '';
                    $scope.VmStudent.UserInfo.MicrosoftId = '';
                    $scope.VmStudent.UserInfo.TwitterId = '';
                    $scope.VmStudent.Student.CurrentRollNo = '';
                    $scope.VmStudent.Student.CurrentAcademicBranchId = '';
                    $scope.VmStudent.Student.CurrentAcademicClassId = '';
                    $scope.VmStudent.Student.CurrentAcademicGroupId = '';
                    $scope.VmStudent.Student.CurrentAcademicVerssionId = '';
                    $scope.VmStudent.Student.CurrentAcademicSectionId = '';
                    $scope.VmStudent.Student.CurrentAcademicSessionId = '';
                    $scope.VmStudent.Student.CurrentAcademicShiftId = '';
                    $scope.isLoading = false;
                }, function (err) {
                    showErrors(toaster, err);
                    $scope.isLoading = false;
                });
            }

        };
        //------------ Guardian -------------------
        $scope.addGuardian = function () {
            if ($scope.VmStudent.Guardians == null) {
                $scope.VmStudent.Guardians = [];
            }
            $scope.VmStudent.Guardians.push(angular.copy($scope.VmStudent.SingleGuardian));
        };
        $scope.removeGuardian = function (index) {
            $scope.VmStudent.Guardians.splice(index, 1);
        };
        //------------ Addresses -------------------
        $scope.addAddress = function () {
            if ($scope.VmStudent.Addresses == null)
                $scope.VmStudent.Addresses = [];
            $scope.VmStudent.Addresses.push(angular.copy($scope.VmStudent.SingleAddresses));
        };
        $scope.removeAddress = function (index) {
            $scope.VmStudent.Addresses.splice(index, 1);
        };


        //--------------- file uploader ---------------
        $scope.pUploaderPhotoUrl = false;
        var imageApi =apiUrlPrefix + "api/image";
        var uploaderPhotoUrl = $scope.uploaderPhotoUrl = new FileUploader({
            url: imageApi,
            autoUpload: false,
        });

      
        uploaderPhotoUrl.onCompleteAll = function () {

            CreateStudent();
        };

        //----------------
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

