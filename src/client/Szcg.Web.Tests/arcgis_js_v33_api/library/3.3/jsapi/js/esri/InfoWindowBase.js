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
define("esri/InfoWindowBase",["dijit","dojo","dojox","dojo/require!dijit/_base/manager"],function(_1,_2,_3){_2.provide("esri.InfoWindowBase");_2.require("dijit._base.manager");_2.declare("esri.InfoWindowBase",null,{constructor:function(){var _4=_2.hitch;this.__set_title=_4(this,this.__set_title);this.__err_title=_4(this,this.__err_title);this.__set_content=_4(this,this.__set_content);this.__err_content=_4(this,this.__err_content);},setMap:function(_5){this.map=_5;},unsetMap:function(_6){delete this.map;},setTitle:function(){},setContent:function(){},show:function(){},hide:function(){},resize:function(){},onShow:function(){},onHide:function(){},place:function(_7,_8){if(esri._isDefined(_7)){if(_2.isObject(_7)){_2.place(_7,_8,"only");}else{_8.innerHTML=_7;}}else{_8.innerHTML="";}},startupDijits:function(_9){this._processDijits(_9);},destroyDijits:function(_a){this._processDijits(_a,true);},_processDijits:function(_b,_c){if(_b&&_b.children.length===1){var _d=_b.children[0];if(_d){var _e=_1.byNode(_d);var _f=_e?[_e]:_1.findWidgets(_d);_2.forEach(_f,function(_10){if(_c){if(_10._started&&!_10._destroyed){try{if(_10.destroyRecursive){_10.destroyRecursive();}else{if(_10.destroy){_10.destroy();}}}catch(ex){console.debug("An error occurred when destroying a widget embedded within InfoWindow: "+ex.message);}}}else{if(!_10._started){try{_10.startup();}catch(ex2){console.debug("An error occurred when starting a widget embedded within InfoWindow: "+ex2.message);}}}});}}},__registerMapListeners:function(){this.__unregisterMapListeners();var map=this.map;this.__handles=[_2.connect(map,"onPan",this,this.__onMapPan),_2.connect(map,"onZoomStart",this,this.__onMapZmStart),_2.connect(map,"onExtentChange",this,this.__onMapExtChg)];},__unregisterMapListeners:function(){var _11=this.__handles;if(_11){_2.forEach(_11,_2.disconnect,_2);this.__handles=null;}},__onMapPan:function(_12,_13){this.move(_13,true);},__onMapZmStart:function(){this.__mcoords=this.mapCoords||this.map.toMap(new esri.geometry.ScreenPoint(this.coords));this.hide(null,true);},__onMapExtChg:function(_14,_15,_16){var map=this.map,_17=this.mapCoords;if(_17){this.show(_17,null,true);}else{var _18;if(_16){_18=map.toScreen(this.__mcoords);}else{_18=this.coords.offset((_15&&_15.x)||0,(_15&&_15.y)||0);}this.show(_18,null,true);}},__setValue:function(_19,_1a){this[_19].innerHTML="";var dfd="_dfd"+_19,_1b=this[dfd];if(_1b&&_1b.fired===-1){_1b.cancel();this[dfd]=null;}if(esri._isDefined(_1a)){if(_1a instanceof _2.Deferred){this[dfd]=_1a;_1a.addCallbacks(this["__set"+_19],this["__err"+_19]);}else{this.__render(_19,_1a);}}},__set_title:function(_1c){this._dfd_title=null;this.__render("_title",_1c);},__err_title:function(_1d){this._dfd_title=null;},__set_content:function(_1e){this._dfd_content=null;this.__render("_content",_1e);},__err_content:function(_1f){this._dfd_content=null;},__render:function(_20,_21){var _22=this[_20];this.place(_21,_22);if(this.isShowing){this.startupDijits(_22);if(_20==="_title"&&this._adjustContentArea){this._adjustContentArea();}}}});});