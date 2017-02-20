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
define("esri/tasks/geoenrichment/GeometryStudyArea",["../../declare","esri/geometry/jsonUtils","esri/geometry/Geometry","./StudyArea"],function(_1,_2,_3,_4){return _1("esri.tasks.geoenrichment.GeometryStudyArea",[_4],{geometry:null,constructor:function(_5){if(_5){if(_5.geometry){if(_5.geometry instanceof _3){this.geometry=_5.geometry;}else{this.geometry=_2.fromJson(_5.geometry);}}}},toJson:function(){var _6=this.inherited(arguments);_6.geometry=this.geometry.toJson();return _6;}});});