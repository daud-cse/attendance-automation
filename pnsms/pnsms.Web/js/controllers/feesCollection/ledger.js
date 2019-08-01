'use strict';
/* Controllers */
app.controller('FeesColeectionLedgerCtrl', ['$scope', '$state', '$http', '$stateParams', 'FeesCollectionService', 'modalService', '$filter', 'toaster', '$modal', '$sce',
function ($scope, $state, $http, $stateParams, FeesCollectionService, modalService, $filter, toaster, $modal, $sce) {

        $scope.entity = { Name: "Student Ledger" };
        $scope.student = null;
        $scope.vmFeesCollectionModel = null;
        $scope.ProfileImage = "";
        $scope.noData = "";
        $scope.receiptText = "";
        $scope.teacher = { selected: {} };
        $scope.Math = window.Math;
        $scope.totalSum = 0;

        $scope.searchModel = {};
        $scope.isLadger = true;
        $scope.isSearch = false;
        $scope.branchId = 0;

        $scope.isLoading = true;
        FeesCollectionService.stuLadgerInit(function (result) {
            $scope.isLoading = false;

            $scope.totalDebit = 0;
            $scope.totalCredit = 0;
            $scope.totalWOVCredit = 0;
            $scope.totalVCredit = 0;
            $scope.totalSum = 0;
            $scope.vmFeesCollectionModel = result;
        });


        $scope.refreshTeacher = function (teacher) {
            if (teacher.length > 2) {
                return $http.get(apiUrlPrefix + 'api/student/autoCompletebyNameId?s=' + teacher + '+&branchId=' + $scope.vmFeesCollectionModel.BranchId
                ).then(function (teacher) {
                    $scope.teachers = teacher.data;
                });
            }
        };

        $scope.ShowSearch = function () {
            $scope.isLadger = false;
            $scope.isSearch = true;

            $scope.totalDebit = 0;
            $scope.totalCredit = 0;
            $scope.totalWOVCredit = 0;
            $scope.totalVCredit = 0;
            $scope.totalSum = 0;
        }

        $scope.HideSearch = function () {
            $scope.isLadger = true;
            $scope.isSearch = false;
            $scope.history1 = null;

            $scope.totalDebit = 0;
            $scope.totalCredit = 0;
            $scope.totalWOVCredit = 0;
            $scope.totalVCredit = 0;
            $scope.totalSum = 0;
        }

        $scope.GetAdvSearchResult = function () {
            $scope.isLadger = false;
            $scope.isSearch = true;
            $scope.isLoading = true;

            $scope.history1 = null;
            $scope.totalDebit = 0;
            $scope.totalCredit = 0;
            $scope.totalWOVCredit = 0;
            $scope.totalVCredit = 0;
            $scope.totalSum = 0;

            $scope.searchModel.UpToDate = $scope.vmFeesCollectionModel.UpToDate;
            $scope.searchModel.SessionId = $scope.vmFeesCollectionModel.SessionId;
            $scope.searchModel.ReportTypeId = $scope.reportTypeId;
            $scope.searchModel.StudentId = $scope.teacher.selected.Id;

            FeesCollectionService.getStuFeesLedger($scope.searchModel, function (result) {
                $scope.isLoading = false;
                $scope.history1 = result;
                angular.forEach($scope.history1, function (item1) {
                    $scope.totalDebit += (item1.Debit != null ? item1.Debit : 0);
                    $scope.totalCredit += (item1.Credit != null ? item1.Credit : 0);
                    $scope.totalWOVCredit += (item1.WOVat != null ? item1.WOVat : 0);
                    $scope.totalVCredit += (item1.Vat != null ? item1.Vat : 0);
                });
                $scope.totalSum = Math.round(($scope.totalDebit != null ? $scope.totalDebit : 0) - ($scope.totalCredit != null ? $scope.totalCredit : 0));
            });
        }

        $scope.GetSearchResult = function () {
            $scope.isLoading = true;
            $scope.history1 = null;
            $scope.totalSum = 0;
            $scope.vmFeesCollectionModel.StudentId = $scope.teacher.selected.Id;
            $scope.branchId = $scope.vmFeesCollectionModel.BranchId;
            FeesCollectionService.getdetailsByLadger($scope.vmFeesCollectionModel, function (result) {
                $scope.formStatus = true;
                $scope.isSearch = false;
                $scope.isLadger = true;
                $scope.recepitStatus = false;
                $scope.$on('$destroy', function () {
                    $scope.vmFeesCollectionModel.feesDetails.TotalAdvanceReceived;
                    $scope.vmFeesCollectionModel.feesDetails.TotalDiscountAmount;
                    $scope.vmFeesCollectionModel.feesDetails.TotalReceivedAmount;
                });
                if (result.feesDetails != null) {
                    $scope.vmFeesCollectionModel = result;
                    $scope.vmFeesCollectionModel.feesDetails.DueAmount = $scope.vmFeesCollectionModel.feesDetails.TotalDueAmount;
                    $scope.vmFeesCollectionModel.feesDetails.TotalReceivedAmount = '';
                    $scope.vmFeesCollectionModel.feesDetails.TotalAdvanceReceived = 0;
                    $scope.vmFeesCollectionModel.feesDetails.TotalDiscountAmount = 0;
                    $scope.vmFeesCollectionModel.feesDetails.TotalDueAmount = 0;
                } else {
                    $scope.vmFeesCollectionModel.feesDetails = null;
                    $scope.noData = "Sorry, No Students Found!";
                }
                $scope.vmFeesCollectionModel.BranchId = $scope.branchId;
                $scope.isLoading = false;
            });
        }

        $scope.ChangeRptType = function () {
            $scope.history1 = null;
            $scope.totalDebit = 0;
            $scope.totalCredit = 0;
            $scope.totalSum = 0;
            $scope.totalWOVCredit = 0;
            $scope.totalVCredit = 0;
        };

        //$scope.getCollectionType = function () {
        //    FeesCollectionService.getcoltype($scope.vmFeesCollectionModel.CollectionTypeId, function (result) {
        //        if (result != null) {
        //            $scope.vmFeesCollectionModel.feesCollectionType = result;
        //        }
        //    });
        //}


        //$scope.getAlert = function () {

        //    var discount = 0;
        //    var receive = 0;
        //    var receivable = 0;

        //    receive = $scope.vmFeesCollectionModel.feesDetails.TotalReceivedAmount;
        //    receivable = ($scope.vmFeesCollectionModel.feesDetails.TotalReceiveableAmount != 0) ? 0 : $scope.vmFeesCollectionModel.feesDetails.TotalReceiveableAmount;
        //    discount = ($scope.vmFeesCollectionModel.feesDetails.TotalDiscountAmount != 0) ? 0 : $scope.vmFeesCollectionModel.feesDetails.TotalDiscountAmount;

        //    var resultDue = receivable - discount - receive;
        //    var sub = receivable - discount;
        //    var resultAdv = (receive - sub);

        //    if (discount > receivable) {
        //        alert("Discount amount is not greater than Due amount!");
        //        $scope.$on('$destroy', function () {
        //            $scope.vmFeesCollectionModel.feesDetails.TotalDiscountAmount;
        //            $scope.vmFeesCollectionModel.feesDetails.TotalDueAmount;
        //        });
        //        $scope.vmFeesCollectionModel.feesDetails.TotalDiscountAmount = 0;
        //        $scope.vmFeesCollectionModel.feesDetails.TotalDueAmount = receivable.toFixed(2);
        //    }
        //    else {
        //        if (receive > sub) {
        //            $scope.$on('$destroy', function () {
        //                $scope.vmFeesCollectionModel.feesDetails.TotalAdvanceReceived;
        //                $scope.vmFeesCollectionModel.feesDetails.TotalDueAmount;
        //            });
        //            $scope.vmFeesCollectionModel.feesDetails.TotalAdvanceReceived = resultAdv.toFixed(2) == 'NaN' ? 0 : resultAdv.toFixed(2);
        //            $scope.vmFeesCollectionModel.feesDetails.TotalDueAmount = (resultDue <= 0 || resultDue == 'NaN') ? 0 : resultDue.toFixed(2);
        //        } else {
        //            $scope.$on('$destroy', function () {
        //                $scope.vmFeesCollectionModel.feesDetails.TotalDueAmount;
        //                $scope.vmFeesCollectionModel.feesDetails.TotalAdvanceReceived;
        //            });
        //            $scope.vmFeesCollectionModel.feesDetails.TotalDueAmount = resultDue.toFixed(2) == 'NaN' ? 0 : resultDue.toFixed(2);
        //            $scope.vmFeesCollectionModel.feesDetails.TotalAdvanceReceived = (resultAdv <= 0 || resultAdv == 'NaN') ? 0 : resultAdv.toFixed(2);
        //        }
        //    }
        //}

        //$scope.AddFeesCollection = function () {

        //    FeesCollectionService.save($scope.vmFeesCollectionModel, function (result) {
        //        $stateParams.id = result.id;
        //        FeesCollectionService.feesColMonRecp($stateParams.id, function (result) {
        //            if (result != null) {
        //                $scope.formStatus = false;
        //                $scope.recepitStatus = true;
        //                $scope.thisCanBeusedInsideNgBindHtml = $sce.trustAsHtml(result.template);
        //            }
        //        });

        //    }, function (err) {
        //        if (err.status == 409) {
        //            toaster.pop("error", "Error", err.statusText);
        //        }
        //        showErrors(toaster, err);
        //    });
        //}

        $scope.ReceiptPrint = function () {
            $('#paymentReceipt').jqprint();
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

