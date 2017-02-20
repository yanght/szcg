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
define("esri/tasks/geoenrichment/EnrichParameters",["../../declare","esri/SpatialReference","./EnrichParametersBase","./StandardIntersectingGeography"],function(_1,_2,_3,_4){return _1("esri.tasks.geoenrichment.EnrichParameters",[_3],{variables:null,returnGeometry:false,outSR:null,intersectingGeographies:null,forStorage:true,constructor:function(_5){this.variables=[];if(_5){this.variables=_5.analysisVariables||_5.variables||null;if(_5.returnGeometry){this.returnGeometry=true;}if(_5.outSR){this.outSR=new _2(_5.outSR);}if(_5.forStorage){this.forStorage=_5.forStorage;}if(_5.intersectingGeographies){this.intersectingGeographies=[];for(var i=0;i<_5.intersectingGeographies.length;i++){var _6=_5.intersectingGeographies[i];if(_6.geographyType=="standard"){this.intersectingGeographies.push(new _4(_6));}else{if(_6.geographyType=="external"){}}}}}},toJson:function(){var _7=this.inherited(arguments);_7.analysisVariables=this.variables;if(this.returnGeometry){_7.returnGeometry=true;}if(this.outSR){_7.outSR=this.outSR.toJson();}if(!this.forStorage){_7.forStorage=false;}if(this.intersectingGeographies){_7.intersectingGeographies=[];for(var i=0;i<this.intersectingGeographies.length;i++){_7.intersectingGeographies.push(this.intersectingGeographies[i].toJson());}}return _7;}});});