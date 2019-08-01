$stateProvider
               .state('app.settings.package', {
                   url: '/Package',
                   templateUrl: 'tpl/settings/Package.html',
                   // use resolve to load other dependences

                   resolve: {
                       deps: ['$ocLazyLoad',
                         function ($ocLazyLoad) {
                             return $ocLazyLoad.load('toaster').then(
                                function () {
                                    return $ocLazyLoad.load(['js/controllers/settings/Package.js',
                                                  'js/services/settings/PackageService.js'
                                    ]);
                                }
                            );

                         }]
                   }

               })
              .state('app.settings.department', {
                  url: '/department',
                  templateUrl: 'tpl/settings/department.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load('toaster').then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/settings/Department.js',
                                                 'js/services/settings/DepartmentService.js'
                                   ]);
                               }
                           );

                        }]
                  }

              })
           .state('app.settings.maritalStatus', {
               url: '/maritalStatus',
               templateUrl: 'tpl/settings/MaritalStatus.html',
               // use resolve to load other dependences

               resolve: {


                   deps: ['$ocLazyLoad',
                       function ($ocLazyLoad) {
                           return $ocLazyLoad.load('toaster').then(
                              function () {
                                  return $ocLazyLoad.load(['js/controllers/settings/maritalStatus.js',
                                              'js/services/settings/MaritalStatusService.js'
                                  ]);
                              }
                          );

                       }]

               }

           }).state('app.settings.academicClass', {
               url: '/academicClass',
               templateUrl: 'tpl/settings/AcademicClass.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/AcademicClass.js',
                                              'js/services/settings/AcademicClassService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.academicGroup', {
               url: '/academicGroup',
               templateUrl: 'tpl/settings/AcademicGroup.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/AcademicGroup.js',
                                              'js/services/settings/academicGroupService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.academicSection', {
               url: '/academicSection',
               templateUrl: 'tpl/settings/AcademicSection.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/AcademicSection.js',
                                              'js/services/settings/academicSectionService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.academicSession', {
               url: '/academicSession',
               templateUrl: 'tpl/settings/AcademicSession.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/AcademicSession.js',
                                              'js/services/settings/academicSessionService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.students', {
               url: '/student',
               templateUrl: 'tpl/students/student.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/students/student.js',
                                              'js/services/students/studentService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.studentsdetails', {
               url: '/students/{studentId:[0-9]{1,4}}',
               templateUrl: 'tpl/students/studentdetails.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/students/studentdetails.js',
                                              'js/services/students/studentService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.studentsedit', {
               url: '/students/{studentId:[0-9]{1,4}}/edit',
               templateUrl: 'tpl/students/studentEdit.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/students/studentedit.js',
                                              'js/services/students/studentService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.studentlist', {
               url: '/students',
               templateUrl: 'tpl/students/studentList.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/students/studentList.js',
                                              'js/services/students/studentService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.AcademicBranch', {
               url: '/academicBranch',
               templateUrl: 'tpl/settings/AcademicBranch.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/AcademicBranch.js',
                                              'js/services/settings/AcademicBranchService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.AcademicShift', {
               url: '/AcademicShift',
               templateUrl: 'tpl/settings/AcademicShift.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/AcademicShift.js',
                                              'js/services/settings/AcademicShiftService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.AcademicVersion', {
               url: '/AcademicVersion',
               templateUrl: 'tpl/settings/AcademicVersion.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/AcademicVersion.js',
                                              'js/services/settings/AcademicVersionService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.AddressType', {
               url: '/AddressType',
               templateUrl: 'tpl/settings/AddressType.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/AddressType.js',
                                              'js/services/settings/AddressTypeService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.BloodGroup', {
               url: '/BloodGroup',
               templateUrl: 'tpl/settings/BloodGroup.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/BloodGroup.js',
                                              'js/services/settings/BloodGroupService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.Country', {
               url: '/Country',
               templateUrl: 'tpl/settings/Country.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/Country.js',
                                              'js/services/settings/CountryService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.Designation', {
               url: '/Designation',
               templateUrl: 'tpl/settings/Designation.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/Designation.js',
                                              'js/services/settings/DesignationService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.DistrictOrState', {
               url: '/DistrictOrState',
               templateUrl: 'tpl/settings/DistrictOrState.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/DistrictOrState.js',
                                              'js/services/settings/DistrictOrStateService.js',
                                              'js/services/settings/CountryService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.EducationalQualification', {
               url: '/EducationalQualification',
               templateUrl: 'tpl/settings/EducationalQualification.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/EducationalQualification.js',
                                              'js/services/settings/EducationalQualificationService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.Gender', {
               url: '/Gender',
               templateUrl: 'tpl/settings/Gender.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/Gender.js',
                                              'js/services/settings/GenderService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.GuardianType', {
               url: '/GuardianType',
               templateUrl: 'tpl/settings/GuardianType.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/GuardianType.js',
                                              'js/services/settings/GuardianTypeService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.Institute', {
               url: '/Institute',
               templateUrl: 'tpl/settings/Institute.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/Institute.js',
                                              'js/services/settings/InstituteService.js'
                                ]);
                            }
                        );

                     }]
               }
           }).state('app.settings.Nationality', {
               url: '/Nationality',
               templateUrl: 'tpl/settings/Nationality.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/Nationality.js',
                                              'js/services/settings/NationalityService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.Profession', {
               url: '/Profession',
               templateUrl: 'tpl/settings/Profession.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/Profession.js',
                                              'js/services/settings/ProfessionService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.Religion', {
               url: '/Religion',
               templateUrl: 'tpl/settings/Religion.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/Religion.js',
                                              'js/services/settings/ReligionService.js'
                                ]);
                            }
                        );

                     }]
               }

           }).state('app.settings.Colours', {
               url: '/Colours',
               templateUrl: 'tpl/settings/Colours.html',
               // use resolve to load other dependences

               resolve: {
                   deps: ['$ocLazyLoad',
                     function ($ocLazyLoad) {
                         return $ocLazyLoad.load('toaster').then(
                            function () {
                                return $ocLazyLoad.load(['js/controllers/settings/Colours.js',
                                              'js/services/settings/ColoursService.js'
                                ]);
                            }
                        );

                     }]
               }

           })
              .state('app.settings.PackageList', {
                  url: '/Package',
                  templateUrl: 'tpl/settings/Package.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load('toaster').then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/settings/Package.js',
                                                 'js/services/settings/PackageService.js'
                                   ]);
                               }
                           );

                        }]
                  }

              }).state('app.settings.AttendanceType', {
                  url: '/AttendanceType',
                  templateUrl: 'tpl/settings/AttendanceType.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load('toaster').then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/settings/AttendanceType.js',
                                                 'js/services/settings/AttendanceTypeService.js'
                                   ]);
                               }
                           );

                        }]
                  }

              })

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
                                   return $ocLazyLoad.load(['js/controllers/studentAttendanceByTeacher/StudentAttendanceByTeacher.js',
                                                 'js/services/studentAttendance/StudentAttendanceService.js',
                                   ]);
                               }
                           );

                        }]
                   }
               })
              .state('app.studentAttendanceByTeacher.StudentAttendanceListByTeacher', {
                  url: '/StudentAttendanceListByTeacher',
                  templateUrl: 'tpl/studentAttendanceByTeacher/studentAttendanceListByTeacher.html'
              })
              .state('app.studentAttendanceByTeacher.StudentAttendanceByTeacher', {
                  url: '/StudentAttendanceByTeacher',
                  templateUrl: 'tpl/studentAttendanceByTeacher/studentAttendanceByTeacher.html'
              }).state('app.editStudentAttendanceByTeacher', {
                  url: '/StudentAttendanceByTeacher/{attenanceId:[0-9]{1,4}}/edit',
                  templateUrl: 'tpl/studentAttendanceByTeacher/studentAttendanceEditByTeacher.html',
                  // use resolve to load other dependences
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/studentAttendanceByTeacher/StudentAttendanceEditByTeacher.js',
                                                 'js/services/studentAttendance/StudentAttendanceService.js'
                                   ]);
                               }
                           );

                        }]
                  }

              }).state('app.detailsStudentAttendanceByTeacher', {
                  url: '/StudentAttendanceByTeacher/getsingle/{attenanceId:[0-9]{1,4}}',
                  templateUrl: 'tpl/studentAttendanceByTeacher/studentAttendanceDetailsByTeacher.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/studentAttendanceByTeacher/StudentAttendanceDetailsByTeacher.js',
                                                 'js/services/studentAttendance/StudentAttendanceService.js'
                                   ]);
                               }
                           );

                        }]
                  }

              })
              /*By Mangement */
               .state('app.studentAttendanceByManagement', {
                   url: '/studentAttendanceByManagement',
                   template: '<div ui-view class="fade-in-up"></div>',
                   resolve: {
                       deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load('toaster').then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/studentAttendanceByManagement/StudentAttendanceByManagement.js',
                                                 'js/services/studentAttendance/StudentAttendanceService.js',
                                   ]);
                               }
                           );

                        }]
                   }
               })
              .state('app.studentAttendanceByManagement.StudentAttendanceListByManagement', {
                  url: '/StudentAttendanceListByManagement',
                  templateUrl: 'tpl/studentAttendanceByManagement/studentAttendanceListByManagement.html'
              })
              .state('app.studentAttendanceByManagement.StudentAttendanceByManagement', {
                  url: '/StudentAttendanceByManagement',
                  templateUrl: 'tpl/studentAttendanceByManagement/studentAttendanceByManagement.html'
              }).state('app.editStudentAttendanceByManagement', {
                  url: '/StudentAttendanceByManagement/{attenanceId:[0-9]{1,4}}/edit',
                  templateUrl: 'tpl/studentAttendanceByManagement/studentAttendanceEditByManagement.html',
                  // use resolve to load other dependences
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/studentAttendanceByManagement/StudentAttendanceEditByManagement.js',
                                                 'js/services/studentAttendance/StudentAttendanceService.js'
                                   ]);
                               }
                           );

                        }]
                  }

              }).state('app.detailsStudentAttendanceByManagement', {
                  url: '/StudentAttendanceByManagement/getsingle/{attenanceId:[0-9]{1,4}}',
                  templateUrl: 'tpl/studentAttendanceByManagement/studentAttendanceDetailsByManagement.html',
                  // use resolve to load other dependences

                  resolve: {
                      deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/studentAttendanceByManagement/StudentAttendanceDetailsByManagement.js',
                                                 'js/services/studentAttendance/StudentAttendanceService.js'
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
                                  return $ocLazyLoad.load(['js/services/studentAttendance/StudentAttendanceService.js'
                                  ]);
                              }
                          );

                       }]
                  }
              })
              .state('app.studentAttendance.AttendanceSheetPrint', {
                  url: '/AttendanceSheetPrint',
                  templateUrl: 'tpl/studentAttendance/AttendanceSheetPrint.html',
                  // use resolve to load other dependences
                  resolve: {
                      deps: ['$ocLazyLoad',
                        function ($ocLazyLoad) {
                            return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                               function () {
                                   return $ocLazyLoad.load(['js/controllers/studentAttendance/AttendanceSheetPrint.js'
                                   ]);
                               }
                           );

                        }]
                  }

              })
          /*  End Attendance */
          /*Start Employee*/
          .state('app.employee', {
              url: '/employee',
              templateUrl: 'tpl/employee/employee.html',
              // use resolve to load other dependences

              resolve: {
                  deps: ['$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                           function () {
                               return $ocLazyLoad.load(['js/controllers/employee/employee.js',
                                             'js/services/employee/employeeService.js'
                               ]);
                           }
                       );

                    }]
              }

          }).state('app.employeedetails', {
              url: '/employee/{employeeId:[0-9]{1,4}}',
              templateUrl: 'tpl/employee/employeedetails.html',
              // use resolve to load other dependences

              resolve: {
                  deps: ['$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                           function () {
                               return $ocLazyLoad.load(['js/controllers/employee/employeedetails.js',
                                             'js/services/employee/employeeService.js'
                               ]);
                           }
                       );

                    }]
              }

          }).state('app.employeeedit', {
              url: '/employee/{employeeId:[0-9]{1,4}}/edit',
              templateUrl: 'tpl/employee/employeeEdit.html',
              // use resolve to load other dependences

              resolve: {
                  deps: ['$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                           function () {
                               return $ocLazyLoad.load(['js/controllers/employee/employeeedit.js',
                                             'js/services/employee/employeeService.js'
                               ]);
                           }
                       );

                    }]
              }

          }).state('app.employeelist', {
              url: '/employees',
              templateUrl: 'tpl/employee/employeeList.html',
              // use resolve to load other dependences

              resolve: {
                  deps: ['$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load('toaster').then(
                           function () {
                               return $ocLazyLoad.load(['js/controllers/employee/employeeList.js',
                                             'js/services/employee/employeeService.js'
                               ]);
                           }
                       );

                    }]
              }

          })
          /*End Employee */
          /*Start Teacher*/
          .state('app.teacher', {
              url: '/teacher',
              templateUrl: 'tpl/teacher/teacher.html',
              // use resolve to load other dependences

              resolve: {
                  deps: ['$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                           function () {
                               return $ocLazyLoad.load(['js/controllers/teacher/teacher.js',
                                             'js/services/teacher/teacherService.js'
                               ]);
                           }
                       );

                    }]
              }

          }).state('app.teacherdetails', {
              url: '/teacher/{teacherId:[0-9]{1,4}}',
              templateUrl: 'tpl/teacher/teacherdetails.html',
              // use resolve to load other dependences

              resolve: {
                  deps: ['$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                           function () {
                               return $ocLazyLoad.load(['js/controllers/teacher/teacherdetails.js',
                                             'js/services/teacher/teacherService.js'
                               ]);
                           }
                       );

                    }]
              }

          }).state('app.teacheredit', {
              url: '/teacher/{teacherId:[0-9]{1,4}}/edit',
              templateUrl: 'tpl/teacher/teacherEdit.html',
              // use resolve to load other dependences

              resolve: {
                  deps: ['$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load(['toaster', 'angularFileUpload']).then(
                           function () {
                               return $ocLazyLoad.load(['js/controllers/teacher/teacheredit.js',
                                             'js/services/teacher/teacherService.js'
                               ]);
                           }
                       );

                    }]
              }

          }).state('app.teacherlist', {
              url: '/teachers',
              templateUrl: 'tpl/teacher/teacherList.html',
              // use resolve to load other dependences

              resolve: {
                  deps: ['$ocLazyLoad',
                    function ($ocLazyLoad) {
                        return $ocLazyLoad.load('toaster').then(
                           function () {
                               return $ocLazyLoad.load(['js/controllers/teacher/teacherList.js',
                                             'js/services/teacher/teacherService.js'
                               ]);
                           }
                       );

                    }]
              }

          })
          /*End Employee */
          /*Start Notice */
        .state('app.notice', {
            url: '/notice',
            template: '<div ui-view class="fade-in-up"></div>'
        })
        .state('app.noticecreate', {
            url: '/noticecreate',
            templateUrl: 'tpl/notice/NoticeCreate.html',
            // use resolve to load other dependences
            resolve: {
                deps: ['$ocLazyLoad',
                  function ($ocLazyLoad) {
                      return $ocLazyLoad.load('toaster').then(
                         function () {
                             return $ocLazyLoad.load(['js/controllers/notice/NoticeCreate.js', 'js/services/notice/NoticeService.js']);
                         }
                     );

                  }]
            }

        })
