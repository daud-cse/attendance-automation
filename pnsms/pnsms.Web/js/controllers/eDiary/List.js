app.controller('DiaryListCtrl', ['$scope', '$state', '$http', 'eDiaryService', '$filter', 'toaster','$modal',
    function ($scope, $state, $http, eDiaryService, $filter, toaster, $modal) {

        $scope.entity = { Name: "Diary" };
        $scope.listModel = null;
        $scope.isLoading = false;
        $scope.diaryLoading = false;
        
        $scope.MyDate = {};
        $scope.TeacherList = [];
        $scope.TeacherId = null;
        var myTimeUnformatted = new Date();
        var date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');

        $scope.NavDiary = function (nextday) {
            var date = new Date();
            var d = new Date($scope.MyDate.Date);
            if (nextday == true) {
                
                  date = date.setDate(d.getDate() + 1);
            } else {
                date = date.setDate(d.getDate() - 1);
            }
            console.log(date);
            $scope.MyDate.Date = $filter('date')(date, 'yyyy-MM-dd');
            console.log($scope.MyDate.Date);
             $scope.GetDiaries('s');
        };
        $scope.GetDiaries = function (mydate) {
            if (mydate == null) {
                date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
            } else {
                date = $filter('date')($scope.MyDate.Date, 'yyyy-MM-dd');
            }

            //var date = $filter('date')($scope.MyDate, 'yyyy-MM-dd');

            $scope.diaryLoading = true;
            eDiaryService.GetDiaries({ teacherid: $scope.TeacherId, date: date }, function (result) {
                $scope.listModel = result;
                if (mydate == null) {
                    $scope.MyDate.Date = $filter('date')(myTimeUnformatted, 'yyyy-MM-dd');
                }
                //$scope.isLoading = false;
                $scope.diaryLoading = false;
            });
        };
        $scope.GetNew = function () {
            $scope.isLoading = false;
            //eDiaryService.New( function (result) {
            //    $scope.TeacherList = result;
            //    $scope.isLoading = false;
               
            //});
        };
        $scope.GetNew();
        //$scope.GetDiaries(null);

        $scope.UpdateDiary2 = function (item) {
            $scope.isLoading = true;
            item.DairyDate = $scope.MyDate.Date;
            eDiaryService.Update(item, function (allList) {
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
                $scope.isLoading = false;
            });

        };

        $scope.UpdateDiary = function (item) {
            console.log(item);
            $scope.DiaryModel = item;
            var modalInstance = $modal.open({
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                size: 'sm',
                resolve: {
                    items: function () {
                        return $scope.items;
                    },
                    Diary: function () {
                        return $scope.DiaryModel;
                    }
                }
            });

            modalInstance.result.then(function (data) {

                $scope.DiaryModel = data.Diary;
                $scope.DiaryModel.DairyDate = $filter('date')($scope.MyDate.Date, 'yyyy-MM-dd');
                console.log($scope.MyDate.Date);
                eDiaryService.Update($scope.DiaryModel, function (result) {
                    $scope.DiaryModel.Id = result.Id;
                    toaster.pop("success", "Success", $scope.entity.Name + " updated.");
                    $scope.isLoading = false;
                });
               
               
                
            }, function () {
                console.log(" modal");
            });
        };

    }]);

app.controller('ModalInstanceCtrl', [
    '$scope', '$modalInstance', 'items', 'Diary', 'modalService', function ($scope, $modalInstance, items, Diary, modalService) {
        $scope.items = items;
        $scope.selectedDiary = Diary;


        $scope.ok = function () {
            //console.log($scope.selectedImage);
            var data = { Diary: $scope.selectedDiary, IsRemove: false };
            $modalInstance.close(data);
        };

        $scope.remove = function () {
            // console.log($scope.selectedImage);
            var modalOptions = {
                closeButtonText: 'No',
                actionButtonText: 'Yes',
                headerText: 'Remove Image',
                bodyText: 'Are you sure you want to remove this image?',
                type: 'danger'
            };
            modalService.showModal({}, modalOptions).then(function (result) {
                if (result === 'ok') {

                    var data = { Image: $scope.selectedImage, IsRemove: true };
                    $modalInstance.close(data);


                }
            });

        };
        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }
]);