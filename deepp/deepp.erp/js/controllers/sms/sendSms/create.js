'use strict';
/* Controllers */
// Teacher controller
app.controller('SmsCreateCtrl', [
    '$scope', '$http', 'SmsService', 'StudentService', 'TeacherService', 'EmployeeService', '$filter', 'toaster', '$location',
function ($scope, $http, SmsService, StudentService, TeacherService, EmployeeService,$filter, toaster, $location) {

    // init
    $scope.entity = { Name: "Sms" };
    $scope.isLoading = true;
    $scope.SmsTemplate = {};
    $scope.VmShortMessage = null;
    $scope.SiblingId = null;
    $scope.PreviewText = "";
    $scope.SmsCountText = "";
    $scope.students = null;
    $scope.ShortMessageDetails = [];
    $scope.selectedSmsTemplate = null;
    $scope.selectAll = true;
    $scope.stepThreeLoading = false;
    $scope.isDate = false;
    //--------------- Teacher ---------------
    $scope.GetSmsTemplate = function () {
        SmsService.New(function (vmShortMessage) {
            $scope.VmShortMessage = vmShortMessage;
            $scope.SmsTemplate = vmShortMessage.ShortMessageTemplate;
            $scope.SmsTemplate.IsForGeneral = true;
            $scope.SmsTemplate.IsForStudent = false;

            $scope.SmsTemplate.IsForGuardian = false;

            $scope.SmsTemplate.IsForTeacher = false;
            $scope.isLoading = false;
        });

    };
    $scope.GetSmsTemplate();
    $scope.CreateSms = function () {

        CreateSms();

    };
    $scope.CheckUnCheckAll = function (selectAll) {
        for (var i = 0; i < $scope.ShortMessageDetails.length; i++) {
            $scope.ShortMessageDetails[i].isSelected = selectAll;
        }

        //angular.forEach($scope.ShortMessageDetails, function (item) {

        //    item.isSelected = $scope.selectAll;
        //}
        //);

    };
    function CreateSms() {
        //var date = $filter('date')($scope.SmsTemplate.SendAt, 'yyyy-MM-dd') + $filter('date')($scope.SmsTemplate.SendAt, 'shortTime');
        
        //$scope.isLoading = true;
        //--- set null to list properties ----
        var vmShortMessage = angular.copy($scope.VmShortMessage);
        vmShortMessage.ShortMessages.SmsTemplate = $scope.SmsTemplate.SmsTemplate;
        vmShortMessage.ShortMessages.SendAt = $scope.SmsTemplate.SendAt;//. new Date();
        vmShortMessage.ShortMessageDetails = [];
        angular.forEach($scope.ShortMessageDetails, function (item) {
            if (item.isSelected) {
                var studentId = null;
                if (item.StudentId!=null && item.StudentId>0) {
                    studentId = item.StudentId;
                }
                vmShortMessage.ShortMessageDetails.push({
                    UserInfoId: item.UserInfoId,
                    UserInfoName:item.UserInfoName,
                    MobileNumber: item.MobileNumber,
                    StudentId: studentId
                });
               // console.log(item.StudentId + "- " + item.isSelected);
            }

        });

        SmsService.save(vmShortMessage, function (result) {
            toaster.pop("success", "Success", $scope.entity.Name + " created.");
            //$scope.isLoading = false;
            $location.path("/app/sendSms/edit/" + result.Id);
        }, function (err) {
            showErrors(toaster, err);
            $scope.isLoading = false;
        }
        );

    };

    $scope.radioClick = function () {

        $scope.SmsTemplate.IsForGeneral = null;
        $scope.SmsTemplate.IsForStudent = null;
        $scope.SmsTemplate.IsForGuardian = null;
        $scope.SmsTemplate.IsForTeacher = null;
        $scope.SmsTemplate.IsForEmployee = null;
        $scope.SmsTemplate.IsForGoverningBody = null;

    };
    $scope.SearchStudent = function () {
        $scope.stepThreeLoading = true;
        $scope.steps.step3 = true;
        StudentService.GetAllSmsDetails($scope.VmShortMessage.Student, function (ShortMessageDetails) {
            $scope.ShortMessageDetails = ShortMessageDetails; // Student.query();
            

            $scope.stepThreeLoading = false;
        });
    };

    $scope.SearchTeacher = function () {
        $scope.stepThreeLoading = true;
        $scope.steps.step3 = true;
        TeacherService.GetAllSmsDetails($scope.VmShortMessage.Teacher, function (ShortMessageDetails) {
            $scope.ShortMessageDetails = ShortMessageDetails; // Student.query();
            // console.log(teachers);
            $scope.stepThreeLoading = false;
        });
    };

    $scope.SearchEmployee = function () {
        $scope.stepThreeLoading = true;
        $scope.steps.step3 = true;
        EmployeeService.GetAllSmsDetails({branchId: $scope.VmShortMessage.Employee.BranchId}, $scope.VmShortMessage.Employee , function (ShortMessageDetails) {
            $scope.ShortMessageDetails = ShortMessageDetails;
            
            $scope.stepThreeLoading = false;
        });
    };
    $scope.SearchGuardian = function () {
        $scope.stepThreeLoading = true;
        $scope.steps.step3 = true;
        StudentService.GetAllGuardianSmsDetails($scope.VmShortMessage.Student, function (ShortMessageDetails) {
            $scope.ShortMessageDetails = ShortMessageDetails;

            $scope.stepThreeLoading = false;
        });
    };

    $scope.SearchGoverningbody = function () {
        $scope.stepThreeLoading = true;
        $scope.steps.step3 = true;
        StudentService.GetAllGoverningbodySmsDetails(function (ShortMessageDetails) {
            $scope.ShortMessageDetails = ShortMessageDetails;

            $scope.stepThreeLoading = false;
        });
    };

    $scope.CalculatePreviewText = function () {

        if ($scope.SmsTemplate.SmsTemplate == undefined) {
            $scope.PreviewText = "";
            $scope.SmsCountText = "";
            return;

        }

        $scope.PreviewText = angular.copy($scope.SmsTemplate.SmsTemplate);
        $scope.SmsCountText = angular.copy($scope.SmsTemplate.SmsTemplate);
        var str = $scope.PreviewText;
        var str2 = $scope.PreviewText;
        //var strlength = str2.length;
        $scope.isDate = false;
        angular.forEach($scope.SmsTemplate.NotificationTags, function (item) {
            var regex = new RegExp(item.Tag, 'g');
            //strlength = str2.length;
            if (str.match(regex, item.PreviewText)!=null) {
                if (item.IsShowToDate == true || item.IsShowFromDate == true) {
                    $scope.isDate = true;
                }
            }
            str = str.replace(regex, item.PreviewText);
            
            str2 = str2.replace(regex, item.TextToCalculate);
            //console.log(strlength + " " + str2.length);
            //if (strlength < str2.length || strlength > str2.length) {
                
            //    console.log(item.IsShowToDate + "  -  " + item.IsShowFromDate + "  -  " + item.Tag);
            //    if (item.IsShowToDate == true || item.IsShowFromDate == true) {
            //        $scope.isDate = true;
            //    }
            //}
            //strlength = str2.length;
        });
        $scope.PreviewText = str;
        $scope.SmsCountText = str2;
        if (str2.length <= 160) {
            $scope.SmsTemplate.SmsCount = 1;
        } else {
            var strlen = Math.ceil((str2.length - 160) / 140) + 1;
            $scope.SmsTemplate.SmsCount = strlen;
        }

    };
    $scope.insertAtCaret = function (areaId, text, textToCalculate) {
        var txtarea = document.getElementById(areaId);


        var scrollPos = txtarea.scrollTop;
        var strPos = 0;
        var br = ((txtarea.selectionStart || txtarea.selectionStart == '0') ?
            "ff" : (document.selection ? "ie" : false));
        if (br == "ie") {
            txtarea.focus();
            var range = document.selection.createRange();
            range.moveStart('character', -txtarea.value.length);
            strPos = range.text.length;
        } else if (br == "ff") strPos = txtarea.selectionStart;

        var front = (txtarea.value).substring(0, strPos);
        var back = (txtarea.value).substring(strPos, txtarea.value.length);
        txtarea.value = front + text + back;
        strPos = strPos + text.length;
        if (br == "ie") {
            txtarea.focus();
            var range = document.selection.createRange();
            range.moveStart('character', -txtarea.value.length);
            range.moveStart('character', strPos);
            range.moveEnd('character', 0);
            range.select();
        } else if (br == "ff") {
            txtarea.selectionStart = strPos;
            txtarea.selectionEnd = strPos;
            txtarea.focus();
        }
        txtarea.scrollTop = scrollPos;
        $scope.SmsTemplate.SmsTemplate = txtarea.value;
        $scope.CalculatePreviewText();
    };

}]);

