'use strict';
/* Controllers */
app.controller('FeesGenerateNewCtrl', ['$scope', '$state', '$http', 'FeesCollectionService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, FeesCollectionService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Fees Generate" };
        $scope.isLoading = true;
        $scope.student = null;
        $scope.VmfeesGenerateModel = null;

        FeesCollectionService.new(function (result) {
            $scope.VmfeesGenerateModel = result;
            $scope.isLoading = false;
        });

        $scope.AddFeesGenerate = function () {
            $scope.VmfeesGenerateModel.AdjStudentList = $scope.items;
            FeesCollectionService.save($scope.VmfeesGenerateModel, function (result) {
                $state.go('app.notice.noticeList');
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
            }, function (err) {
                showErrors(toaster, err);
            });
        }


        //------------ Months---------------
        $scope.Monthes = [
           { "Key": '01', "Value": "January" },
           { "Key": '02', "Value": "February" },
           { "Key": '03', "Value": "March" },
           { "Key": '04', "Value": "April" },
           { "Key": '05', "Value": "May" },
           { "Key": '06', "Value": "June" },
           { "Key": '07', "Value": "July" },
           { "Key": '08', "Value": "August" },
           { "Key": '09', "Value": "September" },
           { "Key": '10', "Value": "October" },
           { "Key": '11', "Value": "November" },
           { "Key": '12', "Value": "December" }
        ];

        var myTimeUnformatted = new Date();
        var date = $filter('date')(myTimeUnformatted, 'yyyy');
        $scope.CurrentYear = date;
        $scope.NextYear = parseInt($scope.CurrentYear) + 1;

        //------------ Years---------------

        $scope.Years = [
           { "Key": $scope.CurrentYear, "Value": $scope.CurrentYear },
           { "Key": $scope.NextYear, "Value": $scope.NextYear }
        ];

        //--------------Input Count---------------
        var count = 0;
        $scope.Load = function (val) {
            count = count + val;
            if (count>2) {
              FeesCollectionService.getstudents($scope.VmfeesGenerateModel.FeesGenerateAcademic, function (result) {
              $scope.studentList = result.SearchStudents;
              });
            }
        }
        
       
      $scope.items = [];
        // Add a Item to the list
      $scope.addItem = function () {

          if ($scope.student != null) {
              var selectedHead = $filter('filter')($scope.VmfeesGenerateModel.FeesHeadList, { Key: $scope.student.FeesHeadId })[0].Value;
              $scope.items.push({
                  Name: $scope.student.originalObject.Text,
                  FeesGenerateStudentId: $scope.student.originalObject.Id,
                  Amount: $scope.student.Amount,
                  FeesHeadId: $scope.student.FeesHeadId,
                  HeadName: selectedHead
              });
          }

          // Clear input fields after push
          $scope.student = null;

          //$scope.ex8.Value = "";
          count = 0;
          $scope.studentList = null;

      };
        // on select set student obj 
      $scope.SelectedStudent = function (selected) {
          $scope.student = selected;
          
      };

      $scope.removeStudent = function (index) {
          $scope.items.splice(index, 1);
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
