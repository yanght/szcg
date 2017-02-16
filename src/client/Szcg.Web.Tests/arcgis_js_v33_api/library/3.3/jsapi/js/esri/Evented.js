/*
 COPYRIGHT 2009 ESRI

 TRADE SECRETS: ESRI PROPRIETARY AND CONFIDENTIAL
 Unpublished material - all rights reserved under the
 Copyright Laws of the United States and applicable international
 laws, treaties, and conventions.

 For additional information, contact:
 Environmental Systems Research Institute, Inc.
 Attn: Contracts and Legal Services Department
 380 New York Street
 Redlands, California, 92373
 USA

 email: contracts@esri.com
 */
//>>built
define("esri/Evented",["dojo/_base/declare","dojo/Evented","dojo/has","esri"],function(_1,_2,_3,_4){var _5=_1([_2],{registerConnectEvents:function(_6,_7){if(!_6){return;}var _8,_9=this.constructor.connectEvents,_a=(_7&&_7.normalized);if(!_9){_9=(this.constructor.connectEvents={});}for(_8 in _6){if(_6.hasOwnProperty(_8)){_9[_8]=_6[_8];if(_a){_9[_8].normalized=true;}}}},on:function(_b,_c){var _d=this.constructor.connectEvents,_e=_d&&_d[_b];if(_e){var _f=function(){var evt,i,_10;if(_e.normalized){evt=arguments[0];}else{_10=_e.length-1;evt={};for(i=1;i<=_10;i++){evt[_e[i]]=arguments[i-1];}}_c.call(this,evt);};return _2.prototype.on.apply(this,[_e[0],_f]);}else{return this.inherited(arguments);}}});if(_3("extend-esri")){_4.Evented=_5;}return _5;});