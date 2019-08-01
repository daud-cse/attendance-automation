'use strict';
/* Controllers */
// Student controller
app.controller('MobilePaymentEditCtrl', ['$scope', '$state', '$http', '$stateParams', 'MobilePaymentService','modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, MobilePaymentService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Mobile Payment" };
        $scope.isLoading = true;
        $scope.mPaymentModel = null;

        $scope.GetmPayment = function () {
            MobilePaymentService.getsingle({ id: $stateParams.mpaymentId }, function (result) {
                $scope.mPaymentModel = result;
                $scope.isLoading = true;
            });
        }
        $scope.GetmPayment();

        //-----------------------------

        $scope.UpdateMobilePayment = function () {
            MobilePaymentService.save($scope.mPaymentModel, function (result) {
                $state.go('app.mobilepayment.list');
                toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            }, function (err) {
                if (err.status == 409) {
                    var modalOptions = {
                        closeButtonText: '',
                        actionButtonText: 'Ok',
                        headerText: 'Student Pin/Mobile Number Invalid',
                        bodyText: err.statusText,
                        type: 'danger'
                    };
                    modalService.showModal({}, modalOptions);
                }
                showErrors(toaster, err);
            }
           );
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