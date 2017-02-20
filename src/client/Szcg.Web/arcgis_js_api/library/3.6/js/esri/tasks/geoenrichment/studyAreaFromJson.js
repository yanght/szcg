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
define("esri/tasks/geoenrichment/studyAreaFromJson",["./GeometryStudyArea","./AddressStudyArea","./StandardGeographyStudyArea","../../extend"],function(_1,_2,_3,_4){var _5=function(_6){if(_6.geometry){return new _1(_6);}else{if(_6.address){return new _2(_6);}else{if(_6.layer){return new _3(_6);}}}};_4("esri.tasks.geoenrichment.studyAreaFromJson",_5);return _5;});