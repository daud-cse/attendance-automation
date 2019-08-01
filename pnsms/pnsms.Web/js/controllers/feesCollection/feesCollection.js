'use strict';
/* Controllers */
app.controller('feesCollection', ['$scope', '$state', '$http', 'feesCollectionService', 'modalService', '$filter', 'toaster', 'UserInfoService',
    function ($scope, $state, $http, feesCollectionService, modalService, $filter, toaster, UserInfoService) {

        $scope.init = function () {
            $scope.isShowTable = false;
            $scope.isLoading = false;
            $scope.studentFees = {};
            $scope.isInit = true;
            $scope.isShowFees = false;
          
            $scope.Monthes = [
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
            $scope.Input = {
                "studentId": 0,
                "month": 0,
                "collectionDate": '',
                "isPaidVat": true,
                "chequeNo":"",
                "Modes": [{
                    "modeId": '1', "name": "Cheque/DD", "chequeNo": "000524"
                },
                { "modeId": '2', "name": "Cheque/MM", "chequeNo": "00587" }],
                "banks": [{ "bankId": '1', "name": "Islami Bank" },
                { "bankId": '2', "name": "Dutch Bangla" }],
            };
            $scope.Input.collectionDate = new Date().toDateString()
            $scope.Input.month = $scope.Monthes[new Date().getMonth()];//because index is starting with 0;
        }
        $scope.dateChange = function (event) {
            $scope.Input.month = $scope.Monthes[event.getMonth()];//because index is starting with 0;
        }
        $scope.getCheque = function (modeId) {
            $scope.Input.chequeNo = $scope.Input.Modes.filter(x => x.modeId == modeId)[0].chequeNo
            }
        $scope.entity = { Name: "Fees Collection" };

        // $scope.selectedOption = previousMonth.getMonth();

        //------------ Months---------------


        var myTimeUnformatted = new Date();
        var date = $filter('date')(myTimeUnformatted, 'yyyy');
        $scope.CurrentYear = date;
        $scope.NextYear = parseInt($scope.CurrentYear) + 1;

        //------------ Years---------------

        $scope.Years = [
            { "Key": $scope.CurrentYear, "Value": $scope.CurrentYear },
            { "Key": $scope.NextYear, "Value": $scope.NextYear }
        ];

        $scope.selectedStudent = function (selected) {
            if (selected.originalObject != undefined) {
                $scope.Input.studentId = selected.originalObject.UserInforId
                $scope.GetFilterResult($scope.Input);
            }

        }

        $scope.globalSearch = function (userInputString, timeoutPromise) {
           
            return UserInfoService.SearchUserInfo(userInputString, 11); //11 for  student
        }

        $scope.Load = function (val) {
            count = count + val;
            if (count > 2) {
                feesCollectionService.getstudents($scope.VmfeesGenerateModel.FeesGenerateAcademic, function (result) {
                    $scope.studentList = result.SearchStudents;
                });
            }
        }

        $scope.makePayment = function (studentFees) {
            $scope.studentFees.CollectionDate = $scope.Input.collectionDate;

          //  alert($scope.studentFees.CollectionDate);

          //  alert($scope.Input.collectionDate);
            feesCollectionService.save(studentFees).then(function () {
                // var previousMonth = new Date();       

                toaster.pop("success", "Success", "Fees Collection Successfully.");
                $scope.isInit = true;
                $scope.GetFilterResult($scope.Input);
            });
        };


        $scope.GetFilterResult = function (input) {
            $scope.isInit = false;
            $scope.isShowTable = false;
            $scope.isLoading = true;
            feesCollectionService.getstudent(input.studentId, $scope.Input.month.Key).then(function (result) {
                if (result.data.StudentFeesList > 0) {
                    $scope.isShowTable = true;
                }
                $scope.studentFees = result.data;
                $scope.studentFees.feesTotal = 0
                $scope.studentFees.feesPaidTotal = 0
                $scope.studentFees.feesRemainingTotal = 0
                angular.forEach($scope.studentFees.StudentFeesList, function (fees) {
                    $scope.studentFees.feesTotal += fees.FeesAmount == undefined?0: parseFloat(fees.FeesAmount);
                    $scope.studentFees.feesPaidTotal += fees.PaidAmount == undefined ? 0 : parseFloat(fees.PaidAmount)
                    $scope.studentFees.feesRemainingTotal += fees.RemainingAmount == undefined ? 0 : parseFloat(fees.RemainingAmount)
                })
                $scope.isLoading = false;
            });
        }
        $scope.printDocument = function () {
            html2canvas($("#pdf"), {
                onrendered: function (canvas) {
                    var imgData = canvas.toDataURL(
                        'image/png');
                    var doc = new jsPDF("p", "mm", "letter");
                    var width = doc.internal.pageSize.width;
                    var height = doc.internal.pageSize.height;
                    doc.addImage(imgData, 'JPEG', 0, 0, width, height);
                    doc.save('Fees Collection.pdf');
                }
            });
            
        }
 
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
//$scope.isShowFees = true;
            //var pdf = new jsPDF('p', 'pt', 'letter');
            //pdf.setFont("helvetica");
            //pdf.setFontType("bold");
            //pdf.setFontSize(14);
            //var ele = document.getElementById('pdf')
            //pdf.addHTML($("#pdf"),function () {
            //    pdf.save('Fees Reports.pdf');
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