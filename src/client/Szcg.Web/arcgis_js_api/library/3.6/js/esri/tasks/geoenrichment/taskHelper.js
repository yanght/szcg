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
define("esri/tasks/geoenrichment/taskHelper",["dojo/_base/lang","dojo/Deferred","dojo/json","esri/request","../../extend","esri/config"],function(_1,_2,_3,_4,_5,_6){var _7=_6.defaults.io.corsEnabledServers;_7.push("geoenrich.arcgis.com");_7.push("geoenrichdev.arcgis.com");_7.push("geoenrichqa.arcgis.com");var TH={invokeMethod:function(_8,_9,_a,_b,_c,_d){var _e=null;var _f=null;function _10(){if(_e){_e.cancel();_e=null;}};function _11(_12){try{var _13=_b(_12);}catch(e){_14(e);return;}if(_f){_f.resolve(_13);}_8[_c](_13);};function _14(_15){if(_f){_f.reject(_15);}_8[_d](_15);};_f=new _2(_10);try{var _16=_a?_a():{};_16["f"]="json";if(_8.token){_16["token"]=_8.token;}_e=_4({url:_8.url+_9,content:_16,handleAs:"json"});_e.then(_11,_14);}catch(e){_14(e);}return _f.promise;},jsonToRest:function(_17){var _18={};for(var p in _17){if(_1.isString(_17[p])){_18[p]=_17[p];}else{_18[p]=_3.stringify(_17[p]);}}return _18;},throwEmptyResponse:function(){throw new Error("Geoenrichment service returned empty response");}};_5("esri.tasks.geoenrichment.taskHelper",TH);return TH;});