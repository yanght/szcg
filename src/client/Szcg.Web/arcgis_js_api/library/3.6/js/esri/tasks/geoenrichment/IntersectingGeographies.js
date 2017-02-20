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
define("esri/tasks/geoenrichment/IntersectingGeographies",["../../declare","./StudyAreaOptions","./GeographyLevel"],function(_1,_2,_3){var _4=_1("esri.tasks.geoenrichment.IntersectingGeographies",[_2],{geographyLevels:null,constructor:function(_5){var _6;if(_5){_6=_5.intersectingGeographies||_5.levels||_5.geographyLevels;}else{_6=[{layerID:"Admin2"}];}this.geographyLevels=_3.fromJsonArray(_6);},toJson:function(){var _7={areaType:"StandardGeography",intersectingGeographies:_3.toJsonArray(this.geographyLevels)};return _7;}});return _4;});