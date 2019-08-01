'use strict';
/* Controllers */
app.controller('OnlineAdmissionEditCtrl', ['$scope', '$http', 'OnlineAdmissionService', '$filter', 'toaster', '$stateParams', 'modalService', '$state',
    function ($scope, $http, OnlineAdmissionService, $filter, toaster, $stateParams, modalService, $state) {

        $scope.entity = { Name: "Applicant" };
        $scope.isLoading = true;
        $scope.VmApplicant = null;

        $scope.GetSingle = function () {
            OnlineAdmissionService.getsingle({ id: $stateParams.onlineAdmissionId }, function (result) {
                $scope.VmApplicant = result;
                $scope.isLoading = false;
            });
        }
        $scope.GetSingle();

        $scope.UpdateApplicationForm = function () {

            $scope.VmApplicant.AdmissionForm.IsSelected = ($scope.VmApplicant.AdmissionForm.IsSelected == 1) ? true : false;
            OnlineAdmissionService.save($scope.VmApplicant.AdmissionForm, function (result) {
                $state.go('app.onlineAdmission.list');
                toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            }, function (err) {
                showErrors(toaster, err);
            }
           );
        };


        $scope.Print = function () {
            $('#myDiv').jqprint();
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



