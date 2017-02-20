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
define("esri/tasks/geoenrichment/StandardGeographyStudyArea",["../../declare","./StudyArea"],function(_1,_2){return _1("esri.tasks.geoenrichment.StandardGeographyStudyArea",[_2],{countryID:null,geographyLayerID:null,ids:null,constructor:function(_3){if(_3){var _4=_3.sourceCountry||_3.countryID;if(_4){this.countryID=_4;}var _5=_3.layer||_3.geographyLayerID;if(_5){this.geographyLayerID=_5;}if(_3.ids){this.ids=_3.ids;}}},toJson:function(){var _6=this.inherited(arguments);if(this.countryID){_6.sourceCountry=this.countryID;}if(this.geographyLayerID){_6.layer=this.geographyLayerID;}if(this.ids){_6.ids=this.ids;}return _6;}});});