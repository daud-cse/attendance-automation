'use strict';
/* Controllers */
// ExamType controller
app.controller('ExamProcessCtrl', ['$scope', '$http', 'ExamProcessService', '$filter', 'toaster', function ($scope, $http, ExamProcessService, $filter, toaster) {

    // init
    $scope.entity = { Name: "Exam Process" };
    //$scope.isLoading = true;
    ///---- paging and search/sort
    $scope.sortingOrder = "Id";
    $scope.reverse = true;
    $scope.filteredItems = [];
    $scope.groupedItems = [];
    $scope.itemsPerPage = 5;
    $scope.pagedItems = [];
    $scope.currentPage = 0;
    $scope.items = [];

    //inline add/edit
    $scope.newField = {};
    $scope.editing = {};
    $scope.isNew = false;
    $scope.selected = {};
    // alert("dfsdf");
    $scope.ExamProcess = null;
    $scope.lstExamProcess = [];

    $scope.Msg = null;




    $scope.newExamProcess = function () {
        ExamProcessService.New(function (ExamProcess) {

            $scope.ExamProcess = ExamProcess;

            //   $scope.isLoading = false;

        });

    };
    $scope.newExamProcess();


    $scope.ExamProcessExecute = function () {
        ExamProcessService.ExamProcessExecute($scope.ExamProcess, function (result) {
            $scope.Msg = result;

            toaster.pop("success", $scope.Msg[0]);
        }, function (err) {
            showErrors(toaster, err);
            $scope.isLoading = false;
        });

    };



    //function GetNewModel() {
    //    return { Id: 0, IsActive: true, GradeName: "", GradePoint: "", MarksFrom: "", MarksUpto: "", Comment: "", InstituteId: 1 };

    //}
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
}]);