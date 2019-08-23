'use strict';

var apiUrlPrefix = 'http://localhost:45871/';
var urlPrefix = '';
//var apiUrlPrefixNew = 'http://localhost:808/';
//var apiUrlPrefixNew = 'http://api.shikkhaforall.com/';

//var serviceBase=''
angular.module('app', [
    'ngAnimate',
    'ngCookies',
    'ngResource',
    'ngSanitize',
    'ngTouch',
    'ngStorage',
    'ui.router',
    'ui.bootstrap',
    'ui.load',
    'ui.jq',
    'ui.validate',
    'oc.lazyLoad',
    'pascalprecht.translate',
    '720kb.tooltips',
    'angucomplete-alt',
    'ngTouch',
    
    //'autocomplete'
]);

//Date.prototype.addHours = function (h) {
//    this.setHours(this.getHours() + h);
//    return this;
//}

function DateConvert(date)
{
    date.setHours(date.getHours() + 6);

    return date;
}

