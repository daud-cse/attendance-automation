'use strict';

/**
 * Config for the router
 */


angular.module('app')
   .run(
    ['$rootScope', '$state', '$stateParams',
      function ($rootScope, $state, $stateParams) {
          $rootScope.$state = $state;
          $rootScope.$stateParams = $stateParams;
      }
    ]
  )
  .config(
    ['$stateProvider', '$urlRouterProvider', '$locationProvider',
      function ($stateProvider, $urlRouterProvider, $locationProvider) {

          
              // Institute Settings
               
          $urlRouterProvider
              .otherwise('/app/dashboard');
          $stateProvider.state('app.institutesettings', {
                  abstract: true,
                  url: '/institutesettings',
                  templateUrl: 'tpl/settings/Institute/Institute.html',
                  // use resolve to load other dependences
                  resolve: {
                      deps: [
                          'uiLoad',
                          function(uiLoad) {
                              return uiLoad.load(['js/app/settings/settings.js']);
                          }
                      ]
                  }
              })
              .state('app.institutesettings.academicClass', {
                  url: '/academicClass',
                  templateUrl: 'tpl/settings/AcademicClass.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AcademicClass.js',
                                          'js/services/settings/AcademicClassService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.institutesettings.academicGroup', {
                  url: '/academicGroup',
                  templateUrl: 'tpl/settings/AcademicGroup.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AcademicGroup.js',
                                          'js/services/settings/academicGroupService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.institutesettings.academicSection', {
                  url: '/academicSection',
                  templateUrl: 'tpl/settings/AcademicSection.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AcademicSection.js',
                                          'js/services/settings/academicSectionService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.institutesettings.academicSession', {
                  url: '/academicSession',
                  templateUrl: 'tpl/settings/AcademicSession.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AcademicSession.js',
                                          'js/services/settings/academicSessionService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.institutesettings.AcademicBranch', {
                  url: '/academicBranch',
                  templateUrl: 'tpl/settings/AcademicBranch.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AcademicBranch.js',
                                          'js/services/settings/AcademicBranchService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.institutesettings.AcademicShift', {
                  url: '/AcademicShift',
                  templateUrl: 'tpl/settings/AcademicShift.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AcademicShift.js',
                                          'js/services/settings/AcademicShiftService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.institutesettings.AcademicVersion', {
                  url: '/AcademicVersion',
                  templateUrl: 'tpl/settings/AcademicVersion.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AcademicVersion.js',
                                          'js/services/settings/AcademicVersionService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.institutesettings.Colours', {
                  url: '/Colours',
                  templateUrl: 'tpl/settings/Colours.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Colours.js',
                                          'js/services/settings/ColoursService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.institutesettings.AttendanceType', {
                  url: '/AttendanceType',
                  templateUrl: 'tpl/settings/AttendanceType.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AttendanceType.js',
                                          'js/services/settings/AttendanceTypeService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.institutesettings.GuardianType', {
                  url: '/GuardianType',
                  templateUrl: 'tpl/settings/GuardianType.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/GuardianType.js',
                                          'js/services/settings/GuardianTypeService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.institutesettings.EducationalQualification', {
                  url: '/EducationalQualification',
                  templateUrl: 'tpl/settings/EducationalQualification.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/EducationalQualification.js',
                                          'js/services/settings/EducationalQualificationService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.institutesettings.Institute', {
                  url: '/Institute',
                  templateUrl: 'tpl/settings/Institute.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Institute.js',
                                          'js/services/settings/InstituteService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }
              })
              // User Settings
              .state('app.usersettings', {
                  abstract: true,
                  url: '/usersettings',
                  templateUrl: 'tpl/settings/user/user.html',
                  // use resolve to load other dependences
                  resolve: {
                      deps: [
                          'uiLoad',
                          function(uiLoad) {
                              return uiLoad.load(['js/app/settings/settings.js']);
                          }
                      ]
                  }
              })
              .state('app.usersettings.BloodGroup', {
                  url: '/BloodGroup',
                  templateUrl: 'tpl/settings/BloodGroup.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/BloodGroup.js',
                                          'js/services/settings/BloodGroupService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.usersettings.Country', {
                  url: '/Country',
                  templateUrl: 'tpl/settings/Country.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Country.js',
                                          'js/services/settings/CountryService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.usersettings.Gender', {
                  url: '/Gender',
                  templateUrl: 'tpl/settings/Gender.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Gender.js',
                                          'js/services/settings/GenderService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.usersettings.DistrictOrState', {
                  url: '/DistrictOrState',
                  templateUrl: 'tpl/settings/DistrictOrState.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/DistrictOrState.js',
                                          'js/services/settings/DistrictOrStateService.js',
                                          'js/services/settings/CountryService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.usersettings.Nationality', {
                  url: '/Nationality',
                  templateUrl: 'tpl/settings/Nationality.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Nationality.js',
                                          'js/services/settings/NationalityService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.usersettings.Religion', {
                  url: '/Religion',
                  templateUrl: 'tpl/settings/Religion.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Religion.js',
                                          'js/services/settings/ReligionService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.usersettings.maritalStatus', {
                  url: '/maritalStatus',
                  templateUrl: 'tpl/settings/MaritalStatus.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/maritalStatus.js',
                                          'js/services/settings/MaritalStatusService.js'
                                      ]);
                                  }
                              );

                          }
                      ]

                  }

              }).state('app.usersettings.Profession', {
                  url: '/Profession',
                  templateUrl: 'tpl/settings/Profession.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Profession.js',
                                          'js/services/settings/ProfessionService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.usersettings.department', {
                  url: '/department',
                  templateUrl: 'tpl/settings/department.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Department.js',
                                          'js/services/settings/DepartmentService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
              .state('app.usersettings.Designation', {
                  url: '/Designation',
                  templateUrl: 'tpl/settings/Designation.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/Designation.js',
                                          'js/services/settings/DesignationService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              }).state('app.usersettings.AddressType', {
                  url: '/AddressType',
                  templateUrl: 'tpl/settings/AddressType.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function() {
                                      return $ocLazyLoad.load([
                                          'js/controllers/settings/AddressType.js',
                                          'js/services/settings/AddressTypeService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              });

      }
    ]
  );