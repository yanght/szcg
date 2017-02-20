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
define("esri/tasks/geoenrichment/GeographyQuery",["../../declare","./GeographyQueryBase"],function(_1,_2){return _1("esri.tasks.geoenrichment.GeographyQuery",[_2],{geographyLayerIDs:null,geographyIDs:null,where:null,constructor:function(_3){if(_3){if(_3.geographyLayers){this.geographyLayerIDs=_3.geographyLayers.split(";");}if(_3.geographyIDs){this.geographyIDs=_3.geographyIDs;}if(_3.geographyQuery){this.where=_3.geographyQuery;}}},toJson:function(){var _4=this.inherited(arguments);if(this.geographyLayerIDs){_4.geographyLayers=this.geographyLayerIDs.join(";");}if(this.geographyIDs){_4.geographyIDs=this.geographyIDs;}if(this.where){_4.geographyQuery=this.where;}return _4;}});});