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
define("esri/tasks/geoenrichment/SubGeographyQuery",["../../declare","./GeographyQueryBase"],function(_1,_2){return _1("esri.tasks.geoenrichment.SubGeographyQuery",[_2],{filterGeographyLayerID:null,filterGeographyIDs:null,filterGeographyWhere:null,subGeographyLayerID:null,subGeographyWhere:null,constructor:function(_3){if(_3){if(_3.geographyLayers){this.filterGeographyLayerID=_3.geographyLayers;}if(_3.geographyIDs){this.filterGeographyIDs=_3.geographyIDs;}if(_3.geographyQuery){this.filterGeographyWhere=_3.geographyQuery;}if(_3.subGeographyLayer){this.subGeographyLayerID=_3.subGeographyLayer;}if(_3.subGeographyQuery){this.subGeographyWhere=_3.subGeographyQuery;}}},toJson:function(){var _4=this.inherited(arguments);_4.returnSubGeographyLayer=true;if(this.filterGeographyLayerID){_4.geographyLayers=this.filterGeographyLayerID;}if(this.filterGeographyIDs){_4.geographyIDs=this.filterGeographyIDs;}if(this.filterGeographyWhere){_4.geographyQuery=this.filterGeographyWhere;}if(this.subGeographyLayerID){_4.subGeographyLayer=this.subGeographyLayerID;}if(this.subGeographyWhere){_4.subGeographyQuery=this.subGeographyWhere;}return _4;}});});