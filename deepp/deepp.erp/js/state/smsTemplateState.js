/*Start sms template*/
var smsTemplate = {
    abstract: true,
    url: '/smsTemplate',
    template: '<div ui-view class="fade-in-up"></div>'
};

var smsTemplateList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/sms/template/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/template/list.js',
                                   urlPrefix + 'js/services/sms/smsTemplateService.js'
                     ]);
                 }
             );

          }]
    }

};

var smsTemplateCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/sms/template/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/template/create.js',
                                   urlPrefix + 'js/services/sms/smsTemplateService.js'
                     ]);
                 }
             );

          }]
    }

};

var smsTemplateEdit = {
    url: '/edit/{templateId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/sms/template/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/template/edit.js',
                                   urlPrefix + 'js/services/sms/smsTemplateService.js'
                     ]);
                 }
             );

          }]
    }

};

var smsTemplateDetails = {
    url: '/details',
    templateUrl: urlPrefix + 'tpl/sms/template/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/template/details.js',
                                   urlPrefix + 'js/services/sms/smsTemplateService.js'
                     ]);
                 }
             );

          }]
    }

};
//  sms
/*Start sms template*/
var sendSms = {
    abstract: true,
    url: '/sendSms',
    template: '<div ui-view class="fade-in-up"></div>'
};

var sendSmsList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/sms/sendSms/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/sendSms/list.js',
                                   urlPrefix + 'js/services/sms/smsService.js'
                     ]);
                 }
             );

          }]
    }

};

var sendSmsCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/sms/sendSms/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/sendSms/create.js',
                                   urlPrefix + 'js/services/sms/smsService.js',
                                   urlPrefix + 'js/services/students/studentService.js',
                                   urlPrefix + 'js/services/teacher/teacherService.js',
                                   urlPrefix + 'js/services/employee/employeeService.js'
                     ]);
                 }
             );

          }]
    }

};

var sendSmsEdit = {
    url: '/edit/{sendSmsId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/sms/sendSms/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/sendSms/edit.js',
                                   urlPrefix + 'js/services/sms/SmsService.js'
                     ]);
                 }
             );

          }]
    }

};

var sendSmsDetails = {
    url: '/details',
    templateUrl: urlPrefix + 'tpl/sms/sendSms/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/sendSms/details.js',
                                   urlPrefix + 'js/services/sms/smsService.js'
                     ]);
                 }
             );

          }]
    }

};
