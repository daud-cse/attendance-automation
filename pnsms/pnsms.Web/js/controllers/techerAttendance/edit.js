'use strict';
/* Controllers */
app.controller('TeacherAttendanceEditCtrl', ['$scope', '$state', '$stateParams', '$http', 'TeacherAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $stateParams, $http, TeacherAttendanceService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Employee Attendance" };
        $scope.isLoading = true;
        $scope.VmUserAttendance = null;

        $scope.GetSingleAttendance = function () {
            
            TeacherAttendanceService.getsingle({ id: $stateParams.teacherattendanceId }, function (result) {
                $scope.VmUserAttendance = result;                
                console.log($scope.VmUserAttendance);
                $scope.isLoading = false;
            });
        }
        $scope.GetSingleAttendance();


        //------------------------------
        $scope.EditTeacherAttendance = function () {            
            TeacherAttendanceService.updateteacherattendance($scope.VmUserAttendance, function (result) {
                $state.go('app.techerAttendance.list');
                toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
            }, function (err) {
                showErrors(toaster, err);
            }
           );
        };
        function convertUTCDateToLocalDate(date) {
                var newDate = new Date(date.getTime() + date.getTimezoneOffset() * 60 * 1000);

                var offset = date.getTimezoneOffset() / 60;
                var hours = date.getHours();

                newDate.setHours(hours - offset);

                return newDate;
            };

            $scope.inlineUpdate = function (object) {

                // alert("hello");
                var InTime = convertUTCDateToLocalDate(new Date(object.InTime));
              //  var InTime = new Date(object.InTime);
                var OutTime = convertUTCDateToLocalDate(new Date(object.OutTime));
               // var OutTime = new Date(object.OutTime);
                object.InTime = InTime;//.toString();
                object.OutTime = OutTime;//.toString();
                console.log(object);
            TeacherAttendanceService.updateteacherattendancedetails(object, function (result) {
               // $state.go('app.techerAttendance.list');
                toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
            }, function (err) {
                showErrors(toaster, err);
            }
         );
        }
        $scope.printDocument = function () {            
            var pdf = new jsPDF('p', 'pt', 'a4');
            pdf.setFont("helvetica");
            pdf.setFontType("bold");
            pdf.setFontSize(14);
            pdf.addHTML($("#pdf"), function () {
                pdf.save('Attendance Reports.pdf');
            });
            //var pdf = new jsPDF('p', 'pt', 'letter')
            //    , source = $("#pdf").html()
            //    , specialElementHandlers = {

            //        '#bypassme': function (element, renderer) {

            //            return true;
            //        }
            //    },
            //    margins = {
            //        top: 30,
            //        bottom: 40,
            //        left: 35,
            //        width: 700
            //    };

            //pdf.setFontSize(9);
            //pdf.text(30, 35, 'Branch Name: ' + $scope.VmUserAttendance.UserAttendance.BranchName);
            //pdf.text(150, 35, 'Attendace Date : ' + $scope.VmUserAttendance.UserAttendance.AttendanceDate);
            //pdf.setFontSize(12);
            //pdf.text(200, 20, 'Attendance Sheet');
            //pdf.setFontSize(9);
            ////pdf.setFontStyle('italic');
            //pdf.text(350, 35, 'Total : ' + $scope.VmUserAttendance.UserAttendance.TotalCount);
            //pdf.text(400, 35, 'Present : ' + $scope.VmUserAttendance.UserAttendance.PresentCount);

            //pdf.fromHTML(
            //    source
            //    , margins.left
            //    , margins.top
            //    , {
            //        'width': margins.width
            //        , 'elementHandlers': specialElementHandlers
            //    },
            //    function (dispose) {
            //        pdf.save('test' + '.pdf');
            //    },
            //    margins
            //)
        }
    }]);

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