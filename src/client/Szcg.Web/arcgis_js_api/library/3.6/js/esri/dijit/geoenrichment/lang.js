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
define("esri/dijit/geoenrichment/lang",[],function(){return {arraysEqual:function arraysEqual(a,b){if(!a&&!b){return true;}if(!a||!b){return false;}if(a.length!=b.length){return false;}for(var i=0;i<a.length;i++){if(a[i]!=b[i]){return false;}}return true;},isNumber:function(n){return !isNaN(parseFloat(n))&&isFinite(n);},startsWith:function(_1,_2){return _1.lastIndexOf(_2,0)===0;},endsWith:function(_3,_4){var _5=_3.length-_4.length;if(_5<0){return false;}return _3.indexOf(_4,_5)===_5;},isBoolean:function(b){return (typeof b=="boolean")||(b instanceof Boolean);}};});