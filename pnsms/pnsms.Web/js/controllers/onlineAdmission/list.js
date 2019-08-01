'use strict';
/* Controllers */
app.controller('OnlineAdmissionListCtrl', ['$scope', '$http', 'OnlineAdmissionService', '$filter', 'toaster',
function ($scope, $http, OnlineAdmissionService, $filter, toaster) {

    $scope.entity = { Name: "Online Admission" };
    $scope.VmSearch = null;
    $scope.isLoading = true;
    OnlineAdmissionService.list(function (result) {
        $scope.VmSearch = result;
        if ($scope.VmSearch.SearchData.length != 0) {
            $.each($scope.VmSearch.SearchData, function (i, v) {
                $scope.VmSearch.SearchData[i].IsSelected = ($scope.VmSearch.SearchData[i].IsSelected == true) ? 1 : 0;
            });
        }
        $scope.VmSearch.selectedStatus = 0;
        $scope.isLoading = false;
    });

    $scope.GetFilterResult = function () {
        $scope.isLoading = true;
        $scope.VmSearch.selectedStatus = ($scope.VmSearch.selectedStatus == 1) ? true : false;
        OnlineAdmissionService.list($scope.VmSearch, function (resultList) {
            $scope.VmSearch = resultList;
            if ($scope.VmSearch.SearchData.length != 0) {
                $.each($scope.VmSearch.SearchData, function (i, v) {
                    $scope.VmSearch.SearchData[i].IsSelected = ($scope.VmSearch.SearchData[i].IsSelected == true) ? 1 : 0;
                });
            }
            $scope.VmSearch.selectedStatus = ($scope.VmSearch.selectedStatus == true) ? 1 : 0;
            $scope.isLoading = false;
        });

    };
}]);