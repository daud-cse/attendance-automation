app.controller('NoticeListCtrl', ['$scope', '$state', '$http', 'NoticeService', '$filter', 'toaster',
    function ($scope, $state, $http, NoticeService, $filter, toaster) {

        $scope.entity = { Name: "Notice List" };
        $scope.noticeWithListModel = null;
        $scope.isLoading = true;
        var myTimeUnformatted = new Date();
        var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
        NoticeService.list({ startDateModel: date, endDateModel: date }, function (result) {
            $scope.noticeWithListModel = result;
            date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
            $scope.noticeWithListModel.startDateModel = date;
            $scope.noticeWithListModel.endDateModel = date;
            $scope.isLoading = false;
        });

        $scope.GetFilterResult = function () {
            $scope.isLoading = true;
            NoticeService.list($scope.noticeWithListModel, function (noticeAllList) {
                $scope.noticeWithListModel = noticeAllList;
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