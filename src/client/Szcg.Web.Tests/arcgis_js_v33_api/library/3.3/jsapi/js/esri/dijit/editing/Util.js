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
define("esri/dijit/editing/Util",["dijit","dojo","dojox","dojo/require!dojo/DeferredList"],function(_1,_2,_3){_2.provide("esri.dijit.editing.Util");_2.require("dojo.DeferredList");esri.dijit.editing.Util.LayerHelper={findFeatures:function(_4,_5,_6){var _7=_5.objectIdField;var _8=_5.graphics;var _9=_2.filter(_8,function(_a){return _2.some(_4,function(id){return _a.attributes[_7]===id.objectId;});});if(_6){_6(_9);}else{return _9;}},getSelection:function(_b){var _c=[];_2.forEach(_b,function(_d){var _e=_d.getSelectedFeatures();_2.forEach(_e,function(_f){_c.push(_f);});});return _c;}};});