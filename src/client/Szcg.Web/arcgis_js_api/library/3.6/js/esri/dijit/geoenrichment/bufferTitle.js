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
define("esri/dijit/geoenrichment/bufferTitle",["dojo/string","../../tasks/geoenrichment/DriveBuffer","../../tasks/geoenrichment/IntersectingGeographies","../../tasks/geoenrichment/DriveUnits","dojo/i18n!../../nls/jsapi","../../extend"],function(_1,_2,_3,_4,_5,_6){_5=_5.geoenrichment.dijit.bufferTitle;function _7(_8){return (_8&&_8.radii[0]||1).toString();};function _9(_a){return _a&&_a.units||_4.MILES;};function _b(_c,_d){switch(_c){case "polyline":if(_d instanceof _2){return _1.substitute(_5.lineBuffer[_4.MILES],{radius:"1"});}else{return _1.substitute(_5.lineBuffer[_9(_d)],{radius:_7(_d)});}case "polygon":return _5.polygon;default:if(_d instanceof _2){return _1.substitute(_5.pointDriveTime[_9(_d)],{radius:_7(_d)});}else{if(_d instanceof _3){return _1.substitute(_5.stdGeo,{level:_d.geographyLevels[0].layerID});}else{return _1.substitute(_5.pointRing[_9(_d)],{radius:_7(_d)});}}}};_6("esri.dijit.geoenrichment.bufferTitle",_b);return _b;});