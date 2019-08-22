/*Start sms template*/
var contactFeedback = {
    url: '/contactFeedback',
    template: '<div ui-view class="fade-in-up"></div>'
};

var contactFeedbackList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/contactFeedback/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/contactFeedback/list.js',
                                   urlPrefix + 'js/services/contactFeedback/contactFeedbackService.js'
                     ]);
                 }
             );

          }]
    }

};

var contactFeedbackDetails = {
    url: '/details/{contactFeedbackId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/contactFeedback/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/contactFeedback/details.js',
                                   urlPrefix + 'js/services/contactFeedback/contactFeedbackService.js'
                     ]);
                 }
             );

          }]
    }

};

