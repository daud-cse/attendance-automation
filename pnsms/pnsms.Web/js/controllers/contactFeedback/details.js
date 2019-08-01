'use strict';
/* Controllers */
// Student controller
app.controller('ContactFeedbackDetailsCtrl', ['$scope', '$state', '$http', '$stateParams', 'ContactFeedbackService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, $stateParams, ContactFeedbackService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Contact & Feedback" };
        $scope.isLoading = true;
        $scope.VmUserDetails = null;

        $scope.GetSingleDetails = function () {
            ContactFeedbackService.getsingle({ id: $stateParams.contactFeedbackId }, function (result) {
                $scope.VmUserDetails = result;
                $scope.isLoading = false;
            });
        }
        $scope.GetSingleDetails();

    }]);