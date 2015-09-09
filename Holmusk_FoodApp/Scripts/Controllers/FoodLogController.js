angular.module('MyApp') 
       .controller('FoodLogController', function ($scope, FoodLogService) { 
           $scope.FoodLogs = null;
           FoodLogService.GetFoodLogList().then(function (d) {
               $scope.FoodLogs = d.data; //Success callback
           }, function (error) {
               alert('Error!'); // Failed Callback
           });
       })
       .factory('FoodLogService', function ($http) {

           var fac = {};
           fac.GetFoodLogList = function () {
               return $http.get('/FoodLog/Index')
           }
           return fac;
       });