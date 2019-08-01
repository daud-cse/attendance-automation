'use strict';
/* Controllers */
// Teacher controller
app.controller('SmsTemplateEditCtrl', [
    '$scope', '$http', 'SmsTemplateService', '$filter', 'toaster', '$location', '$stateParams','modalService',
function ($scope, $http, SmsTemplateService, $filter, toaster, $location, $stateParams, modalService) {

    // init
    $scope.entity = { Name: "SmsTemplate" };
    $scope.isLoading = true;
    $scope.SmsTemplate = null;
    $scope.SiblingId = null;
    $scope.isCollapsed = false;


    //--------------- Teacher ---------------
    $scope.GetSmsTemplate = function () {
        SmsTemplateService.get({ id: $stateParams.templateId, 'foobar': new Date().getTime() }, function (SmsTemplate) {
            $scope.SmsTemplate = SmsTemplate; // Student.query();
            $scope.CalculatePreviewText();
            $scope.isLoading = false;
 
            if ($scope.SmsTemplate.IsForGeneral) {
                $scope.isCollapsed = true;
            } else {
                $scope.isCollapsed = false;
            }
            
        }, function (err) {
            showErrors(toaster, err);
        });
         

    };
    $scope.GetSmsTemplate();
    $scope.AddSmsTemplate = function () {

        var modalOptions = {
            closeButtonText: 'No',
            actionButtonText: 'Yes',
            headerText: 'Save updates',
            bodyText: 'Are you sure you want to save changes?',
            type: 'warning'
        };
        modalService.showModal({}, modalOptions).then(function (result) {
            if (result === 'ok') {
                
                    CreateGetSmsTemplate();
               
            }
        });

    };

    function CreateGetSmsTemplate() {
        $scope.isLoading = true;
        //--- set null to list properties ----
        var smsTemplate = angular.copy($scope.SmsTemplate);


        SmsTemplateService.update({ id: smsTemplate.Id }, smsTemplate, function (result) {
            toaster.pop("success", "Success", $scope.entity.Name + " Updated.");
            $scope.isLoading = false;
            $location.path("/app/smsTemplate/edit/" + result.Id);
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
    $scope.PreviewText = "";
    $scope.SmsCountText = "";

    $scope.CalculatePreviewText = function () {
        $scope.PreviewText = angular.copy($scope.SmsTemplate.SmsTemplate);
        $scope.SmsCountText = angular.copy($scope.SmsTemplate.SmsTemplate);
        var str = $scope.PreviewText;
        var str2 = $scope.PreviewText;
        angular.forEach($scope.SmsTemplate.NotificationTags, function (item) {
            //console.log(item);

            var regex = new RegExp(item.Tag, 'g');

            str = str.replace(regex, item.PreviewText);
            str2 = str2.replace(regex, item.TextToCalculate);


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
    };

}]);

