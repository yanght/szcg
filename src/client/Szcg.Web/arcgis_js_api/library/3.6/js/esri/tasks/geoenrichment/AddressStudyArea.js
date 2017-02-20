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
define("esri/tasks/geoenrichment/AddressStudyArea",["../../declare","./StudyArea"],function(_1,_2){return _1("esri.tasks.geoenrichment.AddressStudyArea",[_2],{address:null,constructor:function(_3){if(_3){if(_3.address){this.address=_3.address;}}},toJson:function(){var _4=this.inherited(arguments);_4.address=this.address;return _4;}});});