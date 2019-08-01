'use strict';
/* Controllers */
app.controller('feesAcademicClass', ['$scope', '$state', '$http', 'feesTypeService', 'feesAcademicClassService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, feesTypeService, feesAcademicClassService, modalService, $filter, toaster) {

        $scope.entity = { Name: "Fees Class Type is Mapped" };
        $scope.init = function () {
            $scope.academicClass = [];
            $scope.monthlyReport = [];
            $scope.feestypes = [];

            //feesTypeService.query().then(function (result) {
            //    $scope.feestypes = result;
            //    console.log($scope.feestypes);
            //});
            feesTypeService.query(function (result) {
                $scope.feesTypes = result;
            });


            feesAcademicClassService.getfeesAcademicClass().then(function (result) {
                $scope.classFeesList = result;
                console.log($scope.classFeesList);
                angular.forEach(result, function (item) {
                    var itemobj = { 'FeesAcademicClassId': item.FeesAcademicClassId, 'AcademicClassId': item.AcademicClassId, 'AcademicClassName': item.AcademicClassName }
                    if (item.FeesAcademicClassId != undefined) {
                        if (!exists(item.AcademicClassName)) {
                            $scope.academicClass.push(itemobj);
                        }
                    }
                });
                console.log($scope.academicClass);
            });

            function exists(parm) {
                var output = false;
                angular.forEach($scope.academicClass, function (key, value) {
                    if (key.AcademicClassName == parm) {
                        output = true;
                        return output;
                    }
                });
                return output;
            }
            $scope.selectedOption = {};
            $scope.Monthes = [
                { "Key": '1', "Value": "January" },
                { "Key": '2', "Value": "February" },
                { "Key": '3', "Value": "March" },
                { "Key": '4', "Value": "April" },
                { "Key": '5', "Value": "May" },
                { "Key": '6', "Value": "June" },
                { "Key": '7', "Value": "July" },
                { "Key": '8', "Value": "August" },
                { "Key": '9', "Value": "September" },
                { "Key": '10', "Value": "October" },
                { "Key": '11', "Value": "November" },
                { "Key": '12', "Value": "December" }
            ];

            var date = new Date();
            $scope.currentMonth = date.getMonth() + 1;
            console.log($scope.currentMonth);
            $scope.appliedMonths = [];
            $scope.appliedMonths.push({ "Key": '0', "Value": 'All' });
            angular.forEach($scope.Monthes, function (value, key) {
                if (parseInt(value.Key, 10) <= $scope.currentMonth) {
                    $scope.appliedMonths.push(value);
                }
            });
            console.log($scope.appliedMonths);
        }
        $scope.setChange = function (feesAcademicClass) {
            feesAcademicClass.IsDirty = true;
        }

        $scope.save = function (classFeesList) {
            var classFeesListChanges = classFeesList.filter((item) => item.IsDirty === true)
            //var items = $filter('filter')(classFeesList, { IsDirty: true }, true);
            var msg = "";

            console.log(classFeesListChanges);
            angular.forEach(classFeesListChanges, function (item) {


                //if (!item.OrderBy) {
                //    msg += "Subject Order by is required for " + item.SubjectName + "." + "\n"

                //}
                //if (!item.SubjectMarks) {
                //    msg += "Subject Exam Marks Entry  is required for " + item.SubjectName + "." + "\n"

                //}
                if (item.kvpFeesTypes.length > 0) {
                    if (item.kvpFeesTypes.length == 0) {
                        msg += "Fees Types is required for " + item.FeesHeadName + "." + "\n"

                    }

                }

                if (item.FeesTypeId == null || item.FeesTypeId === "") {
                    msg += "Fees Types is required for " + item.FeesHeadName + "." + "\n"

                }
               
            });
            if (msg.length > 0) {
                toaster.pop("warning", msg);
                $scope.isLoading = false;
                return false;
            }

            feesAcademicClassService.edit(classFeesListChanges).then(function () {
                toaster.pop("success", "Success", $scope.entity.Name + " created.");
            });
        }
        $scope.getreport = function (selectedOption, classfees) {
            feesAcademicClassService.getreport(selectedOption, classfees.AcademicClassId).then(function (result) {
                $scope.monthlyReport = result;
                console.log();
            });
        }
        $scope.setSelectedType = function (id) {
            //return $scope.feesTypes.filter(x => x.Id == id);

            //$scope.feesTypes.filter(function (item) {
            //    return item.Id === id;
            //})

            angular.forEach($scope.feesTypes, function (value, key) {
                if (value.Id == id) {
                    return key;
                }
            });


            //var item = $scope.feesTypes.map(function (e) { return e.Id; }).indexOf(id);
            //console.log(item);
            //return item;

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
