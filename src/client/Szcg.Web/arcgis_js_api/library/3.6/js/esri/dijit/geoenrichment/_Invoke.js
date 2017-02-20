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
define("esri/dijit/geoenrichment/_Invoke",["../../declare"],function(_1){return _1("esri.dijit.geoenrichment._Invoke",null,{_invokeTimeoutIDs:null,invoke:function(_2,_3){if(!this._invokeTimeoutIDs){this._invokeTimeoutIDs={};}else{if(this._invokeTimeoutIDs[_2]){if(_3===undefined){return;}else{clearTimeout(this._invokeTimeoutIDs[_2]);}}}var _4=this;this._invokeTimeoutIDs[_2]=setTimeout(function(){_4._invokeTimeoutIDs[_2]=0;_4[_2]();},_3||0);},pendingInvoke:function(_5){if(!this._invokeTimeoutIDs){return false;}return this._invokeTimeoutIDs[_5];},cancelInvoke:function(_6){if(!this._invokeTimeoutIDs){return;}var id=this._invokeTimeoutIDs[_6];if(id){clearTimeout(id);this._invokeTimeoutIDs[_6]=0;}}});});