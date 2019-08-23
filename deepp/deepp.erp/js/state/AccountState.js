/*Start template*/
var Exam = {
    url: '/Account',
    template: '<div ui-view class="fade-in-up"></div>'
};

///Bank Account State
var BankAccount = {
    url: '/Account/BankAccount',
    templateUrl: urlPrefix + 'tpl/Account/BankAccount.html',
    // use resolve to load other dependences

    resolve: {
        deps: [
            '$ocLazyLoad',
            function ($ocLazyLoad) {
                return $ocLazyLoad.load('toaster').then(
                    function () {
                        return $ocLazyLoad.load([
                         urlPrefix + 'js/controllers/Accounts/BankAccount.js',
                         urlPrefix + 'js/services/Account/BankAccountService.js'
                        ]);
                    }
                );

            }
        ]
    }

};


///Vendor Account State
var VendorAccount = {
    url: '/Account/VendorAccount',
    templateUrl: urlPrefix + 'tpl/Account/Vendor/Vendor.html',
    // use resolve to load other dependences

    resolve: {
        deps: [
            '$ocLazyLoad',
            function ($ocLazyLoad) {
                return $ocLazyLoad.load('toaster').then(
                    function () {
                        return $ocLazyLoad.load([
                         urlPrefix + 'js/controllers/Accounts/VendorAccount.js',
                         urlPrefix + 'js/services/Account/VendorAccountService.js'
                        ]);
                    }
                );

            }
        ]
    }

};

///GL Account  Tree
var GLAccount = {
    url: '/Account/GLAccount',
    templateUrl: urlPrefix + 'tpl/Account/GLAccount/GLAccountTree.html',
    // use resolve to load other dependences

    resolve: {
        deps: [
            '$ocLazyLoad',
            function ($ocLazyLoad) {
                return $ocLazyLoad.load('toaster').then(
                    function () {
                        return $ocLazyLoad.load([
                         urlPrefix + 'js/controllers/Accounts/GLAccount.js',
                         urlPrefix + 'js/services/Account/GLAccountService.js'
                        ]);
                    }
                );

            }
        ]
    }

};

///GL Subsidy Type
var GLSubsidyType = {
    url: '/Account/GLSubsidyType',
    templateUrl: urlPrefix + 'tpl/Account/GLSubsidyTypeMap/GLSubsidy.html',
    // use resolve to load other dependences

    resolve: {
        deps: [
            '$ocLazyLoad',
            function ($ocLazyLoad) {
                return $ocLazyLoad.load('toaster').then(
                    function () {
                        return $ocLazyLoad.load([
                         urlPrefix + 'js/controllers/Accounts/GLSubsidyType.js',
                         urlPrefix + 'js/services/Account/GLSubsidyTypeService.js'
                        ]);
                    }
                );

            }
        ]
    }

};

//Related Account

var RelatedAccount = {
    url: '/Account/RelatedAccount',
    templateUrl: urlPrefix + 'tpl/Account/RelatedAccount.html',
    // use resolve to load other dependences

    resolve: {
        deps: [
            '$ocLazyLoad',
            function ($ocLazyLoad) {
                return $ocLazyLoad.load('toaster').then(
                    function () {
                        return $ocLazyLoad.load([
                         urlPrefix + 'js/controllers/Accounts/RelatedAccount.js',
                         urlPrefix + 'js/services/Account/RelatedAccountService.js'
                        ]);
                    }
                );

            }
        ]
    }

};


var ExamEdit = {
    url: '/edit/{ExamId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/Exam/edit.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/Exam/edit.js',
                                   urlPrefix + 'js/services/Exam/ExamService.js'
                     ]);
                 }
             );

          }]
    }

};


var ExamDetails = {
    url: '/details/{ExamId:[0-9]{1,4}}',
    templateUrl: urlPrefix + 'tpl/Exam/details.html',
    // use resolve to load other dependences

    resolve: {
        deps: ['$ocLazyLoad',
          function ($ocLazyLoad) {
              return $ocLazyLoad.load(['toaster', 'textAngular']).then(
                 function () {
                     return $ocLazyLoad.load([urlPrefix + 'js/controllers/Exam/details.js',
                                   urlPrefix + 'js/services/Exam/ExamService.js'
                     ]);
                 }
             );

          }]
    }

};

