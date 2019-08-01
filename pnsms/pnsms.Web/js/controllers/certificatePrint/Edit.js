'use strict';
/* Controllers */
// Student controller
app.controller('CertificatePrintEditCtrl', ['$scope', '$state', '$http', '$stateParams', 'CertificatePrintService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, CertificatePrintService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Certificate Print" };
        $scope.isLoading = true;
        $scope.certificatePrintModel = null;

        $scope.GetSingleCertificatePrint = function () {
            CertificatePrintService.getsingle({ id: $stateParams.certificateprintId }, function (result) {
                $scope.certificatePrintModel = result;
                $scope.certificatePrintModel.IsActive = (result.IsActive == true) ? 1 : 0;

                $scope.isLoading = true;
            });
        }
        $scope.GetSingleCertificatePrint();

        //-----------------------------
        $scope.UpdateCertificatePrint = function () {

            $scope.certificatePrintModel.IsActive = ($scope.certificatePrintModel.IsActive == 1) ? true : false;

            CertificatePrintService.save($scope.certificatePrintModel, function (result) {
                $state.go('app.certificatePrint.list');
                toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            }, function (err) {
                showErrors(toaster, err);
            }
           );
        };

        $scope.Print = function () {
            $(".ta-scroll-window.ng-scope.ta-text.ta-editor.form-control").jqprint();
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