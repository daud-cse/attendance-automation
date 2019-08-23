app.controller('EventListCtrl', ['$scope', '$state', '$http', 'EventService', '$filter', 'toaster',
    function ($scope, $state, $http, EventService, $filter, toaster) {

        $scope.entity = { Name: "Event List" };
        $scope.eventWithListModel = null;
        $scope.isLoading = true;
        var myTimeUnformatted = new Date();
        var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
        EventService.list({ startDateModel: date, endDateModel: date }, function (result) {
            $scope.eventWithListModel = result;
            date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
            $scope.eventWithListModel.startDateModel = date;
            $scope.eventWithListModel.endDateModel = date;
            $scope.isLoading = false;
        });

        $scope.GetFilterResult = function () {
            $scope.isLoading = true;
            EventService.list($scope.eventWithListModel, function (eventAllList) {
                $scope.eventWithListModel = eventAllList;
                $scope.isLoading = false;
            });

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