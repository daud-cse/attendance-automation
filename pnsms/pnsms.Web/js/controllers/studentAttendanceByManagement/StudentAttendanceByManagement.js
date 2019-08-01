'use strict';
/* Controllers */
// Student controller
app.controller('StudentAttendanceByManagementCtrl', ['$scope', '$state', '$http','$stateParams', 'StudentAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams,StudentAttendanceService, modalService, $filter, toaster) {
         
            $scope.entity = { Name: "Student Attendance" };
            $scope.isLoading = true;
            $scope.VmStudentAttendance = null;
            $scope.VmStudentAttendanceSearch = null;
            $scope.AcademicSectionList = null;
            $scope.Isupdate = false;
            $scope.Issubmit = false;
            //$scope.iseditLoading = false;
            //$scope.isnewLoading = true;
            $scope.selected = {};
            $scope.isLoadinglist = false;
            $scope.StudentAttendanceId = 0;
            StudentAttendanceService.newmanagement(function (result) {
                $scope.VmStudentAttendanceSearch = result;               
                $scope.isLoading = false;
                $scope.Issubmit = true
                $scope.Isupdate = true;              
               
            });

            $scope.getTeachersBranchWise = function () {
                if ($scope.VmStudentAttendanceSearch.StudentAttendance.AcademicBranchId != null) {
                    StudentAttendanceService.getteachers({ id: $scope.VmStudentAttendanceSearch.StudentAttendance.AcademicBranchId }, function (result) {
                        $scope.VmStudentAttendanceSearch.StudentAttendance.TeacherList = result.StudentAttendance.TeacherList;
                    })
                }
            };
            $scope.getScetionClasswise = function () {
               
                if ($scope.VmStudentAttendanceSearch.StudentAttendance.AcademicClassId != null) {
                    StudentAttendanceService.getScetionClasswise({ id: $scope.VmStudentAttendanceSearch.StudentAttendance.AcademicClassId }, function (result) {

                        $scope.AcademicSectionList = result;
                        $scope.VmStudentAttendanceSearch.StudentAttendance.AcademicSectionList = $scope.AcademicSectionList;

                    })
                }
            };
            $scope.Load = function () {
                var selectedClass = $filter('filter')($scope.VmStudentAttendanceSearch.StudentAttendance.AcademicClassList, { Key: $scope.VmStudentAttendanceSearch.StudentAttendance.AcademicClassId })[0].Value;
                var selectedSection = $filter('filter')($scope.VmStudentAttendanceSearch.StudentAttendance.AcademicSectionList, { Key: $scope.VmStudentAttendanceSearch.StudentAttendance.AcademicSectionId })[0].Value;
                    var modalOptions = {
                    closeButtonText: 'No',
                    actionButtonText: 'Yes',
                    headerText: 'Do you want to take Attendance of Student List:',
                    bodyText: '<h3>Class:' + selectedClass + '<b></b><br/>Section: <b>' + selectedSection + '</b></h3>',
                    type: 'warning'
                };
                    modalService.showModal({}, modalOptions).then(function (result) {
                        $scope.isLoading = true;
                       // alert("sdf");
                    if (result === 'ok') {
                        StudentAttendanceService.loadstudents($scope.VmStudentAttendanceSearch, function (result1) {
                            $scope.isLoading = true;
                            if (result1.AttendanceDetails != null) {
                               
                                $scope.VmStudentAttendance = result1;
                                $scope.Issubmit = false;
                                $scope.Isupdate = true;
                                $scope.isLoading = false;                                
                                if ($scope.VmStudentAttendance.StudentAttendance.Id>0) {
                                    $scope.Issubmit = true;
                                    $scope.Isupdate = false;
                                   
                                }
                                else {
                                                                       
                                }
                               
                            }
                            else {
                                var modalOptions = {
                                    closeButtonText: 'No',
                                    actionButtonText: 'Yes',
                                    headerText: 'No Students Found!',
                                    bodyText: '<h3>Please Select Right Combination.</h3>',
                                    type: 'danger'
                                };
                                modalService.showModal({}, modalOptions).then(function (result) {
                                    if (result === 'ok') {
                                        StudentAttendanceService.new(function (result) {
                                            $scope.VmStudentAttendance = result;
                                          //  $scope.steps.step1 = true;
                                          //  $scope.steps.step2 = false;
                                            $scope.isLoading = false;
                                            $scope.isnewLoading = false;
                                           
                                        });
                                    }
                                });
                            }
                        }, function (err) {
                            showErrors(toaster, err);
                        });
                    }
                });
            };
           
            $scope.StudentListReset = function () {
                var selectedClass = $filter('filter')($scope.VmStudentAttendance.StudentAttendance.AcademicClassList, { Key: $scope.VmStudentAttendance.StudentAttendance.AcademicClassId })[0].Value;
                var selectedSection = $filter('filter')($scope.VmStudentAttendance.StudentAttendance.AcademicSectionList, { Key: $scope.VmStudentAttendance.StudentAttendance.AcademicSectionId })[0].Value;
                var modalOptions = {
                    closeButtonText: 'No',
                    actionButtonText: 'Yes',
                    headerText: 'Do you want Reset Attendance Process?',
                    bodyText: '<h3>Class:' + selectedClass + '<b></b><br/>Section: <b>' + selectedSection + '</b></h3>',
                    type: 'danger'
                };
                modalService.showModal({}, modalOptions).then(function (result) {
                    if (result === 'ok') {
                        StudentAttendanceService.new(function (result) {
                            $scope.VmStudentAttendance = result;
                           // $scope.steps.step1 = true;
                           // $scope.steps.step2 = false;
                            $scope.isLoading = true;
                        })
                    }
                });
            };
           
            //------------------------------
            $scope.AttendanceSubmit = function () {
                $scope.VmStudentAttendance.StudentAttendance = $scope.VmStudentAttendance.StudentAttendance;
                StudentAttendanceService.saveattendance($scope.VmStudentAttendance, function (result) {
                    $state.go('app.studentAttendanceByManagement.StudentAttendanceListByManagement');
                    toaster.pop("success", "Success", $scope.entity.Name + " created.");
                }, function (err) {
                    showErrors(toaster, err);
                }
               );
            };
        //------------------------------

            if ($stateParams.attenanceId=='undefined') {
                $stateParams.attenanceId = 0;
            }

            if ($stateParams.attenanceId > 0) {
               
                $scope.GetSingleAttendance = function () {
                   
                    StudentAttendanceService.get({ id: $stateParams.attenanceId }, function (result) {
                       
                        $scope.VmStudentAttendance = result;
                        $scope.VmStudentAttendanceSearch = result;
                        //$scope.VmStudentAttendanceSearch.
                       // $scope.selected = angular.copy(result.StudentAttendance);
                       // alert($scope.VmStudentAttendance.StudentAttendance.AcademicSectionId);
                        //$scope.isnewLoading = true;
                        //$scope.iseditLoading = false;
                        $scope.Issubmit = true;
                        $scope.Isupdate = false;
                       
                    });                   
                }
                $scope.GetSingleAttendance();
            }
           

            $scope.AttendanceEditSubmit = function () {
                 var StudentAttendanceId = $scope.VmStudentAttendance.StudentAttendance.Id;                
                 $scope.VmStudentAttendance.StudentAttendance = $scope.VmStudentAttendance.StudentAttendance;
                $scope.VmStudentAttendance.StudentAttendance.Id =StudentAttendanceId;
                //alert($scope.VmStudentAttendance.StudentAttendance.Id)
                StudentAttendanceService.updateattendance($scope.VmStudentAttendance, function (result) {
                    $state.go('app.studentAttendanceByManagement.StudentAttendanceListByManagement');
                    toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
                }, function (err) {
                    showErrors(toaster, err);
                }
               );
            };

    }]);

