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
define("esri/dijit/geoenrichment/theme",["require","dojo/_base/declare","dojo/_base/lang","dojo/Evented","dojo/dom-class","dojo/dom-construct","./dom","dojo/query","dojo/Deferred","../../extend"],function(_1,_2,_3,_4,_5,_6,_7,_8,_9,_a){var _b="./themes/";var _c="common";var _d=null;function _e(o1,o2){for(var p in o2){try{o1[p]=o2[p].constructor==Object?_e(o1[p],o2[p]):o2[p];}catch(e){o1[p]=o2[p];}}return o1;};var _f=new (_2([_4],{set:function(_10,_11){if(_11){var _12=_1.toUrl(_b+_11+"/main.css");var _13=_8("link").some(function(_14){return _14.href.toLowerCase().indexOf(_12)>-1;});if(!_13){_6.create("link",{"rel":"stylesheet","href":_12},_7.head());}}if(_d){_5.remove(_10,_d);}_d=_11;_5.add(_10,_11);this.emit("change");},get:function(){return _d;},load:function(_15){var _16=new _9();var _17=null;var _18=_1.on("error",_19);function _19(){if(_18){_18.remove();}_16.resolve(_17);};_1([_b+_c+"/"+_15],function(_1a){_17=_3.clone(_1a);if(!_d){_19();}else{_1([_b+_d+"/"+_15],function(_1b){_e(_17,_1b);_19();});}});return _16.promise;}}))();_a("esri.dijit.geoenrichment.theme",_f);return _f;});