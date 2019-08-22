'use strict';
/* Controllers */
// Notice controller
app.controller('EventCreateCtrl', ['$scope', '$state', '$http', 'EventService', 'modalService', 'FileUploader', '$filter', 'toaster',
    function ($scope, $state, $http, EventService, modalService, FileUploader, $filter, toaster) {

        $scope.entity = { Name: "Event" };
        $scope.isLoading = true;
        $scope.eventModel = null;
        $scope.uploader = null;
        EventService.new(function (result) {
            $scope.eventModel = result;
            $scope.eventModel.IsActive = 1;
            $scope.uploader.queue[0]._file = "";
            $scope.isLoading = false;
        });

        //------------------------------

        var imageApi = apiUrlPrefix + "api/image/multiple";

        var uploader = $scope.uploader = new FileUploader({
            url: imageApi,
            autoUpload: false,
        });

        //-----------------------------

        $scope.AddEvent = function () {

            var length = $scope.uploader.queue.length;

            if (length != 0) {
                uploader.uploadAll();
                $scope.uploader.onCompleteAll = function () {
                    EventService.save($scope.eventModel, function (result) {
                        $scope.uploader = null;
                        $state.go('app.event.eventList');
                        toaster.pop("success", "Success", $scope.entity.Name + " created.");
                    }, function (err) {
                        showErrors(toaster, err);
                    }
                   );
                }
            } else {

                EventService.save($scope.eventModel, function (result) {
                    $scope.uploader = null;
                    $state.go('app.event.eventList');
                    toaster.pop("success", "Success", $scope.entity.Name + " created.");
                }, function (err) {
                    showErrors(toaster, err);
                });

            }
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
