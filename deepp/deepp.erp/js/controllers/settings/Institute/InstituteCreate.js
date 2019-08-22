'use strict';
/* Controllers */
// Institute controller
app.controller('SettingsInstituteCreateCtrl', ['$scope', '$http', 'InstituteService', '$filter', 'toaster', '$location',
    function ($scope, $http, InstituteService, $filter, toaster, $location) {

    // init
    $scope.entity = { Name: "Institute" };
    $scope.isLoading = true;

        // apiUrlPrefix
    $scope.apiUrlPrefix = apiUrlPrefix;

    ///---
    // Get all data
    $scope.GetNew = function () {
        InstituteService.New(function (Institute) {
            $scope.Institute = Institute; // Institute.query();
            $scope.isLoading = false;
        });
    };

    // inline Add /Edit
    $scope.GetNew();
   
    $scope.Add = function () {

        InstituteService.save($scope.Institute, function (result) {
                console.log(result.Id);
                $location.path("/app/applicationsettings/Institute/edit/" + result.Id);
            toaster.pop("success", "Success", $scope.entity.Name + " created.");
        }, function (err) {
            showErrors(toaster, err);
        }
        );
    };
    

    ///
    // gets the template to ng-include for a table row / item
    $scope.getTemplate = function (item) {

        if (item.Id === $scope.selected.Id) return "edit";
        else

            return 'display';
    };

    $scope.reset = function () {
        $scope.pagedItems[$scope.currentPage][$scope.editing] = angular.copy($scope.selected);
        $scope.selected = {};
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