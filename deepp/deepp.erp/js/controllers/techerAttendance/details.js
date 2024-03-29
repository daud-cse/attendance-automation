﻿'use strict';
/* Controllers */
// Student controller
app.controller('TeacherAttendanceDetailsCtrl', ['$scope', '$state', '$http', '$stateParams', 'TeacherAttendanceService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, TeacherAttendanceService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Employee  Attendance" };
        $scope.isLoading = true;
        $scope.VmUserAttendance = null;

        $scope.GetSingleAttendance = function () {
            TeacherAttendanceService.getsingle({ id: $stateParams.teacherattendanceId }, function (result) {
                $scope.VmUserAttendance = result;
                $scope.isLoading = false;
            });
        }
        $scope.GetSingleAttendance();

        $scope.printDocument = function () {
            html2canvas($("#pdf"), {
                onrendered: function (canvas) {
                    var imgData = canvas.toDataURL(
                        'image/png');
                    var doc = new jsPDF("P", "mm", "a4");
                    var width = doc.internal.pageSize.width;
                    var height = doc.internal.pageSize.height;
                    doc.addImage(imgData, 'JPEG', 0, 0, width, height-100);
                    doc.save('sample-file.pdf');
                }
            });
            //var pdf = new jsPDF('p', 'pt', 'a4');
            //html2canvas($("#pdf"),)
            //var options = { format: 'PNG' };
            //pdf.setFont("helvetica");
            //pdf.setFontType("bold");
            //pdf.setFontSize(14);
            //pdf.setFillColor(255, 255, 255, 0);
            //pdf.setFontSize(9);
            //pdf.text(30, 35, 'Branch Name: ' + $scope.VmUserAttendance.UserAttendance.BranchName);
            //pdf.text(150, 35, 'Attendace Date : ' + $scope.VmUserAttendance.UserAttendance.AttendanceDate);
            //pdf.setFontSize(12);
            //pdf.text(200, 20, 'Attendance Sheet');
            //pdf.setFontSize(9);
            ////pdf.setFontStyle('italic');
            //pdf.text(350, 35, 'Total : ' + $scope.VmUserAttendance.UserAttendance.TotalCount);
            //pdf.text(400, 35, 'Present : ' + $scope.VmUserAttendance.UserAttendance.PresentCount);          
            //pdf.addHTML($("#pdf"), x, y, options, function () {
            //    pdf.save('web.pdf');
            //});
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
            //        left: 30,
            //        width: 1000
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
            //    function (disdeeppe) {
            //        pdf.save('Attendance Reports' + '.pdf');
            //    },
            //    margins
            //)
        }
        

    }]);