/*Start sms template*/
var libraryBook = {
    abstract: true,
    url: '/LibraryBook',
    template: '<div ui-view class="fade-in-up"></div>'
};

var libraryBookList = {
    url: '/list',
    templateUrl: urlPrefix + 'tpl/LibraryBook/LibraryBooklist.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'ui.select']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/LibraryBook/LibraryBooklist.js',
                                   urlPrefix + 'js/services/LibraryBook/LibraryBookService.js',
                                   urlPrefix + 'js/services/LibraryBook/LibraryBookAuthoreService.js'
                     ]);
                 }
             );

          }]
    }

};

var libraryBookCreate = {
    url: '/create',
    templateUrl: urlPrefix + 'tpl/sms/template/create.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/template/create.js',
                                   urlPrefix + 'js/services/sms/LibraryBookService.js'
                     ]);
                 }
             );

          }]
    }

};

var libraryBookEdit = {
    url: '/edit/{templateId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/sms/template/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/template/edit.js',
                                   urlPrefix + 'js/services/sms/LibraryBookService.js'
                     ]);
                 }
             );

          }]
    }

};

var libraryBookDetails = {
    url: '/details',
    templateUrl: urlPrefix + 'tpl/sms/template/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load('toaster').then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/sms/template/details.js',
                                   urlPrefix + 'js/services/sms/LibraryBookService.js'
                     ]);
                 }
             );

          }]
    }

}; 