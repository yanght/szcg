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
define("esri/dijit/_EventedWidget",["dojo/_base/declare","dojo/_base/lang","dojo/aspect","dojo/on","esri/Evented","dijit/_WidgetBase"],function(_1,_2,_3,on,_4,_5){var _6=_1([_4,_5],{_onMap:function(_7){var _8=this.constructor._onMap,_9,_a;if(!_8){_8=this.registerConnectEvents();}_7=_7.toLowerCase();if(_8[_7]){_9=this[_8[_7].name];}else{_a=this._onCamelCase(_7);if(this[_a]){_9=_a;}}return _9;},on:function(_b,_c){_4.prototype.on.apply(this,arguments);return this.inherited(arguments);},emit:function(_d,_e,_f){var _10,_11,_12,_13=_d.toLowerCase(),_14=this.constructor._onMap||this.registerConnectEvents();_11=this[this._onMap(_13)];_e=_e||{};if(!_e.target){_e.target=this;}if(_11&&_14&&_14[_13]){this._onObj2Arr(function(){_10=Array.prototype.slice.call(arguments);},_14[_13].argKeys)(_e);_12=_2.mixin({},arguments);_12[2]=_10;_12[0]=_14[_13].name.replace(/^on/,"");}return this.inherited(_12||arguments);}});return _6;});