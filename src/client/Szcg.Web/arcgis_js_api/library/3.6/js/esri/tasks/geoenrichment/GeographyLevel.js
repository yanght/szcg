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
define("esri/tasks/geoenrichment/GeographyLevel",["../../declare"],function(_1){var _2=_1("esri.tasks.geoenrichment.GeographyLevel",null,{layerID:null,datasetID:null,countryID:null,constructor:function(_3){if(_3){this.layerID=_3.layer||_3.layerID||null;this.datasetID=_3.dataset||_3.datasetID||null;this.countryID=_3.sourceCountry||_3.countryID||null;}},toJson:function(){var _4={};if(this.layerID){_4.layer=this.layerID;}if(this.datasetID){_4.dataset=this.datasetID;}if(this.countryID){_4.sourceCountry=this.countryID;}return _4;}});_2.fromJsonArray=function(_5){var _6=[];if(!_5){return _6;}for(var i=0;i<_5.length;i++){var _7=_5[i];if(!(_7 instanceof _2)){_7=new _2(_7);}_6.push(_7);}return _6;};_2.toJsonArray=function(_8){if(!_8||_8.length==0){return null;}var _9=[];for(var i=0;i<_8.length;i++){_9.push(_8[i].toJson());}return _9;};return _2;});