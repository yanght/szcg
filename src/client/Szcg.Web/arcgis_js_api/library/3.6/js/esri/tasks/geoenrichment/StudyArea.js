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
define("esri/tasks/geoenrichment/StudyArea",["../../declare","dojo/_base/lang","./StudyAreaOptions","./studyAreaOptionsFromJson","./GeographyLevel"],function(_1,_2,_3,_4,_5){return _1("esri.tasks.geoenrichment.StudyArea",null,{attributes:null,options:null,returnGeometry:false,comparisonGeographyLevels:null,constructor:function(_6){if(_6){if(_6.attributes){this.attributes=_6.attributes;}if(_6.areaType){this.options=_4(_6);}else{if(_6.options instanceof _3){this.options=_6.options;}}if(_6.returnGeometry){this.returnGeometry=true;}var _7=_6.comparisonGeographyLevels||_6.comparisonLevels;this.comparisonGeographyLevels=_5.fromJsonArray(_7);}if(!this.comparisonGeographyLevels){this.comparisonGeographyLevels=[];}},toJson:function(){var _8={};if(this.attributes){_8.attributes=this.attributes;}if(this.options){_2.mixin(_8,this.options.toJson());}if(this.returnGeometry){_8.returnGeometry=true;}var _9=_5.toJsonArray(this.comparisonGeographyLevels);if(_9){_8.comparisonLevels=_9;}return _8;}});});