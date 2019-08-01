'use strict';
/* Controllers */
// ExamType controller
app.controller('ExamGradeCtrl', ['$scope', '$http', 'ExamGradeService', '$filter', 'toaster', function ($scope, $http, ExamGradeService, $filter, toaster) {

    // init
    $scope.entity = { Name: "Exam Grade" };
    $scope.isLoading = true;
    ///---- paging and search/sort
    $scope.sortingOrder = "Id";
    $scope.reverse = true;
    $scope.filteredItems = [];
    $scope.groupedItems = [];
    $scope.itemsPerPage = 10;
    $scope.pagedItems = [];
    $scope.currentPage = 0;
    $scope.items = [];

    //inline add/edit
    $scope.newField = {};
    $scope.editing = {};
    $scope.isNew = false;
    $scope.selected = {};
    // alert("dfsdf");
    var searchMatch = function (haystack, needle) {
        if (!needle) {
            return true;
        }
        return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
    };

    // init the filtered items
    $scope.search = function () {
        //console.log($scope.items);
        $scope.filteredItems = $filter('filter')($scope.items, function (item) {

            // Todo: implement multiple filter.
            ///for (var attr in item) {
            if (searchMatch(item["GradeName"], $scope.query))
                return true;
            ///}
            return false;
        });
        // take care of the sorting order
        if ($scope.sortingOrder !== '') {
            $scope.filteredItems = $filter('orderBy')($scope.filteredItems, $scope.sortingOrder, $scope.reverse);
        }
        $scope.currentPage = 0;
        // now group by pages
        $scope.groupToPages();
    };

    // calculate page in place
    $scope.groupToPages = function () {
        $scope.pagedItems = [];
        if ($scope.filteredItems) {
            for (var i = 0; i < $scope.filteredItems.length; i++) {
                if (i % $scope.itemsPerPage === 0) {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)] = [$scope.filteredItems[i]];
                } else {
                    $scope.pagedItems[Math.floor(i / $scope.itemsPerPage)].push($scope.filteredItems[i]);
                }
            }
        }

    };

    $scope.range = function (start, end) {
        var ret = [];
        if (!end) {
            end = start;
            start = 0;
        }
        for (var i = start; i < end; i++) {
            ret.push(i);
        }
        return ret;
    };

    $scope.prevPage = function () {
        if ($scope.currentPage > 0) {
            $scope.currentPage--;
        }
    };

    $scope.nextPage = function () {
        if ($scope.currentPage < $scope.pagedItems.length - 1) {
            $scope.currentPage++;
        }
    };

    $scope.setPage = function () {
        $scope.currentPage = this.n;
    };

    // functions have been describe process the data for display
    // $scope.search();

    // change sorting order
    $scope.sort_by = function (newSortingOrder) {
        if ($scope.sortingOrder == newSortingOrder)
            $scope.reverse = !$scope.reverse;

        $scope.sortingOrder = newSortingOrder;

        // icon setup
        $('tr span i').each(function () {
            // icon reset
            $(this).removeClass().addClass('fa fa-sort');
        });
        if ($scope.reverse)
            $('span.' + newSortingOrder + ' i').removeClass().addClass('fa fa-sort-asc');
        else
            $('span.' + newSortingOrder + ' i').removeClass().addClass('fa fa-sort-desc');
    };



    // Function Define : For Getting All the Exam Grade Data
    $scope.GetAllGrades = function () {      
        ExamGradeService.query(function (ExamGrade) {
          //  $scope.ExamGrades = ExamGrade;
           $scope.items = ExamGrade;
            $scope.query = "";
            $scope.search();
            $scope.isLoading = false;
        });
    };

    // Call The Funtion for Rendering Data
    $scope.GetAllGrades();
    $scope.ExamGrade = GetNewModel();
    $scope.Add = function (item) {       
        ExamGradeService.save(item, function (result) {
            $scope.Exam = GetNewModel();
            $scope.ExamGrade = false;
            $scope.GetAllGrades();
            toaster.pop("success", "Success", $scope.entity.Name + " created.");
        }, function (err) {
            showErrors(toaster, err);
        }
        );
    };
    $scope.Edit = function (item) {       
        $scope.editing = $scope.pagedItems[$scope.currentPage].indexOf(item);
        $scope.selected = angular.copy(item);
    };

    $scope.Update = function (item) {
        ExamGradeService.update({ id: item.Id }, item, function (result) {
            toaster.pop("success", "Success", $scope.entity.Name + " updated.");
            $scope.selected = {};
        }, function (err) { showErrors(toaster, err); });

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


// For Number and Decimal Input Purpose
app.directive('validNumber', function () {
    return {
        require: '?ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            if (!ngModelCtrl) {
                return;
            }

            ngModelCtrl.$parsers.push(function (val) {
                if (angular.isUndefined(val)) {
                    var val = '';
                }

                var clean = val.replace(/[^-0-9\.]/g, '');
                var negativeCheck = clean.split('-');
                var decimalCheck = clean.split('.');
                if (!angular.isUndefined(negativeCheck[1])) {
                    negativeCheck[1] = negativeCheck[1].slice(0, negativeCheck[1].length);
                    clean = negativeCheck[0] + '-' + negativeCheck[1];
                    if (negativeCheck[0].length > 0) {
                        clean = negativeCheck[0];
                    }

                }

                if (!angular.isUndefined(decimalCheck[1])) {
                    decimalCheck[1] = decimalCheck[1].slice(0, 2);
                    clean = decimalCheck[0] + '.' + decimalCheck[1];
                }

                if (val !== clean) {
                    ngModelCtrl.$setViewValue(clean);
                    ngModelCtrl.$render();
                }
                return clean;
            });

            element.bind('keypress', function (event) {
                if (event.keyCode === 32) {
                    event.preventDefault();
                }
            });
        }
    };
});
function GetNewModel() {
    return { Id: 0, GradeName: "", GradePoint: "", MarksFrom: "", MarksUpto: "", IsActive: true, Comment: "", InstituteId: 1 };

}
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