'use strict';
/* Controllers */
// ExamTabulationSheet controller
app.controller('ExamTabulationSheetCtrl', ['$scope', '$http', 'ExamTabulationSheetService', '$filter', 'toaster', '$location', '$window',

    function ($scope, $http, ExamTabulationSheetsService, $filter, toaster, $location, $window) {

        // init
        $scope.entity = { Name: "ExamTabulationSheet" };
        // $scope.isLoading = true;
        //   $scope.isUpload = true;
        //  $scope.isUploading = false;
        $scope.showModalExamTabulationSheet = false;
        $scope.VmExamTabulationSheet = null;
        $scope.isTrabulation = true;
        $scope.VmExamTabulationSheetList = [];
        $scope.objExamTypeWiseTabulationSheetMaster = null;
        $scope.lstExamTypeWiseTabulationSheetDetails = [];
        $scope.VmCommonSearch = {};

        //  $scope.SiblingId = null;
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
      //  alert("dsf");
        //--------------- ExamTabulationSheet ---------------

        $scope.newExamTabulationSheet = function () {
            ExamTabulationSheetsService.New(function (vmExamTabulationSheet) {
                //   var id = vmExamTabulationSheet.ExamTabulationSheet.AcademicBranchList[0].Key;
                $scope.VmExamTabulationSheet = vmExamTabulationSheet;
                // set userInfo.Name
                // $scope.VmExamTabulationSheet.UserInfo.FirstName = '';
                //  $scope.VmExamTabulationSheet.UserInfo.MiddleName = '';
                //  $scope.VmExamTabulationSheet.UserInfo.LastName = '';
                $scope.isLoading = false;
                //set profile image crop envent
                //angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);

                //  $scope.VmExamTabulationSheet.ExamTabulationSheet.CurrentAcademicBranchId = id;
            });

        };

        $scope.newExamTabulationSheet();

        $scope.getScetionClasswise = function () {

           // alert($scope.VmExamTabulationSheet.AcademicClassesId);

            if ($scope.VmExamTabulationSheet.AcademicClassesId != null) {
                ExamTabulationSheetsService.getScetionClasswise({ id: $scope.VmExamTabulationSheet.AcademicClassesId }, function (result) {
                    $scope.AcademicSectionList = result;
                    $scope.VmExamTabulationSheet.AcademicSectionList = [];
                    console.log($scope.AcademicSectionList);
                    $scope.VmExamTabulationSheet.AcademicSectionList = $scope.AcademicSectionList;

                })
            }
        };


        $scope.examtypechange = function (VmExamTabulationSheet) {
            // GetExamByCriteria
            //$window.alert($scope.VmExamTabulationSheet.ExamTypeList.$filter);
            ExamTabulationSheetsService.GetExamByCriteria($scope.VmExamTabulationSheet, function (result) {

                $scope.VmExamTabulationSheet.ExamList = result;
                //$window.alert($scope.VmExamTabulationSheet.ExamList);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });

            ExamTabulationSheetsService.GetSubjectCriteria($scope.VmExamTabulationSheet, function (result) {

                $scope.VmExamTabulationSheet.SubjectList = result;
                //$window.alert($scope.VmExamTabulationSheet.ExamList);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
        };
        $scope.GetFilterResult = function (VmExamTabulationSheet) {
            // GetExamByCriteria
            ExamTabulationSheetsService.GetTabulationSheetMasterCriteria($scope.VmExamTabulationSheet, function (result) {

                $scope.VmExamTabulationSheetList = result;
                //  $scope.isLoading = true;
                // $window.alert($scope.VmExamTabulationSheetList);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });

        };

        //$scope.VmCommonSearch(){
        //    var phone = {number: '123456789'};
        //    $scope.person.phones.push(phone);
        //}
        $scope.GetExamTabulationSheetCriteria = function (ExamTypeId, StudentId) {
           
          //  $window.alert(StudentId);
            $scope.VmCommonSearch = {
                ExamTypeId: ExamTypeId,
                StudentId: StudentId
            };

            ExamTabulationSheetsService.GetExamTabulationSheetCriteria($scope.VmCommonSearch, function (result) {

                //  $window.alert(VmCommonSearch);
                 $scope.objExamTypeWiseTabulationSheetMaster = result.objExamTypeWiseTabulationSheetMaster;
                 $scope.lstExamTypeWiseTabulationSheetDetails = result.lstExamTypeWiseTabulationSheetDetails;
                  $scope.showModalExamTabulationSheet = true;
                //  $scope.isLoading = true;
                // $window.alert($scope.VmExamTabulationSheetList);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
        };

        $scope.ExamTabulationSheetsSubmit = function (VmExamTabulationSheet) {
            ExamTabulationSheetsService.save($scope.VmExamTabulationSheetList, function (result) {
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
                //  $scope.VmExamTabulationSheetList = result;
                //  $scope.isLoading = true;
                // $window.alert($scope.VmExamTabulationSheetList);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
        };
      
    }]);

