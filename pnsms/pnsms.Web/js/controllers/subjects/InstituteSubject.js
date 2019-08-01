'use strict';

app.controller('InstituteSubjectCtrl', ['$scope', '$http', 'InstituteSubjectService', '$filter', 'toaster', '$modal',
    function ($scope, $http, InstituteSubjectService, $filter, toaster, $modal) {

        // init
        $scope.entity = { Name: "Subjects Map" };
        $scope.isLoading = true;
        $scope.authorLoading = true;
        $scope.isDataLoading = false;
        $scope.lstInstituteSubject = null;
        $scope.RefSubjectAcademicClassMapping = {};
        $scope.TempSubjectAcademicClassMapping = {};
        $scope.SubjectAcademicClassMapping =
            {
                AcademicBranchId: "",
                AcademicShiftId: "",
                AcademicClassId: "",
                AcademicSectionId: "",
                SubjectId: "",
                SubjectSplitId: "",
                TeacherId: ""
            };


        $scope.items = [];

        //inline add/edit
        $scope.newField = {};
        $scope.editing = {};
        $scope.isNew = false;
        $scope.selected = {};
        $scope.infoText = "";

        $scope.GetAll = function () {
            InstituteSubjectService.query(function (result) {
                //$scope.RefSubjectAcademicClassMapping = result;
                $scope.lstInstituteSubject = result;

                $scope.items = $scope.lstInstituteSubject;
              //  alert($scope.lstInstituteSubject);
                $scope.isLoading = false;
                //$scope.GetAll();
            });
        };

        $scope.GetAll();      
      
        $scope.Add = function () {
            $scope.isLoading = true;

            var items = $filter('filter')($scope.items, { IsActive: true }, true);

            var msg = "";

            if (items.length == 0) {
                msg += "Select Subject is required for .";
            }


            angular.forEach(items, function (item) {


                if (!item.Name) {
                    msg += "Subject Name is required for " + item.Name + "." + "\n"

                }
               
               
            });
            if (msg.length > 0) {
                toaster.pop("warning", msg);
                $scope.isLoading = false;
                return false;
            }
          //  angular.copy($scope.items);
           // alert(items);
            InstituteSubjectService.save(items, function (result) {

                $scope.isNew = false;
                $scope.isLoading = false;
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
            }, function (err) {
                $scope.isLoading = false;
                showErrors(toaster, err);
            }
           );


        };      

    }]);

//function GetNewModel() {
//    return { Id: 0, IsActive: true, Name: "", Quantity: 1, ISBN: "" };

//}
function GetNewAuthorModel() {
    return { Id: 0, IsActive: true, Name: "" };

}

function myCkeditor(a, b, c) {
    window.parent.CKEDITOR.tools.callFunction(a, b, c);
}