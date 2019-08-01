/*Start template*/
var voucherEntry = {
    url: '/voucherEntry',
    template: '<div ui-view class="fade-in-up"></div>'
};

var voucherEntryList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/voucherEntry/list.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/voucherEntry/list.js',
                                   urlPrefix + 'js/services/voucherEntry/voucherEntryService.js'
                     ]);
                 }
             );

          }]
    }

};

var voucherEntryCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/voucherEntry/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/voucherEntry/create.js',
                                   urlPrefix + 'js/services/voucherEntry/voucherEntryService.js'
                     ]);
                 }
             );

          }]
    }

};

var voucherEntryEdit = {
    url: '/edit/{voucherEntryId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/voucherEntry/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/voucherEntry/edit.js',
                                   urlPrefix + 'js/services/voucherEntry/voucherEntryService.js'
                     ]);
                 }
             );

          }]
    }

};


var voucherEntryDetails = {
    url: '/details/{voucherEntryId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/voucherEntry/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/voucherEntry/details.js',
                                   urlPrefix + 'js/services/voucherEntry/voucherEntryService.js'
                     ]);
                 }
             );

          }]
    }

};

