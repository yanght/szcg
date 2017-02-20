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
define("esri/tasks/geoenrichment/IntersectingGeography",["../../declare"],function(_1){var _2=_1("esri.tasks.geoenrichment.IntersectingGeography",null,{name:null,geometryType:"esriGeometryPoint",spatialRel:"esriSpatialRelIntersects",outFields:null,constructor:function(_3){if(_3){this.name=_3.name||null;this.outFields=_3.outFields||null;if(_3.intersectionInfo){if(_3.intersectionInfo.geometryType){this.geometryType=_3.intersectionInfo.geometryType;}if(_3.intersectionInfo.spatialRel){this.spatialRel=_3.intersectionInfo.spatialRel;}}else{if(_3.geometryType){this.geometryType=_3.geometryType;}if(_3.spatialRel){this.spatialRel=_3.spatialRel;}}}},toJson:function(){return {name:this.name,outFields:this.outFields,intersectionInfo:{geometryType:this.geometryType,spatialRel:this.spatialRel}};}});return _2;});