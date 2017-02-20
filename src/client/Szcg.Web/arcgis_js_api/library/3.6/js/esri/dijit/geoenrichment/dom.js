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
define("esri/dijit/geoenrichment/dom",["dojo/dom-construct","dijit/registry"],function(_1,_2){return {text:function(_3,_4){_3.appendChild(document.createTextNode(_4));},clear:function(_5){if(!_5){return;}var _6=_2.findWidgets(_5);if(_6){for(var i=0;i<_6.length;i++){_6[i].destroy();}}_5.innerHTML="";},pct:function(_7){return (_7*100).toFixed(2)+"%";},head:function(){return document.getElementsByTagName("head")[0];},createCols:function(_8,_9){var _a=_1.create("colgroup",null,_8);for(var i=0;i<_9.length;i++){_1.create("col",{"style":"width:"+this.pct(_9[i])},_a);}}};});