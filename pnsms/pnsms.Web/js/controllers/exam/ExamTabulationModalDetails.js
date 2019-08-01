'use strict';
/* Controllers */
// ExamTabulationModalDetails controller
app.controller('ExamTabulationModalDetailsCtrl', ['$scope', '$http', 'ExamTabulationModalDetailsService', '$filter', 'toaster', '$location', '$window', '$stateParams',

    function ($scope, $http, ExamTabulationModalDetailsService, $filter, toaster, $location, $window, $stateParams) {


        // redirect to previous page
        $scope.redirect = function () {
            window.location = "/#/app/exam/ExamTabulationSheet";
        }

        // init
        $scope.entity = { Name: "Exam Tabulation Sheet" };

        $scope.VmExamTabulationSheet = null;
        $scope.VmExamTabulationSheetList = [];
        $scope.objExamTypeWiseTabulationSheetMaster = {};
        $scope.lstExamTypeWiseTabulationSheetDetails = [];
        $scope.VmExamTabulationSheet = [];
        $scope.VmCommonSearch = {};
        $scope.apiUrlPrefix = apiUrlPrefix;
        var ExamTypeId = $stateParams.ExamTypeId;
        var StudentId = $stateParams.StudentId;

        $scope.VmCommonSearch = {
            ExamTypeId: ExamTypeId,
            StudentId: StudentId
        };

        $scope.GetExamTabulationSheetCriteria = function () {


            ExamTabulationModalDetailsService.GetExamTabulationSheetCriteria($scope.VmCommonSearch, function (result) {
                $scope.VmExamTabulationSheet = result;
              //  alert("asd");
                // $window.alert($scope.VmExamTabulationSheet);
            }, function (err) {
                showErrors(toaster, err);
                $scope.isLoading = false;
            });
        };

        $scope.GetExamTabulationSheetCriteria();
    }]);





