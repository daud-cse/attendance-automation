'use strict';
/* Controllers */
// AttendanceReports controller
app.controller('AttendanceReportsCtrl', ['$scope', '$http', '$window', 'AttendanceReportsService', '$filter', 'toaster','UserInfoService', function ($scope, $http, $window, AttendanceReportsService, $filter, toaster, UserInfoService) {

    // init
    $scope.entity = { Name: "AttendanceReports" };
    $scope.isLoading = true;
    ///---- paging and search/sort
    $scope.sortingOrder = "Id";
    $scope.reverse = true;
    $scope.filteredItems = [];
    $scope.groupedItems = [];
    $scope.itemsPerPage = 5;
    $scope.pagedItems = [];
    $scope.currentPage = 0;
    $scope.items = [];
    //$scope.NewAttendanceReports = null;
    //inline add/edit
    $scope.newField = {};
    $scope.editing = {};
    $scope.isNew = false;
    $scope.selected = {};
    $scope.NewAttendanceReports = null;
    $scope.AcademicSectionList = null;
    var myTimeUnformatted = new Date();
    var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
    // alert("dfsdf");
    $scope.Months = [
       { "Key": '1', "Value": "January" },
       { "Key": '2', "Value": "February" },
       { "Key": '3', "Value": "March" },
       { "Key": '4', "Value": "April" },
       { "Key": '5', "Value": "May" },
       { "Key": '6', "Value": "June" },
       { "Key": '7', "Value": "July" },
       { "Key": '8', "Value": "August" },
       { "Key": '9', "Value": "September" },
       { "Key": '10', "Value": "October" },
       { "Key": '11', "Value": "November" },
       { "Key": '12', "Value": "December" }
    ];
    $scope.selectedTeacher = function (selected) {
        if (selected.originalObject != undefined) {
            $scope.NewAttendanceReports.UserInfoId = selected.originalObject.UserInforId
        }
    }
    $scope.selectedStudent = function (selected) {
        if (selected.originalObject != undefined) {
            $scope.NewAttendanceReports.UserInfoId = selected.originalObject.UserInforId
        }
      
    }
    $scope.globalSearch = function (userInputString, timeoutPromise) {

        return UserInfoService.SearchUserInfo(userInputString,11); //11 for  student
    }

  
    $scope.teacherGlobalSearch = function (userInputString, timeoutPromise) {

        return UserInfoService.SearchUserInfo(userInputString, 13); //13 for  teacher
    }
   
    $scope.New = function () {

       // alert("safds");
        AttendanceReportsService.New(function (AttendanceReports) {

            $scope.NewAttendanceReports = AttendanceReports;
            // alert($scope.NewAttendanceReports);   
            $scope.NewAttendanceReports.Month = $scope.Months[(new Date()).getMonth()].Key
            $scope.NewAttendanceReports.Year = (new Date()).getFullYear();
          //  $scope.NewAttendanceReports.startDate = date;
           // $scope.NewAttendanceReports.endDate = date;
            //$scope.NewAttendanceReports.Day = (new Date()).getDay()
            $scope.isEdit = false;
            $scope.isLoading = false;
            $scope.isNew = true;

        });

    };

    $scope.New();

  //  $scope.AttendanceReports = GetNewModel();

    $scope.GetAttendanceReports = function (AttendanceReports) {
       
        // alert($scope.NewAttendanceReports.UserInfoId);
        
        //for Live
      // $window.open("/erp/AttendanceReport/GetAttendanceReportsDataSet?AcademicBranchId=" + $scope.NewAttendanceReports.AcademicBranchId + "&AcademicClassesId=" + $scope.NewAttendanceReports.AcademicClassesId + "&AcademicSectionId=" + $scope.NewAttendanceReports.AcademicSectionId + "&AcademicPeriodId=" + $scope.NewAttendanceReports.AcademicPeriodId + "", "_blank");
        //For Development
        $window.open("AttendanceReport/GetAttendanceReportsDataSet?AcademicBranchId=" + $scope.NewAttendanceReports.AcademicBranchId + "&AcademicClassesId=" + $scope.NewAttendanceReports.AcademicClassesId + "&AcademicSectionId=" + $scope.NewAttendanceReports.AcademicSectionId + "&AcademicPeriodId=" + $scope.NewAttendanceReports.AcademicPeriodId + "&Month=" + $scope.NewAttendanceReports.Month + "&Day=" + $scope.NewAttendanceReports.Day + "&Year=" + $scope.NewAttendanceReports.Year + "&AcademicDepartmentId=" + $scope.NewAttendanceReports.AcademicDepartmentId + "", "_blank");
    };

    $scope.GetUserAttendanceReports = function (AttendanceReports) {
       
        $scope.NewAttendanceReports.startDate = $filter('date')($scope.NewAttendanceReports.startDate, 'yyyy-MM-dd');

        $scope.NewAttendanceReports.endDate = $filter('date')($scope.NewAttendanceReports.endDate, 'yyyy-MM-dd');

        
        //for Live        
        $window.open("/erp/AttendanceReport/GetUserAttendanceReportsDataSet?AcademicBranchId=" + $scope.NewAttendanceReports.AcademicBranchId + "&UserInfoId=" + $scope.NewAttendanceReports.UserInfoId + "&Month=" + $scope.NewAttendanceReports.Month + "&Day=" + $scope.NewAttendanceReports.Day + "&Year=" + $scope.NewAttendanceReports.Year + "&AcademicDepartmentId=" + $scope.NewAttendanceReports.AcademicDepartmentId + "&startDate=" + $scope.NewAttendanceReports.startDate + "&endDate=" + $scope.NewAttendanceReports.endDate +   "", "_blank");
        //For Development
       // $window.open("/AttendanceReport/GetUserAttendanceReportsDataSet?AcademicBranchId=" + $scope.NewAttendanceReports.AcademicBranchId + "&UserInfoId=" + $scope.NewAttendanceReports.UserInfoId + "&Month=" + $scope.NewAttendanceReports.Month + "&Day=" + $scope.NewAttendanceReports.Day + "&Year=" + $scope.NewAttendanceReports.Year + "&AcademicDepartmentId=" + $scope.NewAttendanceReports.AcademicDepartmentId + "&startDate=" + $scope.NewAttendanceReports.startDate + "&endDate=" + $scope.NewAttendanceReports.endDate + "", "_blank");
    };
    $scope.GetUserAttendanceSummaryReports = function (AttendanceReports) {

        // alert($scope.NewAttendanceReports.UserInfoId);

        //for Live        
       // $window.open("/erp/AttendanceReport/GetUserAttendanceSummaryReportsDataSet?AcademicBranchId=" + $scope.NewAttendanceReports.AcademicBranchId + "&AcademicSessionId=" + $scope.NewAttendanceReports.AcademicSessionId + "&UserInfoId=" + $scope.NewAttendanceReports.UserInfoId + "&Month=" + $scope.NewAttendanceReports.Month + "&Day=" + $scope.NewAttendanceReports.Day + "&Year=" + $scope.NewAttendanceReports.Year + "", "_blank");
        //For Development
         $window.open("AttendanceReport/GetUserAttendanceSummaryReportsDataSet?AcademicBranchId=" + $scope.NewAttendanceReports.AcademicBranchId + "&AcademicSessionId=" + $scope.NewAttendanceReports.AcademicSessionId + "&UserInfoId=" + $scope.NewAttendanceReports.UserInfoId + "&Month=" + $scope.NewAttendanceReports.Month + "&Day=" + $scope.NewAttendanceReports.Day + "&Year=" + $scope.NewAttendanceReports.Year + "&AcademicDepartmentId=" + $scope.NewAttendanceReports.AcademicDepartmentId + "", "_blank");
    };
    $scope.getScetionClasswise = function () {

        if ($scope.NewAttendanceReports.AcademicClassesId != null) {
            AttendanceReportsService.getScetionClasswise({ id: $scope.NewAttendanceReports.AcademicClassesId }, function (result) {

                $scope.AcademicSectionList = result;
                $scope.NewAttendanceReports.AcademicSectionList = $scope.AcademicSectionList;

            })
        }
    };
    $scope.Edit = function (item) {
        $scope.editing = $scope.pagedItems[$scope.currentPage].indexOf(item);
        $scope.isEdit = true;
        $scope.isNew = false;
        $scope.isLoading = false;
        $scope.selected = angular.copy(item);
        AttendanceReportsService.New(function (AttendanceReports) {
            $scope.NewAttendanceReports = AttendanceReports;
        });
    };
    $scope.Update = function (item) {
        // alert(item.Id);
        AttendanceReportsService.update({ id: item.Id }, item, function (result) {
            toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            $scope.selected = {};
            $scope.GetAll();
        }, function (err) { showErrors(toaster, err); });

    };

    ///
    // gets the template to ng-include for a table row / item
    $scope.getTemplate = function (item) {
        //alert('is call');
        if (item.Id === $scope.selected.Id) return "edit";
        else

            return 'display';
    };

    $scope.reset = function () {
        $scope.pagedItems[$scope.currentPage][$scope.editing] = angular.copy($scope.selected);
        $scope.selected = {};
    };

   


}]);






function GetNewModel() {
    return { Id: 0, IsActive: true, Name: "", AttendanceReportsDateFrom: "", AttendanceReportsDateTo: "", AttendanceReportsTime: "", TotalMarks: "", HighestMarks: "", PassMarks: "", AcceptMarks: "" };

}
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