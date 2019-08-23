'use strict';
/* Controllers */
// Notice controller
app.controller('DashBoardCtrl', ['$scope', '$state', '$http', 'DashBoardService', 'modalService', '$filter', 'toaster',
    function ($scope, $state, $http, DashBoardService, modalService, $filter, toaster) {       
        $scope.arraylistClassName = [];
        $scope.arraylistCountClassWise = [];
        $scope.malefemaleratio = [];
        DashBoardService.summary($scope.DashBoardService, function (result) {
            $scope.List = result;           
            angular.forEach($scope.List.lstClassWiseStudent, function(data){
                $scope.arraylistClassName.push(data.ClassName);
                $scope.arraylistCountClassWise.push(parseInt(data.CountClassWise));             
            });
            angular.forEach($scope.List.lstMaleFemaleRatio, function (data) {
              
                var obj = { 'name': data.GenderName, 'y': parseInt(data.CountGenderWise) }
               
                $scope.malefemaleratio.push(obj);
               
            });
          
            //pie Chart
            Highcharts.chart('containerPie', {
                chart: {
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    plotShadow: false,
                    type: 'pie'
                },
                title: {
                    text: 'Male Female Ratio'
                },
                tooltip: {
                    pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
                },
                plotOptions: {
                    pie: {
                        allowPointSelect: true,
                        cursor: 'pointer',
                        dataLabels: {
                            enabled: true,
                            format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                            style: {
                                color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                            }
                        }
                    }
                },
                series:
                    [{
                    name: 'Class',
                    colorByPoint: true,
                    data: $scope.malefemaleratio
                    //    [{
                    //    name: 'One',
                    //    y: 56
                    //}, {
                    //    name: 'Two',
                    //    y: 24
                    //    //sliced: true,
                    //    //selected: true
                    //}, {
                    //    name: 'Three',
                    //    y: 10
                    //}, {
                    //    name: 'Five',
                    //    y: 4.77
                    //}, {
                    //    name: 'Six',
                    //    y: 91
                    //}, {
                    //    name: 'Seven',
                    //    y:4
                    //}]
                }]
            });
          
            //Bar Chart
            Highcharts.chart('containerBar', {
                chart: {
                    type: 'column'
                },
                title: {
                    text: 'Department Wise Employee Summary'
                },
                xAxis: {
                    categories: $scope.arraylistClassName
                },
                yAxis: {
                    min: 0,
                    title: {
                        text: 'Total Employee'
                    },
                    stackLabels: {
                        enabled: true,
                        style: {
                            fontWeight: 'bold',
                            color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
                        }
                    }
                },
                legend: {
                    align: 'right',
                    x: -30,
                    verticalAlign: 'top',
                    y: 25,
                    floating: true,
                    backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
                    borderColor: '#CCC',
                    borderWidth: 1,
                    shadow: false
                },
                tooltip: {
                    headerFormat: '<b>{point.x}</b><br/>',
                    pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
                },
                plotOptions: {
                    column: {
                        stacking: 'normal',
                        dataLabels: {
                            enabled: true,
                            color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white'
                        }
                    }
                },
                series: [{
                    name: 'Class',                    
                    data:$scope.arraylistCountClassWise
                }]
            });
        });              

        $scope.test = 100;

        $scope.d = [[1, 6.5], [2, 6.5], [3, 7], [4, 8], [5, 7.5], [6, 7], [7, 6.8], [8, 7], [9, 7.2], [10, 7], [11, 6.8], [12, 7]];

        $scope.d0_1 = [[0, 7], [1, 6.5], [2, 12.5], [3, 7], [4, 9], [5, 6], [6, 11], [7, 6.5], [8, 8], [9, 7]];

        $scope.d0_2 = [[0, 4], [1, 4.5], [2, 7], [3, 4.5], [4, 3], [5, 3.5], [6, 6], [7, 3], [8, 4], [9, 3]];

        $scope.d1_1 = [[10, 120], [20, 70], [30, 70], [40, 60]];

        $scope.d1_2 = [[10, 50], [20, 60], [30, 90], [40, 35]];

        $scope.d1_3 = [[10, 80], [20, 40], [30, 30], [40, 20]];

        $scope.d2 = [];

        for (var i = 0; i < 20; ++i) {
            $scope.d2.push([i, Math.sin(i)]);
        }

        $scope.d3 = [
          { label: "iPhone5S", data: 40 },
          { label: "iPad Mini", data: 10 },
          { label: "iPad Mini Retina", data: 20 },
          { label: "iPhone4S", data: 12 },
          { label: "iPad Air", data: 18 }
        ];

        $scope.refreshData = function () {
            $scope.d0_1 = $scope.d0_2;
        };

        $scope.getRandomData = function () {
            var data = [],
            totalPoints = 150;
            if (data.length > 0)
                data = data.slice(1);
            while (data.length < totalPoints) {
                var prev = data.length > 0 ? data[data.length - 1] : 50,
                  y = prev + Math.random() * 10 - 5;
                if (y < 0) {
                    y = 0;
                } else if (y > 100) {
                    y = 100;
                }
                data.push(y);
            }
            // Zip the generated y values with the x values
            var res = [];
            for (var i = 0; i < data.length; ++i) {
                res.push([i, data[i]])
            }
            return res;
        }

        $scope.d4 = $scope.getRandomData();

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
