'use strict';
/* Controllers */
// Student controller
app.controller('StudentListCtrl', ['$scope', '$http', 'StudentService', '$filter', 'toaster',

    function ($scope, $http, StudentService, $filter, toaster) {

        // init
        $scope.entity = { Name: "Student" };
        $scope.isLoading = true;
        $scope.isSearchLoading = true;
        ///---- paging and search/sort
        $scope.sortingOrder = "StudentId";
        $scope.reverse = true;
        $scope.filteredItems = [];
        $scope.groupedItems = [];
        $scope.itemsPerPage = 10;
        $scope.pagedItems = [];
        $scope.currentPage = 0;
        $scope.items = [];
        $scope.AcademicSectionList = null;
        //inline add/edit
        $scope.newField = {};
        $scope.editing = {};
        $scope.isNew = false;
        $scope.selected = {};
        // apiUrlPrefix
        $scope.apiUrlPrefix = apiUrlPrefix;

        var searchMatch = function (haystack, needle) {
            if (!needle) {
                return true;
            }
            return haystack.toLowerCase().indexOf(needle.toLowerCase()) !== -1;
        };
         $scope.getScetionClasswise = function () {

            if ($scope.VmStudent.Student.CurrentAcademicClassId != null) {
                StudentService.getScetionClasswise({ id: $scope.VmStudent.Student.CurrentAcademicClassId }, function (result) {

                    $scope.AcademicSectionList = result;
                    $scope.VmStudent.Student.AcademicSectionList = $scope.AcademicSectionList;

                })
            }
        };

        // init the filtered items
        $scope.search = function () {
            //console.log($scope.items);
            $scope.filteredItems = $filter('filter')($scope.items, function (item) {

                // Todo: implement multiple filter.
                ///for (var attr in item) {
                if (searchMatch(item.Name, $scope.query))
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

        ///---
        // Get all data
        $scope.GetAll = function () {
            StudentService.query(function (students) {
                //console.log(students);
                $scope.students = students; // Student.query();
                $scope.items = students;
                $scope.query = "";
                $scope.search();
                $scope.isLoading = false;
            });
        };
        $scope.getScetionClasswise = function () {
            if ($scope.VmStudent.CurrentAcademicClassId != null) {
                StudentService.getScetionClasswise({ id: $scope.VmStudent.CurrentAcademicClassId }, function (result) {
                    $scope.AcademicSectionList = result;
                    $scope.VmStudent.AcademicSectionList = $scope.AcademicSectionList;

                })
            }
        };

        $scope.GetStudentSearchModel = function () {
            StudentService.NewStudent(function (student) {
                var id = student.AcademicBranchList[0].Key;
                $scope.VmStudent = student; 
                $scope.isSearchLoading = false;
                $scope.VmStudent.CurrentAcademicBranchId = id;
            });
        };
        $scope.GetStudentSearchModel();
        $scope.GetAll();

        $scope.SearchStudent = function () {
            $scope.isLoading = true;
            StudentService.SearchStudent($scope.VmStudent, function (students) {
                $scope.students = students; // Student.query();
                //console.log(students.length);
                $scope.items = students;
                $scope.query = "";
                $scope.search();
                $scope.isLoading = false;
            });
        };
         
        $scope.getTemplate = function (item) {

            if (item.StudentId === $scope.selected.StudentId) return "edit";
            else

                return 'display';
        };
        $scope.reset = function () {
            $scope.pagedItems[$scope.currentPage][$scope.editing] = angular.copy($scope.selected);
            $scope.selected = {};
        };


    }]);