app.controller('StudentAttendanceListByManagementCtrl', ['$scope', '$state', '$http', 'StudentAttendanceService', '$filter', 'toaster',
    function ($scope, $state, $http, StudentAttendanceService, $filter, toaster) {

        $scope.entity = { Name: "Student Attendance List" };
        $scope.VmSearchAttendance = null;
        $scope.AcademicSectionList = null;
        $scope.isLoading = true;
        var myTimeUnformatted = new Date();
        var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
        StudentAttendanceService.listbymanagement({ startDate: date, endDate: date }, function (result) {
            $scope.VmSearchAttendance = result;
            date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
            $scope.VmSearchAttendance.startDate = date;
            $scope.VmSearchAttendance.endDate = date;
          //  alert("sdf");
            $scope.isLoading = false;
        });
        $scope.getScetionClasswise = function () {
           
            if ($scope.VmSearchAttendance.ClassId != null) {
                StudentAttendanceService.getScetionClasswise({ id: $scope.VmSearchAttendance.ClassId }, function (result) {

                    $scope.AcademicSectionList = result;
                    $scope.VmSearchAttendance.SectionList = $scope.AcademicSectionList;

                })
            }
        };
        $scope.GetFilterResult = function () {
            $scope.isLoading = true;
            StudentAttendanceService.listbymanagement($scope.VmSearchAttendance, function (attendanceList) {
                $scope.VmSearchAttendance = attendanceList;
                $scope.isLoading = false;
            });

        };


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