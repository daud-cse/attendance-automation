'use strict';

/*** Config for the router*/
//app.run(function ($rootScope, $templateCache) {
//    $rootScope.$on('$viewContentLoaded', function () {
//        $templateCache.removeAll();
//    });
//});
var apiUrlPrefix = 'http://localhost:45871/';
angular.module('app').run(
    ['$rootScope', '$state', '$stateParams', '$templateCache',
        function ($rootScope, $state, $stateParams, $templateCache) {
            //$rootScope.$on('$viewContentLoaded', function () {
            //    $templateCache.removeAll();
            //});
            $rootScope.$state = $state;
            $rootScope.$stateParams = $stateParams;
        }
    ]
).constant('ngAuthSettings', {
    apiServiceBaseUri: apiUrlPrefix,
    clientId: 'ngAuthApp'
}).config(
    ['$stateProvider', '$urlRouterProvider', '$locationProvider',
        function ($stateProvider, $urlRouterProvider, $locationProvider) {

            $urlRouterProvider
                .otherwise('/app/dashboard');
            $stateProvider
                .state('app', {
                    abstract: true,
                    url: '/app',
                    templateUrl: urlPrefix + 'Home/Dashboard'
                })
                .state('app.dashboard', {
                    url: '/dashboard',
                    templateUrl: urlPrefix + 'tpl/app_dashboard_v1.html',
                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load(['toaster']).then(
                            function () {
                                return $ocLazyLoad.load([urlPrefix + 'js/controllers/Dashboard.js',
                                              urlPrefix + 'js/services/DashboardService.js'
                                ]);
                            }
                             );
                          }]
                    }
                })

           .state('app.students', {
               url: '/student',
               templateUrl: urlPrefix + 'tpl/students/student.html',
               // use resolve to load other dependences
               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                            function () {
                                return $ocLazyLoad.load([urlPrefix + 'js/controllers/students/student.js',
                                              urlPrefix + 'js/services/students/studentService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.studentsdetails', {
               url: '/students/{studentId:[0-9]{1,4}}',
               templateUrl: urlPrefix + 'tpl/students/studentdetails.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                            function () {
                                return $ocLazyLoad.load([urlPrefix + 'js/controllers/students/studentdetails.js',
                                              urlPrefix + 'js/services/students/studentService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.studentsedit', {
               url: '/students/{studentId:[0-9]{1,4}}/edit',
               templateUrl: urlPrefix + 'tpl/students/studentEdit.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                            function () {
                                return $ocLazyLoad.load([urlPrefix + 'js/controllers/students/studentedit.js',
                                             urlPrefix + 'js/services/students/studentService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.studentlist', {
               url: '/students',
               templateUrl: urlPrefix + 'tpl/students/studentList.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load([urlPrefix + 'js/controllers/students/studentList.js',
                                             urlPrefix + 'js/services/students/studentService.js'
                                ]);
                            }
                        );

                     }]
               }

           })

             //page split

            /*Start Employee*/
            .state('app.employee', {
                url: '/employee',
                templateUrl: urlPrefix + 'tpl/employee/employee.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/employee/employee.js',
                                               urlPrefix + 'js/services/employee/employeeService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.employeedetails', {
                url: '/employee/{employeeId:[0-9]{1,4}}',
                templateUrl: urlPrefix + 'tpl/employee/employeedetails.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/employee/employeedetails.js',
                                              urlPrefix + 'js/services/employee/employeeService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.employeeedit', {
                url: '/employee/{employeeId:[0-9]{1,4}}/edit',
                templateUrl: urlPrefix + 'tpl/employee/employeeEdit.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/employee/employeeedit.js',
                                               urlPrefix + 'js/services/employee/employeeService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.employeelist', {
                url: '/employees',
                templateUrl: urlPrefix + 'tpl/employee/employeeList.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load('toaster').then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/employee/employeeList.js',
                                               urlPrefix + 'js/services/employee/employeeService.js'
                                 ]);
                             }
                         );

                      }]
                }

            })
            /*End Employee */
            /*Start Teacher*/
            .state('app.teacher', {
                url: '/employeeNew',
                templateUrl: urlPrefix + 'tpl/teacher/teacher.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/teacher/teacher.js',
                                               urlPrefix + 'js/services/teacher/teacherService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.teacherdetails', {
                url: '/Employee/{teacherId:[0-9]{1,4}}',
                templateUrl: urlPrefix + 'tpl/teacher/teacherdetails.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/teacher/teacherdetails.js',
                                               urlPrefix + 'js/services/teacher/teacherService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.teacheredit', {
                url: '/Employee/{teacherId:[0-9]{1,4}}/edit',
                templateUrl: urlPrefix + 'tpl/teacher/teacherEdit.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/teacher/teacheredit.js',
                                               urlPrefix + 'js/services/teacher/teacherService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.teacherlist', {
                url: '/employees',
                templateUrl: urlPrefix + 'tpl/teacher/teacherList.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load('toaster').then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/teacher/teacherList.js',
                                               urlPrefix + 'js/services/teacher/teacherService.js'
                                 ]);
                             }
                         );

                      }]
                }

            })
            /*End teacher */
            /*Start Notice */
                      .state('app.notice', {
                          url: '/notice',
                          template: '<div ui-view class="fade-in-up"></div>'
                      })
                        .state('app.notice.noticeList', {
                            url: '/noticeList',
                            templateUrl: urlPrefix + 'tpl/notice/noticeList.html',
                            // use resolve to load other dependences
                            resolve: {
                                deps: ['$ocLazyLoad',
                                  function ($ocLazyLoad) {
                                      return $ocLazyLoad.load('toaster').then(
                                         function () {
                                             return $ocLazyLoad.load([urlPrefix + 'js/controllers/notice/NoticeList.js', urlPrefix + 'js/services/notice/NoticeService.js']);
                                         }
                                     );

                                  }]
                            }

                        })
                  .state('app.notice.noticecreate', {
                      url: '/noticecreate',
                      templateUrl: urlPrefix + 'tpl/notice/NoticeCreate.html',
                      // use resolve to load other dependences
                      resolve: {
                          deps: ['$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                                   function () {
                                       return $ocLazyLoad.load([urlPrefix + 'js/controllers/notice/NoticeCreate.js', urlPrefix + 'js/services/notice/NoticeService.js', urlPrefix + 'js/controllers/file-upload.js']);
                                   }
                               );

                            }]
                      }

                  }).state('app.notice.editNotice', {
                      url: '/Notice/{noticeId:[0-9]{1,4}}/edit',
                      templateUrl: urlPrefix + 'tpl/notice/NoticeEdit.html',
                      // use resolve to load other dependences
                      resolve: {
                          deps: ['$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                                   function () {
                                       return $ocLazyLoad.load([urlPrefix + 'js/controllers/notice/NoticeEdit.js', urlPrefix + 'js/services/notice/NoticeService.js', urlPrefix + 'js/controllers/file-upload.js']);
                                   }
                               );

                            }]
                      }

                  })
            /*End Notice */
                /*Start Event */
                      .state('app.event', {
                          url: '/event',
                          template: '<div ui-view class="fade-in-up"></div>'
                      })
                        .state('app.event.eventList', {
                            url: '/eventList',
                            templateUrl: urlPrefix + 'tpl/event/EventList.html',
                            // use resolve to load other dependences
                            resolve: {
                                deps: ['$ocLazyLoad',
                                  function ($ocLazyLoad) {
                                      return $ocLazyLoad.load('toaster').then(
                                         function () {
                                             return $ocLazyLoad.load([urlPrefix + 'js/controllers/event/EventList.js', urlPrefix + 'js/services/event/EventService.js']);
                                         }
                                     );

                                  }]
                            }

                        })
                  .state('app.event.eventcreate', {
                      url: '/eventcreate',
                      templateUrl: urlPrefix + 'tpl/event/EventCreate.html',
                      // use resolve to load other dependences
                      resolve: {
                          deps: ['$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                                   function () {
                                       return $ocLazyLoad.load([urlPrefix + 'js/controllers/event/EventCreate.js', urlPrefix + 'js/services/event/EventService.js', urlPrefix + 'js/controllers/file-upload.js']);
                                   }
                               );

                            }]
                      }

                  }).state('app.event.editEvent', {
                      url: '/Event/{eventId:[0-9]{1,4}}/edit',
                      templateUrl: urlPrefix + 'tpl/event/EventEdit.html',
                      // use resolve to load other dependences
                      resolve: {
                          deps: ['$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                                   function () {
                                       return $ocLazyLoad.load([urlPrefix + 'js/controllers/event/EventEdit.js', urlPrefix + 'js/services/event/EventService.js', urlPrefix + 'js/controllers/file-upload.js']);
                                   }
                               );

                            }]
                      }

                  })
            /*End Event */
              /*Start Gallery*/
            .state('app.gallery', {
                url: '/gallery',
                template: '<div ui-view class="fade-in-up"></div>'
            }).state('app.gallery.new', {
                url: '/new',
                templateUrl: urlPrefix + 'tpl/gallery/gallery.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/gallery/gallery.js',
                                               urlPrefix + 'js/services/gallery/galleryService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.gallery.edit', {
                url: '/edit/{galleryId:[0-9]{1,4}}',
                templateUrl: urlPrefix + 'tpl/gallery/galleryEdit.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/gallery/galleryedit.js',
                                               urlPrefix + 'js/services/gallery/galleryService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.gallery.list', {
                url: '/list',
                templateUrl: urlPrefix + 'tpl/gallery/galleryList.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load('toaster').then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/gallery/galleryList.js',
                                               urlPrefix + 'js/services/gallery/galleryService.js'
                                 ]);
                             }
                         );

                      }]
                }

            })
            /*End gallery */

           /*Start Mobile Banking*/
            .state('app.mobilepayment', {
                url: '/mobilepayment',
                template: '<div ui-view class="fade-in-up"></div>'
            }).state('app.mobilepayment.new', {
                url: '/new',
                templateUrl: urlPrefix + 'tpl/mobilePayment/MobilePaymentNew.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/mobilePayment/MobilePaymentNew.js',
                                               urlPrefix + 'js/services/mobilePayment/MobilePaymentService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.mobilepayment.edit', {
                url: '/edit/{mpaymentId:[0-9]{1,4}}',
                templateUrl: urlPrefix + 'tpl/mobilePayment/MobilePaymentEdit.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load(['toaster']).then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/mobilePayment/MobilePaymentEdit.js',
                                               urlPrefix + 'js/services/mobilePayment/MobilePaymentService.js'
                                 ]);
                             }
                         );

                      }]
                }

            }).state('app.mobilepayment.list', {
                url: '/list',
                templateUrl: urlPrefix + 'tpl/mobilePayment/MobilePaymentList.html',
                // use resolve to load other dependences

                resolve: {
                    deps: ['$ocLazyLoad',
                      function ($ocLazyLoad) {
                          return $ocLazyLoad.load('toaster').then(
                             function () {
                                 return $ocLazyLoad.load([urlPrefix + 'js/controllers/mobilePayment/MobilePaymentList.js',
                                               urlPrefix + 'js/services/mobilePayment/MobilePaymentService.js'
                                 ]);
                             }
                         );

                      }]
                }

            })
            /*End Mobile Banking */

            ///------------------------------Settings menu -----------------------------
            .state('app.institutesettings', {
                abstract: true,
                url: '/institutesettings',
                templateUrl: urlPrefix + 'tpl/settings/Institute/Institute.html',
                // use resolve to load other dependences
                resolve: {
                    deps: [
                        'uiLoad',
                        function (uiLoad) {
                            return uiLoad.load([urlPrefix + 'js/app/settings/settings.js']);
                        }
                    ]
                }
            })
                .state('app.institutesettings.academicClass', {
                    url: '/academicClass',
                    templateUrl: urlPrefix + 'tpl/settings/AcademicClass.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/AcademicClass.js',
                                          urlPrefix + 'js/services/settings/AcademicClassService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
      .state('app.institutesettings.academicPeriod', {
          url: '/academicPeriod',
          templateUrl: urlPrefix + 'tpl/settings/AcademicPeriod.html',
          // use resolve to load other dependences

          resolve: {
              deps: [
                  '$ocLazyLoad',
                  function ($ocLazyLoad) {
                      return $ocLazyLoad.load('toaster').then(
                          function () {
                              return $ocLazyLoad.load([
                                 urlPrefix + 'js/controllers/settings/AcademicPeriod.js',
                                  urlPrefix + 'js/services/settings/AcademicPeriodService.js'
                              ]);
                          }
                      );

                  }
              ]
          }

      })
                .state('app.institutesettings.academicGroup', {
                    url: '/academicGroup',
                    templateUrl: urlPrefix + 'tpl/settings/AcademicGroup.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/AcademicGroup.js',
                                           urlPrefix + 'js/services/settings/academicGroupService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                }).state('app.institutesettings.academicSection', {
                    url: '/academicSection',
                    templateUrl: urlPrefix + 'tpl/settings/AcademicSection.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/AcademicSection.js',
                                          urlPrefix + 'js/services/settings/academicSectionService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.institutesettings.AcademicClassSectionMapping', {
                    url: '/AcademicClassSectionMapping',
                    templateUrl: urlPrefix + 'tpl/settings/AcademicClassSectionMapping.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load(['toaster', 'ui.select']).then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/AcademicClassSectionMapping.js',
                                          urlPrefix + 'js/services/settings/AcademicClassSectionMappingService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.institutesettings.academicSession', {
                    url: '/academicSession',
                    templateUrl: urlPrefix + 'tpl/settings/AcademicSession.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/AcademicSession.js',
                                           urlPrefix + 'js/services/settings/academicSessionService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                }).state('app.institutesettings.AcademicBranch', {
                    url: '/academicBranch',
                    templateUrl: urlPrefix + 'tpl/settings/AcademicBranch.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/AcademicBranch.js',
                                          urlPrefix + 'js/services/settings/AcademicBranchService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                }).state('app.institutesettings.AcademicShift', {
                    url: '/AcademicShift',
                    templateUrl: urlPrefix + 'tpl/settings/AcademicShift.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/AcademicShift.js',
                                           urlPrefix + 'js/services/settings/AcademicShiftService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                }).state('app.institutesettings.AcademicVersion', {
                    url: '/AcademicVersion',
                    templateUrl: urlPrefix + 'tpl/settings/AcademicVersion.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/settings/AcademicVersion.js',
                                         urlPrefix + 'js/services/settings/AcademicVersionService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.institutesettings.Colours', {
                    url: '/Colours',
                    templateUrl: urlPrefix + 'tpl/settings/Colours.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/Colours.js',
                                           urlPrefix + 'js/services/settings/ColoursService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.institutesettings.AttendanceType', {
                    url: '/AttendanceType',
                    templateUrl: urlPrefix + 'tpl/settings/AttendanceType.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                            urlPrefix + 'js/controllers/settings/AttendanceType.js',
                                          urlPrefix + 'js/services/settings/AttendanceTypeService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.institutesettings.GuardianType', {
                    url: '/GuardianType',
                    templateUrl: urlPrefix + 'tpl/settings/GuardianType.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/GuardianType.js',
                                            urlPrefix + 'js/services/settings/GuardianTypeService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.institutesettings.EducationalQualification', {
                    url: '/EducationalQualification',
                    templateUrl: urlPrefix + 'tpl/settings/EducationalQualification.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                         urlPrefix + 'js/controllers/settings/EducationalQualification.js',
                                          urlPrefix + 'js/services/settings/EducationalQualificationService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                 .state('app.institutesettings.InstituteCurrent', {
                     url: '/Institute/Current',
                     templateUrl: urlPrefix + 'tpl/settings/Institute/InstituteCurrent.html',
                     // use resolve to load other dependences

                     resolve: {
                         deps: [
                             '$ocLazyLoad',
                             function ($ocLazyLoad) {
                                 return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                                     function () {
                                         return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/settings/Institute/InstituteCurrent.js',
                                           urlPrefix + 'js/services/settings/InstituteService.js'
                                         ]);
                                     }
                                 );

                             }
                         ]
                     }
                 }).state('app.institutesettings.Testimonial', {
                     url: '/Testimonial',
                     templateUrl: urlPrefix + 'tpl/settings/Testimonial.html',
                     // use resolve to load other dependences

                     resolve: {
                         deps: [
                             '$ocLazyLoad',
                             function ($ocLazyLoad) {
                                 return $ocLazyLoad.load('toaster').then(
                                     function () {
                                         return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/settings/Testimonial.js',
                                          urlPrefix + 'js/services/settings/TestimonialService.js'
                                         ]);
                                     }
                                 );

                             }
                         ]
                     }
                 }).state('app.institutesettings.CoCurricularActivity', {
                     url: '/CoCurricularActivity',
                     templateUrl: urlPrefix + 'tpl/settings/CoCurricularActivity.html',
                     resolve: {
                         deps: [
                             '$ocLazyLoad',
                             function ($ocLazyLoad) {
                                 return $ocLazyLoad.load('toaster').then(
                                     function () {
                                         return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/settings/CoCurricularActivity.js',
                                          urlPrefix + 'js/services/settings/CoCurricularActivityService.js'
                                         ]);
                                     }
                                 );

                             }
                         ]
                     }
                 })
                .state('app.institutesettings.Scholarship', {
                    url: '/Scholarship',
                    templateUrl: urlPrefix + 'tpl/settings/Scholarship.html',
                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                         urlPrefix + 'js/controllers/settings/Scholarship.js',
                                         urlPrefix + 'js/services/settings/ScholarshipService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }
                })
            .state('app.institutesettings.Certificate', {
                url: '/Certificate',
                templateUrl: urlPrefix + 'tpl/settings/Certificate/Certificate.html',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load('toaster').then(
                                function () {
                                    return $ocLazyLoad.load([
                                         urlPrefix + 'vendor/libs/fabric.min.js',

                                     urlPrefix + 'js/controllers/settings/Certificate/Certificate.js',
                                     urlPrefix + 'js/services/settings/Certificate/CertificateService.js'
                                    ]);
                                }
                            );

                        }
                    ]
                }
            }).state('app.institutesettings.CertificatePrintType', {
                url: '/CertificatePrintType',
                templateUrl: urlPrefix + 'tpl/settings/CertificatePrintType.html',
                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load('toaster').then(
                                function () {
                                    return $ocLazyLoad.load([
                                     urlPrefix + 'js/controllers/settings/CertificatePrintType.js',
                                     urlPrefix + 'js/services/settings/CertificatePrintTypeService.js'
                                    ]);
                                }
                            );

                        }
                    ]
                }
            }).state('app.institutesettings.IncomeHead', {
                url: '/IncomeHead',
                templateUrl: urlPrefix + 'tpl/settings/IncomeHead.html',
                // use resolve to load other dependences

                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load('toaster').then(
                                function () {
                                    return $ocLazyLoad.load([
                                      urlPrefix + 'js/controllers/settings/IncomeHead.js',
                                     urlPrefix + 'js/services/settings/IncomeHeadService.js'
                                    ]);
                                }
                            );

                        }
                    ]
                }

            }).state('app.institutesettings.ExpenseHead', {
                url: '/IncomeHead',
                templateUrl: urlPrefix + 'tpl/settings/ExpenseHead.html',
                // use resolve to load other dependences

                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load('toaster').then(
                                function () {
                                    return $ocLazyLoad.load([
                                      urlPrefix + 'js/controllers/settings/ExpenseHead.js',
                                     urlPrefix + 'js/services/settings/ExpenseHeadService.js'
                                    ]);
                                }
                            );

                        }
                    ]
                }

            })

                 // Application Settings
                .state('app.applicationsettings', {
                    abstract: true,
                    url: '/applicationsettings',
                    templateUrl: urlPrefix + 'tpl/settings/application/application.html',
                    resolve: {
                        deps: [
                            'uiLoad',
                            function (uiLoad) {
                                return uiLoad.load([urlPrefix + 'js/app/settings/settings.js']);
                            }
                        ]
                    }
                })
                  .state('app.applicationsettings.Institute', {
                      url: '/Institute',
                      templateUrl: urlPrefix + 'tpl/settings/Institute.html',
                      // use resolve to load other dependences

                      resolve: {
                          deps: [
                              '$ocLazyLoad',
                              function ($ocLazyLoad) {
                                  return $ocLazyLoad.load('toaster').then(
                                      function () {
                                          return $ocLazyLoad.load([
                                             urlPrefix + 'js/controllers/settings/Institute.js',
                                             urlPrefix + 'js/services/settings/InstituteService.js'
                                          ]);
                                      }
                                  );

                              }
                          ]
                      }
                  })
                 .state('app.applicationsettings.InstituteEdit', {
                     url: '/Institute/edit/{instituteId:[0-9]{1,4}}',
                     templateUrl: urlPrefix + 'tpl/settings/Institute/InstituteEdit.html',
                     // use resolve to load other dependences

                     resolve: {
                         deps: [
                             '$ocLazyLoad',
                             function ($ocLazyLoad) {
                                 return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                                     function () {
                                         return $ocLazyLoad.load([
                                            urlPrefix + 'js/controllers/settings/Institute/InstituteEdit.js',
                                           urlPrefix + 'js/services/settings/InstituteService.js'
                                         ]);
                                     }
                                 );

                             }
                         ]
                     }
                 })
                 .state('app.applicationsettings.InstituteCreate', {
                     url: '/InstituteCreate',
                     templateUrl: urlPrefix + 'tpl/settings/Institute/InstituteCreate.html',
                     // use resolve to load other dependences

                     resolve: {
                         deps: [
                             '$ocLazyLoad',
                             function ($ocLazyLoad) {
                                 return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                                     function () {
                                         return $ocLazyLoad.load([
                                            urlPrefix + 'js/controllers/settings/Institute/InstituteCreate.js',
                                            urlPrefix + 'js/services/settings/InstituteService.js'
                                         ]);
                                     }
                                 );

                             }
                         ]
                     }
                 })
                .state('app.applicationsettings.package', {
                    url: '/Package',
                    templateUrl: urlPrefix + 'tpl/settings/Package.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/settings/Package.js',
                                               urlPrefix + 'js/services/settings/PackageService.js'
                                     ]);
                                 }
                             );

                          }]
                    }

                })
                // User Settings
                .state('app.usersettings', {
                    abstract: true,
                    url: '/usersettings',
                    templateUrl: urlPrefix + 'tpl/settings/user/user.html',
                    // use resolve to load other dependences
                    resolve: {
                        deps: [
                            'uiLoad',
                            function (uiLoad) {
                                return uiLoad.load([urlPrefix + 'js/app/settings/settings.js']);
                            }
                        ]
                    }
                })
                .state('app.usersettings.BloodGroup', {
                    url: '/BloodGroup',
                    templateUrl: urlPrefix + 'tpl/settings/BloodGroup.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/BloodGroup.js',
                                           urlPrefix + 'js/services/settings/BloodGroupService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                }).state('app.usersettings.Country', {
                    url: '/Country',
                    templateUrl: urlPrefix + 'tpl/settings/Country.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                         urlPrefix + 'js/controllers/settings/Country.js',
                                          urlPrefix + 'js/services/settings/CountryService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.usersettings.Gender', {
                    url: '/Gender',
                    templateUrl: urlPrefix + 'tpl/settings/Gender.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/settings/Gender.js',
                                         urlPrefix + 'js/services/settings/GenderService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.usersettings.DistrictOrState', {
                    url: '/DistrictOrState',
                    templateUrl: urlPrefix + 'tpl/settings/DistrictOrState.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/DistrictOrState.js',
                                           urlPrefix + 'js/services/settings/DistrictOrStateService.js',
                                          urlPrefix + 'js/services/settings/CountryService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.usersettings.Nationality', {
                    url: '/Nationality',
                    templateUrl: urlPrefix + 'tpl/settings/Nationality.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/settings/Nationality.js',
                                          urlPrefix + 'js/services/settings/NationalityService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                }).state('app.usersettings.Religion', {
                    url: '/Religion',
                    templateUrl: urlPrefix + 'tpl/settings/Religion.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/Religion.js',
                                           urlPrefix + 'js/services/settings/ReligionService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.usersettings.maritalStatus', {
                    url: '/maritalStatus',
                    templateUrl: urlPrefix + 'tpl/settings/MaritalStatus.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/maritalStatus.js',
                                          urlPrefix + 'js/services/settings/MaritalStatusService.js'
                                        ]);
                                    }
                                );

                            }
                        ]

                    }

                }).state('app.usersettings.Profession', {
                    url: '/Profession',
                    templateUrl: urlPrefix + 'tpl/settings/Profession.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                           urlPrefix + 'js/controllers/settings/Profession.js',
                                           urlPrefix + 'js/services/settings/ProfessionService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.usersettings.department', {
                    url: '/department',
                    templateUrl: urlPrefix + 'tpl/settings/department.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/settings/Department.js',
                                          urlPrefix + 'js/services/settings/DepartmentService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                .state('app.usersettings.Designation', {
                    url: '/Designation',
                    templateUrl: urlPrefix + 'tpl/settings/Designation.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/settings/Designation.js',
                                          urlPrefix + 'js/services/settings/DesignationService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                }).state('app.usersettings.AddressType', {
                    url: '/AddressType',
                    templateUrl: urlPrefix + 'tpl/settings/AddressType.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: [
                            '$ocLazyLoad',
                            function ($ocLazyLoad) {
                                return $ocLazyLoad.load('toaster').then(
                                    function () {
                                        return $ocLazyLoad.load([
                                         urlPrefix + 'js/controllers/settings/AddressType.js',
                                         urlPrefix + 'js/services/settings/AddressTypeService.js'
                                        ]);
                                    }
                                );

                            }
                        ]
                    }

                })
                // Roles Settings-------------------------------------------
                .state('app.rolessettings', {
                    abstract: true,
                    url: '/rolessettings',
                    templateUrl: urlPrefix + 'tpl/settings/roles/RolesSettings.html',
                    resolve: {
                        deps: [
                            'uiLoad',
                            function (uiLoad) {
                                return uiLoad.load([urlPrefix + 'js/app/settings/settings.js']);
                            }
                        ]
                    }
                })
                  .state('app.rolessettings.Roles', {
                      url: '/roles',
                      templateUrl: urlPrefix + 'tpl/settings/roles/role.html',
                      // use resolve to load other dependences

                      resolve: {
                          deps: [
                              '$ocLazyLoad',
                              function ($ocLazyLoad) {
                                  return $ocLazyLoad.load(['toaster', 'ui.select']).then(
                                      function () {
                                          return $ocLazyLoad.load([
                                             urlPrefix + 'js/controllers/settings/Roles/Roles.js',
                                             urlPrefix + 'js/services/settings/roles/RoleService.js',
                                               urlPrefix + 'js/services/settings/rights/rightsService.js'
                                          ]);
                                      }
                                  );

                              }
                          ]
                      }
                  })
                  .state('app.rolessettings.rights', {
                      url: '/rights',
                      templateUrl: urlPrefix + 'tpl/settings/rights/rights.html',
                      // use resolve to load other dependences

                      resolve: {
                          deps: [
                              '$ocLazyLoad',
                              function ($ocLazyLoad) {
                                  return $ocLazyLoad.load('toaster').then(
                                      function () {
                                          return $ocLazyLoad.load([
                                             urlPrefix + 'js/controllers/settings/rights/rights.js',
                                             urlPrefix + 'js/services/settings/rights/rightsService.js'
                                          ]);
                                      }
                                  );

                              }
                          ]
                      }
                  })

            ///------------------------------End roles menu -----------------------------

            /*  Start Attendance */
                /*By Teacher */
                 .state('app.studentAttendanceByTeacher', {
                     url: '/studentAttendanceByTeacher',
                     template: '<div ui-view class="fade-in-up"></div>',
                     resolve: {
                         deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendanceByTeacher/StudentAttendanceByTeacher.js',
                                                  urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js',
                                     ]);
                                 }
                             );

                          }]
                     }
                 })
                .state('app.studentAttendanceByTeacher.StudentAttendanceListByTeacher', {
                    url: '/StudentAttendanceListByTeacher',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByTeacher/studentAttendanceListByTeacher.html'
                })
                .state('app.studentAttendanceByTeacher.StudentAttendanceByTeacher', {
                    url: '/StudentAttendanceByTeacher',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByTeacher/studentAttendanceByTeacher.html'
                }).state('app.editStudentAttendanceByTeacher', {
                    url: '/StudentAttendanceByTeacher/{attenanceId:[0-9]{1,4}}/edit',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByTeacher/studentAttendanceByTeacher.html',
                    // use resolve to load other dependences
                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendanceByTeacher/StudentAttendanceByTeacher.js',
                                                   urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js'
                                     ]);
                                 }
                             );

                          }]
                    }

                }).state('app.detailsStudentAttendanceByTeacher', {
                    url: '/StudentAttendanceByTeacher/getsingle/{attenanceId:[0-9]{1,4}}',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByTeacher/studentAttendanceDetailsByTeacher.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendanceByTeacher/StudentAttendanceDetailsByTeacher.js',
                                                  urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js'
                                     ]);
                                 }
                             );

                          }]
                    }

                }).state('app.abscondingStudentAttendanceByTeacher', {
                    url: '/StudentAttendanceByTeacher/{attenanceId:[0-9]{1,4}}/adsconding',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByTeacher/studentAttendanceAbscondingByTeacher.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendanceByTeacher/studentAttendanceAbscondingByTeacher.js',
                                                  urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js'
                                     ]);
                                 }
                             );

                          }]
                    }

                })
                /*By Mangement */

                .state('app.abscondingStudentAttendanceByManagement', {
                    url: '/studentAttendanceByManagement/{attenanceId:[0-9]{1,4}}/adsconding',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByManagement/studentAttendanceAbscondingByManagement.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendanceByManagement/StudentAttendanceAbscondingByManagement.js',
                                                  urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js'
                                     ]);
                                 }
                             );

                          }]
                    }

                })

                 .state('app.studentAttendanceByManagement', {
                     url: '/studentAttendanceByManagement',
                     template: '<div ui-view class="fade-in-up"></div>',
                     resolve: {
                         deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendanceByManagement/StudentAttendanceByManagement.js',
                                                   urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js',
                                     ]);
                                 }
                             );

                          }]
                     }
                 })
                .state('app.studentAttendanceByManagement.StudentAttendanceListByManagement', {
                    url: '/StudentAttendanceListByManagement',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByManagement/studentAttendanceListByManagement.html'
                })
                .state('app.studentAttendanceByManagement.StudentAttendanceByManagement', {
                    url: '/StudentAttendanceByManagement',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByManagement/studentAttendanceByManagement.html'
                }).state('app.editStudentAttendanceByManagement', {
                    url: '/StudentAttendanceByManagement/{attenanceId:[0-9]{1,4}}/edit',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByManagement/studentAttendanceByManagement.html',
                    // use resolve to load other dependences
                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendanceByManagement/StudentAttendanceByManagement.js',
                                                  urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js'
                                     ]);
                                 }
                             );

                          }]
                    }

                }).state('app.detailsStudentAttendanceByManagement', {
                    url: '/StudentAttendanceByManagement/getsingle/{attenanceId:[0-9]{1,4}}',
                    templateUrl: urlPrefix + 'tpl/studentAttendanceByManagement/studentAttendanceDetailsByManagement.html',
                    // use resolve to load other dependences

                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendanceByManagement/StudentAttendanceDetailsByManagement.js',
                                                   urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js'
                                     ]);
                                 }
                             );

                          }]
                    }

                })
                .state('app.studentAttendance', {
                    url: '/studentAttendance',
                    template: '<div ui-view class="fade-in-up"></div>',
                    resolve: {
                        deps: ['$ocLazyLoad',
                         function ($ocLazyLoad) {
                             return $ocLazyLoad.load('toaster').then(
                                function () {
                                    return $ocLazyLoad.load([urlPrefix + 'js/services/studentAttendance/StudentAttendanceService.js'
                                    ]);
                                }
                            );

                         }]
                    }
                })
                .state('app.studentAttendance.AttendanceSheetPrint', {
                    url: '/AttendanceSheetPrint',
                    templateUrl: urlPrefix + 'tpl/studentAttendance/AttendanceSheetPrint.html',
                    // use resolve to load other dependences
                    resolve: {
                        deps: ['$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                                 function () {
                                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/studentAttendance/AttendanceSheetPrint.js'
                                     ]);
                                 }
                             );

                          }]
                    }

                })


            /*  End Attendance */
                //
                   .state('app.changePassword', {
                       url: '/changePassword',
                       templateUrl: urlPrefix + 'tpl/ChangePassword.html',
                       // use resolve to load other dependences
                       resolve: {
                           deps: ['$ocLazyLoad',
                             function ($ocLazyLoad) {
                                 return $ocLazyLoad.load(['toaster']).then(
                                    function () {
                                        return $ocLazyLoad.load([urlPrefix + 'js/controllers/ChangePassword.js',
                                            urlPrefix + 'js/services/ChangePasswordService.js']);
                                    }
                                );

                             }]
                       }
                   })

                //SMS Template
                .state('app.smsTemplate', smsTemplate)
                .state('app.smsTemplate.create', smsTemplateCreate)
                .state('app.smsTemplate.edit', smsTemplateEdit)
                .state('app.smsTemplate.details', smsTemplateDetails)
                .state('app.smsTemplate.list', smsTemplateList)
                 // send SMS  
                .state('app.sendSms', sendSms)
                .state('app.sendSms.create', sendSmsCreate)
                .state('app.sendSms.edit', sendSmsEdit)
                .state('app.sendSms.details', sendSmsDetails)
                .state('app.sendSms.list', sendSmsList)
                //Teacher Attendance Template
                .state('app.techerAttendance', techerAttendance)
                .state('app.techerAttendance.create', techerAttendanceCreate)
                .state('app.techerAttendance.edit', techerAttendanceEdit)
                .state('app.techerAttendance.details', techerAttendanceDetails)
                .state('app.techerAttendance.list', techerAttendanceList)
                //Employee Attendance Template
                .state('app.employeeAttendance', employeeAttendance)
                .state('app.employeeAttendance.create', employeeAttendanceCreate)
                .state('app.employeeAttendance.edit', employeeAttendanceEdit)
                .state('app.employeeAttendance.details', employeeAttendanceDetails)
                .state('app.employeeAttendance.list', employeeAttendanceList)
               //Online Admission Template
                .state('app.onlineAdmission', onlineAdmission)
                .state('app.onlineAdmission.edit', onlineAdmissionEdit)
                .state('app.onlineAdmission.list', onlineAdmissionList)
                .state('app.onlineAdmission.details', onlineAdmissionDetails)
                //Contact Us & FeedBack
                .state('app.contactFeedback', contactFeedback)
                .state('app.contactFeedback.list', contactFeedbackList)
                .state('app.contactFeedback.details', contactFeedbackDetails)
                //Contents Upload & Download
                .state('app.contents', contents)
                .state('app.contents.list', contentsList)
                .state('app.contents.create', contentsCreate)
                .state('app.contents.edit', contentsEdit)
                .state('app.contents.details', contentsDetails)
                // web manager
                .state('app.webmanager', webmanager)
                .state('app.webmanager.welcometext', webmanagerWelcometext)

                   //fees Generate Conf
               // .state('app.feesGenerateConfig', feesGenerateConfig)
               // .state('app.feesGenerateConfig.panel', feesGenerateConfigNew)


                             
            //Attendance Reports
             .state('app.AttendanceReports', {
                 url: '/AttendanceReports',
                 templateUrl: urlPrefix + 'tpl/AttendanceReports/AttendanceReports.html',
                 // use resolve to load other dependences

                 resolve: {
                     deps: [
                         '$ocLazyLoad',
                         function ($ocLazyLoad) {
                             return $ocLazyLoad.load('toaster').then(
                                 function () {
                                     return $ocLazyLoad.load([
                                      urlPrefix + 'js/controllers/AttendanceReports/AttendanceReports.js',
                                      urlPrefix + 'js/services/AttendanceReports/AttendanceReportsService.js',
                                      urlPrefix + 'js/services/UserInfo/UserInfoService.js'
                                     ]);
                                 }
                             );

                         }
                     ]
                 }

             })
              .state('app.UserAttendanceReports', {
                  url: '/UserAttendanceReports',
                  templateUrl: urlPrefix + 'tpl/AttendanceReports/UserAttendanceReports.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: [
                          '$ocLazyLoad',
                          function ($ocLazyLoad) {
                              return $ocLazyLoad.load('toaster').then(
                                  function () {
                                      return $ocLazyLoad.load([
                                       urlPrefix + 'js/controllers/AttendanceReports/AttendanceReports.js',
                                       urlPrefix + 'js/services/AttendanceReports/AttendanceReportsService.js',
                                       urlPrefix + 'js/services/UserInfo/UserInfoService.js'
                                      ]);
                                  }
                              );

                          }
                      ]
                  }

              })
                 .state('app.UserAttendanceSummaryReports', {
                     url: '/UserAttendanceSummaryReports',
                     templateUrl: urlPrefix + 'tpl/AttendanceReports/UserAttendanceSummaryReports.html',
                     // use resolve to load other dependences

                     resolve: {
                         deps: [
                             '$ocLazyLoad',
                             function ($ocLazyLoad) {
                                 return $ocLazyLoad.load('toaster').then(
                                     function () {
                                         return $ocLazyLoad.load([
                                          urlPrefix + 'js/controllers/AttendanceReports/AttendanceReports.js',
                                          urlPrefix + 'js/services/AttendanceReports/AttendanceReportsService.js',
                                          urlPrefix + 'js/services/UserInfo/UserInfoService.js'
                                         ]);
                                     }
                                 );

                             }
                         ]
                     }

                 })
             //Attendacne File Upload
            .state('app.AttendanceFileUpload', {
                url: '/AttendanceFileUpload',
                templateUrl: urlPrefix + 'tpl/AttendanceFileUpload/AttendanceFileUpload.html',
                // use resolve to load other dependences

                //resolve: {
                //    deps: [
                //        '$ocLazyLoad',
                //        function ($ocLazyLoad) {
                //            return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                //                function () {
                //                    return $ocLazyLoad.load([
                //                     urlPrefix + 'js/controllers/settings/Institute/InstituteCurrent.js',
                //                      urlPrefix + 'js/services/settings/InstituteService.js'
                //                    ]);
                //                }
                //            );

                //        }
                //    ]
                //}



                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['toaster', 'textAngular', 'angularFileUpload']).then(
                                function () {
                                    return $ocLazyLoad.load([
                                     urlPrefix + 'js/controllers/AttendanceFileUpload/AttendanceFileUploads.js',
                                     urlPrefix + 'js/services/AttendanceFileUpload/AttendanceFileUploadService.js'
                                    ]);
                                }
                            );

                        }
                    ]
                }

            }).state('app.globalUser', {
                url: '/globalUser',
                templateUrl: urlPrefix + 'tpl/globalUser/globalUser.html',
                // use resolve to load other dependences

                resolve: {
                    deps: [
                        '$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['toaster', 'ngImgCrop', 'angularFileUpload']).then(
                                function () {
                                    return $ocLazyLoad.load([
                                        urlPrefix + 'js/controllers/globalUser/globalUser.js',
                                        urlPrefix + 'js/services/globalUser/GlobalUserService.js'
                                    ]);
                                }
                            );

                        }
                    ]
                }

            })

                

        }
    ]
  );


