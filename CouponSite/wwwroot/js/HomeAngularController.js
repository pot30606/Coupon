angular.module('myApp', []).controller('ContentController', function ($scope, $http, $location, $window) {

    $scope.receiveInfo = {};
    $scope.submitForm = function () {
        console.log('Do submitForm !', $scope.receiveInfo);
        /*
        $http({
            method: 'POST',
            url: '/api/ReceiveCoupon',
            data: $scope.receiveInfo
        })
        */
    }



})