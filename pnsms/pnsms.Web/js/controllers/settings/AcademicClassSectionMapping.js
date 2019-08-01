'use strict';

app.controller('AcademicClassSectionMappingCtrl', ['$scope', '$http', 'AcademicClassSectionMappingService', '$filter', 'toaster', '$modal',
    function ($scope, $http, AcademicClassSectionMappingService, $filter, toaster, $modal) {

        // init
        $scope.entity = { Name: "Academic Class Section Mapping" };
        $scope.isLoading = true;
        $scope.authorLoading = true;

        $scope.RefAcademicClassSectionMapping = {};
       
        $scope.person = {};
        $scope.person.selected = undefined;

        
        $scope.items = [];

        //inline add/edit
        $scope.newField = {};
        $scope.editing = {};
        $scope.isNew = false;
        $scope.selected = {};
        $scope.infoText = "";
        
        $scope.GetNew = function () {
            $scope.isLoading = true;
            AcademicClassSectionMappingService.new(function (result) {
                var id = result.AcademicBranchList[0].Key;
                $scope.RefAcademicClassSectionMapping = result;
                $scope.RefAcademicClassSectionMapping.AcademicBranchId = id;
                $scope.isLoading = false;
                //$scope.GetAll();
            }, function (err) {
                showErrors(toaster, err);
            });
        };
        
        $scope.SearchMappiing = function() {
            $scope.isLoading = true;
            AcademicClassSectionMappingService.query({
                academicBranchId: $scope.RefAcademicClassSectionMapping.AcademicBranchId,
                academicClassId: $scope.RefAcademicClassSectionMapping.AcademicClassId,
                routinePeriodTypeId: $scope.RefAcademicClassSectionMapping.RoutinePeriodTypeId,
                academicShiftId: $scope.RefAcademicClassSectionMapping.AcademicShiftId,
            }, function (AcademicClassSectionMappings) {
                $scope.AcademicClassSectionMappings = AcademicClassSectionMappings;
                $scope.items = AcademicClassSectionMappings;
                $scope.TempAcademicClassSectionMapping = angular.copy($scope.RefAcademicClassSectionMapping);
                 
                $scope.isLoading = false;

            });
        };
         
      
        $scope.GetNew();
        $scope.GetTeacher = function (teacherId) {
            
            if (teacherId !== undefined) {
                var m = $filter('filter')($scope.RefAcademicClassSectionMapping.TeacherList, { Key: teacherId }, true)[0];
                return m;
            }
            return {};

        };

        $scope.Add = function () {
            $scope.isLoading = true;
             
            AcademicClassSectionMappingService.save($scope.items, function (result) {
                    $scope.isNew = false;
                        $scope.SearchMappiing();
                    toaster.pop("success", "Success", $scope.entity.Name + " created.");
                }, function (err) {
                    showErrors(toaster, err);
                }
           );
            

        };
     
        $scope.getTemplate = function (item) {
             
                return 'display';
        };

        
    

    }]);

 