'use strict';
/* Controllers */
// ExamSubjectMark controller
app.controller('ExamSubjectMarksCtrl', ['$scope', '$http', 'ExamSubjectMarksService', '$filter', 'toaster', '$location', '$window',
    function ($scope, $http, ExamSubjectMarksService, $filter, toaster, $location, $window) {

        // init
        $scope.entity = { Name: "Exam Subject Mark" };
      // $scope.isLoading = true;
     //   $scope.isUpload = true;
        //  $scope.isUploading = false;
        $scope.AcademicSectionList = null;
        $scope.AcademicSubjectList = null;
        $scope.VmExamSubjectMark = null;
        $scope.VmExamSubjectMarkList = [];
        $scope.totalStudent = 0;
        $scope.VmSubjectAcademicClassMapping =null;
        //  $scope.SiblingId = null;
      //  $scope.objExam = null;
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

        //--------------- ExamSubjectMark ---------------

        $scope.GetExamSubjectMark = function () {
            ExamSubjectMarksService.New(function (vmExamSubjectMark) {             
                $scope.VmExamSubjectMark = vmExamSubjectMark;             
                $scope.isLoading = false;                             
            });

        };

        $scope.getScetionClasswise = function () {

                 //alert($scope.VmExamSubjectMark.AcademicClassesId);

            if ($scope.VmExamSubjectMark.AcademicClassesId!= null) {
                ExamSubjectMarksService.getScetionClasswise({ id: $scope.VmExamSubjectMark.AcademicClassesId }, function (result) {
                    $scope.AcademicSectionList = result;
                    $scope.VmExamSubjectMark.AcademicSectionList = [];
                    console.log($scope.AcademicSectionList);
                    $scope.VmExamSubjectMark.AcademicSectionList = $scope.AcademicSectionList;

                })
            }
        };

        $scope.subjectchange = function (VmExamSubjectMark) {
                 
            var subject = $filter('filter')($scope.VmExamSubjectMark.SubjectList, $scope.VmExamSubjectMark.SubjectAcademicClassMappingsMapId)
            if (subject.length > 0) {

            var teacher= $filter('filter')($scope.VmSubjectAcademicClassMapping.lstSubjectAcademicClassMapping, $scope.VmExamSubjectMark.SubjectAcademicClassMappingsMapId);
            $scope.VmSubjectAcademicClassMapping.SubjectName = subject[0].Value; //+ teacher[0].SubjectGroup==null?' ':'('+teacher[0].SubjectGroup.Name+ ')';
            $scope.VmSubjectAcademicClassMapping.SubjectMarks = teacher[0].SubjectMarks;
            

            console.log(teacher);
            $scope.VmSubjectAcademicClassMapping.TeacherName = teacher[0].Teacher.UserInfo.Name;
            }
            else {
                $scope.VmSubjectAcademicClassMapping.SubjectName = "subject not found";
            }               
        }
        $scope.GetSubjectAcademicClassMapping = function () {

            if ($scope.VmExamSubjectMark.AcademicClassesSectionMapId != null) {
                ExamSubjectMarksService.GetSubjectAcademicClassMapping({ academicClassSectionMapId: $scope.VmExamSubjectMark.AcademicClassesSectionMapId }, function (result) {

                   // alert("df");
                    $scope.VmSubjectAcademicClassMapping = result;
                    //alert($scope.VmSubjectAcademicClassMapping)
                    console.log($scope.VmSubjectAcademicClassMapping);
                    //alert($scope.VmSubjectAcademicClassMapping.kvpSubjectList);
                    $scope.AcademicSubjectList = $scope.VmSubjectAcademicClassMapping.kvpSubjectList;
                    $scope.VmExamSubjectMark.SubjectList = $scope.AcademicSubjectList;                  

                })
            }
        };

        $scope.GetExamSubjectMark();

        $scope.markValidation = function (x) {
            //angular.forEach($scope.VmExamSubjectMarkList, m => {

            if (x.MarksObtained > $scope.VmSubjectAcademicClassMapping.SubjectMarks) {
                toaster.pop("error", "Error", `Marks cannot be greater than Subject Marks`);
                x.MarksObtained = 0;
            }
            if (x.MarksObtained==0) {
                toaster.pop("error", "Error", `Marks cannot be zero`);
            }
            //if (isNaN(x.MarksObtained) || x.MarksObtained < 1 || x > $scope.objExam.TotalMarks) {
            //    toaster.pop("error", "Error", `Marks cannot be greater than ${$scope.objExam.TotalMarks} or less than 0`);
            //    x.MarksObtained = 0;
            //}
            //else {
                  
                    
            //    }
            
               
         

        }
        //Exam Type Info
        $scope.examtypechange = function (VmExamSubjectMark) {
            // GetExamByCriteria
            //$window.alert($scope.VmExamSubjectMark.ExamTypeList.$filter);
            ExamSubjectMarksService.GetExamByCriteria($scope.VmExamSubjectMark, function (result) {
               
                $scope.VmExamSubjectMark.ExamList = result;                
                }, function (err) {
                    showErrors(toaster, err);
                    $scope.isLoading = false;
                });
        };

        ///Exam Info
        $scope.examchange = function (VmExamSubjectMark) {
          //  alert($scope.VmExamSubjectMark.ExamId);
            ExamSubjectMarksService.GetExamById({ id: $scope.VmExamSubjectMark.ExamId }, function (result) {

                $scope.objExam = result;
               // $window.alert($scope.objExam);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });

        
        };
        $scope.GetFilterResult = function (VmExamSubjectMark) {
            // GetExamByCriteria
            ExamSubjectMarksService.GetExamSubjectMarksByCriteria($scope.VmExamSubjectMark, function (result) {

                $scope.VmExamSubjectMarkList = result;
                //alert()
              //  $scope.isLoading = true;
                $scope.totalStudent = $scope.VmExamSubjectMarkList.length;
                if($scope.totalStudent>0){
                    $scope.VmExamSubjectMarkList[0].SubjectMarks;
                }
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
           
        };


        $scope.ExamSubjectMarksSubmit = function (VmExamSubjectMark) {
            ExamSubjectMarksService.save($scope.VmExamSubjectMarkList, function (result) {


                toaster.pop("success", "Success", $scope.entity.Name + " saved.");
                $scope.GetFilterResult(VmExamSubjectMark);
              //  $scope.VmExamSubjectMarkList = result;
                //  $scope.isLoading = true;
                // $window.alert($scope.VmExamSubjectMarkList);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
        };       

    }]);


// For Number and Decimal Input Purpose
app.directive('validNumber', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }

            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }

                var clean = val.replace(/[^-0-9\.]/g, '');
                var negativeCheck = clean.split('-');
                var decimalCheck = clean.split('.');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    clean = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        clean = negativeCheck[0];
                    }

                }

                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 2);
                    clean = decimalCheck[0] + '.' + decimalCheck[1];
                }

                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });

            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});

