'use strict';
/* Controllers */
// Country controller
app.controller('CertificateCtrl', ['$scope', '$http', 'CertificateService', '$filter', 'toaster', function ($scope, $http, CertificateService, $filter, toaster) {

    $scope.addText = "Add text";


    /// print---------------------------
    $scope.Print = function () {
        updatePrint();
        $('#myDiv').jqprint();
    };
    function updatePrint() {

        var img = document.getElementById('printImage'),  /// get image element
            canvas = document.getElementById('canvas');  /// get canvas element
        
        img.src = canvas.toDataURL();                     /// update image
    }

    $scope.selectText = null;
    //------------------- add text------------------
    $scope.AddNewText = function () {

        var text = new fabric.Text($scope.addText, { left: 200, top: 200, fontSize: 24 });
        text.on('selected', function () {
            $scope.selectText = this;
            $scope.addText = $scope.selectText.text;
            console.log($scope.addText + "selected ");
            $scope.$apply();
        });
        //var canvas = fabric.Canvas('canvas');
        canvas.add(text);
    };

    $scope.UpdateText = function () {
        $scope.selectText.text = $scope.addText;
        //$scope.selectText.fontSize = 24;
      
        $scope.$apply();
      
    };
    $scope.RemoveText = function () {
        canvas.remove($scope.selectText);
        $scope.$apply();
    };
    
}]);

 